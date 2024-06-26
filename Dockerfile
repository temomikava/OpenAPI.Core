# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["OpenAPI.Core/OpenAPI.Core/OpenAPI.Core.csproj", "OpenAPI.Core/OpenAPI.Core/"]
COPY ["OpenAPI.Core/IntegrationEvents/IntegrationEvents.csproj", "OpenAPI.Core/IntegrationEvents/"]
COPY ["OpenAPI.Core/SharedKernel/SharedKernel.csproj", "OpenAPI.Core/SharedKernel/"]
RUN dotnet restore "OpenAPI.Core/OpenAPI.Core/OpenAPI.Core.csproj"

# Copy the entire project and build it
COPY . .
WORKDIR "/src/OpenAPI.Core/OpenAPI.Core"
RUN dotnet build "OpenAPI.Core.csproj" -c Release -o /app/build

# Publish the application to a folder
FROM build AS publish
RUN dotnet publish "OpenAPI.Core.csproj" -c Release -o /app/publish

# Copy the published application to the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenAPI.Core.dll"]