USE [iRecruit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prcGetOfferJoiningRatio]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[prcGetOfferJoiningRatio]
GO

-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcGetOfferJoiningRatio   1 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
select * from item
GO
prcGetOfferJoiningRatio 1
*/
CREATE PROCEDURE prcGetOfferJoiningRatio
(
   @CompanyID INT
)
AS
BEGIN
	
	
	declare @OffersMadeTypeID INT 
	select @OffersMadeTypeID = TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='6'
	declare @OnBoardTypeID INT 
	select @OnBoardTypeID = TypeID from Type t (nolock)
								join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID
								where tc.Code = '8' and t.Code ='7'	
	
	declare @Temp table(DepartmentName varchar(100), CandidateStatusTypeID int, CandidateCount int)
	insert into @Temp
	select d.Name, c.CandidateStatusTypeID, count(c.CandidateID)
	from Candidates c (nolock)
	join Indent i (nolock) on c.IndentNumber = i.IndentNumber
	join Departments d (nolock) on i.DepartmentID = d.DepartmentID
	join Company co (nolock) on d.CompanyID = co.CompanyID
	where co.CompanyID = @CompanyID
	and i.Indent_Status > 0
	and c.CandidateStatusTypeID in (@OffersMadeTypeID, @OnBoardTypeID)
	group by d.Name, c.CandidateStatusTypeID 
	
	declare @Result table(DepartmentName varchar(100), OffersMadeCount int, OnBoardCount int)
	insert into @Result (DepartmentName)
	select DepartmentName from @Temp 
	
	update @Result set OffersMadeCount = CandidateCount from @Temp t
							join @Result r on r.DepartmentName = t.DepartmentName 
							where t.CandidateStatusTypeID = @OffersMadeTypeID
	update @Result set OnBoardCount = CandidateCount from @Temp t
							join @Result r on r.DepartmentName = t.DepartmentName 
							where t.CandidateStatusTypeID = @OnBoardTypeID
	update @Result set OffersMadeCount = 0 where OffersMadeCount is null
	update @Result set OnBoardCount = 0 where OnBoardCount is null
	select * from @Result
			
END
GO
