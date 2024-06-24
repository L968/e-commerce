FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Ecommerce.Gateway.API/*.csproj ./Ecommerce.Gateway.API/
WORKDIR /app/Ecommerce.Gateway.API
RUN dotnet restore

COPY Ecommerce.Gateway.API/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Ecommerce.Gateway.API/out .
ENTRYPOINT [ "dotnet", "Ecommerce.Gateway.API.dll" ]
