FROM mcr.microsoft.com/dotnet/sdk:8.0

RUN apt-get update --allow-releaseinfo-change && \
    apt-get install -y \
      default-jdk \
      ca-certificates-java && \
    apt-get clean;

RUN update-ca-certificates -f
ENV JAVA_HOME /usr/lib/jvm/java-11-openjdk-amd64/
RUN export JAVA_HOME

ARG APP_ENV="Local"
ARG API_VERSION="latest"
ENV API_VERSION ${API_VERSION}
ENV SERVICE_NAME="ctutorial"
ENV ASPNETCORE_ENVIRONMENT ${APP_ENV}

WORKDIR /app

COPY . ./
# Copy csproj and restore as distinct layers
COPY *.csproj  ./
COPY NuGet.Config ./
RUN dotnet restore --configfile ./NuGet.Config
RUN dotnet publish -c Release --self-contained -o out


WORKDIR /app

COPY ./Liquibase /app/out/Liquibase
COPY run.bash /app/out/run.bash
RUN sed -i 's/\r$//'  /app/out/run.bash

WORKDIR /app/out
EXPOSE 8080