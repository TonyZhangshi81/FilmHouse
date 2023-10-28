@echo off
dotnet clean FilmHouse.sln
if %errorlevel% neq 0 (
  pause
  exit /b 1
)

dotnet build FilmHouse.sln -m:1
if %errorlevel% neq 0 (
  pause
  exit /b 1
)

pause