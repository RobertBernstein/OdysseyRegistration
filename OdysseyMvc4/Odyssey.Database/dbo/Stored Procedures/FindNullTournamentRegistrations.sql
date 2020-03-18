-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 1/22/2012
-- Description:	Find NULL Tournament Registration records where
--              the TimeRegistered is null, indicating an
--              incomplete registration.
-- ============================================================
CREATE PROCEDURE [dbo].[FindNullTournamentRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM [DB_12824_registration].[dbo].[TournamentRegistration]
	WHERE TimeRegistered is null
END