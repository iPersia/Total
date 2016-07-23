@echo off

set rssmonpath="D:\Program Files\Nzl\Web

echo Install Rss Reader Monitor

echo Copy exe.
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Web.Forms.exe %rssmonpath%\"
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Web.Forms.exe.config %rssmonpath%\"

echo Copy dll
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Web.ProductClawer.dll %rssmonpath%\"
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Log4Net.dll %rssmonpath%\"
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Web.WebPage.dll %rssmonpath%\"
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Web.Interface.dll %rssmonpath%\"
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Web.Util.dll %rssmonpath%\"
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Web.Core.dll %rssmonpath%\"
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\Nzl.Util.dll %rssmonpath%\"

rd /S /Q %rssmonpath%\RssReaders

echo Rss readers.
xcopy /r /y ..\Web\Nzl.Web.Forms\bin\Release\RssReaders\Nzl.Web.Rss.dll %rssmonpath%\RssReaders\"