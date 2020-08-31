#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TiendaDeMicroServicios.API.Gateway/TiendaDeMicroServicios.API.Gateway.csproj", "TiendaDeMicroServicios.API.Gateway/"]
RUN dotnet restore "TiendaDeMicroServicios.API.Gateway/TiendaDeMicroServicios.API.Gateway.csproj"
COPY . .
WORKDIR "/src/TiendaDeMicroServicios.API.Gateway"
RUN dotnet build "TiendaDeMicroServicios.API.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaDeMicroServicios.API.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaDeMicroServicios.API.Gateway.dll"]
