@ECHO off
CLS
:init
setlocal DisableDelayedExpansion
set "batchPath=%~0"
for %%k in (%0) do set batchName=%%~nk
set "vbsGetPrivileges=%temp%\OEgetPriv_%batchName%.vbs"
setlocal EnableDelayedExpansion
:checkPrivileges
NET FILE 1>NUL 2>NUL
if '%errorlevel%' == '0' ( 
   goto gotPrivileges 
) else ( 
   goto getPrivileges
)
:getPrivileges
if '%1'=='ELEV' (ECHO ELEV & shift /1 & goto gotPrivileges)
ECHO.
ECHO ********************************
ECHO 请求 UAC 权限批准……
ECHO ********************************
ECHO Set UAC = CreateObject^("Shell.Application"^) > "%vbsGetPrivileges%"
ECHO args = "ELEV " >> "%vbsGetPrivileges%"
ECHO For Each strArg in WScript.Arguments >> "%vbsGetPrivileges%"
ECHO args = args ^& strArg ^& " "  >> "%vbsGetPrivileges%"
ECHO Next >> "%vbsGetPrivileges%"
ECHO UAC.ShellExecute "!batchPath!", args, "", "runas", 1 >> "%vbsGetPrivileges%"
"%SystemRoot%\System32\WScript.exe" "%vbsGetPrivileges%" %*
exit /B
:gotPrivileges
setlocal & pushd .
cd /d %~dp0
if '%1'=='ELEV' (del "%vbsGetPrivileges%" 1>nul 2>nul  &  shift /1)
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::     安装过程     ::
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:-------------------------------------- 
set "starttime=%time%"
ZFile.exe -i
net start ZFile
cd ..
ECHO.
set "endtime=%time%"
set /a "times=(%endtime:~,2%-%starttime:~,2%%add%)*360000+(1%endtime:~3,2%%%100-1%starttime:~3,2%%%100)*6000+(1%endtime:~6,2%%%100-1%starttime:~6,2%%%100)*100+(1%endtime:~-2%%%100-1%starttime:~-2%%%100)" 
ECHO  安装完成.................................%times%ms【OK】  
pause
exit