# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code and build the app
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Use the runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Expose port
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "Dockerized-DotNetAPI-LoadBalanced.dll"]
