# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy the project files and restore the dependencies
COPY *.sln .
COPY Grimoire.Core/*.csproj ./Grimoire.Core/
COPY Grimoire.Core.UnitTests/*.csproj ./Grimoire.Core.UnitTests/
COPY Grimoire.Infrastructure.Fakes/*.csproj ./Grimoire.Infrastructure.Fakes/
COPY Grimoire.Bootstrapper/*.csproj ./Grimoire.Bootstrapper/
COPY Grimoire.WebApi/*.csproj ./Grimoire.WebApi/
RUN dotnet restore

# Copy the remaining files and build the project
COPY Grimoire.Core/. ./Grimoire.Core/
COPY Grimoire.Infrastructure.Fakes/. ./Grimoire.Infrastructure.Fakes/
COPY Grimoire.Bootstrapper/. ./Grimoire.Bootstrapper/
COPY Grimoire.WebApi/. ./Grimoire.WebApi/
WORKDIR /source/Grimoire.WebApi
RUN dotnet publish -c release -o /app --no-restore

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
EXPOSE 8080
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Grimoire.WebApi.dll"]