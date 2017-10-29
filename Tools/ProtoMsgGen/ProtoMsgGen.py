
from proto import loadProto
from csfile import genCSfile
from gofile import genGolangfile

class ProtoMsgGen:

    # 消息处理函数
    def process(self):
        protos = loadProto()
        genCSfile(protos)
        genGolangfile(protos)