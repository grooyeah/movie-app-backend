# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["movie-app-backend.csproj", "."]
RUN dotnet restore "./movie-app-backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "movie-app-backend.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "movie-app-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core image as the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the published application to the runtime image
COPY --from=publish /app/publish .

# Set the entry point to start your .NET application
ENTRYPOINT ["dotnet", "movie-app-backend.dll"]
