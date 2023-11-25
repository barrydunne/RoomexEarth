#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RoomexEarth.Algorithms/RoomexEarth.Algorithms.csproj", "RoomexEarth.Algorithms/"]
RUN dotnet restore "RoomexEarth.Algorithms/RoomexEarth.Algorithms.csproj"
COPY ["RoomexEarth.Logic/RoomexEarth.Logic.csproj", "RoomexEarth.Logic/"]
RUN dotnet restore "RoomexEarth.Logic/RoomexEarth.Logic.csproj"
COPY ["RoomexEarth.Api/RoomexEarth.Api.csproj", "RoomexEarth.Api/"]
RUN dotnet restore "RoomexEarth.Api/RoomexEarth.Api.csproj"
COPY . .
RUN dotnet build "RoomexEarth.Api/RoomexEarth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet test "RoomexEarth.Algorithms.Tests/RoomexEarth.Algorithms.Tests.csproj" -c $BUILD_CONFIGURATION --framework net8.0 -l "console;verbosity=normal" --results-directory:"TestResults" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage /p:Include=[RoomexEarth.Algorithms]* /p:Threshold=100
RUN dotnet test "RoomexEarth.Logic.Tests/RoomexEarth.Logic.Tests.csproj" -c $BUILD_CONFIGURATION --framework net8.0 -l "console;verbosity=normal" --results-directory:"TestResults" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage /p:Include=[RoomexEarth.Logic]* /p:Threshold=100
RUN dotnet test "RoomexEarth.Api.Tests/RoomexEarth.Api.Tests.csproj" -c $BUILD_CONFIGURATION --framework net8.0 -l "console;verbosity=normal" --results-directory:"TestResults" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage /p:Include=[RoomexEarth.Api]* /p:Exclude=[*]Program /p:Threshold=100

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RoomexEarth.Api/RoomexEarth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
# Install curl as root to provide health check to verify container is running OK
USER root
RUN DEBIAN_FRONTEND=noninteractive apt-get update && apt-get install -yq curl
HEALTHCHECK --interval=10s --timeout=5s --start-period=30s \
  CMD curl -f http://localhost:8080/health/status || exit 1
# Switch to non-root user to run app
USER app
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoomexEarth.Api.dll"]