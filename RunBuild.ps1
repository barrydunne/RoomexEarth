$cwd = Get-Location
Set-Location $PSScriptRoot

Write-Host '############################################'
Write-Host '## Building Docker image                  ##'
Write-Host '############################################'

docker build -t roomex-earth-api:dev -f ./RoomexEarth.Api/Dockerfile .

Write-Host '############################################'
Write-Host '## Running Docker container               ##'
Write-Host '############################################'

docker rm RoomexEarth.Api
docker run -d --name RoomexEarth.Api -p 48080:8080 --restart unless-stopped roomex-earth-api:dev

Write-Host '############################################'
Write-Host '## Performing API verification            ##'
Write-Host '############################################'

./RunVerification.ps1

Set-Location $cwd
