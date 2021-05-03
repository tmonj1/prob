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

# run
EXPOSE 80
EXPOSE 443
ENTRYPOINT dotnet $APP_DLL