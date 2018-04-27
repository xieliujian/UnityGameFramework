
package gamedata

import (
	"container/list"
)

type Ship struct {
	ID int			//		数字索引
	Name string			//		字符串索引
	HP float32			//		数字类型
	LevelAP []int			//		整型数组
	LevelAP1 []float32			//		整型数组
	LevelAP2 []string			//		整型数组
	Map1 map[int]int			//		整型数组
	Map2 map[int]float32			//		整型数组
	Map3 map[int]string			//		整型数组
	Map4 map[string]int			//		整型数组
	Map5 map[string]float32			//		整型数组
	Map6 map[string]string			//		整型数组
}

var (
	ShipData = list.New()
)

func ShipInit() {
	cf := ReadConfigFile(Ship{}, "/Ship/")
	for i := 0; i < cf.NumRecord(); i++ {
		r := cf.Record(i).(*Ship)
		ShipData.PushBack(*r)
	}
}
