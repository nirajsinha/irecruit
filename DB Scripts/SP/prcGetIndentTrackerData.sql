USE [iRecruit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prcGetIndentTrackerData]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[prcGetIndentTrackerData]
GO

-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcGetIndentTrackerData   1 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
select * from item
GO
prcGetIndentTrackerData 1, 1, 0

select * from indent
*/
CREATE PROCEDURE prcGetIndentTrackerData
(
   @CompanyID INT,
   /*– Pagination Parameters */
   @PageNo INT = null,
   @PageSize INT = null
)
AS
BEGIN
					
declare @Temp table( TotalCount int, ROWNUM int, IndentDate datetime, IndentNumber varchar(100), PositionTitle varchar(100), Technologies varchar(500), NoOfPositions int, 
		Client_Domain varchar(200), Indent_Status varchar(200), AssignedTo varchar(200))
		
;
WITH CTE_Results
		AS (SELECT ROW_NUMBER() OVER (ORDER BY i.IndentDate DESC) AS ROWNUM,
		Count(*) over () AS TotalCount,
		IndentDate = i.IndentDate,
		IndentNumber = i.IndentNumber,
		PositionTitle = i.PositionTitle,
		Technologies = i.Technologies,
		NoOfPositions = i.NoOfPositions,
		Client_Domain = i.Client_Domain,
		Indent_Status = t.Description,
		AssignedTo = u.Name
 from Company c (nolock)
 join Departments d (nolock) on c.CompanyID = d.DepartmentID
 join Indent i (nolock) on d.DepartmentID = i.DepartmentID
 join Type t (nolock) on i.Indent_Status = t.TypeID
 left join Users u (nolock) on i.AssignedTo = u.UserID
 WHERE c.CompanyID = @CompanyID
 and i.Indent_Status > 0
 
 
)
INSERT INTO @Temp
SELECT
    TotalCount,
	ROWNUM,
    IndentDate,
	IndentNumber,
	PositionTitle,
	Technologies,
	NoOfPositions,
	Client_Domain,
	Indent_Status,
	AssignedTo
FROM CTE_Results AS CPC

 
if isnull(@PageSize,0) > 0 and isnull(@PageNo,0) > 0
begin
	declare @lFirstRec int,@lLastRec int
	SET @lFirstRec = ( @PageNo - 1 ) * @PageSize
    SET @lLastRec = ( @PageNo * @PageSize + 1 )
    
	SELECT * FROM @Temp
	WHERE
		ROWNUM > @lFirstRec
		AND ROWNUM < @lLastRec
	 ORDER BY ROWNUM ASC
end
else
begin
	SELECT * FROM @Temp
end
END
GO
