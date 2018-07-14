set CurDir=%~dp0
set LibName=%1
pdb2mdb %LibName%.dll
xcopy %LibName%.dll %CurDir%..\..\Client\Assets\Plugins\ManagedLib\ /y
xcopy %LibName%.dll.mdb %CurDir%..\..\Client\Assets\Plugins\ManagedLib\ /y