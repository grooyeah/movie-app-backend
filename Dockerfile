# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .

# Disable parallel restores to address any potential race conditions
RUN dotnet restore "./movie-app-backend.csproj" --disable-parallel

# Publish the application
RUN dotnet publish "./movie-app-backend.csproj" -c release -o /app --no-restore

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app ./

# Expose the port that your application listens on (adjust this based on your app's configuration)
EXPOSE 80

# Set the environment variable for ASP.NET Core Environment
ENV ASPNETCORE_ENVIRONMENT=Development

# Health check (optional, adjust based on your application)
HEALTHCHECK --interval=30s --timeout=3s \
    CMD curl --fail http://localhost:80/health || exit 1

# Entry point to run the application
ENTRYPOINT ["dotnet", "movie-app-backend.dll"]
