# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Check if the 'app' user exists; if not, create it
RUN id -u app &>/dev/null || useradd -m app

# Switch to the 'app' user
USER app

# Set the working directory
WORKDIR /app

# Expose the ports the application will listen on
EXPOSE 8080
EXPOSE 8081

# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore any dependencies
COPY ["ArtGallery.csproj", "."]
RUN dotnet restore "./ArtGallery.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/."
RUN dotnet build "./ArtGallery.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ArtGallery.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: copy the published application and set the entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtGallery.dll"]
