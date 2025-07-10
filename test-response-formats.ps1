# Test script to analyze response formats of all 4 VLite Core APIs
# Tests vcInitialise, vcConnect, vcDisconnect, and vcTerminate

$baseUrl = "http://localhost:5000/api"
$headers = @{
    "Content-Type" = "application/json"
    "Accept" = "application/json"
}

Write-Host "=== VLite API Response Format Analysis ===" -ForegroundColor Green
Write-Host "Testing all 4 core API endpoints..." -ForegroundColor Yellow
Write-Host ""

# Test 1: vcInitialise
Write-Host "1. Testing vcInitialise..." -ForegroundColor Cyan
$initRequest = @{
    licenseKey = "test-license-key"
    versionMajor = 1
    versionMinor = 0
} | ConvertTo-Json

try {
    $response1 = Invoke-RestMethod -Uri "$baseUrl/vcInitialise" -Method POST -Body $initRequest -Headers $headers
    Write-Host "vcInitialise Response:" -ForegroundColor Green
    $response1 | ConvertTo-Json -Depth 5 | Write-Host
} catch {
    Write-Host "vcInitialise Error:" -ForegroundColor Red
    $_.Exception.Message | Write-Host
    if ($_.ErrorDetails) {
        $_.ErrorDetails.Message | Write-Host
    }
}
Write-Host ""

# Test 2: vcConnect  
Write-Host "2. Testing vcConnect..." -ForegroundColor Cyan
$connectRequest = @{
    ipAddr = "192.168.1.100"
    portNumber = 502
    password = "test"
    readOnly = $true
} | ConvertTo-Json

try {
    $response2 = Invoke-RestMethod -Uri "$baseUrl/vcConnect" -Method POST -Body $connectRequest -Headers $headers
    Write-Host "vcConnect Response:" -ForegroundColor Green
    $response2 | ConvertTo-Json -Depth 5 | Write-Host
} catch {
    Write-Host "vcConnect Error:" -ForegroundColor Red
    $_.Exception.Message | Write-Host
    if ($_.ErrorDetails) {
        $_.ErrorDetails.Message | Write-Host
    }
}
Write-Host ""

# Test 3: vcDisconnect (RAW API - should return just integer)
Write-Host "3. Testing vcDisconnect (RAW API)..." -ForegroundColor Cyan
try {
    $response3 = Invoke-RestMethod -Uri "$baseUrl/vcDisconnect/123" -Method POST -Headers $headers
    Write-Host "vcDisconnect Response (RAW):" -ForegroundColor Green
    Write-Host "Type: $($response3.GetType().Name)" -ForegroundColor Magenta
    $response3 | Write-Host
} catch {
    Write-Host "vcDisconnect Error:" -ForegroundColor Red
    $_.Exception.Message | Write-Host
    if ($_.ErrorDetails) {
        $_.ErrorDetails.Message | Write-Host
    }
}
Write-Host ""

# Test 4: vcTerminate
Write-Host "4. Testing vcTerminate..." -ForegroundColor Cyan
try {
    $response4 = Invoke-RestMethod -Uri "$baseUrl/vcTerminate" -Method POST -Headers $headers
    Write-Host "vcTerminate Response:" -ForegroundColor Green
    $response4 | ConvertTo-Json -Depth 5 | Write-Host
} catch {
    Write-Host "vcTerminate Error:" -ForegroundColor Red
    $_.Exception.Message | Write-Host
    if ($_.ErrorDetails) {
        $_.ErrorDetails.Message | Write-Host
    }
}

Write-Host ""
Write-Host "=== Summary ===" -ForegroundColor Green
Write-Host "Expected Formats:" -ForegroundColor Yellow
Write-Host "  vcInitialise: Standard API wrapper format" -ForegroundColor White
Write-Host "  vcConnect:    Standard API wrapper format" -ForegroundColor White  
Write-Host "  vcDisconnect: RAW integer response (no wrapper)" -ForegroundColor White
Write-Host "  vcTerminate:  Standard API wrapper format" -ForegroundColor White
Write-Host ""
Write-Host "Standard Format:" -ForegroundColor Yellow
Write-Host '{
  "success": true/false,
  "data": {...},
  "error": null/{...},
  "timestamp": "2025-06-25T..."
}' -ForegroundColor Gray
