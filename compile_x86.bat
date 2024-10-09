@echo off

:: Set current directory to the folder where the batch file is located
cd /d "%~dp0"

:: Define the subfolder name (replace "NewFolder" with the desired folder name)
set "subfolder=%~dp0\CCrystal\Release_x86"

:: Check if the subfolder exists, and if not, create it
if not exist "%subfolder%" (
    echo Subfolder does not exist. Creating subfolder...
    mkdir "%subfolder%"
) else (
    echo Subfolder already exists.
)


"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe" CCrystal\CCrystal.csproj /t:Clean,Build /p:OutputPath="%subfolder%" /p:BuildProjectReferences=false /p:Configuration=Release /p:Platform="x86" /logger:FileLogger,Microsoft.Build.Engine;logfile=%subfolder%\CCrystal.log

cmd