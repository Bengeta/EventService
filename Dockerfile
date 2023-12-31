﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY docker-startup.sh .
COPY data .
EXPOSE 80
EXPOSE 443


RUN apt-get -y update
RUN apt  install -y -v libgdiplus
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["EventService.csproj", "./"]
RUN dotnet restore "EventService.csproj"
WORKDIR "/src"
COPY . .
RUN dotnet build "EventService.csproj" -c Release -o /app/build

RUN dotnet tool install --global dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools

FROM build AS publish
RUN dotnet publish "EventService.csproj" -c Release -o /app/publish

RUN dotnet-ef migrations bundle -r linux-x64 -o /app/publish/bundle --verbose

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["bash","docker-startup.sh"]
