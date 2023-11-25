$cwd = Get-Location
Set-Location $PSScriptRoot

function Invoke-Tests {
    param(
        [Parameter(Mandatory)]$csproj,
        [Parameter(Mandatory)]$includeAssembly,
        [Parameter(Mandatory)]$coverageName,
        [string]$exclude = 'NONE'
    )
    Write-Host "Running tests from $csproj"
    dotnet test $csproj -c Release --framework net8.0 -l "console;verbosity=normal" --results-directory:"$PSScriptRoot\TestResults" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=$PSScriptRoot/TestResults/$coverageName.xml /p:Include=[$includeAssembly]* /p:Exclude=[*]$exclude
    if ($LastExitCode -ne 0) {
        Write-Host "TESTS FAILED $csproj" -ForegroundColor Red
        exit 1
    }
}

Write-Host Running Tests
Invoke-Tests -csproj 'RoomexEarth.Algorithms.Tests/RoomexEarth.Algorithms.Tests.csproj' -includeAssembly 'RoomexEarth.Algorithms' -coverageName 'alg'
Invoke-Tests -csproj 'RoomexEarth.Logic.Tests/RoomexEarth.Logic.Tests.csproj' -includeAssembly 'RoomexEarth.Logic' -coverageName 'logic'
Invoke-Tests -csproj 'RoomexEarth.Api.Tests/RoomexEarth.Api.Tests.csproj' -includeAssembly 'RoomexEarth.Api' -exclude 'Program' -coverageName 'api'

Write-Host Generating Reports
Set-Location $PSScriptRoot
# Check dotnet-reportgenerator-globaltool is installed
dotnet tool list -g dotnet-reportgenerator-globaltool | Out-Null
if ($LastExitCode -ne 0) {
    dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.1.26
}
reportgenerator -reports:'TestResults/alg.xml;TestResults/logic.xml;TestResults/api.xml' -targetdir:TestResults/CoverageReport -reporttypes:"Html_Dark;TextSummary"
if ($IsLinux) {
    $reportPath = Join-Path -Path $PSScriptRoot -ChildPath './TestResults/CoverageReport/index.html'
    Write-Host "HTML Coverage report: $reportPath"
    $report = Get-Content 'TestResults/CoverageReport/Summary.txt' -Raw
    Write-Host $report
}
else {
    ./TestResults/CoverageReport/index.html
}

Set-Location $cwd
