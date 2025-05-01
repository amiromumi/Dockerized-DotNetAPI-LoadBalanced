# Dockerized DotNet API with Load Balancing

A simple .NET API application running two instances behind an NGINX load balancer using Docker Compose.

## How to Run (Using Docker Compose)

1. **Build and start the containers:**

   ```sh
   docker-compose up --build -d
   ```

2. **Access the API via load balancer:**\
   Open your browser and visit: [http://localhost:2524](http://localhost:2524)

   - This endpoint is served through NGINX and will load balance requests between two API instances.

3. **Stop the containers:**

   ```sh
   docker-compose down
   ```

4. **Rebuild the images if changes are made:**

   ```sh
   docker-compose up --build -d
   ```

## Load Balancing with NGINX

This project uses an `nginx.conf` configuration to set up a round-robin load balancer:

```nginx
upstream dotnet_apis {
    server api1:8080;
    server api2:8080;
}

server {
    listen 80;

    location / {
        proxy_pass http://dotnet_apis;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

Every time you refresh [http://localhost:2524](http://localhost:2524), NGINX forwards the request to either `api1` or `api2` in a round-robin manner.

## Project Structure

```
Dockerized-DotNetAPI-LoadBalanced/
│-- Controllers/
│   └── HomeController.cs
│-- Properties/
│   └── launchSettings.json
│-- Program.cs
│-- Dockerfile
│-- docker-compose.yml
│-- nginx.conf
│-- Dockerized-DotNetAPI-LoadBalanced.csproj
│-- appsettings.json
│-- appsettings.Development.json
```

## Environment Configuration

By default, the API runs in **Production** mode as set in `Program.cs` and project settings.\

Each instance identifies itself using the container name (`api1`, `api2`) retrieved at runtime.

## Logs

Currently, logs are printed to the console.
You can view them using:

```sh
docker-compose logs -f
```

## Notes

- Demonstrates container-based load balancing using Docker Compose and NGINX.
- Each API instance returns its identity based on container hostname.
- Useful for learning how to scale .NET services with Docker and basic load balancing setup.

### Ports Used

- `2504`: API instance 1 (mapped but not directly accessed)
- `2514`: API instance 2 (mapped but not directly accessed)
- `2524`: Load balancer (NGINX) – main access point

Make sure Docker is installed and these ports are available.

