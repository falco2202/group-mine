# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first to optimize layer caching
COPY ["group-mine.sln", "./"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Contract/Contract.csproj", "Contract/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]

# Restore packages
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build -c Release --no-restore

# Publish the application
RUN dotnet publish -c Release -o /app/publish --no-restore --no-build

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Set environment variables
ENV ASPNETCORE_URLS=
ENV ASPNETCORE_ENVIRONMENT=
ENV SECRET_KEY=

# Copy the published application
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

# Set the entry point
ENTRYPOINT ["dotnet", "Presentation.dll"]