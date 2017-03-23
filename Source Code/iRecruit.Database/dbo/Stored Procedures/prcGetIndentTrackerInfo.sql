
-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcGetIndentTrackerInfo   1 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
declare @p1 int, @p2 int, @p3 int,@p4 int, @p5 int, @p6 int
set @p1 = 0
set @p2 = 0
set @p3 = 0
set @p4 = 0
set @p5 = 0
set @p6 = 0
exec prcGetIndentTrackerInfo @IndentNumber='iHT-IT-2014-0005', @NoOfPositions=@p1 output, @OffersMade=@p2 output, @OnBoard=@p3 output, @Rejected=@p4 output, @OfferDenied=@p5 output, @InProcess=@p6 output
select @p1, @p2, @p3, @p4, @p5, @p6
*/
CREATE PROCEDURE prcGetIndentTrackerInfo
(
   @IndentNumber varchar(200),
   @NoOfPositions INT OUTPUT,
   @OffersMade INT OUTPUT,
   @OnBoard INT OUTPUT,
   @Rejected INT OUTPUT,
   @OfferDenied INT OUTPUT,
   @InProcess INT OUTPUT
   
)
AS
BEGIN
	
	
	select @NoOfPositions = NoOfPositions from Indent where IndentNumber = @IndentNumber
	declare @Temp table( CandidateID int, CandidateStatusTypeID int)
	insert into @Temp
	select c.CandidateID, c.CandidateStatusTypeID 
		from Candidates c (nolock)
		join Type t (nolock) on c.CandidateStatusTypeID = t.TypeID
		join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
		where tc.Code = '8' and t.Code in('1','4','5','6','7','8','9','10')
		and c.IndentNumber = @IndentNumber
	
    declare @OffersMadeTypeID INT 
	select  @OffersMadeTypeID= TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='6'
    declare @OnBoardTypeID INT 
	select  @OnBoardTypeID= TypeID from Type t (nolock)
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

	select @OffersMade = count(CandidateID) from @Temp where CandidateStatusTypeID = @OffersMadeTypeID
	select @OnBoard = count(CandidateID) from @Temp where CandidateStatusTypeID = @OnBoardTypeID
	select @Rejected = count(CandidateID) from @Temp where CandidateStatusTypeID = @RejectedTypeID
	select @OfferDenied = count(CandidateID) from @Temp where CandidateStatusTypeID = @OfferDeniedTypeID
	select @InProcess = count(CandidateID) from @Temp where CandidateStatusTypeID in(@ShortListedTypeID, @InProcessTypeID, @SelectedTypeID)
	
END
