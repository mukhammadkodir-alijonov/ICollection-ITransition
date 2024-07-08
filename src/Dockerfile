# Use ASP.NET Core runtime as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["ICollection.Presentation/ICollection.Presentation.csproj", "ICollection.Presentation/"]
COPY ["ICollection.DataAccess/ICollection.DataAccess.csproj", "ICollection.DataAccess/"]
COPY ["ICollection.Domain/ICollection.Domain.csproj", "ICollection.Domain/"]
COPY ["ICollection.Service/ICollection.Service.csproj", "ICollection.Service/"]

# Restore dependencies
RUN dotnet restore "ICollection.Presentation/ICollection.Presentation.csproj"

# Copy the entire project files
COPY . .

# Set the working directory to the presentation project folder
WORKDIR "/src/ICollection.Presentation"

# Build the project
RUN dotnet build "ICollection.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ICollection.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Final stage: use the base image and copy the published files
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ICollection.Presentation.dll"]