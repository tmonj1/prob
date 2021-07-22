# Prob

A test container for probing container runtime environment.

This tool is **NOT FOR PRODUCTION USE**, just for doing some research in a local testing environment.

---

## Usage

```bash
# copy your pkcs12 certificate to src folder
$ cd src
$ cp ~/.aspnet/https/aspnetapp.pfx .

# for local dev environment (use local image)
$ docker-compose up -d

# for CI (use ECR image)
$ APP_TAG=${AWS_ECR_URL}/`cat .env|grep APP_TAG|sed -e 's/APP_TAG=//'` docker-compose up -d
```

## Endpoints

|Path|Description|
|:--|:--|
|/|"Hello, world"|
|/envs|shows all environment variables. |
|/headers|shows all request headers. |
|/cookies|shows all cookies. |
|/echo|returns the query string part of the request. <br>`ex: /echo?foo=bar`|
|/ping|execute ping. <br>`ex: /ping?host=prob`|
|/dns|execute dig command. <br>`ex: /dig?host=prob`|
|/redirect|redirect to url specified by query string. <br>`ex: /redirect?url=https://prob`|
|/bash|executes an arbitrary command in bash and returns the result. <br>`ex: /bash?cmd=curl -k https://prob`|
