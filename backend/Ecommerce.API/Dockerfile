FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Ecommerce.API/*.csproj", "Ecommerce.API/"]
COPY ["Ecommerce.Application/*.csproj", "Ecommerce.Application/"]
COPY ["Common/Ecommerce.Common.Infra/*.csproj", "Common/Ecommerce.Common.Infra/"]
COPY ["Ecommerce.Domain/*.csproj", "Ecommerce.Domain/"]
COPY ["Ecommerce.Infra.IoC/*.csproj", "Ecommerce.Infra.IoC/"]
COPY ["Ecommerce.Infra.Data/*.csproj", "Ecommerce.Infra.Data/"]
RUN dotnet restore "Ecommerce.API/Ecommerce.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Ecommerce.API"
RUN dotnet build "Ecommerce.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.API.dll"]
