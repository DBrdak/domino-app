#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/OnlineShop/ShoppingCart/OnlineShop.ShoppingCart.API/OnlineShop.ShoppingCart.API.csproj", "Services/OnlineShop/ShoppingCart/OnlineShop.ShoppingCart.API/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "Services/OnlineShop/ShoppingCart/OnlineShop.ShoppingCart.API/OnlineShop.ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/Services/OnlineShop/ShoppingCart/OnlineShop.ShoppingCart.API"
RUN dotnet build "OnlineShop.ShoppingCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineShop.ShoppingCart.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.ShoppingCart.API.dll"]