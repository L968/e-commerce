FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Ecommerce.Domain/*.csproj", "Ecommerce.Domain/"]
COPY ["Ecommerce.Application/*.csproj", "Ecommerce.Application/"]
COPY ["Ecommerce.Order.API/*.csproj", "Ecommerce.Order.API/"]
COPY ["Common/Ecommerce.Common.Infra/*.csproj", "Common/Ecommerce.Common.Infra/"]
RUN dotnet restore "Ecommerce.Order.API/Ecommerce.Order.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Ecommerce.Order.API"
RUN dotnet build "Ecommerce.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Order.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Order.API.dll"]
