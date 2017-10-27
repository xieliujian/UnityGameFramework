
from Gen.CodeGen import CodeGen
from Config import EXCEL_DIR
from Config import EXCEL_EXT
from Config import KEY_MODIFIER_NAME
from Config import UNITY_TABLE_ROOT_DIR
from Config import UNITY_TABLE_CODE_DIR
from Config import UNITY_TABLE_CODE_EXT
from Config import UNITY_TABLE_DATA_DIR
from Config import UNITY_TABLE_DATA_EXT
from Config import UNITY_TABLE_PARSECODE_DIR
from Config import UNITY_CONFIGMANAGER_FILENAME
from Config import UINTY_TABLE_USE_RESOURCE_PATH_READ
import os
import shutil


class UnityCodeGen(CodeGen):
	# 构造函数
	def __init__(self):
		self.mFileContent = ""

	# 代码生成函数
	def process(self, filename, fields, table):
		# -----------------------table cfg class-----------------------

		# 创建输出路径
		path = filename.replace(EXCEL_DIR, "")
		path = UNITY_TABLE_ROOT_DIR + UNITY_TABLE_CODE_DIR + path
		path = os.path.splitext(path)[0]
		path = path + UNITY_TABLE_CODE_EXT

		# 生成文件目录, 不重复创建目录
		filedir = os.path.dirname(path)
		if os.path.exists(filedir) == False:
			os.makedirs(filedir)

		# 填充内容
		self.mFileContent = ""
		self.mFileContent += "using System.Collections.Generic;\n"
		self.mFileContent += "using System.IO;\n"
		self.mFileContent += "using System.Text;\n"
		self.mFileContent += "using UnityEngine;\n"
		self.mFileContent += "\n"

		# table class
		tablebasename = os.path.basename(path)
		tablebasename = tablebasename.split(".")[0]
		tablename = tablebasename + "Cfg"
		self.mFileContent += "public class " + tablename + "\n"
		self.mFileContent += "{\n"

		for index in fields:
			fielddesc = table.cell(0, index).value
			fieldtype = table.cell(2, index).value
			fieldname = table.cell(3, index).value
			fieldtype = fieldtype.lower()
			self.define_fieldtype(fieldtype, fieldname, fielddesc)

		self.mFileContent += "\n"
		self.mFileContent += "	public " + tablename + "(string line)\n"
		self.mFileContent += "	{\n"
		self.mFileContent += "		string []fields = line.Split('\t');\n"

		# 解析字段
		for index in fields:
			fieldtype = table.cell(2, index).value
			fieldname = table.cell(3, index).value
			fieldtype = fieldtype.lower()
			self.parse_fieldtype(fieldtype, fieldname, index)

		self.mFileContent += "	}\n"
		self.mFileContent += "}\n"
		self.mFileContent += "\n"

		# -----------------------table cfg manager class-----------------------

		# 获得keylist
		keylist = []
		for index in fields:
			value = table.cell(4, index).value
			if value == KEY_MODIFIER_NAME:
				keylist.append(index)

		tablemgrname = tablename + "Manager";
		format = "{0}.txt"
		self.mFileContent += "public class " + tablemgrname + "\n"
		self.mFileContent += "{\n"

		# 根据keylist判断
		uselist = (keylist.__len__() != 1)
		if uselist:
			self.mFileContent += "	private List<" + tablename + "> mList = new List<" + tablename + ">();\n"
		else:
			self.mFileContent += "	private Dictionary<int, " + tablename + "> mDict = new Dictionary<int, " + tablename + ">();\n"

		self.mFileContent += "\n"
		self.mFileContent += "	public void InitTable()\n"
		self.mFileContent += "	{\n"

		datapath = filename.replace(EXCEL_DIR, "")
		datapath = datapath.replace(EXCEL_EXT, "")
		if UINTY_TABLE_USE_RESOURCE_PATH_READ == True:
			datapath = UNITY_TABLE_DATA_DIR + datapath
		else:
			datapath = UNITY_TABLE_DATA_DIR + datapath + UNITY_TABLE_DATA_EXT
		datapath = datapath.replace("\\", "/")
		datapath = datapath.replace("//", "/")
		self.mFileContent += "		string data = ConfigUtil.GetConfigData(\"" + datapath + "\");\n"
		self.mFileContent += "		string[] splits = data.Split('" + "\\" + "n" + "');\n"
		self.mFileContent += "		foreach (string split in splits)\n"
		self.mFileContent += "		{\n"
		self.mFileContent += "			string line = split.Trim();\n"
		self.mFileContent += "			if (line.Length > 0)\n"
		self.mFileContent += "			{\n"
		self.mFileContent += "				" + tablename + " rowdata = new " + tablename + "(line);\n"

		# 根据keylist判断
		if uselist:
			self.mFileContent += "				mList.Add(" "rowdata);\n"
		else:
			self.mFileContent += "				mDict.Add(rowdata." + table.cell(3, 0).value + ", rowdata);\n"

		self.mFileContent += "			}\n"
		self.mFileContent += "			else\n"
		self.mFileContent += "			{\n"
		self.mFileContent += "				continue;\n"
		self.mFileContent += "			}\n"
		self.mFileContent += "		}\n"
		self.mFileContent += "	}\n"

		# 生成生成数据函数
		self.gen_gendatafunc(table, tablename, keylist)

		self.mFileContent += "\n"
		self.mFileContent += "	private " + tablemgrname + "() { }\n"
		self.mFileContent += "\n"
		self.mFileContent += "	public static readonly " + tablemgrname + " Instance = new " + tablemgrname + "();\n"
		self.mFileContent += "}\n"

        # 保存
		file = open(path, "wb")
		file.write(self.mFileContent.encode())
		file.close()

	# 定义字段类型
	def define_fieldtype(self, fieldtype, fieldname, fielddesc):
		if fieldtype == "int" or fieldtype == "float" or fieldtype == "string":
			self.mFileContent += "	public " + fieldtype + " " + fieldname + ";"
		elif fieldtype == "list[int]":
			self.mFileContent += "	public List<int> " + fieldname + " = new List<int>();"
		elif fieldtype == "list[float]":
			self.mFileContent += "	public List<float> " + fieldname + " = new List<float>();"
		elif fieldtype == "list[string]":
			self.mFileContent += "	public List<string> " + fieldname + " = new List<string>();"
		elif fieldtype == "map[int|int]":
			self.mFileContent += "	public Dictionary<int, int> " + fieldname + " = new Dictionary<int, int>();"
		elif fieldtype == "map[int|float]":
			self.mFileContent += "	public Dictionary<int, float> " + fieldname + " = new Dictionary<int, float>();"
		elif fieldtype == "map[int|string]":
			self.mFileContent += "	public Dictionary<int, string> " + fieldname + " = new Dictionary<int, string>();"
		elif fieldtype == "map[string|int]":
			self.mFileContent += "	public Dictionary<string, int> " + fieldname + " = new Dictionary<string, int>();"
		elif fieldtype == "map[string|float]":
			self.mFileContent += "	public Dictionary<string, float> " + fieldname + " = new Dictionary<string, float>();"
		elif fieldtype == "map[string|string]":
			self.mFileContent += "	public Dictionary<string, string> " + fieldname + " = new Dictionary<string, string>();"

		self.mFileContent += "			//		" + fielddesc + "\n"

	# 解析字段类型
	def parse_fieldtype(self, fieldtype, fieldname, index):
		if fieldtype == "int" or fieldtype == "float":
			self.mFileContent += "		" + fieldname + " = " + fieldtype + ".Parse(fields[" + str(index) + "]);\n"
		elif fieldtype == "string":
			self.mFileContent += "		" + fieldname + " = fields[" + str(index) + "];\n"
		elif fieldtype == "list[int]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseListInt(fields[" + str(index) + "]);\n"
		elif fieldtype == "list[float]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseListFloat(fields[" + str(index) + "]);\n"
		elif fieldtype == "list[string]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseListString(fields[" + str(index) + "]);\n"
		elif fieldtype == "map[int|int]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseDictIntInt(fields[" + str(index) + "]);\n"
		elif fieldtype == "map[int|float]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseDictIntFloat(fields[" + str(index) + "]);\n"
		elif fieldtype == "map[int|string]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseDictIntString(fields[" + str(index) + "]);\n"
		elif fieldtype == "map[string|int]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseDictStringInt(fields[" + str(index) + "]);\n"
		elif fieldtype == "map[string|float]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseDictStringFloat(fields[" + str(index) + "]);\n"
		elif fieldtype == "map[string|string]":
			self.mFileContent += "		" + fieldname + " = ConfigUtil.ParseDictStringString(fields[" + str(index) + "]);\n"

	# 生成生成数据函数
	def gen_gendatafunc(self, table, tablename, keylist):
		keylen = keylist.__len__()
		if keylen == 0:		# 没有key值没有生成函数
			return;
		elif keylen == 1:	# 有一个key值使用dict取值
			keytype = table.cell(2, keylist[0]).value
			keytype = keytype.lower()
			self.mFileContent += "\n"
			self.mFileContent += "	public " + tablename + " GetDataByID(" + keytype + " id)\n"
			self.mFileContent += "	{\n"
			self.mFileContent += "		" + tablename + " rowdata = null;\n"
			self.mFileContent += "		mDict.TryGetValue(id, out rowdata);\n"
			self.mFileContent += "		return rowdata;\n"
			self.mFileContent += "	}\n"
		else:
			self.mFileContent += "\n"

			self.mFileContent += "	public " + tablename + " GetDataByID("
			for keyindex in keylist:
				keytype = table.cell(2, keyindex).value
				keytype = keytype.lower()
				keyval = table.cell(3, keyindex).value
				self.mFileContent += keytype + " _" + keyval
				if keyindex != (keylen - 1):
					self.mFileContent += ", "
			self.mFileContent += ")\n"

			self.mFileContent += "	{\n"
			self.mFileContent += "		foreach (" + tablename + " data in mList)\n"
			self.mFileContent += "		{\n"

			self.mFileContent += "			if ("
			for keyindex in keylist:
				keyval = table.cell(3, keyindex).value
				self.mFileContent += "data." + keyval + " == _" + keyval
				if keyindex != (keylen - 1):
					self.mFileContent += " && "
			self.mFileContent += ")\n"

			self.mFileContent += "			{\n"
			self.mFileContent += "				return data;\n"
			self.mFileContent += "			}\n"
			self.mFileContent += "		}\n"
			self.mFileContent += "\n"
			self.mFileContent += "		return null;\n"
			self.mFileContent += "	}\n"

	# 生成配置管理类
	@staticmethod
	def gen_configmangercode(files):
		path = UNITY_TABLE_ROOT_DIR + UNITY_TABLE_CODE_DIR + UNITY_CONFIGMANAGER_FILENAME + UNITY_TABLE_CODE_EXT

		filecontent = ""
		filecontent += "using System.Collections;\n"
		filecontent += "using System.Collections.Generic;\n"
		filecontent += "using UnityEngine;\n"
		filecontent += "\n"
		filecontent += "public class " +  UNITY_CONFIGMANAGER_FILENAME + "\n"
		filecontent += "{\n"
		filecontent += "	public static void Load()\n"
		filecontent += "	{\n"
		for file in files:
			tablename = os.path.basename(file)
			tablename = tablename.split(".")[0]
			tablename += "Cfg"
			filecontent += "		" +	tablename + "Manager.Instance.InitTable();\n"
		filecontent += "	}\n"
		filecontent += "}\n"

		# 保存
		file = open(path, "wb")
		file.write(filecontent.encode())
		file.close()

	# 复制解析代码
	@staticmethod
	def copy_parsecode():
		listfile = os.listdir(UNITY_TABLE_PARSECODE_DIR)
		for file in listfile:
			if os.path.splitext(file)[1] == UNITY_TABLE_CODE_EXT:
				srcpath = os.path.join(UNITY_TABLE_PARSECODE_DIR, file)
				dstpath = os.path.join(UNITY_TABLE_ROOT_DIR + UNITY_TABLE_CODE_DIR, file)
				shutil.copy(srcpath, dstpath)

