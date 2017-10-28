package internal

import (
	"reflect"
	"server/msg"
	"github.com/name5566/leaf/gate"
	"fmt"
)

func init(){
	// 向当前模块（game 模块）注册 Hello 消息的消息处理函数 handleHello
	handler(&msg.TosChat{}, handletosChat)
}

func handler(m interface{}, h interface{})  {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func handletosChat(args []interface{}){
	// 收到的 Hello 消息
	m := args[0].(*msg.TosChat)
	// 消息的发送者
	a := args[1].(gate.Agent)

	// 输出收到的消息的内容
	fmt.Println(m.Name)
	
	//给发送者回应一个 Hello 消息
	a.WriteMsg(&msg.TocChat{
		Name : m.Name,
		Content:m.Content,
	})
}
