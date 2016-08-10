Include ..\baseSettings.ps1

function ValidateCreatedPackage {
	[CmdletBinding()]
    param(   
        [string] $PackageFilePath	
    )
	
    if (-Not (Test-Path $PackageFilePath)) {
		throw "Problem occurred during packaging : $($PackageFilePath) not created"		
	} 
}

task default -depends ValidateInputs, Init, Clean, Build, RunUnitTests, RunWebUnitTests, UpdateReleaseNumber, Package

task Init {
	cls 
}

task ValidateInputs{ 
  Assert ($ReleaseNumber -ne $null -and $ReleaseNumber -ne "") "ReleaseNumber has not been provided"     
}

task Clean {
    RemoveDirectory($ReleasePackageDirectory)
}

task Build {	
	exec { msbuild $SolutionFile /p:Configuration=Release  }
}

task RunUnitTests {		
	RemoveDirectory($UnitTests_ReportPath)
	
	mkdir $UnitTests_ReportPath  

	exec {  & "$UnitTests_ToolPath" $UnitTests_ProjectFile /config:Release /out=$UnitTests_ReportPath\TestResult.txt /xml:$UnitTests_ReportPath\TestResult.xml  }
}

task RunWebUnitTests {	
	exec {  & "$WebUnitTests_ToolPath"  $WebUnitTests_ProjectDirectory }
}

task UpdateReleaseNumber {	
	UpdateConfigFileValue $WebProject_Directory web.config "/configuration/appSettings/add[@key='application.ReleaseNumber']"  $ReleaseNumber
}

task Package {	
	RemoveDirectory($ReleasePackageDirectory)

	mkdir $ReleasePackageDirectory
	
    exec { msbuild $WebProject_ProjectFile /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPublishPackageToFileShare=$ReleasePackageDirectory /p:VisualStudioRelease=12.0 /p:OctoPackPackageVersion=$ReleaseNumber }	
	
	ValidateCreatedPackage("$ReleasePackageDirectory\BC.EQCS.Web.$ReleaseNumber.nupkg")
	
    exec { msbuild $DatabaseProject_ProjectFile /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPublishPackageToFileShare=$ReleasePackageDirectory /p:VisualStudioRelease=12.0 /p:OctoPackPackageVersion=$ReleaseNumber }	
	
	ValidateCreatedPackage("$ReleasePackageDirectory\BC.EQCS.Database.$ReleaseNumber.nupkg")	
}