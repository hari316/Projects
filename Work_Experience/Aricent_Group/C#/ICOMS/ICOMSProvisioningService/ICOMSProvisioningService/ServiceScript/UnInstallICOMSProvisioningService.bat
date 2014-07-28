@ECHO OFF 
 
REM The following directory is for .NET 4.0 
set fileName=ICOMSProvisioningService.exe
set currDir=%~dp0
echo Installing ICOMSProvisioningService... 
echo --------------------------------------------------- 
installutil.exe -u %currDir%%fileName%
echo --------------------------------------------------- 
echo Done. 
pause