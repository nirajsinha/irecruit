USE [iRecruit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prcExecuteIndentWorkflow]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[prcExecuteIndentWorkflow]
GO

-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcExecuteIndentWorkflow   24 
-- Author: Niraj Sinha  
-- Create Date: 22/09/2014    
-- Description: assigns indent, and returns data for further action
-- Parameters: 
-- @IndentID INT
-- Return Values:    
-- i.e:   prcExecuteIndentWorkflow @IndentID=10
-- ********************************************************    
CREATE PROCEDURE [dbo].[prcExecuteIndentWorkflow]    
	@IndentID INT
AS     
BEGIN    
  	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	declare @UserName varchar(100),
	@IndentNumber varchar(100),
	@DepartmentID int,
	@BranchID int,
	@FH varchar(100),
	@FHName varchar(100),
	@FHStatusID INT,
	@SVP varchar(100),
	@SVPName varchar(100),
	@SVPStatusID INT,
	@CurrentStatus INT,
	@ConfiguredFH varchar(100),
	@ConfiguredSVP varchar(100),
	@AssignedTo varchar(100),
	@StatusChangedBy varchar(100),
	@CreatedBy varchar(100),
	@ToEmailNotifications varchar(500),
	@CcEmailNotifications varchar(500),
	@ActivityTypeID INT,
	@ActivityHeader varchar(500),
	@ActivityDescription varchar(400),
	@ActivityComments varchar(500),
	@IndentRemarks  varchar(500),
	@FHRemarks  varchar(500),
	@SVPRemarks  varchar(500),
	@RecordDate datetime
	-- Get current status
	select @UserName = CreatedBy,
		@DepartmentID = DepartmentID,
		@BranchID = BranchID,
		@IndentNumber = IndentNumber,
		@FHName = FunctionHead,
		@FHStatusID = FunctionHeadStatusTypeID,
		@SVPName = SeniorVicePresident,
		@SVPStatusID = SeniorVicePresidentStatusTypeID,
		@CurrentStatus = Indent_Status,
		@StatusChangedBy = StatusChangedBy,
		@CreatedBy = CreatedBy,
		@IndentRemarks = IndentorRemarks,
		@FHRemarks  = FunctionHeadRemarks,
		@SVPRemarks  = SeniorVicePresidentRemarks
	from Indent
	where IndentID = @IndentID
	select @RecordDate = getutcdate()
	-- Get function head and svp details
	select @ConfiguredFH = FunctionHead, @ConfiguredSVP = SVP from DepartmentRoles where DepartmentID =@DepartmentID  and BranchID = @BranchID and Active = 1
	select @FH = UserID from Users where Name like @FHName
	select @SVP = UserID from Users where Name like @SVPName
	
	-- Get all status values 
	declare @SubmitedStatusID INT 
	select @SubmitedStatusID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 7 and Type.Code = 2 
	declare @ApprovedStatusID INT 
	select @ApprovedStatusID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 7 and Type.Code = 3 
	declare @OnHoldStatusID INT 
	select @OnHoldStatusID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 7 and Type.Code = 4 
	declare @RejectedStatusID INT 
	select @RejectedStatusID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 7 and Type.Code = 5 
	declare @CancelledStatusID INT 
	select @CancelledStatusID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 7 and Type.Code = 6 
	
	-- get activity types
	declare @IndentRaisedActivityID INT 
	select @IndentRaisedActivityID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 1 
	declare @IndentApprovedActivityID INT 
	select @IndentApprovedActivityID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 2 
	declare @IndentRejectedActivityID INT 
	select @IndentRejectedActivityID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 3 
	declare @IndentOnHoldActivityID INT 
	select @IndentOnHoldActivityID = TypeID from Type join TypeClass on Type.TypeClassID = TypeClass.TypeClassID where TypeClass.Code = 9 and Type.Code = 4 
	
	
	-- If indent submitted by function head then assign it to svp
	if @CurrentStatus = @SubmitedStatusID
	begin
		
		if @StatusChangedBy = @FH or @StatusChangedBy = @ConfiguredFH
		begin
			set @AssignedTo = @ConfiguredSVP
			update Indent set FunctionHeadStatusDate = @RecordDate	where IndentID = @IndentID
		end
		else
		begin
			set @AssignedTo = @ConfiguredFH
		end
		update Indent set IndentDate = @RecordDate	where IndentID = @IndentID
		
		set @ToEmailNotifications = @AssignedTo
		set @CcEmailNotifications = isnull(@StatusChangedBy,'') -- + ',' + isnull(@CreatedBy,'')
		set @ActivityTypeID = @IndentRaisedActivityID

		set @ActivityDescription = 'Indent '+ @IndentNumber +' Raised by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
		set @ActivityHeader = 'Indent Raised'
		set @ActivityComments = @IndentRemarks
	end
	-- If indent is approved by fh then assign it to svp else assign back to HR
	if @CurrentStatus = @ApprovedStatusID
	begin
		if @StatusChangedBy = @FH or @StatusChangedBy = @ConfiguredFH
		begin
			set @AssignedTo = @ConfiguredSVP
			set @ActivityComments = @FHRemarks
			update Indent set FunctionHeadStatusDate = @RecordDate	where IndentID = @IndentID
		end
		else
		begin
			set @AssignedTo = 'HR'
			set @ActivityComments = @SVPRemarks
			update Indent set SeniorVicePresidentStatusDate = @RecordDate	where IndentID = @IndentID
		end
		set @ToEmailNotifications = @AssignedTo
		set @CcEmailNotifications = isnull(@StatusChangedBy,'') + ',' + isnull(@CreatedBy,'')
		set @ActivityTypeID = @IndentApprovedActivityID
		set @ActivityDescription = 'Indent '+ @IndentNumber +' Approved by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
		set @ActivityHeader = 'Indent Approved'
	end
	-- If indent is on hold then do not assign it to anybody, send notification email to stakeholders
	if @CurrentStatus = @OnHoldStatusID
	begin
		set @ToEmailNotifications = @CreatedBy
		set @CcEmailNotifications = isnull(@StatusChangedBy,'') + ',' + isnull(@ConfiguredFH,'') + ',' + isnull(@FH,'') + ',' + isnull(@ConfiguredSVP,'') + ',' + isnull(@SVP,'')
		set @ActivityTypeID = @IndentOnHoldActivityID
		set @ActivityDescription = 'Indent '+ @IndentNumber +' is kept on Hold by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
		set @ActivityHeader = 'Indent On Hold'
		
		if @StatusChangedBy = @SVP or @StatusChangedBy = @ConfiguredSVP
		begin
			set @ActivityComments = @SVPRemarks
			update Indent set SeniorVicePresidentStatusDate = @RecordDate	where IndentID = @IndentID
		end
		else
		begin
			set @ActivityComments = @FHRemarks
			update Indent set FunctionHeadStatusDate = @RecordDate	where IndentID = @IndentID
		end
	end
	-- If indent is cancelled or rejected
	if @CurrentStatus = @RejectedStatusID or @CurrentStatus = @CancelledStatusID
	begin
		set @ToEmailNotifications = @CreatedBy
		set @CcEmailNotifications = isnull(@StatusChangedBy,'') + ',' + isnull(@ConfiguredFH,'') + ',' + isnull(@FH,'') + ',' + isnull(@ConfiguredSVP,'') + ',' + isnull(@SVP,'')
		set @ActivityTypeID = @IndentRejectedActivityID
		set @ActivityDescription = 'Indent '+ @IndentNumber +' Rejected/Cancelled by '+ (select Name from Users where UserID = isnull(@StatusChangedBy,''))
		set @ActivityHeader = 'Indent Rejected/Cancelled'
		if @StatusChangedBy = @SVP or @StatusChangedBy = @ConfiguredSVP
		begin
			set @ActivityComments = @SVPRemarks
			update Indent set SeniorVicePresidentStatusDate = @RecordDate	where IndentID = @IndentID
		end
		else
		begin
			set @ActivityComments = @FHRemarks
			update Indent set FunctionHeadStatusDate = @RecordDate	where IndentID = @IndentID
		end
	end
	
	update Indent set
		AssignedTo = @AssignedTo,
		ModifiedDate = @RecordDate
		where IndentID = @IndentID
	
	
	-- generate name and email for users list
	declare @ToEmailNotificationsEmailIDs nvarchar(500)
	declare @CcEmailNotificationsEmailIDs nvarchar(500)

	-- read to email notifications
	DECLARE @ToUsers TABLE(ROWID INT IDENTITY(1,1), UserID nvarchar(100))
	insert into @ToUsers(UserID) select value from dbo.fn_Split(@ToEmailNotifications, ',')

	DECLARE @RowCount INT
	SET @RowCount = (SELECT COUNT(UserID) FROM @ToUsers) 
	DECLARE @I INT
	SET @I = 1
	
	-- Loop through the rows of a table @users
	WHILE (@I <= @RowCount)
	BEGIN
			-- Declare variables to hold the data which we get after looping each record
			SELECT @ToEmailNotificationsEmailIDs = isnull(@ToEmailNotificationsEmailIDs,'') + u.Email + ', ' 
			FROM Users u (nolock)
			join @ToUsers usr on u.UserID = usr.UserID
			WHERE ROWID = @I
			SET @I = @I  + 1
	END

	

	-- read cc email notofications
	DECLARE @CcUsers TABLE(ROWID INT IDENTITY(1,1), UserID nvarchar(100))
	insert into @CcUsers(UserID) select value from dbo.fn_Split(@CcEmailNotifications, ',')
	
	SET @I = 1
	SET @RowCount = (SELECT COUNT(UserID) FROM @CcUsers) 
	-- Loop through the rows of a table @CcUsers
	WHILE (@I <= @RowCount)
	BEGIN
			-- Declare variables to hold the data which we get after looping each record
			SELECT @CcEmailNotificationsEmailIDs = isnull(@CcEmailNotificationsEmailIDs,'') + u.Email + ', ' 
			FROM Users u (nolock)
			join @CcUsers usr on u.UserID = usr.UserID
			WHERE ROWID = @I
			SET @I = @I  + 1
	END

	
	-- Insert into Activity log
	if len(isnull(@ActivityDescription,'')) > 0
	begin
		INSERT INTO ActivityLog( IndentID, UserID, LogTypeID, Header, Description, Comments, RecordDate)
		values (@IndentID, @StatusChangedBy, @ActivityTypeID, @ActivityHeader, @ActivityDescription, @ActivityComments, @RecordDate)
	end
	-- return values for further processing
	select @IndentNumber as IndentNumber, @ToEmailNotificationsEmailIDs as ToEmailNotifications, @CcEmailNotificationsEmailIDs as CcEmailNotifications
	
END
GO
