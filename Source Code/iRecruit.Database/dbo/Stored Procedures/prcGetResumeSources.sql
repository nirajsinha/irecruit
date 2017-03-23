
-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcGetResumeSources   1 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
select * from item
GO
prcGetResumeSources 1
*/
CREATE PROCEDURE prcGetResumeSources
(
   @CompanyID INT = null   
)
AS
BEGIN
	
	select t.Name as ResumeSource, count(c.CandidateID) as CandidatesCount
	from Candidates c (nolock)
	right join Type t (nolock) on c.ResumeSourceTypeID = t.TypeID
	right join TypeClass tc (nolock) on t.TypeClassID = tc.TypeClassID	
	where tc.Code = '10'
	group by t.Name
	
END
