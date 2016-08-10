$dbName = $OctopusParameters['Database.Name']
$server = $OctopusParameters['Database.ServerName']
$user = $OctopusParameters['AutomatedDeploymentUser.Name'] 
$pwd = $OctopusParameters['AutomatedDeploymentUser.Password']

$SqlPackageExe = "C:\Program Files (x86)\Microsoft SQL Server\110\DAC\bin\SqlPackage.exe"

Write-Host DbName: $dbName
Write-Host Server: $server
Write-Host User: $user
	
& "$SqlPackageExe"  /Action:Publish  /SourceFile:BC.EQCS.Database.dacpac /TargetServerName:$server /TargetDatabaseName:$dbName /p:IncludeCompositeObjects=true /TargetUser:$user /TargetPassword:$pwd  /p:CreateNewDatabase=True 


