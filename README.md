# Odyssey of the Mind Registration

## Overview

This repository contains the code for the Judge and Tournament Registration websites for NoVA North Odyssey of the Mind.

## Technologies

It is currently built using ASP.NET MVC version 4.

> TODO: Update the project to use ASP.NET (Core).

## Configuration

### Files to Configure

Make sure to copy the web.config file from **this directory**, i.e., the top-most directory, into the root directory of your website.

> ![Note]
> This should be placed in a higher directory than your bin, Content, Views, etc. directories.  The directory containing those subdirectories will likely have its own web.config file.

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

```makefile
Database Name:   DB_12824_registration
Version:         MS SQL 2008 R2
Database Server: s01.winhost.com
Database User:   DB_12824_registration_user
Assigned Quota:  25 MB
```

```makefile
Connection String: "Data Source=tcp:s06.winhost.com;Initial Catalog=DB_12824_registration;User ID=DB_12824_registration_user;Password=******;Integrated Security=False;"
```

### Run the SQL Server database in a Docker container

Open a PowerShell prompt.

```powershell
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker volume create sql-volume
$mssql_sa_password = ""
docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=$mssql_sa_password" -p 1433:1433 --name sql1 --hostname sql1 --mount "source=sql-volume,target=/sqldata" -d mcr.microsoft.com/mssql/server:2022-latest
docker exec -it -u 0 sql1 "bash"   # -u 0 lets us log in as root.
chmod 777 /sqldata
docker container exec sql1 /opt/mssql-tools/bin/sqlcmd -U sa -P "$mssql_sa_password" -Q "CREATE DATABASE [DB_12824_registration] ON  PRIMARY ( NAME = N'DB_12824_registration_data', FILENAME = N'/sqldata/DB_12824_registration_data.mdf' , SIZE = 4160KB , MAXSIZE = 25600KB , FILEGROWTH = 1024KB ) LOG ON ( NAME = N'DB_12824_registration_log', FILENAME = N'/sqldata/DB_12824_registration_log.ldf' , SIZE = 1024KB , MAXSIZE = 1024000KB , FILEGROWTH = 65536KB );"`
docker cp "2022-08-06 - NoVA North Production Database Export Script.sql" sql1:/sqldata
docker container exec sql1 /opt/mssql-tools/bin/sqlcmd -U sa -P "$mssql_sa_password" -i "/sqldata/2022-08-06 - NoVA North Production Database Export Script.sql"
```

### Generate the Odyssey database schema with `mermerd`

This will create a [Mermaid](https://mermaid-js.github.io/mermaid/#/) database schema diagram from your SQL Server database.

1. Download latest version: [Release v0.4.1 � KarnerTh/mermerd � GitHub](https://github.com/KarnerTh/mermerd/releases/tag/v0.4.1)
1. Unzip it.
1. Make sure your SQL Server database is up, e.g., in Docker.
1. Run the following command:

    `Downloads\mermerd_0.4.1_windows_amd64.tar\mermerd -c "sqlserver://sa:********@localhost:1433?database=DB_12824_registration" -s dbo --useAllTables -o OdysseySchema.mmd`

1. You will find your file created as OdysseySchema.mmd in the directory where you ran the tool.

### Shutdown and clean up the Docker container

```powershell
docker stop sql1 ; docker rm sql1`
```

## Create the new test website

### Create new "Application Starting Point" for the test website

