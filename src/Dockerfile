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

# copy all published files
WORKDIR /app
COPY --from=build /app ./

# add a user/group for running kestrel.
RUN addgroup --system --gid 1000 appgroup \
    && adduser --system --uid 1000 --ingroup appgroup --shell /bin/sh appuser

# install basic tools (curl, dig, ping)
RUN apt-get update && \
    apt-get install -y sudo && \
    echo 'appuser ALL=(ALL) NOPASSWD:ALL' >> /etc/sudoers && \
    apt-get install -y curl && \
    apt-get install -y dnsutils && \
    apt-get install -y iputils-ping

# publish 80 for HTTP
ENV ASPNETCORE_URLS=http://+:80

# let ASP.NET know the (external) HTTP port number
ENV ASPNETCORE_HTTP_PORT=80

EXPOSE 80

# set executing user (ERROR: cannot bind previlege ports)
# USER 1000

# run
ENTRYPOINT dotnet Prob.dll