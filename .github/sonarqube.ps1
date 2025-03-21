param (
  [Parameter(Mandatory=$false)] [string] $project = "rn-core",
  [Parameter(Mandatory=$false)] [string] $sqUrl = "http://192.168.0.60:9002",
  [Parameter(Mandatory=$true)] [string] $sqToken
)
$rootDir        = $PSScriptRoot;
$artifactsDir   = [IO.Path]::GetFullPath((Join-Path $rootDir "../artifacts/"));
$coverageDir    = [IO.Path]::GetFullPath((Join-Path $artifactsDir "test-coverage/"));
$sqReportPaths  = ($coverageDir + "**/coverage.opencover.xml");

# dotnet tool install --global dotnet-sonarscanner
dotnet sonarscanner begin /k:$project /d:sonar.host.url=$sqUrl  /d:sonar.login=$sqToken /d:sonar.cs.opencover.reportsPaths=$sqReportPaths
./ci-build.ps1 -project "RnCore.DevConsole"
./ci-test.ps1
dotnet sonarscanner end /d:sonar.login=$sqToken
