@echo off

IF .%1 == . (
	@echo off
	set /p VERSION="Enter version number (eg. 3.2.1): " %=%
	@echo on
)
IF NOT .%1 == . (
	set VERSION=%1
)

REM --------------------------------
REM Update version number
REM --------------------------------

msbuild Target-Update.msbuild
set BUILD_STATUS=%ERRORLEVEL% 

if %BUILD_STATUS%==0 goto continueupdate
if not %BUILD_STATUS%==0 goto failupdate
 
:failupdate
exit /b 1 

:continueupdate

REM --------------------------------
REM Build solution
REM --------------------------------

msbuild Target-Build.msbuild
set BUILD_STATUS=%ERRORLEVEL% 

if %BUILD_STATUS%==0 goto continuebuild
if not %BUILD_STATUS%==0 goto failbuild
 
:failbuild
exit /b 1 

:continuebuild

REM --------------------------------
REM Package solution
REM --------------------------------

msbuild Target-Package.msbuild
BUILD_STATUS=%ERRORLEVEL% 

if %BUILD_STATUS%==0 goto continue 
if not %BUILD_STATUS%==0 goto failpackage 
 
:failpackage
exit /b 1 

:continue

REM --------------------------------
REM Package NuGet 
REM --------------------------------

msbuild Target-NuGet.msbuild
BUILD_STATUS=%ERRORLEVEL% 

if %BUILD_STATUS%==0 goto continue 
if not %BUILD_STATUS%==0 goto failpackage 
 
:failpackage
exit /b 1 

:continue

REM --------------------------------
REM Commit and tag
REM --------------------------------

git add -A
git commit -a -m "Release v%VERSION%"
git tag v%VERSION%

exit /b 0 