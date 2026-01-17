# 2026 Copyright Talaryon Labs
# Project: ERPNext-Shopify-TokenSync
# Maintainer: Talaryon Labs

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["erpnext-shopify-tokensync/erpnext-shopify-tokensync.csproj", "erpnext-shopify-tokensync/"]
RUN dotnet restore "erpnext-shopify-tokensync/erpnext-shopify-tokensync.csproj"
COPY . .
WORKDIR "/src/erpnext-shopify-tokensync"
RUN dotnet build "./erpnext-shopify-tokensync.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./erpnext-shopify-tokensync.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "erpnext-shopify-tokensync.dll"]