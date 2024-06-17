FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Ecommerce.Authorization/*.csproj ./Ecommerce.Authorization/
WORKDIR /app/Ecommerce.Authorization
RUN dotnet restore

COPY Ecommerce.Authorization/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Ecommerce.Authorization/out .
ENTRYPOINT [ "dotnet", "Ecommerce.Authorization.dll" ]
