# Prob

A test container for probing the runtime environment.

---

## Usage

```bash
$ cd src
$ cp ~/.aspnet/https/aspnetapp.pfx .

# for local dev environment (use local image)
$ docker-compose up -d

# for Jenkins CI envionment (use ECR image)
$ APP_TAG=${AWS_ECR_URL}/`cat .env|grep APP_TAG|sed -e 's/APP_TAG=//'` docker-compose up -d
```

## APIs

|Path|Description|
|:--|:--|
|/|"Hello, world"|
|/envs|shows all environment variables|
|/headers|shows all request headers|
|/cookies|shows all cookies|
|/echo|returns the query string part of the request|
|/redirect|redirect to url specified by query string|
|/history|returns request history|
|/bin|executes an arbitrary command and returns the result|

## Todos

* act as fake RP
* echo to serilog / cloudwatch
* request recording
* accept all requests and record them.


