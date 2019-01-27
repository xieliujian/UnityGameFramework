
echo Set Path Param
set UnityPath=D:/Program Files/Unity2017.1.0f3/Editor/
set SvnPath=E:/Xieliujian/UnityGameFramework_Vultr/
set SvnClientPath=E:/Xieliujian/UnityGameFramework_Vultr/Client

echo Step 1 : Update svn
TortoiseProc.exe /command:update /path:%SvnPath% /closeonend:1

echo Step 2 : Compile excel
cd ../Tools/Excel2Unity
python3 main.py

echo Step 3 : Build apk
echo Step 3.1 : Build Android Resource
"%UnityPath%/Unity.exe" -quit -batchmode -projectPath %SvnClientPath% -executeMethod Packager.BuildAndroidResource

echo Step 3.2 : Build Android Apk
"%UnityPath%/Unity.exe" -quit -batchmode -projectPath %SvnClientPath% -executeMethod Packager.BuildAndroidNoPlatform

echo Step 4: CommitAPK
svn commit %SvnPath%/Product/UnityGameFramework.apk -m "autobuild"

echo pause
