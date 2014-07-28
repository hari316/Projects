@ECHO OFF 

REM The following directory is for .NET 4.0 

set currDir=%~dp0
set fileName=ICOMSProvisioningService.exe
echo Installing ICOMSProvisioningService... 
echo --------------------------------------------------- 
installutil.exe -i %currDir%%fileName%
echo --------------------------------------------------- 
echo Done. 
pause