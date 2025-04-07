USE [master];
GO

IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'vaodyssey')
BEGIN
    CREATE LOGIN [vaodyssey] WITH PASSWORD = 'password123', CHECK_POLICY = OFF;
    ALTER SERVER ROLE [sysadmin] ADD MEMBER [vaodyssey];
END
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DB_12824_registration')
BEGIN
    CREATE DATABASE [DB_12824_registration] ON PRIMARY (
        NAME = N'DB_12824_registration_data',
        FILENAME = N'/var/opt/mssql/DB_12824_registration_data.mdf',
        SIZE = 4160KB,
        MAXSIZE = 25600KB,
        FILEGROWTH = 1024KB
    ) LOG ON (
        NAME = N'DB_12824_registration_log',
        FILENAME = N'/var/opt/mssql/sqldata/DB_12824_registration_log.ldf',
        SIZE = 1024KB,
        MAXSIZE = 1024000KB,
        FILEGROWTH = 65536KB
    );
END
GO
