$url = 'http://localhost:48080/distance/from/-33/-56/to/40/12'
$headers = @{
    'Content-Type' = 'application/json'
    'Accept' = 'application/json'
}

for ($attempt = 1; $attempt -le 30; $attempt++) {
    try {
        Write-Host "Verifying API using $url"
        $response = Invoke-RestMethod -Uri $url -Method Get -Headers $Headers
        $distanceKm = $response.distanceKm
        if ($distanceKm -gt 0) {
            break;
        }
    }
    catch {
        Write-Host "Failed on attempt $attempt"
        Write-Host $_.Exception.Message
        if ($attempt -lt 30) {
            Start-Sleep -Seconds 2
        }
    }
}

if ($distanceKm.ToString('0.000000') -eq '10706.043195') {
    Write-Host 'API verification successful.' -ForegroundColor Green
    Start-Process 'http://localhost:48080/index.html'
}
else {
    Write-Host 'API verification failed.' -ForegroundColor Red
    Write-Host $response -ForegroundColor Red
}
