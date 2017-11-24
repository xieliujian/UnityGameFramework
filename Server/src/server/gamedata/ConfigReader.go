package gamedata

import (
	"github.com/name5566/leaf/log"
	"reflect"
	"server/gamedata/internal"
)

func ReadConfigFile(st interface{}, dir string)  *internal.ConfigFile{
	cf, err := internal.New(st)
	if err != nil {
		log.Fatal("%v", err)
	}

	fn := reflect.TypeOf(st).Name() + ".txt"
	err = cf.Read("gamedata/" + dir + fn)
	if err != nil {
		log.Fatal("%v : %v", fn, err)
	}

	return cf
}