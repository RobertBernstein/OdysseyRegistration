-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 01/06/2013
-- Description:	Find NULL Judge Registration records where
--              the TimeRegistered is null, indicating an
--              incomplete registration.
-- ============================================================
CREATE PROCEDURE [dbo].[FindNullJudgeRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM [DB_12824_registration].[dbo].Judges
	WHERE TimeRegistered is null
END