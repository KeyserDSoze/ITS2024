Creare un cluster Kubernetes con due nodi (due macchine separate) pu� essere un ottimo modo per iniziare a sperimentare con Kubernetes in un ambiente pi� realistico. Ecco una guida passo passo su come configurare Kubernetes su due nodi utilizzando **kubeadm**, che � uno degli strumenti pi� comuni per installare Kubernetes. Assumeremo che tu abbia accesso a due macchine Linux e che tu abbia diritti di amministratore su entrambe.

### Requisiti
1. **Due macchine** Linux (Ubuntu 20.04 LTS � raccomandato per facilit� d'uso).
2. **Connessione di rete** tra le due macchine.
3. **Accesso a Internet** su entrambe le macchine per scaricare i pacchetti necessari.
4. **Accesso root** (o sudo) su entrambe le macchine.

### Passaggi

#### Passo 1: Preparare entrambi i nodi
1. **Aggiornare il sistema**
   ```bash
   sudo apt-get update && sudo apt-get upgrade -y
   ```
2. **Disabilitare il swap** (Kubernetes non lavora con il swap abilitato)
   ```bash
   sudo swapoff -a
   sudo sed -i '/ swap / s/^\(.*\)$/#\1/g' /etc/fstab
   ```
3. **Installare Docker** (o un altro runtime di container compatibile)
   ```bash
   sudo apt-get install -y docker.io
   sudo systemctl enable docker
   sudo systemctl start docker
   ```
4. **Aggiungere il repository di Kubernetes** e installare kubeadm, kubelet, kubectl
   ```bash
   sudo apt-get update && sudo apt-get install -y apt-transport-https curl
   curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -
   echo "deb https://apt.kubernetes.io/ kubernetes-xenial main" | sudo tee /etc/apt/sources.list.d/kubernetes.list
   sudo apt-get update
   sudo apt-get install -y kubelet kubeadm kubectl
   sudo apt-mark hold kubelet kubeadm kubectl
   ```

#### Passo 2: Inizializzare il nodo master
Su una delle macchine (che chiameremo **nodo master**):
```bash
sudo kubeadm init --pod-network-cidr=10.244.0.0/16
```
Ricorda di annotare il comando `kubeadm join` che viene visualizzato alla fine dell'inizializzazione: lo userai per collegare il secondo nodo al cluster.

Configura kubectl per l'utente non-root:
```bash
mkdir -p $HOME/.kube
sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
sudo chown $(id -u):$(id -g) $HOME/.kube/config
```

Installare un plugin di rete (es. Flannel):
```bash
kubectl apply -f https://raw.githubusercontent.com/coreos/flannel/master/Documentation/kube-flannel.yml
```

#### Passo 3: Collegare il nodo worker
Sul secondo nodo (il **nodo worker**), esegui il comando `kubeadm join` che hai annotato in precedenza. Assicurati di includere tutte le opzioni che erano presenti.
```bash
sudo kubeadm join [le tue opzioni specifiche]
```

#### Passo 4: Verificare lo stato del cluster
Dopo aver collegato il nodo worker, puoi controllare lo stato del tuo cluster Kubernetes dal nodo master:
```bash
kubectl get nodes
```
Dovresti vedere entrambi i nodi elencati come READY.

To deploy a .NET 8 API containerized application in your Kubernetes cluster that spans across two nodes with multiple instances, you'll follow these steps:

### Prerequisites
- Ensure you have a .NET 8 application containerized and available on a container registry (e.g., Docker Hub, GitHub Container Registry).
- Kubernetes cluster up and running as per the guide above.
- `kubectl` command line tool configured on your local machine or on the master node.

### Steps

#### Step 1: Create a Docker Image for Your .NET 8 API
1. **Create a Dockerfile** in your .NET project directory if not already present. Here's an example Dockerfile for a .NET 8 API:
    ```Dockerfile
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    WORKDIR /src
    COPY ["MyApi.csproj", "./"]
    RUN dotnet restore "MyApi.csproj"
    COPY . .
    WORKDIR "/src/."
    RUN dotnet build "MyApi.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "MyApi.csproj" -c Release -o /app/publish

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "MyApi.dll"]
    ```
2. **Build and push the Docker image** to your container registry:
    ```bash
    docker build -t myregistry/myapi:1.0 .
    docker push myregistry/myapi:1.0
    ```

#### Step 2: Create a Kubernetes Deployment YAML File
Create a deployment file (`myapi-deployment.yaml`) to specify the deployment settings:
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: myapi-deployment
  labels:
    app: myapi
spec:
  replicas: 3  # Number of instances you want to run
  selector:
    matchLabels:
      app: myapi
  template:
    metadata:
      labels:
        app: myapi
    spec:
      containers:
      - name: myapi
        image: myregistry/myapi:1.0
        ports:
        - containerPort: 80
```

#### Step 3: Deploy Your Application
1. **Apply the Deployment**
   ```bash
   kubectl apply -f myapi-deployment.yaml
   ```
2. **Verify Deployment**
   ```bash
   kubectl get deployments
   kubectl get pods
   ```

#### Step 4: Expose Your Application
To make your application accessible, you need to expose it using a Service or Ingress. Here�s how you can expose it using a Service:
```yaml
kind: Service
apiVersion: v1
metadata:
  name: myapi-service
spec:
  selector:
    app: myapi
  ports:
    - protocol: TCP
      port: 80
      targetPort: 80
  type: LoadBalancer  # For cloud environments, on-premise might require different types like NodePort or ClusterIP
```

1. **Create the Service**
   ```bash
   kubectl apply -f myapi-service.yaml
   ```
2. **Get the Service details**
   ```bash
   kubectl get service myapi-service
   ```

This will expose your .NET 8 API on the specified service port and distribute traffic across the instances of your application.

### Next Steps
- Monitor your deployment through Kubernetes dashboard or command-line tools.
- Scale your application as necessary by adjusting the `replicas` in your deployment.
- Implement CI/CD pipelines for automated builds and deployments. 

This setup allows you to leverage Kubernetes' capabilities to manage and scale your .NET 8 application efficiently across multiple nodes.

Setting up your Kubernetes cluster to deploy a .NET 8 API across two Virtual Machines (VMs) essentially involves the following steps. Let's walk through how you can get this setup going from scratch, assuming you're using VMs either on a cloud provider like AWS, Azure, Google Cloud, or on-premise VMs.

### Step 1: Provision the VMs
1. **Provision two VMs** with a recommended Linux distribution (like Ubuntu 20.04).
2. Ensure both VMs are in the same network or can communicate with each other over the network.
3. Set up SSH access for each VM for remote management.

### Step 2: Install Kubernetes
You need to install Docker, Kubeadm, Kubelet, and Kubectl on both VMs. Here's a brief on doing that:

#### On Both VMs:
1. **Update the system**:
   ```bash
   sudo apt-get update && sudo apt-get upgrade -y
   ```
2. **Disable Swap**:
   ```bash
   sudo swapoff -a
   sudo sed -i '/ swap / s/^/#/' /etc/fstab
   ```
3. **Install Docker**:
   ```bash
   sudo apt-get install -y docker.io
   sudo systemctl start docker
   sudo systemctl enable docker
   ```
4. **Add Kubernetes to system repository**:
   ```bash
   curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -
   echo "deb http://apt.kubernetes.io/ kubernetes-xenial main" | sudo tee /etc/apt/sources.list.d/kubernetes.list
   ```
5. **Install Kubeadm, Kubelet, and Kubectl**:
   ```bash
   sudo apt-get update
   sudo apt-get install -y kubeadm kubelet kubectl
   sudo apt-mark hold kubeadm kubelet kubectl
   ```

### Step 3: Initialize Kubernetes on the Master Node
On the first VM, which will act as the master:
1. **Initialize the cluster** using `kubeadm`:
   ```bash
   sudo kubeadm init --pod-network-cidr=10.244.0.0/16
   ```
   Make sure to note down the `kubeadm join` command output�it's crucial for joining the worker node.

2. **Set up local kubeconfig**:
   ```bash
   mkdir -p $HOME/.kube
   sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
   sudo chown $(id -u):$(id -g) $HOME/.kube/config
   ```

3. **Install a Pod network** (using Flannel as an example):
   ```bash
   kubectl apply -f https://raw.githubusercontent.com/coreos/flannel/master/Documentation/kube-flannel.yml
   ```

### Step 4: Join the Worker Node to the Cluster
On the second VM, execute the `kubeadm join` command you got from the master initialization step.

### Step 5: Verify the Cluster
From the master node:
1. **Check the nodes are connected**:
   ```bash
   kubectl get nodes
   ```
   You should see both nodes listed as ready.

### Step 6: Deploy Your .NET 8 Application
1. **Create the deployment and service** files as discussed previously.
2. **Apply the deployment** to Kubernetes:
   ```bash
   kubectl apply -f myapi-deployment.yaml
   kubectl apply -f myapi-service.yaml
   ```

### Step 7: Access Your Application
1. **Get the external IP** if using a LoadBalancer (in cloud environments):
   ```bash
   kubectl get svc
   ```
   The external IP listed is where your application will be accessible.

This is a high-level overview and assumes basic networking and administrative knowledge. Be sure to adjust firewall rules, security settings, and networking configurations according to your environment's specifics, especially if deploying in a more restricted or enterprise environment.