USE [iRecruit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prcGetInterviewSchedule]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[prcGetInterviewSchedule]
GO

-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcGetInterviewSchedule
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
dbo.prcGetInterviewSchedule   1, 2

*/
CREATE PROCEDURE prcGetInterviewSchedule
(
   @CandidateID int,
   @InverviewRound INT = null
)
AS
BEGIN
	
	declare @Temp table( InterviewScheduleID int, CandidateID int, CandidateName varchar(200), ContactNumber varchar(200), InverviewRound INT, ScheduledInterviewers VARCHAR(200), 
	Subject VARCHAR(100), Description VARCHAR(500), StartTime datetime, EndTime datetime, AttachResume bit, Status	INT)

	declare @ScheduleInterviewers varchar(200)
	
	if @InverviewRound >= 2
	begin
		select @ScheduleInterviewers = isnull(i.InterviewPanel2, '') 
		from Indent i (nolock)
		join Candidates c (nolock) on i.IndentNumber = c.IndentNumber
		where c.CandidateID = @CandidateID
	end
	else
	begin
		select @ScheduleInterviewers = isnull(i.InterviewPanel1, '') 
		from Indent i (nolock)
		join Candidates c (nolock) on i.IndentNumber = c.IndentNumber
		where c.CandidateID = @CandidateID
	end
	
	insert into @Temp
	select isnull(t.InterviewScheduleID, 0),
		c.CandidateID,
		c.FirstName + ' ' + c.LastName,
		c.ContactNumber,
		isnull(@InverviewRound,1) as InverviewRound,
		isnull(t.ScheduledInterviewers, @ScheduleInterviewers),
		t.Subject,
		t.Description,
		isnull(t.StartTime, getutcdate()),
		isnull(t.EndTime, getutcdate()),
		t.AttachResume,
		t.Status
		from Candidates c (nolock)
		left join InterviewSchedule t (nolock) on c.CandidateID = t.CandidateID
		where c.CandidateID = @CandidateID
		and t.InverviewRound is null or t.InverviewRound = @InverviewRound
		
	
	select * from @Temp
END
GO
