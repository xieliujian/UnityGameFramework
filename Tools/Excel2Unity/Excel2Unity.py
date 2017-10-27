
import os
import xlrd
from Config import EXCEL_DIR
from Config import UNITY_TABLE_FIELD_FILTER
from Config import SERVER_TABLE_FIELD_FILTER
from Config import EXCEL_EXT
from Gen.UnityDataGen import UnityDataGen
from Gen.UnityCodeGen import UnityCodeGen


class Excel2Unity:
	# 构造函数
	def __init__(self):
		self.mExcelFiles = []		# 所有的excel文件

	# 外部处理函数
	def process(self):
		self.recursive_searchexcel(EXCEL_DIR)
		self.process_excel()

	# 递归查找文件
	def recursive_searchexcel(self, path):
		for pathdir in os.listdir(path):		# 遍历当前目录
			fullpath = os.path.join(path, pathdir)

			if os.path.isdir(fullpath):
				self.recursive_searchexcel(fullpath)
			elif os.path.isfile(fullpath):
				if os.path.splitext(fullpath)[1] == EXCEL_EXT:
					self.mExcelFiles.append(fullpath)

	# 处理excel文件
	def process_excel(self):
		for filename in self.mExcelFiles:
			data = xlrd.open_workbook(filename)
			table = data.sheets()[0]

			if table.nrows == 0 or table.ncols == 0:
				print("empty files : " + filename)

			self.process_excel_client(filename, table)
			# self.process_excel_server(filename, table)

		# 生成unity的配置管理文件
		UnityCodeGen.gen_configmangercode(self.mExcelFiles)
		UnityCodeGen.copy_parsecode()

	# client的excel的处理
	def process_excel_client(self, filename, table):
		fields = self.filter_fielddata(table, UNITY_TABLE_FIELD_FILTER)
		UnityDataGen().process(filename, fields, table)
		UnityCodeGen().process(filename, fields, table)

	# server的excel的处理
	def process_excel_server(self, filename, table):
		fields = self.filter_fielddata(table, SERVER_TABLE_FIELD_FILTER)
		print("process_excel_server : " + filename + str(fields))

	# 筛选字段数据
	def filter_fielddata(self, table, fieldfilter):
		fields = []
		for index in range(table.ncols):
			row = table.cell(1, index).value
			for field in fieldfilter:
				if row == field:
					fields.append(index);

		return fields
