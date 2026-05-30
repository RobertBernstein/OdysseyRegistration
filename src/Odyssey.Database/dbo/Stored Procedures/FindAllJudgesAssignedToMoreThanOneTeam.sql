-- =============================================
-- Author:		Robert Bernstein
-- Create date: 1/22/2012
-- Description:	Find all judges assigned to more than one team.
-- =============================================
CREATE PROCEDURE [dbo].[FindAllJudgesAssignedToMoreThanOneTeam] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT t.JudgeID,
		   j.FirstName as 'Judge First Name',
		   j.LastName as 'Judge Last Name',
		   COUNT(t.[TeamID]) as 'TeamCount'
	FROM [DB_12824_registration].[dbo].[TournamentRegistration] as t,
		 [DB_12824_registration].[dbo].[judges] as j
	WHERE j.JudgeID = t.JudgeID
	GROUP BY t.JudgeID, j.FirstName, j.LastName
	HAVING COUNT(t.[TeamID]) > 1
END