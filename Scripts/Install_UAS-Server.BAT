@echo off
set ualpropath="D:\Program Files\UAS

echo It will clear the directory %ualpropath%\Server, continue?

rd /S /Q %ualpropath%\Server

echo Install Server
xcopy /r /y Nzl.Test.HookServer\bin\Release\UASServer.exe %ualpropath%\Server\"
xcopy /r /y Nzl.Test.HookServer\bin\Release\UASServer.exe.config %ualpropath%\Server\"
xcopy /r /y Nzl.Test.HookServer\bin\Release\Nzl.Hook.dll %ualpropath%\Server\"
xcopy /r /y Nzl.Test.HookServer\bin\Release\Nzl.Utils.dll %ualpropath%\Server\"
xcopy /r /y Nzl.Test.HookServer\Processes.xml %ualpropath%\Server\"
xcopy /r /y Nzl.Test.HookServer\Processes.xml Nzl.Test.HookServer\bin\Debug\
xcopy /r /y Nzl.Test.HookServer\Processes.xml Nzl.Test.HookServer\bin\Release\