# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore dependencies separately to improve layer caching
COPY Blazor.Web/Blazor.Web.csproj Blazor.Web/
COPY Blazor.API/Blazor.API.csproj Blazor.API/
RUN dotnet restore Blazor.Web/Blazor.Web.csproj

# Copy the source and publish the application
COPY Blazor.Web/ Blazor.Web/
COPY Blazor.API/ Blazor.API/
WORKDIR /src/Blazor.Web
RUN dotnet publish Blazor.Web.csproj \
    --configuration Release \
    --output /app/publish \
    /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Blazor.Web.dll"]
