# Odyssey of the Mind Registration

> [!TODO]
> Read the "Database first approach with DotNet Core" article in Obsidian before continuing to work on this project.

## Overview

This repository contains the code for the Judge and Tournament Registration websites for NoVA North Odyssey of the Mind.

## Projects in this Solution (.slnx)

| Folder                | Project Name                         | .NET Version       | Purpose             |
|-----------------------|--------------------------------------|--------------------|---------------------|
| /                     | EFCoreToolReverseEngineeringTest     | .NET 9.0           | ==TBD==    |
| /                     | JudgeRegistrationRazor               | .NET 9.0           | ==TBD==    |
| /OdysseyRegistration/ | docker-compose                       | N/A                | Docker Compose project (.dcproj)<br />* Also builds the OdysseyRegistrationWebApi project.|
| /OdysseyRegistration/ | init                                 | N/A                | SQL Server initialization files for the Docker Compose project |
| /OdysseyRegistration/ | Odyssey.Database                     | SQL Server 2019 (==how can you tell which version?==)   | Database management project (.sqlproj) |
| /OdysseyRegistration/ | OdysseyCoreMvc                       | .NET 9.0           | Unused (I think): Core MVC framework  |
| /OdysseyRegistration/ | OdysseyMvc4                          | .NET Framework 4.8 | MVC framework 4<br />**This is what is running in production today (01/01/2025).** |
| /OdysseyRegistration/ | OdysseyMvc4.Tests                    | .NET Framework 4.8 | MVC framework 4 tests |
| /OdysseyRegistration/ | OdysseyMvc2023                       | **.NET Framework 4.8** | MVC framework 2023, ==I think unused since it's .NET Framework==  |
| /OdysseyRegistration/ | OdysseyMvc2024                       | .NET 9.0           | ==Is this the current project or is it the 2023 project? I think it's this one.== |
| /OdysseyRegistration/ | OdysseyRegistrationWebApi            | .NET 9.0           | Web API for registration |
| /OdysseyRegistration/ | UpdateProblemSynopsesForRegistration | .NET Framework 4.8 | Update problem synopses, ==Need to figure out what this does and if it's worth keeping== |

## Technologies

