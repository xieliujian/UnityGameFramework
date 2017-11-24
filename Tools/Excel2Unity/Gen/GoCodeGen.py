
from Gen.CodeGen import CodeGen
from Config import EXCEL_DIR
from Config import SERVER_TABLE_CODE_DIR
from Config import KEY_MODIFIER_NAME
from Config import SERVER_TABLE_ROOT_DIR
from Config import SERVER_CONFIGMANAGER_FILENAME
from Util import getServerCodeExt
import os

class GoCodeGen(CodeGen):
	# 构造函数
	def __init__(self):
		self.mFileContent = ""

	# 代码生成函数
	def process(self, filename, fields, table):
		# 创建输出路径

		# path = filename.replace(EXCEL_DIR, "")
		path = os.path.basename(filename)		# 代码不生成在层级目录下
		path = SERVER_TABLE_ROOT_DIR + SERVER_TABLE_CODE_DIR + path
		path = os.path.splitext(path)[0]
		path = path + getServerCodeExt()

		# 生成文件目录, 不重复创建目录
		filedir = os.path.dirname(path)
		if os.path.exists(filedir) == False:
			os.makedirs(filedir)

		# 填充内容
		tablebasename = os.path.basename(path)
		tablebasename = tablebasename.split(".")[0]
		tablename = tablebasename

		# 获取相对目录
		middir = filename.replace(EXCEL_DIR, "")
		middirbasename = os.path.basename(middir)
		middir = middir.replace(middirbasename, "")
		middir = middir.replace("\\", "/")

		# 获得keylist
		keylist = []
		for index in fields:
			value = table.cell(4, index).value
			if value == KEY_MODIFIER_NAME:
				keylist.append(index)
		uselist = (keylist.__len__() != 1)

		self.mFileContent = "\n"
		self.mFileContent += "package gamedata\n\n"

		if uselist:
			self.mFileContent += "import (\n"
			self.mFileContent += "\t\"container/list\"\n"
			self.mFileContent += ")\n\n"

		self.mFileContent += "type " + tablename + " struct {\n"
		for index in fields:
			fielddesc = table.cell(0, index).value
			fieldtype = table.cell(2, index).value
			fieldname = table.cell(3, index).value
			fieldtype = fieldtype.lower()
			self.define_fieldtype(fieldtype, fieldname, fielddesc)
		self.mFileContent += "}\n\n"

		# 根据keylist判断
		self.mFileContent += "var (\n"
		if uselist:
			self.mFileContent += "\t{0}Data = list.New()\n".format(tablename)
		else:
			fieldtype = table.cell(2, keylist[0]).value
			fieldtype = fieldtype.lower()
			self.mFileContent += "\t{0}Data = make(map[{1}]{2})\n".format(tablename, fieldtype, tablename)
		self.mFileContent += ")\n\n"

		self.mFileContent += "func " + tablename + "Init() {\n"
		self.mFileContent += "\tcf := ReadConfigFile(" + tablename + "{}, " + "\"{0}\")\n".format(middir)
		self.mFileContent += "\tfor i := 0; i < cf.NumRecord(); i++ {\n"
		self.mFileContent += "\t\tr := cf.Record(i).(*{0})\n".format(tablename)
		fieldname = table.cell(3, 0).value
		if uselist:
			self.mFileContent += "\t\t{0}Data.PushBack(*r)\n".format(tablename)
		else:
			self.mFileContent += "\t\t{0}Data[r.{1}] = *r\n".format(tablename, fieldname)
		self.mFileContent += "\t}\n"
		self.mFileContent += "}\n"

		# 获取函数
		# self.mFileContent += "func Get" + tablename + "ByID(id int) (" + tablename + ") {\n"
		# self.mFileContent += "	return  {0}Data[id]\n".format(tablename)
		# self.mFileContent += "}\n"

		keylen = keylist.__len__()
		if keylen == 1:
			keytype = table.cell(2, keylist[0]).value
			keytype = keytype.lower()
			self.mFileContent += "\n"
			self.mFileContent += "func Get" + tablename + "ByID(id " + keytype + ") " + tablename + "{\n"
			self.mFileContent += "\treturn " + tablename + "Data[id]\n"
			self.mFileContent += "}\n"
		elif keylen > 1:
			self.mFileContent += "\n"

			self.mFileContent += "func Get" + tablename + "ByID("
			for keyindex in keylist:
				keytype = table.cell(2, keyindex).value
				keytype = keytype.lower()
				keyval = table.cell(3, keyindex).value
				self.mFileContent += "_" + keyval + " " + keytype
				if keyindex != (keylen - 1):
					self.mFileContent += ", "
			self.mFileContent += ") " + tablename + " {\n"

			self.mFileContent += "\tfor e := {0}Data.Front(); e != nil; e = e.Next() ".format(tablename) + "{\n"

			self.mFileContent += "\t\tif "
			for keyindex in keylist:
				keyval = table.cell(3, keyindex).value
				self.mFileContent += "e.Value.({0}).{1} == _{1}".format(tablename, keyval)
				if keyindex != (keylen - 1):
					self.mFileContent += " && "
			self.mFileContent += " {\n"

			self.mFileContent += "\t\t\treturn e.Value.({0})\n".format(tablename)
			self.mFileContent += "\t\t}\n"
			self.mFileContent += "\t}\n\n"
			self.mFileContent += "\treturn {0}Data.Front().Value.({0})\n".format(tablename)
			self.mFileContent += "}\n"

		# 保存
		file = open(path, "wb")
		file.write(self.mFileContent.encode())
		file.close()

	# 定义字段类型
	def define_fieldtype(self, fieldtype, fieldname, fielddesc):
		self.mFileContent += "\t"
		if fieldtype == "int" or fieldtype == "string":
			self.mFileContent += fieldname + " " + fieldtype
		elif fieldtype == "float":
			self.mFileContent += fieldname + " float32"
		elif fieldtype == "list[int]":
			self.mFileContent += fieldname + " []int"
		elif fieldtype == "list[float]":
			self.mFileContent += fieldname + " []float32"
		elif fieldtype == "list[string]":
			self.mFileContent += fieldname + " []string"
		elif fieldtype == "map[int|int]":
			self.mFileContent += fieldname + " map[int]int"
		elif fieldtype == "map[int|float]":
			self.mFileContent += fieldname + " map[int]float32"
		elif fieldtype == "map[int|string]":
			self.mFileContent += fieldname + " map[int]string"
		elif fieldtype == "map[string|int]":
			self.mFileContent += fieldname + " map[string]int"
		elif fieldtype == "map[string|float]":
			self.mFileContent += fieldname + " map[string]float32"
		elif fieldtype == "map[string|string]":
			self.mFileContent += fieldname + " map[string]string"

		self.mFileContent += "			//		" + fielddesc + "\n"

	# 生成配置管理类
	@staticmethod
	def gen_configmangercode(files):
		path = SERVER_TABLE_ROOT_DIR + SERVER_TABLE_CODE_DIR + SERVER_CONFIGMANAGER_FILENAME + getServerCodeExt()

		filecontent = "\n"
		filecontent += "package gamedata\n\n"

		filecontent += "func LoadTables() {\n"
		for file in files:
			tablename = os.path.basename(file)
			tablename = tablename.split(".")[0]
			filecontent += "\t{0}Init()\n".format(tablename)
		filecontent += "}\n"

		# 保存
		file = open(path, "wb")
		file.write(filecontent.encode())
		file.close()





