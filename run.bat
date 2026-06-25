@echo off
title GARMENT SHOP POS Terminal
cd /d "%~dp0"
echo ==============================================
echo   Starting GARMENT SHOP POS Terminal System
echo ==============================================
echo.
dotnet run --configuration Release
if %errorlevel% neq 0 (
    echo.
    echo [ERROR] The application failed to launch.
    echo Please make sure SQL Server Express is running and .NET SDK is installed.
    echo.
    pause
)
