
-- **********************************************************    
-- @@Verion: 1    
-- SP Name: dbo.prcResumeSearch   24 
-- Author: Niraj Sinha  
-- Description: assigns indent, and returns data for further action
-- ********************************************************    
/*
select * from item
GO
prcResumeSearch @PageNo=1,@PageSize=4,@SortColumn= 'FirstName', @SortOrder= 'ASC', @CandidateStatus=''
*/
CREATE PROCEDURE prcResumeSearch
(
   /* Optional Filters for Dynamic Search*/
   @SearchString NVARCHAR(200) = NULL,
   @FirstName varchar(100) = NULL,
   @LastName varchar(100) = NULL,
   @Email varchar(100) = NULL,
   @ContactNumber varchar(100) = NULL,
   @MinExperience INT = NULL,
   @ResumeSourceTypeID INT = NULL,
   @ResumeSourceDetail  varchar(100) = NULL,
   @Skills varchar(100) = NULL,
   @Passport BIT = NULL,
   @Visa BIT = NULL,
   @TravelledOnsiteBefore BIT = NULL,
   @Gender varchar(1) = NULL,
   @Certifications varchar(200) = NULL,
   @CandidateStatus varchar(100) = NULL,
   /*– Pagination Parameters */
   @PageNo INT = 1,
   @PageSize INT = 10,
   /*– Sorting Parameters */
   @SortColumn NVARCHAR(50) = 'FirstName',
   @SortOrder NVARCHAR(4)= 'ASC'
)
AS
BEGIN
    /*–Declaring Local Variables corresponding to parameters for modification */
    DECLARE
	@lSearchString NVARCHAR(200) = NULL,
    @lFirstName varchar(100) = NULL,
    @lLastName varchar(100) = NULL,
    @lEmail varchar(100) = NULL,
    @lContactNumber varchar(100) = NULL,
    @lMinExperience INT = NULL,
    @lResumeSourceTypeID INT = NULL,
    @lResumeSourceDetail  varchar(100) = NULL,
    @lSkills varchar(100) = NULL,
    @lPassport BIT = NULL,
    @lVisa BIT = NULL,
    @lTravelledOnsiteBefore BIT = NULL,
    @lGender varchar(1) = NULL,
	@lCertifications varchar(200) = NULL,
	@lCandidateStatusTypeID INT = NULL,
	@lPageNo INT = 1,
	@lPageSize INT = 10,
	/*– Sorting Parameters */
	@lSortColumn NVARCHAR(20),
	@lSortOrder NVARCHAR(4),
	@lFirstRec INT,
    @lLastRec INT,
	@lTotalRows INT
	
	declare @Temp table( TotalCount int, ROWNUM int, CandidateID int, FirstName varchar(100), LastName varchar(100), ContactNumber varchar(20), Email varchar(100), 
		TotalExperience int, Certifications varchar(200), CreatedDate datetime, Remarks varchar(500), Reference1 varchar(500), HireStatus varchar(200), ResumeVirtualPath varchar(500), ResumeFileName varchar(100))

    /*Setting Local Variables*/
	SET @lSearchString = @SearchString
	SET @lFirstName = @FirstName
    SET @lLastName = @LastName
    SET @lEmail = @Email
    SET @lContactNumber = @ContactNumber
    SET @lMinExperience = @MinExperience
    SET @lResumeSourceTypeID = @ResumeSourceTypeID
    SET @lResumeSourceDetail = @ResumeSourceDetail
    SET @lSkills = @Skills
    SET @lPassport = @Passport
    SET @lVisa = @Visa
    SET @lTravelledOnsiteBefore = @TravelledOnsiteBefore
    SET @lGender = @Gender
    SET @lCertifications = @Certifications
	SELECT @lCandidateStatusTypeID = TypeID from Type where Name = @CandidateStatus
	SET @lPageNo = @PageNo
    SET @lPageSize = @PageSize
    SET @lSortColumn = LTRIM(RTRIM(@SortColumn))
    SET @lFirstRec = ( @lPageNo - 1 ) * @lPageSize
    SET @lLastRec = ( @lPageNo * @lPageSize + 1 )
    SET @lTotalRows = @lFirstRec - @lLastRec + 1

    ; WITH CTE_Results
    AS (
		SELECT ROW_NUMBER() OVER (ORDER BY
			CASE WHEN (@lSortColumn = 'FirstName' AND @SortOrder='ASC')
						THEN FirstName
			END ASC,
			CASE WHEN (@lSortColumn = 'FirstName' AND @SortOrder='DESC')
					   THEN FirstName
			END DESC,

			CASE WHEN (@lSortColumn = 'MinExperience' AND @SortOrder='ASC')
					  THEN TotalExperience
			END ASC,
			CASE WHEN @lSortColumn = 'MinExperience' AND @SortOrder='DESC'
					 THEN TotalExperience
			END DESC
	   ) AS ROWNUM,
	   Count(*) over () AS TotalCount,
	   CandidateID,
	   FirstName, 
	   LastName, 
	   ContactNumber, 
	   Email,
	   TotalExperience,
	   Certifications, 
	   CreatedDate, 
		Remarks, 
		Reference1,	
		'' as HireStatus,
		CandidateStatusTypeID,
		'' ResumeVirtualPath, 
		null as ResumeFileName 
 FROM Candidates
 WHERE
	
	(@lSearchString IS NULL OR CandidateID LIKE '%' + @lSearchString + '%' OR FirstName LIKE '%' + @lSearchString + '%' OR LastName LIKE '%' + @lSearchString + '%' OR Email LIKE '%' + @lSearchString + '%' OR Skills LIKE '%' + @lSearchString + '%' OR Certifications LIKE '%' + @lCertifications + '%')
	AND(@lFirstName IS NULL OR FirstName LIKE '%'+ @lFirstName + '%')
    AND(@lLastName IS NULL OR LastName LIKE '%'+ @lLastName + '%')
	AND(@lEmail IS NULL OR Email LIKE '%'+ @lEmail + '%')
    AND(@lContactNumber IS NULL OR ContactNumber LIKE '%'+ @lContactNumber + '%')
    AND(@lMinExperience IS NULL OR TotalExperience >= @lMinExperience)
    AND(@lResumeSourceTypeID IS NULL OR ResumeSourceTypeID = @lResumeSourceTypeID)
    AND(@lResumeSourceDetail IS NULL OR ResumeSourceDetail LIKE '%'+ @lResumeSourceDetail + '%')
    AND(@lSkills IS NULL OR Skills LIKE '%'+ @lSkills + '%')
    AND(@lPassport IS NULL OR Passport = @lPassport)
    AND(@lVisa IS NULL OR Visa = @lVisa)
    AND(@lTravelledOnsiteBefore IS NULL OR TravelledOnsiteBefore = @lTravelledOnsiteBefore)
    AND(@lGender IS NULL OR Gender = @lGender)
	AND(@lCertifications IS NULL OR Certifications LIKE '%'+ @lCertifications + '%')
	AND(@lCandidateStatusTypeID IS NULL OR CandidateStatusTypeID = @lCandidateStatusTypeID)
)
INSERT INTO @Temp
SELECT
    TotalCount,
	ROWNUM,
    CandidateID,
	FirstName, 
	LastName, 
	ContactNumber, 
	Email,
	TotalExperience,
	Certifications, 
	CreatedDate, 
	Remarks, 
	Reference1,	
	HireStatus = (select Name from Type where TypeID = CandidateStatusTypeID),
	ResumeVirtualPath = (select top 1 ResumePath from Resumes where CandidateID = CPC.CandidateID),
	ResumeFileName = null
FROM CTE_Results AS CPC

SELECT * FROM @Temp
WHERE
	ROWNUM > @lFirstRec
    AND ROWNUM < @lLastRec
 ORDER BY ROWNUM ASC

END