The OdysseyMvc4 (original) and OdysseyMvc2023 (current? I think it's really OdysseyMvc2024 build on .NET) projects are currently built using ASP.NET MVC version 4 on .NET Framework 4.8.

## Conversion Progress

### Copying OdysseyMvc4 (.NET Framework) to OdysseyMvc2024 (.NET)

1. ✅ BaseRegistrationController - Complete
2. ✅ HomeController - Complete
3. ✅ JudgesRegistrationController - Complete
4. ❓TournamentRegistrationController - Stopped reviewing on line 144.

## TODO

1. [x] Update (all) the projects to use ASP.NET (Core) 9.0 or later.
2. [ ] Document how I added the SQL Project to Visual Studio and how to modify, use, and deploy it.
3. [ ] Rolled back to EF 4.4 to make sure everything worked. See if the code works as-is with EF 6.x.
4. [ ] Create Elmah test DB
5. [ ] Determine what all the projects in this solution are for.
6. [ ] Upgrade the Odyssey.Database project to SQL Server 2022.
7. [ ] Clean up the "Purpose" column in the Projects table above.
8. [ ] Add logging to all projects!
9. [ ] Make all "Return to the Home Page" buttons return to the home page at the current base URL, not hard-coded to a specific Odyssey Registration home page.
10. [ ] Move docker-compose.dcproj and its associated files into its own subdirectory.
11. [ ] Set up automated SQL Server Backups for Odyssey Registration.
12. [ ] 09/29/2024: Read "Get started with Entity Framework Core and an existing database in minutes - Quick Start Guide":
   1. https://x.com/ErikEJ/status/1740635086742069720, 2:25 AM · Dec 29, 2023.
   2. This can produce Mermaid diagrams, as well.
13. [ ] 11/09/2024: Add [sweetalert2](https://github.com/sweetalert2/sweetalert2) to replace alerts.
   1. A beautiful, responsive, customizable, accessible (WAI-ARIA) replacement for JavaScript's popup boxes. Zero dependencies.
14. [ ] 11/17/2024: Mandatory: Add the membership name and number to the tournament registration email sent to the coach.
15. [ ] 11/17/2024: Preferable: Populate the membership name and number from the schools table into the tournament registration table at the time of registration.
16. [ ] 01/01/2025: Roll back anything in the .NET updates that are incompatible with the .NET Framework / ASP.NET v4 version of the website just to get onto .NET (Core) and be finished with .NET Framework once and for all.
   1. [ ] 01/01/2025: This includes the changes I made to the Odyssey database schema.
17. [ ] 01/01/2025: Once the .NET (not Framework) site is in production, then re-add updates/changes that were checked in today.
18. [ ] 01/01/2025: Convert the [Run the SQL Server database in a Docker container (manually)](#run-the-sql-server-database-in-a-docker-container-manually) section into a Dockerfile that can be built instead of step-by-step PowerShell cmdlets.
19. [ ] 04/06/2025: Add [Humanizer: meets all your .NET needs for manipulating and displaying strings, enums, dates, times, timespans, numbers and quantities](https://github.com/Humanizr/Humanizer).
20. [ ] 04/06/2025: Figure out why the `sqlserver.configurator` docker container doesn't run once the `sqlserver` container is healthy.

## Get the local Odyssey SQL database and website running to debug it

1. Start Docker Desktop for Windows.
1. Set the `docker-compose` VS 2022 project as the start up project.
1. Build the `docker-compose` project in the .slnx. This builds two projects:
   1. OdysseyRegistrationWebApi
   2. docker-compose
1. If this is not the first time you've built the project, it will kill and remove the existing containers.
1. Right click the `docker-compose` project and select `Start Without Debugging` to start three containers:
   1. odysseyregistrationwebapi
   2. sqlserver.configurator
   3. sqlserver
1. This resulted in a `There were build errors. Would you like to continue and run the
last successful build?` dialog box.
   1. The `docker-compose` project build showed the following error in the Error List window in VS 2022:
      1. `Volume sharing is not enabled. On the Settings screen in Docker Desktop, click Resources -> Shared Drives, and select the drive(s) or folder(s) containing your project files. For more information, please visit - https://aka.ms/DockerToolsTroubleshooting`
1. You may now connect to the running SQL Server instance using VS 2022, etc., with the following settings:
   1. Server Name: localhost
   2. Authentication: SQL Server Authentication
   3. User name: sa
   4. Password: \<whatever is defined in the `docker-compose\sa_password.txt` file\>
   5. Encrypt: Optional (False)
   6. Connect to database: OdysseyEntities

> [!warning]
> The files used to populate the SQL Server database are in the `init` directory of the `docker-compose` project.
> - The same files in the root directory of the `OdysseyRegistration` project don't seem to do anything.
> - For now, make changes to both files until I figure out how to just have one set of files or understand what the difference is between the two.
> - The difference may be between dev and prod?

## Deploying to Production

### Files to Configure

Make sure to copy the web.config file from **the `OdysseyRegistration` directory**, i.e., the top-most/root directory, into the root directory of your website at WinHost (the hosting company).

> [!note]
> This should be placed in a higher directory than your `bin`, `Content`, `Views`, etc. directories.  The directory containing those subdirectories will likely have its own web.config file.

### Winhost (our Hosting Company) Configuration

1. Make sure that the ASP.NET MVC website directory is set as an application starting point.
    1. Log into the [Winhost Control Panel](https://cp.winhost.com).
    2. Navigate to the Odyssey website.
    3. Navigate to the Sites List ➡️ Site Manager ➡️ Application Starting Point page.
    4. Make sure that the path to the directory containing the website is registered as an application starting point, e.g. `/registration`.

## SQL Server Database: Odyssey Registration Data (vs. WordPress data in MySQL)

### Back up the SQL Server Registration database after every season (or before the next one)

1. TODO: Document this procedure.
1. Run this from the command prompt or PowerShell

```bash
docker container exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S ServerName -U username -P password -Q "BACKUP DATABASE [DatabaseName] TO DISK='/Backups/DatabaseName.bak'"
```

```bash
docker container exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P <password> -No
```

### Connect to the hosting company's instance of the Odyssey database via a local SQL Server container

```sql
docker container exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd -S s06.winhost.com -U DB_12824_registration_user -P <password> -No

use DB_12824_registration
go

select * from config
go

quit

docker container exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd -S s06.winhost.com -U DB_12824_registration_user -P <password> -No

BACKUP DATABASE [DB_12824_registration] TO DISK='/var/opt/mssql/backups/odyssey.bak'
go

Msg 3201, Level 16, State 1, Server S06, Line 1
Cannot open backup device 'D:\DbBackup\/var/opt/mssql/backups/odyssey.bak'. Operating system error 3(The system cannot find the path specified.).
Msg 3013, Level 16, State 1, Server S06, Line 1
BACKUP DATABASE is terminating abnormally.

1> BACKUP DATABASE [DB_12824_registration] TO DISK='odyssey.bak'
2> go
Processed 448 pages for database 'DB_12824_registration', file 'DB_12824_registration_data' on file 1.
Processed 2 pages for database 'DB_12824_registration', file 'DB_12824_registration_log' on file 1.
BACKUP DATABASE successfully processed 450 pages in 0.110 seconds (31.924 MB/sec).
```

Did this get written to the Winhost server in D:\DbBackup?
How do I get at that???
How do I delete it???
**5/10/2025: Left off here**

    Key parameters:
    
        -S: Server name
        -E: Use Windows Authentication
        -U and -P: For SQL Authentication (username/password)
        -Q: Query to execute
    
    Tips:
    
        Make sure the backup directory exists
        Ensure your account has sufficient permissions
        Replace ServerName and DatabaseName with your values

### Connect to a shell in the local SQL Server container

```bash
docker container exec -it sqlserver bash
```

### Manage the database in production

[WinHost Control Panel](https://cp.winhost.com/login.aspx?ReturnUrl=%2fsites%2fmanage.aspx)
Site: novanorth.org

As of 09/29/2024, this is the configuration of the registration database.

```makefile
Database Name:   DB_12824_registration
Version:         MS SQL 2008 R2
Database Server: s06.winhost.com
Database User:   DB_12824_registration_user
Assigned Quota:  25 MB
Usage:	         4 MB
```

```makefile
Connection String: "Data Source=tcp:s06.winhost.com;Initial Catalog=DB_12824_registration;User ID=DB_12824_registration_user;Password=******;Integrated Security=False;"
```

### Run the SQL Server database in a Docker container

#### Docker Compose

==How did I create this VS project in the first place?==

1. See [docker-compose.yml](OdysseyRegistration/docker-compose.yml).
2. Manually run the `sqlserver.configurator` docker container (from within the Docker Desktop UI) to create the Odyssey login and database.
   1. It should run automatically once the `sqlserver` container is healthy. I added to TODO to figure out why this isn't running.

#### Manually (Don't need to use this any longer)

Open a PowerShell prompt.

```powershell
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker volume create sql-volume
$mssql_sa_password = "" # set this to a strong password
docker run -e 'ACCEPT_EULA=Y' -e "MSSQL_SA_PASSWORD=$mssql_sa_password" -p 1433:1433 --name sqlserver --hostname sqlserver --mount "source=sql-volume,target=/sqldata" -d mcr.microsoft.com/mssql/server:2022-latest
docker exec -it -u 0 sqlserver "bash"   # -u 0 lets us log in as root.
chmod 777 /sqldata
docker container exec sqlserver /opt/mssql-tools18/bin/sqlcmd -U sa -P "$mssql_sa_password" -Q "CREATE DATABASE [DB_12824_registration] ON  PRIMARY ( NAME = N'DB_12824_registration_data', FILENAME = N'/sqldata/DB_12824_registration_data.mdf' , SIZE = 4160KB , MAXSIZE = 25600KB , FILEGROWTH = 1024KB ) LOG ON ( NAME = N'DB_12824_registration_log', FILENAME = N'/sqldata/DB_12824_registration_log.ldf' , SIZE = 1024KB , MAXSIZE = 1024000KB , FILEGROWTH = 65536KB );"`
docker cp "2022-08-06 - NoVA North Production Database Export Script.sql" sqlserver:/sqldata
docker container exec sqlserver /opt/mssql-tools18/bin/sqlcmd -U sa -P "$mssql_sa_password" -i "/sqldata/2022-08-06 - NoVA North Production Database Export Script.sql"
```

### Generate the Odyssey database schema with `mermerd`

This will create a [Mermaid](https://mermaid-js.github.io/mermaid/#/) database schema diagram from your SQL Server database.

1. Download latest version: [Releases � KarnerTh/mermerd � GitHub](https://github.com/KarnerTh/mermerd/releases/)
1. Unzip it.
1. Make sure your SQL Server database is up, e.g., in Docker.
1. Run the following command:

    ```powershell
    Downloads\mermerd_0.4.1_windows_amd64.tar\mermerd -c "sqlserver://sa:********@localhost:1433?database=DB_12824_registration" -s dbo --useAllTables -o OdysseySchema.mmd
    ```

1. You will find your file created as OdysseySchema.mmd in the directory where you ran the tool.

#### TODO

### Shutdown and clean up the Docker container

```powershell
docker rm -f sqlserver
```

> [!note]
> Using `rm -f` will remove the container even if it is still running.

## Create the new test website

### Create new "Application Starting Point" for the test website

1. Navigate to [Application Starting Point](https://cp.winhost.com/sites/application.aspx?create=success) at the hosting company's website.
1. Click "Create".
1. Enter "/test" as the subdirectory.
1. Click "Create".
1. You should see a message that the site was created successfully.

### Create new SQL registration database for the test website

1. [ ] TODO: Add instructions.

### Create new SQL Elmah database for the test website

1. [ ] TODO: Add instructions.
2. Note that there is an Elmah.xsd file in the root of `OdysseyMvc2023`.

### Add connection strings for new SQL registration and Elmah test databases for the test website

1. [ ] TODO: Add instructions.

## Adding the Odyssey Registration WebAPI

### 08/04/2024

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

## Enabling SQL Server to run via a Docker Compose project (.dcproj)

### 08/05/2024

1. In VS 2022, I right-clicked on the solution and selected Add, Container Orchestrator Support.
   1. Docker Compose
   2. Linux Containers
1. I updated the `docker-compose.yml` file with the contents of one I found for adding a SQL Server 2022 database, modifying it by leveraging Docker Compose secrets.

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
1. I created `init\init.sql` with the following content.

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

1. I copied `OdysseyRegistration\Odyssey.Database\Scripts\2023-12-02 - NoVA North Registration Prod Backup.sql` to `OdysseyRegistration\init\novanorth-prod.sql`.
1. I added a new command to the `docker-compose.yml` file to create and initialize the Odyssey Registration database:

   ```bash
   /opt/mssql-tools18/bin/sqlcmd -S sqlserver -U sa -P ${Sa_Password:-********} -d master -i docker-entrypoint-initdb.d/novanorth-prod.sql;
    ```

1. Replaced passwords in docker-compose.yml file: <https://docs.docker.com/compose/use-secrets/>
2. Right-click on the docker-compose project in VS and select "Compose Up".
   1. [ ] TODO: Are any steps missing before this?

## 10/01/2024

1. I'm figuring out where I left off and what everything does in the .slnx.
1. I believe the OdysseyMvc2023 project is the main project that runs on my desktop against a SQL Server database running in a local Docker container.
1. When I built the docker-compose project in the .slnx, it built two projects:
    1. OdysseyRegistrationWebApi
    1. docker-compose
1. Then I right-click on the docker-compose project and select "Compose Up".
    1. The sqlserver.configurator-1 container starts and exits.
    1. The sqlserver-1 container starts and keeps running.
    1. The odysseyregistrationwebapi-1 container starts and keeps running.
    1. I can now run SQL Server Management Studio (SSMS) and connect to the SQL Server database running in the sqlserver-1 container.
        1. The database is fully populated with the data from the `init\novanorth-prod.sql` script.
            1. [ ] TODO: I think that's where the data came from. I need to verify this.
1. The login for the user in the Odyssey Registration MVC app failed, but otherwise I think it's working on my desktop.
    1. I updated `OdysseyRegistration\OdysseyMvc2023\Web.config` with Data Source=localhost (i.e., Docker), user=sa, and password=********.
1. The following code threw an exception in OdysseyRegistration\OdysseyMvc2023\Models\OdysseyRepository.cs on line 355.
    1. `Event tournamentInfo = this.tournamentInfo ?? (this.TournamentInfo = Queryable.Where<Event>((IQueryable<Event>)this.context.Events, (Expression<Func<Event, bool>>)(o => o.EventName.StartsWith(this.RegionName) && o.EventName.Contains("Tournament"))).First<Event>());`
    1. The `EventName` in the `Events` DB table needed to be renamed from "NoVA North Regional Tournament" to "NoVA North and NoVA South Regional Tournament".
    1. This is because I'm using last year's DB data and we were a combined region last year.
       1. [ ] TODO: Consider adding some kind of check for a valid region name to match the number.
       2. [ ] TODO: DEFINITELY add logging for this issue.
    2. The code continued after this name change!!!
2. The web site is running locally!
3. Judges and Tournament Registration show as "(Coming Soon!)"
    1. I commented these lines out and uncommented the lines that allow you to proceed to the registration pages.
4. Now both the Judges and Tournament Registration pages are showing Closed.

## 10/13/2024

1. Used [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools) to reverse engineer the Odyssey Registration database.
2. To export data from the current SQL Server database as JSON, run a query like the following in SSMS:

    ```sql
    SELECT * FROM Schools FOR JSON AUTO
    ```

3. Add a link like `entity.HasData(SeedHelper.SeedData<School>("Schools.json"));` to the `modelBuilder.Entity<School>()` code within the `OnModelCreating` method in the `OdysseyContext` class.
4. Added EF migrations using the Developer PowerShell for VS 2022 window.
5. Used the following commands:

   ```powershell
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   dotnet ef migrations add UpdateConfigSeedData
   dotnet ef database update
   dotnet ef migrations add UpdateSchoolsSeedData
   dotnet ef database update
   dotnet ef migrations add UpdateEventSeedData
   dotnet ef database update
   dotnet ef migrations add UpdateProblemSeedData
   dotnet ef database update
   ```
        
## 10/15/2024

1. Don't forget that the Problem table Id had to start at 1 where it used to start at 0. So, the code needs to be updated to handle this.
   1. ==Why is this starting at 1 now? I don't remember.== 😟

## 01/01/2025

1. Committed ALL of the modified files to the git repo after removing all passwords.
   1. Pushed to GitHub.

## 03/02/2025

1. Trying to continue making progress, updated this readme file.
1. Trying to remember how to get the website up and running.
   1. I started Docker Desktop in Windows.
   2. I set the `docker-compose` project as the start up project.
   3. I hit F5 to run the `docker-compose` project.

## References

1. [MySQL 9.2 Reference Manual](https://dev.mysql.com/doc/refman/9.2/en/)
1. [MySQL for WordPress docs](OdysseyRegistration/docs/MySQL-for-WordPress.md)
