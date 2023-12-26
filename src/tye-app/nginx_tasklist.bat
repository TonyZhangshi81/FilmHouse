@echo off
CD ..\..\Tools\nginx-1.24.0
tasklist /fi "imagename eq nginx.exe"

PAUSE
