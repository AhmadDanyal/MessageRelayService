## Microservices Project
This project consists of two locally developed solutions containerized and deployed in the same Kubernetes cluster, communicating with each other using RabbitMQ message bus service through an Nginx configured API gateway. The service models have been mapped on to each other using AutoMapper. While the Platform Service has been configured to persist in a SQL Server database, the Command Service uses an In-Memory database to issue commands and run requests.

### Setting Up The Project:
This is a Docker and Kubernetes intensive project. For this reason, it is highly recommended to use Docker Desktop as it integrates smoothly with Kubernetes. For API testing, Postman API has been preferred. The use of SQLServer is optional as it is used to verify existing data objects, but strictly speaking is not necessary. You do not need a server instance as the server used inside our cluster is deployed using Kubernetes. The links to download all of these are listed below:
- [Download Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Download Postman API](https://www.postman.com/downloads/)
- [Download SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) (optional)

Once the downloads are complete, go ahead and pull the project to local system. There are 3 folders:
- CommandService
- K8S
- PlatformService

To get started, start Docker Desktop and enable Kubernetes. All services needed to run the project have been packed as **.yaml** files inside the K8s folder, which can be deployed once Kubernetes is up and running. To start deploying the yaml files, navigate to the K8S folder using terminal of choice, and enter the commands in the following order to deploy the services one-by-one:

(Note: For _mssql-plat-depl.yaml_ file, which sets up the SQL Server, make sure to edit login information to provide your own username and password)
```
kubectl apply -f plaftorms-depl.yaml
kubectl apply -f commands-depl.yaml
kubectl apply -f rabbitmq-depl.yaml
kubectl apply -f mssql-plat-depl.yaml
kubectl apply -f local-pvc.yaml
kubectl apply -f ingress-srv.yaml
kubectl apply -f plaftorms-np-srv.yaml
```


### Configuring the Host:
The _ingress-srv.yaml_ file uses a dedicated URL instead of localhost to launch the API gateway. To configure this URL, navigate to the **_hosts_** file located inside the the Windows folder of your main drive. This file is usually found at:
```
C:\Windows\System32\drivers\etc
```
Type the following inside the file, outside of any comments:
```
127.0.0.1 acme.com
```
You can use any website name, but just make sure to change it in the _ingress-srv.yaml_ file accordingly. Of course you will have to redeploy the _ingress-srv.yaml_ file if you do so, so it is consistent with whatever URL you use.

### Launching Rabbit MQ Message Bus:
If everything has been done properly, you should be able to launch the Rabbit MQ message bus using the following address:
```
localhost:15672
```
The default username and password are both "_guest_". You can change this information after logging in.

### Interacting with the API:
To start interacting with the API, open up Postman. The base URL for interacting with the Platform Service (if acme.com was used as the host name) is:
```
http://acme.com/api/platforms
```
For interacting with the Command Service, use the following URL:
```
http://acme.com/api/c/platforms
```
These services allow different HTTP requests to be sent (they are not configured with https, just to keep things simple). To find out what type of requests you can send, check out the **Controllers** folder of each project. Both services are configured to handle different types of **GET** and **POST** requests.
