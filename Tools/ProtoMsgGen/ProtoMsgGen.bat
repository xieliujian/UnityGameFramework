
protoc --proto_path=./Msg --csharp_out=../../Client/Assets/Scripts/Msg ./Msg/*.proto
protoc --proto_path=./Msg --go_out=../../Server/src/server/msg ./Msg/*.proto

python3 main.py

pause