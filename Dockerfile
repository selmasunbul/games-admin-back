# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["games-admin-back/games-admin-back.csproj", "games-admin-back/"]
COPY ["data-access/data-access.csproj", "data-access/"]
COPY ["core/Core.csproj", "core/"]
COPY ["business/Business.csproj", "business/"]

# Restore dependencies
RUN dotnet restore "games-admin-back/games-admin-back.csproj"

# Copy all files
COPY . .

# Build and publish the application
WORKDIR "/src/games-admin-back"
RUN dotnet build "games-admin-back.csproj" -c Release -o /app/build
RUN dotnet publish "games-admin-back.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage - Runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "games-admin-back.dll"]
