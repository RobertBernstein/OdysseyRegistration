# Docker Documentation

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
