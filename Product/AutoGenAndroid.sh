#!/bin/sh

echo Set Path Param

UnityPath="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
SvnPath="/Users/xieliujian/Desktop/xieliujian/unity/unitygameframework_vultr/"
SvnClientPath="/Users/xieliujian/Desktop/xieliujian/unity/unitygameframework_vultr/Client"

echo Step 1 : Update svn

cd $SvnPath
svn update --username xieliujian --password 15351909786abc ./

echo Step 2 : Compile excel

cd ./Tools/Excel2Unity
chmod 777 ./main.py
python3 main.py

echo Step 3 : Build apk
echo Step 3.1 : Build Android Resource

$UnityPath -quit -batchmode -projectPath $SvnClientPath -executeMethod Packager.BuildiPhoneResource

echo Step 3.2 : Build Android Apk

$UnityPath -quit -batchmode -projectPath $SvnClientPath -executeMethod Packager.BuildAndroidNoPlatform

echo Step 4: CommitAPK

svn commit --username xieliujian --password 15351909786abc $SvnPath/Product/UnityGameFramework.apk -m "autobuild"
