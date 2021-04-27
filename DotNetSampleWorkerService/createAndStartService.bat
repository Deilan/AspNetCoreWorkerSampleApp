sc create DotNetSampleWorkerService binPath= "%~dp0\DotNetSampleWorkerService.exe" DisplayName= "DotNetSampleWorkerService" start= auto
sc failure DotNetSampleWorkerService reset= 86400 actions= restart/1/restart/1/restart/1
sc start DotNetSampleWorkerService