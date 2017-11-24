
from Config import SERVER_GO_CODE_TYPE
from Config import SERVER_GO_CODE_EXT
from Config import SERVER_CODE_TYPE

# 获取服务器后缀
def getServerCodeExt():
    if SERVER_GO_CODE_TYPE == SERVER_CODE_TYPE:
        return SERVER_GO_CODE_EXT

    return SERVER_GO_CODE_EXT