#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$ROOT_DIR"

docker compose up -d

echo "Servidor GraphQL: http://localhost:8081/graphql"
echo "El cliente WinForms requiere Windows y debe ejecutarse desde Visual Studio o mediante start.ps1."
