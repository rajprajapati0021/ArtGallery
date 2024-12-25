#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#USER app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["ArtGallery.csproj", "."]
#RUN dotnet restore "./ArtGallery.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "./ArtGallery.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./ArtGallery.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ArtGallery.dll"]
#
# Use the official .NET runtime as a base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore any dependencies (via dotnet restore)
COPY ["ArtGallery.csproj", "./"]
RUN dotnet restore "./ArtGallery.csproj"

# Copy the remaining source code into the container
COPY . . 

# Build the project
RUN dotnet build "./ArtGallery.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application to the /app/publish directory
FROM build AS publish
RUN dotnet publish "./ArtGallery.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image to run the application
FROM base AS final
WORKDIR /app

# Copy the published output from the previous stage
COPY --from=publish /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "	"]
