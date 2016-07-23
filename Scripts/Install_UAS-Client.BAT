@echo off

set ualpropath="D:\Program Files\UAS

echo It will clear the directory %ualpropath%\Client, continue?

pause

rd /S /Q %ualpropath%\Client

echo Install Client
xcopy /r /y ..\Test\Nzl.Test.HookClient\bin\Release\UASClient.exe %ualpropath%\Client\"
xcopy /r /y ..\Test\Nzl.Test.HookClient\bin\Release\UASClient.exe.config %ualpropath%\Client\"
xcopy /r /y ..\Test\Nzl.Test.HookClient\bin\Release\Nzl.Hook.dll %ualpropath%\Client\"
xcopy /r /y ..\Test\Nzl.Test.HookClient\bin\Release\Nzl.Util.dll %ualpropath%\Client\"