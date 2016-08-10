param([string]$environment)

function ExitWithCode([string]$exitCode)
{
	$host.SetShouldExit($exitcode)
	exit 
}

Try 
{
	Set-ExecutionPolicy RemoteSigned
	Import-Module ..\..\packages\psake.4.4.1\tools\psake.psm1
	Invoke-Psake -framework 4.0 .\build.ps1 -parameters @{environment=$environment;}
	ExitWithCode($LastExitCode)
}
Catch 
{
	Write-Error $_
	Write-Host "GO.PS1 EXITS WITH ERROR"
	ExitWithCode 9
}