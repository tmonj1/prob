#
# build stage
#

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0.202 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# copy everything else and build app
COPY . .
RUN dotnet publish -c release -o /app --no-restore

#
# final stage
#

# use runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:5.0.5

# set executable assembly name from build arg
ARG app
ENV APP_DLL=$app

# copy dev-certs
RUN mkdir /https
COPY aspnetapp.pfx /https/aspnetapp.pfx

# copy all published files
WORKDIR /app
COPY --from=build /app ./

# add a user/group for running kestrel.
RUN addgroup --system --gid 1000 appgroup \
    && adduser --system --uid 1000 --ingroup appgroup --shell /bin/sh appuser

# publish 8080 for HTTP, 8081 for HTTPS
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_URLS=https://+:8081;http://+:8080
ENV ASPNETCORE_HTTPS_PORT=8081
EXPOSE 8080
EXPOSE 8081

# set executing user
USER 1000

# run
ENTRYPOINT dotnet $APP_DLL