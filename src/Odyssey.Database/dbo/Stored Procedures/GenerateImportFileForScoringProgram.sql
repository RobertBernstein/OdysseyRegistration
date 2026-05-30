-- =============================================
-- Author:		Robert Bernstein
-- Create date: 1/21/2012
-- Description:	Generate the correct columns and headers for the import file for the Odyssey scoring program
-- =============================================
CREATE PROCEDURE [dbo].[GenerateImportFileForScoringProgram] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT '' as 'Number'
		  ,[ProblemID] as 'Problem'
		  ,[Division] = CASE t.Division WHEN '0' THEN 'Primary' ELSE t.Division END
		  ,s.Name as 'Name'
		  ,'' as 'Homeroom'
		  ,[CoachFirstName] as 'coachFirst'
		  ,[CoachLastName] as 'coachLast'
		  ,[CoachAddress] as 'coach_addr1'
		  ,[CoachCity] as 'coach_city'
		  ,[CoachState] as 'coach_state'
		  ,[CoachZipCode] as 'coach_zip'
		  ,[CoachDaytimePhone] as 'coach_phone'
		  ,[CoachEveningPhone] as 'coach_fax'
		  ,[CoachEmailAddress] as 'coach_email'
	FROM [DB_12824_registration].[dbo].[TournamentRegistration] as t, Schools as s
	WHERE t.SchoolID = s.ID
	ORDER BY ProblemID, Division
END