$cwd = Get-Location
Set-Location $PSScriptRoot

dotnet run -c Release --project .\RoomexEarth.Algorithms.Benchmarks\RoomexEarth.Algorithms.Benchmarks.csproj

Set-Location $cwd
