properties {
    $BaseDirectory = Resolve-Path "..\..\"
    $SolutionFile = "$BaseDirectory\BC.EQCS.sln"
	
	$WebProject_Directory = "$BaseDirectory\BC.EQCS.Web"
	$WebProject_ProjectFile = "$WebProject_Directory\BC.EQCS.Web.csproj"
		
	$DatabaseProject_ProjectFile = "$BaseDirectory\BC.EQCS.Db\BC.EQCS.Database.sqlproj"

	$DeployDirectory = "$BaseDirectory\Deploy"       
    $ReleasePackageDirectory = "$DeployDirectory\_release"
	
	$ReportsDirectory = "$DeployDirectory\_reports"    	
		
	$WebUnitTests_ToolPath = "$BaseDirectory\packages\Chutzpah.4.0.1\tools\chutzpah.console.exe"
	$WebUnitTests_ProjectDirectory = "$BaseDirectory\BC.EQCS.Web.UnitTest\"
		
	$UnitTests_ProjectFile = "$BaseDirectory\BC.EQCS.UnitTests\BC.EQCS.UnitTests.csproj"		
	$UnitTests_ToolPath = "$DeployDirectory\Tools\Nunit\nunit-console.exe"
	$UnitTests_ReportPath = "$ReportsDirectory\unit_tests" 
	
	$IntegrationTests_ProjectDirectory = "$BaseDirectory\BC.EQCS.Integration"	
	$IntegrationTests_ProjectFile = "$IntegrationTests_ProjectDirectory\BC.EQCS.Integration.csproj"
	
	$IntegrationTests_ToolPath = "$BaseDirectory\packages\SpecFlow.1.9.0\tools\SpecFlow.exe"	
	$IntegrationTests_ReportPath = "$ReportsDirectory\integration_tests" 
	
	$NunitToolPath = "$DeployDirectory\Tools\Nunit\nunit-console.exe"	   
}

function UpdateConfigFileValue {
	[CmdletBinding()]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]        
        [string] $ProjectDirectory,
		[string] $ConfigFileName,
		[string] $ConnectionStringXPath ,
		[string] $ValueToInsert   		
    )
	
    Get-ChildItem $ProjectDirectory -Include $ConfigFileName  -Recurse | %{ 	
		 
		$filePath = $_.FullName    
   
		[xml] $fileXml = Get-Content $filePath 
		$node = $fileXml.SelectSingleNode($ConnectionStringXPath) 
		if ($node) { 
			$node.Value = $ValueToInsert   

			$fileXml.Save($filePath)  
		} 		
		
	}
}

function RemoveDirectory {
	[CmdletBinding()]
    param(
              
        [string] $Directory	
    )
	
    if (Test-Path $Directory) {
		remove-item $Directory -Recurse					  
	}
}