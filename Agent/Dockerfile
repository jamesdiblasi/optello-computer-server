# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ComputerUse.Agent.Api/*.csproj", "ComputerUse.Agent.Api/"]
COPY ["ComputerUse.Agent.Application/*.csproj", "ComputerUse.Agent.Application/"]
COPY ["ComputerUse.Agent.Contracts/*.csproj", "ComputerUse.Agent.Contracts/"]
COPY ["ComputerUse.Agent.Core/*.csproj", "ComputerUse.Agent.Core/"]
COPY ["ComputerUse.Agent.Infrastructure/*.csproj", "ComputerUse.Agent.Infrastructure/"]
COPY ["ComputerUse.Agent.Infrastructure.Anthropic/*.csproj", "ComputerUse.Agent.Infrastructure.Anthropic/"]
RUN dotnet restore "ComputerUse.Agent.Api"
COPY . .
#WORKDIR "/src/ComputerUse.Agent.Api"
RUN dotnet build "ComputerUse.Agent.Api/ComputerUse.Agent.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ComputerUse.Agent.Api/ComputerUse.Agent.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ComputerUse.Agent.Api.dll"]