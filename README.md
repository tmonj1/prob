# Prob

A test container for probing the runtime environment.

---

## Usage

```bash
$ cd src
$ cp ~/.aspnet/https/aspnetapp.pfx .
$ docker-compose up -d
```

## APIs

|Path|Content Type|Description|
|:--|:--|:--|
|/|text/plain|"Hello, world"|
|/envs||shows all environment variables|
|/headers||shows all request headers|
|/cookies||shows all cookies|
|/body||shows contents of body|
|/upload|||
|/download|||
|/echo||returns the query string part of the request|
|/history||returns request history|
|/bin||executes an arbitrary command and returns the result|

.text/.json

## Access Logs


## Todos

* echo to serilog / cloudwatch
* request recording (at least emit logs)
* can specify port numbers (80 and 443 by default, but can change them)
* accept all requests and record them.


