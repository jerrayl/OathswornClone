FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node

RUN curl -sL https://deb.nodesource.com/setup_20.x | bash -
RUN apt-get update && \
    apt-get install -y nodejs && \
    apt-get clean -y && \
    npm install -g npm
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM with-node AS build

# Define build arguments for environment variables
ARG VITE_GOOGLE_CLIENT_ID
ENV VITE_GOOGLE_CLIENT_ID=$VITE_GOOGLE_CLIENT_ID

WORKDIR /src

COPY *.sln .
COPY Oathsworn.Server/*.csproj Oathsworn.Server/
COPY Oathsworn.Tests/*.csproj Oathsworn.Tests/
COPY Oathsworn.Client/*.esproj Oathsworn.Client/

RUN dotnet restore
COPY . .
WORKDIR "/src/Oathsworn.Server"
RUN dotnet build "./Oathsworn.Server.csproj"  -c Release

FROM build as migrations
ENTRYPOINT dotnet-ef database update

FROM build AS publish
RUN dotnet publish "./Oathsworn.Server.csproj" -c Release --no-restore --no-build -o /app/publish

# Pull Request Stage for Testing
FROM build as test
ENTRYPOINT [ "dotnet", "test", ".", "-c", "Release", "--no-restore", "--no-build"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oathsworn.Server.dll"]
