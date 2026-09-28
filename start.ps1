$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$client = Join-Path $root "POCInstrumentoCuerda\POCInstrumentoCuerda\POCInstrumentoCuerda\bin\Debug\POCInstrumentoCuerda.exe"

Push-Location $root
try {
    if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
        Write-Host "[!] Docker Desktop no esta instalado o no esta disponible en el PATH."

        if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
            throw "[x] No se encontro winget. Instala Docker Desktop manualmente desde https://www.docker.com/products/docker-desktop/ y vuelve a ejecutar el script."
        }

        Write-Host "[+] Instalando Docker Desktop mediante winget..."
        winget install --id Docker.DockerDesktop --exact --source winget --accept-source-agreements --accept-package-agreements
        if ($LASTEXITCODE -ne 0) {
            throw "[x] La instalacion de Docker Desktop fallo. Ejecuta PowerShell como administrador e intenta nuevamente."
        }

        $env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' + [System.Environment]::GetEnvironmentVariable('Path', 'User')

        if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
            throw "[!] Docker Desktop fue instalado. Cierra y vuelve a abrir PowerShell para actualizar el PATH, luego ejecuta este script nuevamente."
        }
    }

    Write-Host "[+] Construyendo el servidor y descargando dependencias Maven si hacen falta..."
    docker compose up --build -d

    Write-Host "[+] Servidor construido y en ejecucion."

    if (-not (Test-Path $client)) {
        throw "[x] No se encontro el cliente compilado. Compilalo en Visual Studio antes de ejecutar este script: $client"
    }
    Write-Host "[+] Iniciando cliente WinForms..."
    Start-Process -FilePath $client -WorkingDirectory (Split-Path $client)
    Write-Host "[+] Servidor GraphQL: http://localhost:8081/graphql"
    Write-Host "[+] Interfaz para hacer consultas: http://localhost:8081/graphiql"
    Write-Host "[+] Iniciando navegador para GraphiQL..."
    Start-Process "http://localhost:8081/graphiql"
    Write-Host "[+] Cliente WinForms iniciado."
}
finally {
    Pop-Location
}
