-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 12/09/2012
-- Description:	Delete all NULL judge registrations older
--              than the specified number of days.
-- ============================================================
CREATE PROCEDURE [dbo].[DeleteNullJudgeRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE
    FROM [DB_12824_registration].[dbo].[judges]
    WHERE TimeRegistered IS NULL
    AND TimeRegistrationStarted < DATEADD(DAY, -5, GETDATE())
END