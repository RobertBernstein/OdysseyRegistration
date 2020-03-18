-- =======================================================================
-- Author:		Robert Bernstein
-- Create date: 11/19/2013
-- Description:	Uses the TRUNCATE TABLE command to delete all the records
--				in the TournamentRegistration table and reset its identity
--				counter to zero.
-- =======================================================================
CREATE PROCEDURE [dbo].[DeleteAllTournamentRegistrationRecordsAndResetIdToZero]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 TRUNCATE table TournamentRegistration
END