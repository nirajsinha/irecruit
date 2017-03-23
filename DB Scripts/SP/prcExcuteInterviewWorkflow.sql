USE [iRecruit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prcExcuteInterviewWorkflow]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[prcExcuteInterviewWorkflow]
GO

-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcExcuteInterviewWorkflow   1 
-- Author: Niraj Sinha  
-- Create Date: 23/12/2014    
-- Description: process interview feedbacks
-- Parameters: 
-- @CandidateID INT
-- Return Values:    
-- i.e:   prcExcuteInterviewWorkflow @CandidateID=1
-- ********************************************************    
CREATE PROCEDURE [dbo].[prcExcuteInterviewWorkflow]    
	@CandidateID INT
AS     
BEGIN    
  	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	declare @SelectedTypeID INT
	select @SelectedTypeID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 8 and Type.Code = 5 
	declare @InProcessTypeID INT
	select @InProcessTypeID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 8 and Type.Code = 4 
	declare @RejectedTypeID INT 
	select @RejectedTypeID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 8 and Type.Code = 9 
	
	declare @Round1Status INT, 
	@Round2Status INT,
	@PositionRecomended INT,
	@HREmail varchar(200),
	@StatusChangedBy varchar(200),
	@ActivityTypeID INT,
	@ActivityHeader  varchar(200),
	@ActivityDescription varchar(500),
	@ActivityComments varchar(500),
	@RecordDate datetime,
	@IndentID INT,
	@CandidateName   varchar(200)
	
	select @HREmail = Email from Users where UserID = 'HR'
	select @IndentID = IndentID from indent i (nolock) 
						join Candidates c on i.IndentNumber = c.IndentNumber
						where CandidateID = @CandidateID
	select @CandidateName = FirstName + ' ' + LastName from Candidates where CandidateID=@CandidateID
	
	
	-- update candidate based on round2 status
	if exists (select 1 from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 2)
	begin
		select @Round2Status = Status, @PositionRecomended = PositionRecomended from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 2
		if @Round2Status = 1
		begin
			select @StatusChangedBy = isnull(ModifiedBy, CreatedBy) from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 2
			select @RecordDate = isnull(ModifiedDate, CreatedDate) from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 2
			if @PositionRecomended = 1
			begin
				update Candidates set CandidateStatusTypeID= @SelectedTypeID where CandidateID = @CandidateID
				select @ActivityTypeID =  TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 6
				select @ActivityDescription = @CandidateName + ' selected in Technical Round 2 by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
			end
			else
			begin
				update Candidates set CandidateStatusTypeID= @RejectedTypeID where CandidateID = @CandidateID
				select @ActivityTypeID =  TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 8
				select @ActivityDescription = @CandidateName + ' rejected in Technical Round 2 by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
			end
			set @ActivityHeader = 'Technical Interview Round 2'
			select @ActivityComments = OverallRatingComments from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 2
			-- return values for further processing
			select @HREmail as ToEmailNotifications
		end

	end
	else
	begin
		select @Round1Status = Status, @PositionRecomended = PositionRecomended from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 1
		-- update candidate based on round1 status
		if @Round1Status = 1
		begin
			select @StatusChangedBy = isnull(ModifiedBy, CreatedBy) from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 1
			select @RecordDate = isnull(ModifiedDate, CreatedDate) from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 1
			if @PositionRecomended = 1
			begin
				update Candidates set CandidateStatusTypeID= @InProcessTypeID where CandidateID = @CandidateID
				select @ActivityTypeID =  TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 5 
				select @ActivityDescription = @CandidateName + ' selected in Technical Round 1 by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
			end
			else
			begin
				update Candidates set CandidateStatusTypeID= @RejectedTypeID where CandidateID = @CandidateID
				select @ActivityTypeID =  TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 7
				select @ActivityDescription = @CandidateName + ' rejected in Technical Round 1 by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
			end
		
			set @ActivityHeader = 'Technical Interview Round 1'
			select @ActivityComments = OverallRatingComments from InterviewFeedbacks where CandidateID = @CandidateID and InterviewRound = 1
				
			-- return values for further processing
			select @HREmail as ToEmailNotifications
		end
	end	
	-- Insert into Activity log
	
	if len(isnull(@ActivityDescription,'')) > 0
	begin
		INSERT INTO ActivityLog( IndentID, UserID, LogTypeID, Header, Description, Comments, RecordDate)
		values (@IndentID, @StatusChangedBy, @ActivityTypeID, @ActivityHeader, @ActivityDescription, @ActivityComments, @RecordDate)
	end

	
END
GO
