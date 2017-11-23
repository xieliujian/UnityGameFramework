package gamedata

import (
	"github.com/name5566/leaf/log"
	"reflect"
	"server/gamedata/internal"
)

func ReadConfigFile(st interface{})  *internal.ConfigFile{
	cf, err := internal.New(st)
	if err != nil {
		log.Fatal("%v", err)
	}

	fn := reflect.TypeOf(st).Name() + ".txt"
	err = cf.Read("gamedata/" + fn)
	if err != nil {
		log.Fatal("%v : %v", fn, err)
	}

	return cf
}