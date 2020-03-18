-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 01/06/2013
-- Description:	Delete all NULL Tournament Registrations older
--              than the specified number of days.
-- ============================================================
CREATE PROCEDURE [dbo].[DeleteNullTournamentRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE
    FROM [DB_12824_registration].[dbo].TournamentRegistration
    WHERE TimeRegistered IS NULL
    AND TimeRegistrationStarted < DATEADD(DAY, -5, GETDATE())
END