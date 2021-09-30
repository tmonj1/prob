# Prob

A test container for probing container runtime environment.

This tool is **NOT FOR PRODUCTION USE**, just for doing some research in a local testing environment.

---

## Usage

```bash
$ cd k8s/chart

# install pod (deployment) and service only.
$ k create ns prob
$ helm install prob .

# install ingress, too.
$ helm install prob . --set ingress.enable=true

# install to AWS EKS cluster (set up ALB and register its public route to Route53)
$ k create ns external-dns
$ helm install prob . --set ingress.enable=true --set cluster=aws --create-namespace
```

## Endpoints

| Path      | Description                                                                                                                          |
| :-------- | :----------------------------------------------------------------------------------------------------------------------------------- |
| /         | "Hello, world"                                                                                                                       |
| /envs     | shows all environment variables.                                                                                                     |
| /headers  | shows all request headers.                                                                                                           |
| /cookies  | shows all cookies.                                                                                                                   |
| /echo     | returns the query string part of the request. <br>`ex: /echo?foo=bar`                                                                |
| /ping     | executes ping. <br>`ex: /ping?host=prob`                                                                                             |
| /dns      | executes dig command. <br>`ex: /dig?host=prob`                                                                                       |
| /redirect | redirects to url specified by query string. <br>`ex: /redirect?url=https://prob`                                                     |
| /bash     | executes an arbitrary command in bash and returns the result in "Content-Type: text/plain". <br>`ex: /bash?cmd=curl -k https://prob` |

### deploy to an EKS cluster

For EKS, first create an EKS cluster, then install either Nginx Ingress Controller for CLB (Classic Load Balancer) / NLB (Network Load Balancer), or [AWS Load Balancer Controller](https://docs.aws.amazon.com/eks/latest/userguide/aws-load-balancer-controller.html) for ALB (Application Load Balancer).

```
# (1) set up a VPC and subnets for your cluster, using `vpc-cfn.yaml` as a starting CFn template
# (2) install AWS Load Balancer Controller
# (3) install ExernalDNS
# (4) helm install or use `k apply -f` commands
```

### Deploy to a local k8s cluster (kind, minikube, Docker Desktop etc.)

For a local cluster such as minikube, kind and kubernetes in Docker Desktop, use [NGINX Ingress Controller](https://kubernetes.github.io/ingress-nginx/). The installation guide is [here](https://kubernetes.github.io/ingress-nginx/deploy/#provider-specific-steps)(for kind, [here](https://kind.sigs.k8s.io/docs/user/ingress/#ingress-nginx)).

```
# (1) create a local cluster
# (2) install Nginx Ingress Controller
# (3) helm install or use `k apply -f` commands
```

### Deploy using k8s manifests (not helm chart)

Having installed an ingress controller, you can install Prob:

```Bash
# create "prob" namespace
$ k create ns prob

# deploy deploy/rs/pod, svc, and then ingress
$ k prob-deploy.yml
$ k prob-svc-ip.yml
$ k prob-ing-ip.yml  # k prob-ing-ip-aws.yml for ALB
```
