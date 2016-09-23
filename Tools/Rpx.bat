REM @echo off

set workingDirectory=%~1
set compressedDirectory=%~2

for /f "delims=" %%i in ('dir /ad /b /on "%workingDirectory%"') do if not "%%i"=="%compressedDirectory%" xcopy /s /c /d /e /h /i /r /k /y "%workingDirectory%\%%i" "%workingDirectory%\%compressedDirectory%\%%i"

echo Copy GUI Type Descriptors Assembly
xcopy /y "%workingDirectory%NgimuGui.TypeDescriptors.dll" "%workingDirectory%%compressedDirectory%\"
