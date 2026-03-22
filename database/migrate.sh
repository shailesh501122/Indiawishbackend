#!/usr/bin/env bash
set -euo pipefail

cd "$(dirname "$0")/.."

dotnet ef migrations add InitialCreate --project src/BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj --startup-project src/API/RealEstatePlatform.API/RealEstatePlatform.API.csproj --output-dir Persistence/Migrations

dotnet ef database update --project src/BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj --startup-project src/API/RealEstatePlatform.API/RealEstatePlatform.API.csproj
