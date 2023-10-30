#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Shops/Shops.API/Shops.API.csproj", "Services/Shops/Shops.API/"]
COPY ["Services/Shops/Shops.Application/Shops.Application/Shops.Application.csproj", "Services/Shops/Shops.Application/Shops.Application/"]
COPY ["BuildingBlocks/Shared.Domain/Shared.Domain.csproj", "BuildingBlocks/Shared.Domain/"]
COPY ["Services/Shops/Shops.Domain/Shops.Domain.csproj", "Services/Shops/Shops.Domain/"]
COPY ["Services/Shops/Shops.Infrastructure/Shops.Infrastructure.csproj", "Services/Shops/Shops.Infrastructure/"]
RUN dotnet restore "Services/Shops/Shops.API/Shops.API.csproj"
COPY . .
WORKDIR "/src/Services/Shops/Shops.API"
RUN dotnet build "Shops.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shops.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shops.API.dll"]