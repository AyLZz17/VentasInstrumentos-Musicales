$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$client = Join-Path $root "POCInstrumentoCuerda\POCInstrumentoCuerda\POCInstrumentoCuerda\bin\Debug\POCInstrumentoCuerda.exe"

Push-Location $root
try {
    docker compose up -d

    if (-not (Test-Path $client)) {
        throw "No se encontró el cliente compilado. Compílalo en Visual Studio antes de ejecutar este script: $client"
    }

    Start-Process -FilePath $client -WorkingDirectory (Split-Path $client)
    Write-Host "Servidor GraphQL: http://localhost:8081/graphql"
    Write-Host "Cliente WinForms iniciado."
}
finally {
    Pop-Location
}
