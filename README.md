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

| Path      | Description                                                                                                                          |
| :-------- | :----------------------------------------------------------------------------------------------------------------------------------- |
| /         | "Hello, world"                                                                                                                       |
| /envs     | shows all environment variables.                                                                                                     |
| /headers  | shows all request headers.                                                                                                           |
| /cookies  | shows all cookies.                                                                                                                   |
| /echo     | returns the query string part of the request. <br>`ex: /echo?foo=bar`                                                                |
| /ping     | execute ping. <br>`ex: /ping?host=prob`                                                                                              |
| /dns      | execute dig command. <br>`ex: /dig?host=prob`                                                                                        |
| /redirect | redirect to url specified by query string. <br>`ex: /redirect?url=https://prob`                                                      |
| /bash     | executes an arbitrary command in bash and returns the result in "Content-Type: text/plain". <br>`ex: /bash?cmd=curl -k https://prob` |

## Deploy to a k8s cluster

You need an ingress controller in you cluster to deploy and publish Prob.

### local cluster (kind, minikube, Docker Desktop etc.)

For a local cluster such as minikube, kind and kubernetes in Docker Desktop, use [NGINX Ingress Controller](https://kubernetes.github.io/ingress-nginx/). The installation guide is [here](https://kubernetes.github.io/ingress-nginx/deploy/#provider-specific-steps)(for kind, [here](https://kind.sigs.k8s.io/docs/user/ingress/#ingress-nginx)).

### AWS EKS

For EKS, first create an EKS cluster, then install either Nginx Ingress Controller for CLB (Classic Load Balancer) / NLB (Network Load Balancer), or [AWS Load Balancer Controller](https://docs.aws.amazon.com/eks/latest/userguide/aws-load-balancer-controller.html) for ALB (Application Load Balancer).

### Deployment

Having installed an ingress controller, you can install Prob:

```Bash
# create "prob" namespace
$ k create ns prob

# deploy deploy/rs/pod, svc, and then ingress
$ k prob-deploy.yml
$ k prob-svc-ip.yml
$ k prob-ing-ip.yml  # k prob-ing-ip-aws.yml for ALB
```

## Deploy with Helm

```Bash
# deploy Prob to "prob" namespace
$ helm install prob src/k8s/chart --create-namespace prob
```
