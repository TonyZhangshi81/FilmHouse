@echo off

dotnet restore

dotnet clean FilmHouse-all.sln
if %errorlevel% neq 0 (
  pause
  exit /b 1
)

dotnet build FilmHouse-all.sln -m:1
if %errorlevel% neq 0 (
  pause
  exit /b 1
)

pause