#!/usr/bin/env bash
set -euo pipefail

SQLCMD="/opt/mssql-tools18/bin/sqlcmd"
SA_PASSWORD="$(cat /run/secrets/sa_password)"
WEBMASTER_EMAIL_PASSWORD="$(cat /run/secrets/webmaster_email_password)"

/opt/mssql/bin/sqlservr &
pid=$!

echo "Waiting for SQL Server startup..."
for i in {1..120}; do
  if "$SQLCMD" -C -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" >/dev/null 2>&1; then
    break
  fi
  sleep 2
done

echo "Waiting for all databases to reach a final state..."
for i in {1..40}; do
  pending=$("$SQLCMD" -C -S localhost -U sa -P "$SA_PASSWORD" -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.databases WHERE state NOT IN (0,6)" -h -1 2>/dev/null | tr -d ' \r\n')
  pending=${pending:-0}
  if [[ "$pending" == "0" ]]; then
    break
  fi
  echo "Databases still starting (attempt $i, pending=$pending)..."
  sleep 3
done

db_exists=$("$SQLCMD" -C -S localhost -U sa -P "$SA_PASSWORD" -Q "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'DB_12824_registration') IS NOT NULL THEN 1 ELSE 0 END" -h -1 2>/dev/null | tr -d ' \r\n')
db_exists=${db_exists:-0}

if [[ ! -f /var/opt/mssql/.db-initialized ]] || [[ "$db_exists" == "0" ]]; then
  echo "Initializing database from SQL scripts..."

  if [[ -f /init/init.sql ]]; then
    "$SQLCMD" -b -C -S localhost -U sa -P "$SA_PASSWORD" -d master -v sa_password="$SA_PASSWORD" webmaster_email_password="$WEBMASTER_EMAIL_PASSWORD" -i /init/init.sql
  fi

  echo "Waiting for DB_12824_registration to be ONLINE and queryable..."
  for i in {1..90}; do
    db_ready=$("$SQLCMD" -C -S localhost -U sa -P "$SA_PASSWORD" -Q "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'DB_12824_registration') IS NOT NULL AND DATABASEPROPERTYEX(N'DB_12824_registration', N'Status') = 'ONLINE' THEN 1 ELSE 0 END" -h -1 2>/dev/null | tr -d ' \r\n')
    if [[ "$db_ready" == "1" ]] && "$SQLCMD" -C -S localhost -U sa -P "$SA_PASSWORD" -Q "SET NOCOUNT ON; SELECT TOP 1 1 FROM [DB_12824_registration].sys.objects" >/dev/null 2>&1; then
      break
    fi
    echo "DB_12824_registration not ready yet (attempt $i)..."
    sleep 2
  done

  if [[ -f /init/novanorth-prod.sql ]]; then
    success=0
    for i in {1..20}; do
      # novanorth script contains idempotent DROP statements that can raise Level 11; only fail on Level 16+.
      script_output=$("$SQLCMD" -C -S localhost -U sa -P "$SA_PASSWORD" -d master -v sa_password="$SA_PASSWORD" webmaster_email_password="$WEBMASTER_EMAIL_PASSWORD" -i /init/novanorth-prod.sql 2>&1 || true)
      printf '%s\n' "$script_output"

      if ! printf '%s\n' "$script_output" | grep -Eq 'Msg [0-9]+, Level (1[6-9]|2[0-5]),'; then
        success=1
        break
      fi
      echo "novanorth-prod.sql attempt $i failed; retrying in 3s..."
      sleep 3
    done

    if [[ "$success" != "1" ]]; then
      echo "novanorth-prod.sql failed after retries"
      exit 1
    fi
  else
    echo "ERROR: /init/novanorth-prod.sql not found"
    exit 1
  fi

  touch /var/opt/mssql/.db-initialized
  echo "Database initialization complete."
else
  echo "Database already initialized; skipping script execution."
fi

wait "$pid"
