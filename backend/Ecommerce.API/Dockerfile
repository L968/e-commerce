# Run from ./backend
# docker build -f Ecommerce.API/Dockerfile -t ecommerce-api:latest .
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY Ecommerce.Domain/*.csproj ./Ecommerce.Domain/
COPY Ecommerce.Application/*.csproj ./Ecommerce.Application/
COPY Ecommerce.Infra.Data/*.csproj ./Ecommerce.Infra.Data/
COPY Ecommerce.Infra.IoC/*.csproj ./Ecommerce.Infra.IoC/
COPY Ecommerce.API/*.csproj ./Ecommerce.API/
COPY Ecommerce.Authorization/*.csproj ./Ecommerce.Authorization/
COPY Ecommerce.Order.API/*.csproj ./Ecommerce.Order.API/
COPY Common/Ecommerce.Common.Infra/*.csproj ./Common/Ecommerce.Common.Infra/

COPY Ecommerce.Application.IntegrationTests/*.csproj ./Ecommerce.Application.IntegrationTests/
COPY Ecommerce.Application.UnitTests/*.csproj ./Ecommerce.Application.UnitTests/
COPY Ecommerce.Architecture.Tests/*.csproj ./Ecommerce.Architecture.Tests/
COPY Ecommerce.Domain.UnitTests/*.csproj ./Ecommerce.Domain.UnitTests/

RUN dotnet restore

COPY Ecommerce.Domain/. ./Ecommerce.Domain/
COPY Ecommerce.Application/. ./Ecommerce.Application/
COPY Ecommerce.Infra.Data/. ./Ecommerce.Infra.Data/
COPY Ecommerce.Infra.IoC/. ./Ecommerce.Infra.IoC/
COPY Ecommerce.API/. ./Ecommerce.API/
COPY Common/Ecommerce.Common.Infra/. ./Common/Ecommerce.Common.Infra/

WORKDIR /app/Ecommerce.API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Ecommerce.API/out .
ENTRYPOINT [ "dotnet", "Ecommerce.API.dll" ]
