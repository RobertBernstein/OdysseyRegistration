# Managing the Odyssey WordPress MySQL database

## Exporting a MySQL database from the Hosting Company's Server and importing it to a local Docker MySQL instance (also remotely manage remote MySQL database from the `mysql` command-line tool)

> [!note]
> We use MySQL for WordPress and SQL Server for registration data, so this doesn't apply to OdysseyRegistration.

1. Run MySQL in a Docker container locally:

```powershell
docker run --name mysql -e MYSQL_ROOT_PASSWORD=<any password to connect> -v C:\Users\Rob\Downloads:/downloads -d mysql:latest
```

2. Open a shell in the Docker container:

```powershell
docker exec -it mysql bash
```

3. Create a dump file of the MySQL database:

```bash
mysqldump -u vaodyss -p mysql_12824_wordpress -h my01.winhost.com > dump.sql
```

or

3. Connect to the MySQL server:

```bash
mysql -u vaodyss -p mysql_12824_wordpress -h my01.winhost.com
```

4. Copy the dump file out of the Docker container:

```powershell
docker cp mysql:/dump.sql C:\Users\rob\Downloads\
```

5. Restore/Import the MySQL database on the new server:

```bash
mysql -u vaodyssey -p mysql_12824_wordpress24 -h my06.winhost.com < dump.sql
```

> [!tip]
> See [Section 6.5.1 mysql — The MySQL Command-Line Client](https://dev.mysql.com/doc/refman/9.1/en/mysql.html) for command-line usage once connected to the remote MySQL server.
> Also see [6.5.1.2 mysql Client Commands](https://dev.mysql.com/doc/refman/9.1/en/mysql-commands.html).

### MySQL command-line tool examples

```sql
SHOW TABLES;
```

```sql
SELECT * FROM wp_table;
```

## MySQL Database for WordPress data (vs. SQL Server for Odyssey Registration data)

### 08/24/2024

#### Original MySQL 5 Database

```
Database Name:      mysql_12824_wordpress
Version:            MySQL 5
Database Server:    my01.winhost.com
Database User:      vaodyss
Database Password:  *****
Assigned Quota:     100 MB
Usage:              37 MB
```

#### New MySQL 8 Database

```
Database Name:      mysql_12824_wordpress24
Version:            MySQL 8
Database Server:    my06.winhost.com
Database User:      vaodyssey
Database Password:  *****
Assigned Quota:     100 MB
Usage:              36 MB
```

> [!note]
> 1. The database version increased from 5 to 8.
> 2. The current database name ends in "24" for 2024.
> 3. The server moved from my01 to my06.
> 4. The username changed from vaodyss to vaodyssey.

I modified our `/wp/wp-config.php` file on the hosting company's site to point to the new MySQL 8.x database via SFTP.
