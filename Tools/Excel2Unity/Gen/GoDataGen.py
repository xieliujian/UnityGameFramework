
from Gen.DataGen import DataGen
from Config import EXCEL_DIR
from Config import SERVER_TABLE_ROOT_DIR
from Config import SERVER_TABLE_DATA_DIR
from Config import SERVER_TABLE_DATA_EXT
import os


class GoDataGen(DataGen):

	# 文件生成函数
	def process(self, filename, fields, table):
		# 创建输出路径
		path = filename.replace(EXCEL_DIR, "")
		path = SERVER_TABLE_ROOT_DIR + SERVER_TABLE_DATA_DIR + path
		path = os.path.splitext(path)[0]
		path += SERVER_TABLE_DATA_EXT

		# 生成文件目录, 不重复创建目录
		filedir = os.path.dirname(path)
		if os.path.exists(filedir) == False:
			os.makedirs(filedir)

		# 生成数据
		fieldendval = fields[len(fields) - 1]

		fileContent = ""
		for row in range(5, table.nrows):
			for col in range(table.ncols):
				if col in fields:
					fieldtype = table.cell(2, col).value
					fieldtype = fieldtype.lower()
					fieldvalue = table.cell(row, col).value
					if fieldtype == "int":
						fieldvalue = int(fieldvalue)

					if col == fieldendval:
						fileContent += ("{0}").format(fieldvalue)
					else:
						fileContent += ("{0}\t").format(fieldvalue)

			fileContent += "\n"

		# 保存
		file = open(path, "wb")
		file.write(fileContent.encode())
		file.close()









