@echo off
setlocal

set "currentPath=%~dp0"

dotnet format "%currentPath%..\ViSort\ViSort.sln" --verbosity detailed

endlocal
