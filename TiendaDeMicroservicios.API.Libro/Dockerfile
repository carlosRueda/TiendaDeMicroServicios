#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TiendaDeMicroservicios.API.Libro/TiendaDeMicroservicios.API.Libro.csproj", "TiendaDeMicroservicios.API.Libro/"]
RUN dotnet restore "TiendaDeMicroservicios.API.Libro/TiendaDeMicroservicios.API.Libro.csproj"
COPY . .
WORKDIR "/src/TiendaDeMicroservicios.API.Libro"
RUN dotnet build "TiendaDeMicroservicios.API.Libro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaDeMicroservicios.API.Libro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaDeMicroservicios.API.Libro.dll"]
