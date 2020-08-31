#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TiendaDeMicroservicios.API.Autor/TiendaDeMicroservicios.API.Autor.csproj", "TiendaDeMicroservicios.API.Autor/"]
RUN dotnet restore "TiendaDeMicroservicios.API.Autor/TiendaDeMicroservicios.API.Autor.csproj"
COPY . .
WORKDIR "/src/TiendaDeMicroservicios.API.Autor"
RUN dotnet build "TiendaDeMicroservicios.API.Autor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaDeMicroservicios.API.Autor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaDeMicroservicios.API.Autor.dll"]
