#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/OnlineShop/Catalog/OnlineShop.Catalog.API/OnlineShop.Catalog.API.csproj", "Services/OnlineShop/Catalog/OnlineShop.Catalog.API/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Domain.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["BuildingBlocks/Shared.Domain/Shared.Domain.csproj", "BuildingBlocks/Shared.Domain/"]
COPY ["Services/OnlineShop/Catalog/OnlineShop.Catalog.Application/OnlineShop.Catalog.Application.csproj", "Services/OnlineShop/Catalog/OnlineShop.Catalog.Application/"]
COPY ["Services/OnlineShop/Catalog/OnlineShop.Catalog.Domain/OnlineShop.Catalog.Domain.csproj", "Services/OnlineShop/Catalog/OnlineShop.Catalog.Domain/"]
COPY ["Services/OnlineShop/Catalog/OnlineShop.Catalog.Infrastructure/OnlineShop.Catalog.Infrastructure.csproj", "Services/OnlineShop/Catalog/OnlineShop.Catalog.Infrastructure/"]
RUN dotnet restore "Services/OnlineShop/Catalog/OnlineShop.Catalog.API/OnlineShop.Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/OnlineShop/Catalog/OnlineShop.Catalog.API"
RUN dotnet build "OnlineShop.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineShop.Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.Catalog.API.dll"]