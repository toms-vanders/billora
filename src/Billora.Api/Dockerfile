# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["global.json", "."]
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Billora.Api/Billora.Api.csproj", "src/Billora.Api/"]
COPY ["src/Modules/Subscriptions/Billora.Subscriptions.Application/Billora.Subscriptions.Application.csproj", "src/Modules/Subscriptions/Billora.Subscriptions.Application/"]
COPY ["src/Modules/Subscriptions/Billora.Subscriptions.Domain/Billora.Subscriptions.Domain.csproj", "src/Modules/Subscriptions/Billora.Subscriptions.Domain/"]
COPY ["src/Modules/Subscriptions/Billora.Subscriptions.Infrastructure/Billora.Subscriptions.Infrastructure.csproj", "src/Modules/Subscriptions/Billora.Subscriptions.Infrastructure/"]
RUN dotnet restore "./src/Billora.Api/Billora.Api.csproj"
COPY . .
WORKDIR "/src/src/Billora.Api"
RUN dotnet build "./Billora.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Billora.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Billora.Api.dll"]
