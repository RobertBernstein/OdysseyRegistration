USE [master];
GO

IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'vaodyssey')
BEGIN
    CREATE LOGIN [vaodyssey] WITH PASSWORD = 'password123', CHECK_POLICY = OFF;
    ALTER SERVER ROLE [sysadmin] ADD MEMBER [vaodyssey];
END
GO
