# Docker Documentation

## Startup Configuration: Running OdysseyMvc2024 with Docker Containers

The OdysseyMvc2024 project can be configured to start up dependent on Docker containers (specifically SQL Server) defined in the `docker-compose.dcproj` project.

### Configuration Files

#### 1. Solution Launch Settings (`OdysseyRegistration/launchSettings.json`)

This file defines launch profiles for the Docker Compose orchestration:

```json
{
  "profiles": {
    "Docker Compose": {
      "commandName": "DockerCompose",
      "commandVersion": "1.0",
      "serviceActions": {
        "odysseyregistrationwebapi": "StartDebugging"
      }
    },
    "Docker Compose + OdysseyMvc2024": {
      "commandName": "DockerCompose",
      "commandVersion": "1.0",
      "serviceActions": {
        "odysseyregistrationwebapi": "StartWithoutDebugging"
      },
      "composerUp": {
        "services": ["sqlserver"]
      },
      "otherProjects": [
        {
          "projectPath": "OdysseyMvc2024\\OdysseyMvc2024.csproj",
          "action": "StartDebugging",
          "waitForHealthCheck": false
        }
      ]
    },
    "SQL Server + OdysseyMvc2024": {
      "commandName": "DockerCompose",
      "commandVersion": "1.0",
      "composerUp": {
        "services": ["sqlserver"]
      },
      "otherProjects": [
        {
          "projectPath": "OdysseyMvc2024\\OdysseyMvc2024.csproj",
          "action": "StartDebugging",
          "waitForHealthCheck": true
        }
      ]
    }
  }
}
```

**Available Profiles:**
- **Docker Compose**: Starts only the WebAPI service with debugging
- **Docker Compose + OdysseyMvc2024**: Starts WebAPI without debugging + SQL Server + OdysseyMvc2024 with debugging
- **SQL Server + OdysseyMvc2024**: Starts only SQL Server container, waits for health check, then starts OdysseyMvc2024 with debugging

#### 2. Solution Launch Configuration (`OdysseyRegistration/OdysseyRegistration.slnlaunch`)

This file defines multiple startup project configurations for the `.slnx` solution format:

```json
[
  {
    "Name": "SQL Server + OdysseyMvc2024",
    "Projects": [
      {
        "Path": "docker-compose.dcproj",
        "Action": "Start"
      },
      {
        "Path": "OdysseyMvc2024\\OdysseyMvc2024.csproj",
        "Action": "Start"
      }
    ]
  },
  {
    "Name": "Docker Compose Only",
    "Projects": [
      {
        "Path": "docker-compose.dcproj",
        "Action": "Start"
      }
    ]
  },
  {
    "Name": "OdysseyMvc2024 Only",
    "Projects": [
      {
        "Path": "OdysseyMvc2024\\OdysseyMvc2024.csproj",
        "Action": "Start"
      }
    ]
  }
]
```

### How to Use in Visual Studio

1. **Open the solution** (`OdysseyRegistration.slnx`) in Visual Studio 2022+
2. **Select startup configuration** using one of these methods:
   - **Debug dropdown**: Select a profile from the Debug toolbar dropdown (profiles from `launchSettings.json`)
   - **Solution Properties**: Right-click solution → "Configure Startup Projects..." → select "Multiple startup projects"
   - **Startup dropdown**: Use the startup project dropdown which shows configurations from `.slnlaunch`
3. **Recommended profile**: Use **"SQL Server + OdysseyMvc2024"** for development

### SQL Server Health Check

The `docker-compose.yml` includes a health check for SQL Server:

```yaml
healthcheck:
  test: ["CMD-SHELL", "/opt/mssql-tools18/bin/sqlcmd -C -S localhost -U sa -P $(cat /run/secrets/sa_password) -Q 'SELECT 1' >/dev/null 2>&1 || exit 1"]
  interval: 15s
  retries: 6
  start_period: 90s
  timeout: 10s
```

