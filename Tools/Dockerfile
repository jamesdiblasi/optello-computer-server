# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM docker.io/ubuntu:22.04 AS base

ENV DEBIAN_FRONTEND=noninteractive
ENV DEBIAN_PRIORITY=high

RUN apt-get update && \
    apt-get -y upgrade && \
    apt-get -y install \
    # UI Requirements
    xvfb \
    xterm \
    xdotool \
    scrot \
    imagemagick \
    sudo \
    mutter \
    x11vnc \
    # Python/pyenv reqs (????)
    build-essential \
    libssl-dev  \
    zlib1g-dev \
    libbz2-dev \
    libreadline-dev \
    libsqlite3-dev \
    curl \
    git \
    libncursesw5-dev \
    xz-utils \
    tk-dev \
    libxml2-dev \
    libxmlsec1-dev \
    libffi-dev \
    liblzma-dev \
    # Network tools
    net-tools \
    netcat \
    # PPA req
    software-properties-common && \
    # Userland apps
    sudo add-apt-repository ppa:mozillateam/ppa && \
    sudo apt-get install -y --no-install-recommends \
    libreoffice \
    firefox-esr \
    x11-apps \
    xpdf \
    gedit \
    xpaint \
    tint2 \
    galculator \
    pcmanfm \
    unzip && \
    # .NET reqs
    sudo add-apt-repository ppa:dotnet/backports && \
    sudo apt-get install -y aspnetcore-runtime-9.0 && \
    apt-get clean

# Install noVNC
RUN git clone --branch v1.5.0 https://github.com/novnc/noVNC.git /opt/noVNC && \
    git clone --branch v0.12.0 https://github.com/novnc/websockify /opt/noVNC/utils/websockify && \
    ln -s /opt/noVNC/vnc.html /opt/noVNC/index.html

# setup user
ENV USERNAME=computeruse
ENV HOME=/home/$USERNAME
RUN useradd -m -s /bin/bash -d $HOME $USERNAME
RUN echo "${USERNAME} ALL=(ALL) NOPASSWD: ALL" >> /etc/sudoers
USER computeruse
WORKDIR $HOME

# This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ComputerUse.Contracts/*.csproj", "ComputerUse.Contracts/"]
COPY ["ComputerUse.Core/*.csproj", "ComputerUse.Core/"]
COPY ["ComputerUse.Infrastructure.OS/*.csproj", "ComputerUse.Infrastructure.OS/"]
COPY ["ComputerUse.Infrastructure.Anthropic/*.csproj", "ComputerUse.Infrastructure.Anthropic/"]
COPY ["ComputerUse.Application/*.csproj", "ComputerUse.Application/"]
COPY ["ComputerUse.Host/*.csproj", "ComputerUse.Host/"]
COPY ["ComputerUse.Scripts/", "ComputerUse.Scripts/"]
RUN dotnet restore "ComputerUse.Host"
#COPY . .
#RUN dotnet restore "./ComputerUse.Host.csproj"
COPY . .
#WORKDIR "/src/."
RUN dotnet build "ComputerUse.Host/ComputerUse.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ComputerUse.Host/ComputerUse.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR $HOME
COPY --from=publish /app/publish .
COPY --from=build ["/src/ComputerUse.Scripts/", "."]

ARG DISPLAY_NUM=1
ARG HEIGHT=768
ARG WIDTH=1024
ENV DISPLAY_NUM=$DISPLAY_NUM
ENV HEIGHT=$HEIGHT
ENV WIDTH=$WIDTH
ENV COMPUTER_USE_AGENT="ComputerUse.Host.dll"
ENV ASPNETCORE_URLS=http://+:8080
#ENV ASPNETCORE_URLS=http://localhost:8080

EXPOSE 5900 8501 6080 8080 8081
EXPOSE 5000

ENTRYPOINT [ "./entrypoint.sh" ]

#ENTRYPOINT ["dotnet", "ComputerUse.Host.dll"]
