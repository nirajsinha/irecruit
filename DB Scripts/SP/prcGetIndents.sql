USE [iRecruit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prcGetIndents]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[prcGetIndents]
GO

-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcGetIndents   1 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
select * from item
GO
prcGetIndents 1
*/
CREATE PROCEDURE prcGetIndents
(
   @CompanyID INT
)
AS
BEGIN
	select i.* 
	from Indent i (nolock)
	join Departments d (nolock) on i.DepartmentID = d.DepartmentID
	join Company c (nolock) on d.CompanyID = c.CompanyID
	where c.CompanyID = @CompanyID
	and i.Indent_Status > 0
END
GO
