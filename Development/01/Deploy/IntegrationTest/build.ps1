param([string]$environment)

Include ..\baseSettings.ps1

properties {    
	$connection_string = "Data Source=10.46.176.52;Initial Catalog=EQCS_$environment;User Id=eqcs_" + $environment  + "_user;Password=EQCS009;Integrated Security=false;MultipleActiveResultSets=True"		
}

task default -depends Init, UpdateConnectionString, RunAcceptanceTests

task Init {
	cls 
}

task RunAcceptanceTests {	
	if (Test-Path $IntegrationTests_ReportPath) {
		remove-item $IntegrationTests_ReportPath -Recurse					  
	} 
	mkdir $IntegrationTests_ReportPath  
	exec { msbuild "$IntegrationTests_ProjectFile" /p:Configuration=Release  }
	exec {  & "$NunitToolPath"  /labels /out=$IntegrationTests_ReportPath\TestResult.txt /xml=$IntegrationTests_ReportPath\TestResult.xml $IntegrationTests_ProjectDirectory\bin\Release\BC.EQCS.Integration.dll }
	exec {  & "$IntegrationTests_ToolPath"  nunitexecutionreport   "$IntegrationTests_ProjectFile" "/out:$IntegrationTests_ReportPath\TestResult.html" "/xmlTestResult:$IntegrationTests_ReportPath\TestResult.xml" } 	          
} 


task UpdateConnectionString {
	UpdateConfigFileValue $IntegrationTests_ProjectDirectory "app.config" "/configuration/connectionStrings/add[@name='EqcsEntities']/@connectionString" $connection_string
}
