# Odyssey of the Mind Registration

## Next Todos

1. Create Elmah test DB

## Overview

This repository contains the code for the Judge and Tournament Registration for NoVA North Odyssey of the Mind.

## Technologies

It is currently built using ASP.NET MVC version 4.

## Configuration

### Files to Configure

Make sure to copy the web.config file from **this directory** into the root directory of your website.

1. Note: This should be placed in a higher directory than your bin, Content, Views, etc. directories.  The directory containing those subdirectories will likely have its own web.config file.

### Hosting Company Configuration

1. Make sure that the ASP.NET MVC website directory is set as an application starting point.
    1. Log into the [Winhost Control Panel](https://cp.winhost.com).
    2. Navigate to the Odyssey website.
    3. Navigate to the Sites List -> Site Manager -> Application Starting Point page.
    4. Make sure that the path to the directory containing the website is registered as an application starting point, e.g. /registration.

## SQL Server Database

1. Make sure to back up the database after every season (or before the next one).
2. TODO: Document how I added the SQL Project to Visual Studio and how to modify, use, and deploy it.

### Manage Database

Site: novanorth.org

```
Database Name:   DB_12824_registration
Version:         MS SQL 2008 R2
Database Server: s06.winhost.com
Database User:   DB_12824_registration_user
Assigned Quota:  25 MB
```

Connection String: "Data Source=tcp:s06.winhost.com;Initial Catalog=DB_12824_registration;User ID=DB_12824_registration_user;Password=******;Integrated Security=False;"

### Run the SQL Server database in a Docker container

1. Open a PowerShell prompt.
1. `docker pull mcr.microsoft.com/mssql/server:2022-latest`
1. `docker volume create sql-volume`
1. `$mssql_sa_password = ""`
1. `docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=$mssql_sa_password" -p 1433:1433 --name sql1 --hostname sql1 --mount "source=sql-volume,target=/sqldata" -d mcr.microsoft.com/mssql/server:2022-latest`
1. `docker exec -it -u 0 sql1 "bash"   # -u 0 lets us log in as root.`
1. `chmod 777 /sqldata`
1. `docker container exec sql1 /opt/mssql-tools/bin/sqlcmd -U sa -P "$mssql_sa_password" -Q "CREATE DATABASE [DB_12824_registration] ON  PRIMARY ( NAME = N'DB_12824_registration_data', FILENAME = N'/sqldata/DB_12824_registration_data.mdf' , SIZE = 4160KB , MAXSIZE = 25600KB , FILEGROWTH = 1024KB ) LOG ON ( NAME = N'DB_12824_registration_log', FILENAME = N'/sqldata/DB_12824_registration_log.ldf' , SIZE = 1024KB , MAXSIZE = 1024000KB , FILEGROWTH = 65536KB );"`
1. `docker cp "2022-08-06 - NoVA North Production Database Export Script.sql" sql1:/sqldata`
1. `docker container exec sql1 /opt/mssql-tools/bin/sqlcmd -U sa -P "$mssql_sa_password" -i "/sqldata/2022-08-06 - NoVA North Production Database Export Script.sql"`

### Generate the Odyssey database schema with `mermerd`

This will create a [Mermaid](https://mermaid-js.github.io/mermaid/#/) database schema diagram from your SQL Server database.

1. Download latest version: [Release v0.4.1 � KarnerTh/mermerd � GitHub](https://github.com/KarnerTh/mermerd/releases/tag/v0.4.1)
1. Unzip it.
1. Make sure your SQL Server database is up, e.g., in Docker.
1. Run the following command:

    `Downloads\mermerd_0.4.1_windows_amd64.tar\mermerd -c "sqlserver://sa:<password>@localhost:1433?database=DB_12824_registration" -s dbo --useAllTables -o OdysseySchema.mmd`

1. You will find your file created as OdysseySchema.mmd in the directory where you ran the tool.

### Shutdown and clean up the Docker container
`docker stop sql1 ; docker rm sql1`