@ECHO OFF
set CY=%date:~6,4%
set CM=%date:~3,2%
set CD=%date:~0,2%
set TH=%time:~0,2%
set TM=%time:~3,2%
set PathSource=D:\Repo\btbw
set Path7Zip=D:\Program Files\7-Zip
set PathDropBox=D:\Dropbox\#BTBW
set NameDropbox=btbw

echo.
echo  Backup started at %TIME%
echo ===============================
echo   5%%.. [Directory copying]
echo f|xcopy /y "%PathSource%" "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\" /e  >nul


echo  35%%.. [Directory clearing]
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.git" rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.git"  >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.vs" rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.vs"  >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Temp" rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Temp" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Obj"  rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Obj" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\UnityGenerated" rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\UnityGenerated" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Library" rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Library" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\ExportedObj" rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\ExportedObj" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.svd"  del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.svd" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.userprefs" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.userprefs" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.csproj" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.csproj" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.pidb" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.pidb" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.suo" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.suo" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.sln" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.sln" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.user" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.user" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.unityproj" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.unityproj" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.booproj" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.booproj"
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.DS_Store"  del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.DS_Store"  >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.DS_Store?" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.DS_Store?" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\._*"  del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\._*" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.Spotlight-V100" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.Spotlight-V100" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.Trashes" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\.Trashes" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Icon?" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Icon?" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\ehthumbs.db" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\ehthumbs.db" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Thumbs.db" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\Thumbs.db" >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.apk"  del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.apk"  >nul
IF EXIST "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.unitypackage" del /s "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\*.unitypackage"  >nul


echo  40%%.. [File compressing]
"%Path7Zip%\7z.exe" a -tzip "%PathSource%_%CD%%CM%%CY%-%TH%%TM%.zip" -i!"%PathSource%_%CD%%CM%%CY%-%TH%%TM%\" >nul

echo  70%%.. [Archives copying]
echo y | copy "%PathSource%_%CD%%CM%%CY%-%TH%%TM%.zip" "%PathDropBox%\%NameDropbox%_%CD%%CM%%CY%-%TH%%TM%.zip" >nul

echo  95%%.. [Deleting temporary files]

rd /s /q "%PathSource%_%CD%%CM%%CY%-%TH%%TM%\"
del "%PathSource%_%CD%%CM%%CY%-%TH%%TM%.zip"

echo 100%%..  DONE! 
echo ===============================
echo  Backup ended at %TIME%.
echo.
pause