This ensures:
- SQL Server has 90 seconds to initialize on first startup
- Health checks run every 15 seconds
- Up to 6 retries before marking unhealthy
- OdysseyMvc2024 waits for the health check to pass when `waitForHealthCheck: true`

### Connection String Configuration

OdysseyMvc2024 uses **ASP.NET Core User Secrets** for the connection string in development:

#### User Secrets (Development)
The connection string with password is stored in User Secrets, which:
- Are stored outside the project at `%APPDATA%\Microsoft\UserSecrets\{UserSecretsId}\secrets.json`
- Are never committed to source control
- Are automatically loaded in the Development environment
- Override any values in `appsettings.json`

**Setting up User Secrets:**
```bash
# Navigate to the project directory
cd OdysseyRegistration\OdysseyMvc2024

# Initialize User Secrets (if not already done)
dotnet user-secrets init

# Set the connection string
dotnet user-secrets set "ConnectionStrings:OdysseyConnection" "Server=localhost;Database=DB_12824_registration;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"

# View stored secrets
dotnet user-secrets list
```

#### Docker Secrets (SQL Server Container)
The SQL Server container uses Docker Secrets for the SA password:
- Password stored in `OdysseyRegistration/sa_password.txt`
- Mounted as `/run/secrets/sa_password` in the container
- Referenced via `MSSQL_SA_PASSWORD_FILE` environment variable

**Important**: The password in your User Secrets must match the password in `sa_password.txt` to connect successfully.

#### Connection Details
- **Server**: `localhost` (port 1433)
- **Database**: `DB_12824_registration`
- **Authentication**: SQL Server authentication
- **TrustServerCertificate**: `True` (for development)

The SQL Server container exposes port 1433 to localhost, making it accessible to the local OdysseyMvc2024 application.

---

## Question: Do I need to set pid or gid in this Docker Compose file for SQL Server, ?

Short answer: you don’t need to set pid or gid in this compose file.

## Details

- The mssql container already runs as the non-root user `mssql` (UID 10001) with group root (GID 0). You typically don’t set PUID/PGID for this image; those env vars are for linuxserver.io images.
- The `pid` you see in the `command` script is just a bash variable capturing the sqlservr process ID (`pid=$!`). Don’t change it.
- Docker Compose does have a service-level `pid:` option(e.g., `pid: "host"`), but you don’t need it here.

Only if you hit permissions issues on Linux host bind mounts (not common with named volumes), you can explicitly run the container as `10001:0`:

```yaml
services:
  sqlserver:
user: "10001:0"
```

Or fix ownership after startup:

```sh
docker exec -it sqlserver bash -lc 'chown -R 10001:0 /var/opt/mssql /var/opt/mssql/backup'
```

On Windows/WSL2 with named volumes (your case), leave as-is.

## How to inspect the fully-resolved config

• Run Compose’s config renderer to see the substituted result:

```docker
docker compose config
# or for older docker-compose binaries:
docker-compose config
```

Check the output for unexpected empty values.

## Stop Docker Compose from trying to interpolate $pid

Here’s a minimal fix to stop Compose from trying to interpolate $pid.
yaml OdysseyRegistration/docker-compose.yml services: sqlserver: ... command: > bash -c " /opt/mssql/bin/sqlservr & pid=$$!

What changed
•	Escaped the dollar signs so Docker Compose doesn’t try to substitute an env var named pid:
•	pid=$! → pid=$$!
•	wait $pid → wait $$pid
Why this works
•	Compose interpolates $VAR or ${VAR}. Using $$ escapes the $, leaving $! and $pid for the bash process at runtime. This removes the warning and preserves the intended shell behavior.
Verify
•	Run: docker compose config and check the rendered command contains wait $pid (with a single $).

> [!note] Rob tried this on his setup
> It showed double $$ in the rendered config, but at runtime the bash process got the single $ as intended.
> So it works as expected.
