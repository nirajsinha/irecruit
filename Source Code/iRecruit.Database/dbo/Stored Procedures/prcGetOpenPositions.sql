
-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcGetOpenPositions   1 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
declare @p1 int, @p2 int, @p3 int, @p4 int, @p5 int
set @p1 = 0
set @p2 = 0
set @p3 = 0
set @p4 = 0
set @p5 = 0
exec prcGetOpenPositions @CompanyID=1, @OpenPositions=@p1 output, @OffersMade=@p2 output, @OnBoard=@p3 output, @RejectedDenied=@p4 output, @InProcess=@p5 output
select @p1, @p2, @p3, @p4, @p5
*/
CREATE PROCEDURE prcGetOpenPositions
(
   @CompanyID INT,
   @OpenPositions INT OUTPUT,
   @OffersMade INT OUTPUT,
   @OnBoard INT OUTPUT,
   @RejectedDenied INT OUTPUT,
   @InProcess INT OUTPUT
)
AS
BEGIN
	
	declare @NoOfPositions INT
	
	select @NoOfPositions = sum(i.NoOfPositions)
	from Indent i (nolock)
	join Departments d (nolock) on i.DepartmentID = d.DepartmentID
	join Company c (nolock) on d.CompanyID = c.CompanyID
	where c.CompanyID = @CompanyID
	and i.Indent_Status > 0
	
	declare @OffersMadeTypeID INT 
	select @OffersMadeTypeID = TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='6'
	declare @OnBoardTypeID int
	select @OnBoardTypeID = TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='7'	
	declare @RejectedTypeID INT 
	select  @RejectedTypeID= TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='9'
    declare @OfferDeniedTypeID INT 
	select  @OfferDeniedTypeID= TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='8'
	declare @ShortListedTypeID INT 
	select  @ShortListedTypeID= TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='1'
	declare @SelectedTypeID INT 
	select  @SelectedTypeID= TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='5'
    declare @InProcessTypeID INT 
	select  @InProcessTypeID= TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='4'

	select @OffersMade = count(*) from Candidates where CandidateStatusTypeID = @OffersMadeTypeID
	select @OnBoard = count(*) from Candidates where CandidateStatusTypeID = @OnBoardTypeID
	select @OpenPositions = @NoOfPositions - @OffersMade + @OnBoard
	select @RejectedDenied = count(*) from Candidates where CandidateStatusTypeID in (@RejectedTypeID, @OfferDeniedTypeID)
	select @InProcess = count(*) from Candidates where CandidateStatusTypeID in(@ShortListedTypeID, @InProcessTypeID, @SelectedTypeID)
END
