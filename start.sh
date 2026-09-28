#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$ROOT_DIR"

if ! command -v docker >/dev/null 2>&1; then
    echo "[!] Docker no esta instalado. Intentando instalarlo..."

    if command -v apt-get >/dev/null 2>&1; then
        sudo apt-get update
        sudo apt-get install -y docker.io docker-compose-plugin
    elif command -v dnf >/dev/null 2>&1; then
        sudo dnf install -y docker docker-compose-plugin
        sudo systemctl enable --now docker
    elif command -v pacman >/dev/null 2>&1; then
        sudo pacman -Sy --noconfirm docker docker-compose
        sudo systemctl enable --now docker
    else
        echo "[x] No se encontro un gestor de paquetes compatible. Instala Docker manualmente y vuelve a ejecutar este script."
        exit 1
    fi
fi

if ! docker info >/dev/null 2>&1; then
    echo "[+] Iniciando el servicio Docker..."
    if command -v systemctl >/dev/null 2>&1; then
        sudo systemctl start docker
    fi
fi

if ! docker info >/dev/null 2>&1; then
    echo "[x] Docker esta instalado, pero el servicio no esta disponible. Inicia Docker y vuelve a ejecutar el script."
    exit 1
fi

echo "[+] Construyendo el servidor y descargando dependencias Maven si hacen falta..."
docker compose up --build -d

echo "[+] Servidor GraphQL: http://localhost:8081/graphql"
echo "[+] Interfaz para hacer consultas: http://localhost:8081/graphiql"

if command -v xdg-open >/dev/null 2>&1; then
    xdg-open "http://localhost:8081/graphiql" >/dev/null 2>&1 &
elif command -v open >/dev/null 2>&1; then
    open "http://localhost:8081/graphiql" >/dev/null 2>&1 &
else
    echo "[!] Abre manualmente http://localhost:8081/graphiql en tu navegador."
fi

echo "[+] El cliente WinForms requiere Windows y debe ejecutarse mediante Visual Studio o start.ps1."
