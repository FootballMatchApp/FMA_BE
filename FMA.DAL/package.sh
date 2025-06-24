#!/bin/bash

PROJECT_NAME="FMA.DAL.csproj"

PACKAGES=(
  "Microsoft.EntityFrameworkCore"
  "Microsoft.EntityFrameworkCore.Design"
  "Microsoft.EntityFrameworkCore.SqlServer"
  "Microsoft.EntityFrameworkCore.Tools"
  "Microsoft.Extensions.Configuration"
  "Microsoft.Extensions.Configuration.Json"
)

# Phiên bản cần thêm
VERSION="8.0.0"

for pkg in "${PACKAGES[@]}"; do
  echo "Adding $pkg..."
  dotnet add "$PROJECT_NAME" package "$pkg" --version "$VERSION"
done

echo "✅ Tất cả gói đã được thêm thành công."
