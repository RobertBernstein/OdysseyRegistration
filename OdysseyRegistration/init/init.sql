USE [master];
GO

IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'vaodyssey')
BEGIN
    CREATE LOGIN [vaodyssey] WITH PASSWORD = '$(sa_password)', CHECK_POLICY = OFF;
    ALTER SERVER ROLE [sysadmin] ADD MEMBER [vaodyssey];
END
GO

-- Drop DB if it exists in a non-online state (leftover from a previously failed init)
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'DB_12824_registration' AND state != 0)
BEGIN
    ALTER DATABASE [DB_12824_registration] SET EMERGENCY;
    ALTER DATABASE [DB_12824_registration] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [DB_12824_registration];
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'DB_12824_registration')
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
