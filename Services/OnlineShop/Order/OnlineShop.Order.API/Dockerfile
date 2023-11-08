#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/OnlineShop/Order/OnlineShop.Order.API/OnlineShop.Order.API.csproj", "Services/OnlineShop/Order/OnlineShop.Order.API/"]
COPY ["BuildingBlocks/IntegrationEvents.Domain/IntegrationEvents.Domain.csproj", "BuildingBlocks/IntegrationEvents.Domain/"]
COPY ["BuildingBlocks/Shared.Domain/Shared.Domain.csproj", "BuildingBlocks/Shared.Domain/"]
COPY ["Services/OnlineShop/Order/OnlineShop.Order.Application/OnlineShop.Order.Application.csproj", "Services/OnlineShop/Order/OnlineShop.Order.Application/"]
COPY ["BuildingBlocks/Shared.Behaviors/Shared.Behaviors.csproj", "BuildingBlocks/Shared.Behaviors/"]
COPY ["Services/OnlineShop/Order/OnlineShop.Order.Domain/OnlineShop.Order.Domain.csproj", "Services/OnlineShop/Order/OnlineShop.Order.Domain/"]
COPY ["Services/OnlineShop/Order/OnlineShop.Order.Infrastructure/OnlineShop.Order.Infrastructure.csproj", "Services/OnlineShop/Order/OnlineShop.Order.Infrastructure/"]
RUN dotnet restore "Services/OnlineShop/Order/OnlineShop.Order.API/OnlineShop.Order.API.csproj"
COPY . .
WORKDIR "/src/Services/OnlineShop/Order/OnlineShop.Order.API"
RUN dotnet build "OnlineShop.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineShop.Order.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.Order.API.dll"]