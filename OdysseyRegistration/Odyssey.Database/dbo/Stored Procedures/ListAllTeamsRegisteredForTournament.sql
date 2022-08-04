-- =============================================
-- Author:		Robert Bernstein
-- Create date: 1/21/2012
-- Description:	List all teams registered for the tournament.
-- =============================================
CREATE PROCEDURE [dbo].[ListAllTeamsRegisteredForTournament] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [TeamID]
		  ,[MembershipName]
		  ,[MembershipNumber]
		  ,p.ProblemName
		  ,[Division] = CASE t.Division WHEN '0' THEN 'Primary' ELSE t.Division END
		  ,s.Name as 'School Name'
		  ,[CoachFirstName]
		  ,[CoachLastName]
		  ,[CoachAddress]
		  ,[CoachCity]
		  ,[CoachState]
		  ,[CoachZipCode]
		  ,[CoachDaytimePhone]
		  ,[CoachEveningPhone]
		  ,[CoachEmailAddress]
		  ,[AltCoachFirstName]
		  ,[AltCoachLastName]
		  ,[AltCoachDaytimePhone]
		  ,[AltCoachEveningPhone]
		  ,[AltCoachEmailAddress]
		  ,[MemberFirstName1]
		  ,[MemberLastName1]
		  ,[MemberGrade1]
		  ,[MemberFirstName2]
		  ,[MemberLastName2]
		  ,[MemberGrade2]
		  ,[MemberFirstName3]
		  ,[MemberLastName3]
		  ,[MemberGrade3]
		  ,[MemberFirstName4]
		  ,[MemberLastName4]
		  ,[MemberGrade4]
		  ,[MemberFirstName5]
		  ,[MemberLastName5]
		  ,[MemberGrade5]
		  ,[MemberFirstName6]
		  ,[MemberLastName6]
		  ,[MemberGrade6]
		  ,[MemberFirstName7]
		  ,[MemberLastName7]
		  ,[MemberGrade7]
		  ,[Spontaneous]
		  ,t.Notes
		  ,[SpecialConsiderations]
		  ,[SchedulingIssues]
		  ,[Paid]
		  ,[TimeRegistered]
		  ,[JudgeID]
		  ,[TeamRegistrationFee]
		  ,[VolunteerID]
		  ,[TimeRegistrationStarted]
		  ,[UserAgent]
	  FROM TournamentRegistration as t,
		   problem as p,
		   Schools as s
	  WHERE t.ProblemID = p.ProblemID
	  AND t.SchoolID = s.ID
	  ORDER BY t.TeamID
END