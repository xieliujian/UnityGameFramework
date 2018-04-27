
package gamedata

import (
	"container/list"
)

type Task struct {
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
	TaskData = list.New()
)

func TaskInit() {
	cf := ReadConfigFile(Task{}, "/Task/")
	for i := 0; i < cf.NumRecord(); i++ {
		r := cf.Record(i).(*Task)
		TaskData.PushBack(*r)
	}
}

func GetTaskByID(_ID int, _Name string) Task {
	for e := TaskData.Front(); e != nil; e = e.Next() {
		if e.Value.(Task).ID == _ID && e.Value.(Task).Name == _Name {
			return e.Value.(Task)
		}
	}

	return TaskData.Front().Value.(Task)
}