1. Navigate to [Application Starting Point](https://cp.winhost.com/sites/application.aspx?create=success).
1. Click "Create".
1. Enter "/test" as the subdirectory.
1. Click "Create".
1. You should see a message that the site was created successfully.

### Create new SQL registration database for the test website

1. TODO: Add instructions.

### Created new SQL Elmah database for the test website

1. TODO: Add instructions.

### Add connection strings for new SQL registration and Elmah test databases for the test website

1. TODO: Add instructions.

## TODO

1. I rolled back to EF 4.4 to make sure everything worked.  See if the code works as-is with EF 6.x.
1. Create Elmah test DB

## 08/04/2024

Created a new project in the solution named `OdysseyRegistrationWebApi`

1. Right-clicked on the sln and selected "Add, New Project..."
2. Selected "ASP.NET Core Web API".
3. Used the following options:
   1. .NET 8.0 (Long Term Support)
   2. Authentication type: None (I may need to fix this later)
   3. Configure for HTTPS: checked
   4. Enable container support: checked
   5. Do not use top-level statements: unchecked
   6. Use controllers: checked
   7. Enlist in .NET Aspire orchestration: unchecked
4. I was then prompted with:
   1. ⚠️ This solution contains packages with vulnerabilities. Manage NuGet Packages.
   2. Clicked the "Manage NuGet Packages" link.
   3. I checked "Show only vulnerable".
   4. jQuery.UI.Combined needed to be updated from 1.10.3 to 1.13.2.
   5. The only project using it looks like OdysseyMvc4.
5. I still got vulnerability warnings in my build output.
   1. Upgraded jQuery 2.0.3 in OdysseyMvc4.
   2. jQuery.UI.Combined 1.10.3 OdysseyMvc4.

## 08/05/2024

1. In VS 2022, I right-clicked on the solution and selected Add, Container Orchestrator Support.
   1. Docker Compose
   2. Linux Containers
1. I updated the docker-compose.yml file with the contents of one I found for adding a SQL Server 2022 database, modifying it by leveraging Docker Compose secrets.

    ```dockerfile
    db:
      image: mcr.microsoft.com/mssql/server:2022-latest
      environment:
        MSSQL_SA_PASSWORD_FILE: /run/secrets/sa_password
        ACCEPT_EULA: "Y"
      ports:
        - "1433:1433"
    ```

1. I followed the instructions here: <https://stackoverflow.com/questions/69941444/how-to-have-docker-compose-init-a-sql-server-database>.
1. I created init\init.sql with the following content.

   ```sql
    USE [master];
    GO
    
    IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'vaodyssey')
    BEGIN
        CREATE LOGIN [vaodyssey] WITH PASSWORD = '********', CHECK_POLICY = OFF;
        ALTER SERVER ROLE [sysadmin] ADD MEMBER [vaodyssey];
    END
    GO
   ```

1. I copied "OdysseyRegistration\Odyssey.Database\Scripts\2023-12-02 - NoVA North Registration Prod Backup.sql" to OdysseyRegistration\init\novanorth-prod.sql.
1. I added a new command to the dockercompose.yml file to create and initialize the Odyssey Registration database:

   ```bash
   /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P ${Sa_Password:-********} -d master -i docker-entrypoint-initdb.d/novanorth-prod.sql;
    ```

1. Replaced passwords in docker-compose.yml file: <https://docs.docker.com/compose/use-secrets/>

## Managing MySQL on the Hosting Company's Server

Run MySQL in a Docker container locally:

```powershell
docker run --name mysql -e MYSQL_ROOT_PASSWORD=<any password to connect> -v C:\Users\Rob\Downloads:/downloads -d mysql:latest
```

Open a shell in the Docker container:

```powershell
docker exec -it mysql bash
```

Create a dump file of the MySQL database:

```bash
mysqldump -u vaodyss -p mysql_12824_wordpress -h my01.winhost.com > dump.sql
```

or

Connect to the MySQL server:

```bash
mysql -u vaodyss -p mysql_12824_wordpress -h my01.winhost.com
```

Copy the dump file out of the Docker container:

```powershell
docker cp mysql:/dump.sql C:\Users\rob\Downloads\
```

Restore/Import the MySQL database on the new server:

```bash
mysql -u vaodyssey -p mysql_12824_wordpress24 -h my06.winhost.com < dump.sql
```

## 08/24/2024

I modified our /wp/wp-config.php file to point to the new MySQL 8.x database.
