DROP DATABASE iRecruit
GO

CREATE DATABASE iRecruit
GO

USE iRecruit
GO


CREATE TABLE Company
(
CompanyID INT IDENTITY(1,1) PRIMARY KEY,
Code		VARCHAR(100),
Name		VARCHAR(100),
Address		VARCHAR(250),
City		VARCHAR(100),
State		VARCHAR(75),
Country VARCHAR(100),
PostalCode		VARCHAR(10),
Email		VARCHAR(100),
Phone		VARCHAR(10),
Fax			VARCHAR(10),
URL			VARCHAR(100)
)
GO


CREATE TABLE Features
(
FeatureID INT IDENTITY(1,1) PRIMARY KEY,
Code VARCHAR(50),
CompanyID INT REFERENCES Company(CompanyID),
Description VARCHAR(250)
)
GO

CREATE TABLE TechnologiesAndSkills
(
TechnologyAndSkillID  INT IDENTITY(1,1) PRIMARY KEY,
Code VARCHAR(50),
CompanyID INT REFERENCES Company(CompanyID),
Name VARCHAR(250),
SkillType INT Default 1,
Active BIT
)
GO

CREATE TABLE Branches
(
BranchID INT IDENTITY(1,1) PRIMARY KEY,
CompanyID	INT	REFERENCES Company(CompanyID),
Code		VARCHAR(100),
Name		VARCHAR(100),
Address		VARCHAR(250),
City		VARCHAR(100),
State		VARCHAR(75),
Country VARCHAR(100),
PostalCode		VARCHAR(10),
Email		VARCHAR(100),
Phone		VARCHAR(10),
Fax			VARCHAR(10),
URL			VARCHAR(100)
)
GO

CREATE TABLE Departments
(
DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
CompanyID INT REFERENCES Company(CompanyID),
Code VARCHAR(50),
Name VARCHAR(200),
Active BIT,
ModifiedDate datetime
)
GO

CREATE TABLE DepartmentRoles
(
DepartmentRoleID  INT IDENTITY(1,1) PRIMARY KEY,
DepartmentID INT REFERENCES Departments(DepartmentID),
BranchID INT REFERENCES Branches(BranchID),
FunctionHead VARCHAR(200),
SVP VARCHAR(200),
Active BIT
)
GO

CREATE TABLE Users
(
UserID VARCHAR(100),
CompanyID INT REFERENCES Company(CompanyID),
Name VARCHAR(250),
Title VARCHAR(250),
Email VARCHAR(250),
Branches VARCHAR(250),
AccessFeatures VARCHAR(1000),
Photo VARCHAR(max)
)
GO

CREATE TABLE InterviewPanel
(
InterviewPanelID INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(250),
CompanyID INT REFERENCES Company(CompanyID),
Departments VARCHAR(200),
Technologies VARCHAR(200),
Level VARCHAR(200)
)
GO

CREATE TABLE TypeClass
(
TypeClassID		INT IDENTITY(1,1)	PRIMARY KEY,
Code			VARCHAR(10),
Name			VARCHAR(30),
Description		VARCHAR(50),
Active			BIT,
ModifiedDate	DATETIME
)
GO

CREATE TABLE Type
(
TypeID		INT IDENTITY(1,1)	PRIMARY KEY,
TypeClassID		INT REFERENCES TypeClass(TypeClassID),
Code			VARCHAR(10),
Name			VARCHAR(50),
Description		VARCHAR(500),
Active			BIT,
ModifiedDate	DATETIME
)
GO

CREATE TABLE Indent
(
IndentID		INT	IDENTITY(1,1)	PRIMARY KEY,
IndentNumber	VARCHAR(25) UNIQUE NOT NULL,
IndentDate		DATETIME,
BranchID		INT REFERENCES Branches(BranchID),
DepartmentID	INT REFERENCES Departments(DepartmentID),
Client_Domain	VARCHAR(50),
ProjectStatusID	INT,
ReasonID		INT,
LocationTypeID	INT,
EmploymentTypeID	INT,
StaffingModeID	INT,
ContractMonths	INT,
Technologies		VARCHAR(250),
TechnicalSkills varchar(1000),
BehaviouralSkills varchar(1000),
PositionTitle	VARCHAR(50),
NoOfPositions	INT,
MinExperiance	INT,
MaxExperiance	INT,
VisaType		VARCHAR(20),
TargetJoinDate	DATETIME,
InterviewPanel1	VARCHAR(250),
InterviewPanel2	VARCHAR(250),
InterviewPanel3	VARCHAR(250),
InterviewPanel4	VARCHAR(250),
InterviewPanel5	VARCHAR(250),
ReportingManager	VARCHAR(100),
DeploymentLocation	VARCHAR(200),
Qualification	VARCHAR(200),
Others			VARCHAR(200),
Indentor		VARCHAR(200),
Indent_Status	INT,
IndentorRemarks		VARCHAR(500),
FunctionHead	VARCHAR(250),
FunctionHeadStatusTypeID	INT,
FunctionHeadStatusDate	DATETIME,
FunctionHeadRemarks		VARCHAR(500),
SeniorVicePresident	VARCHAR(250),
SeniorVicePresidentStatusTypeID	INT,
SeniorVicePresidentStatusDate	DATETIME,
SeniorVicePresidentRemarks		VARCHAR(500),
JobDescription	VARCHAR(max),
UploadFile_Indents	VARCHAR(100),
StatusChangedBy	VARCHAR(100),
AssignedTo	VARCHAR(100),
CreatedBy		VARCHAR(100),
CreatedDate		DATETIME,
ModifiedBy		VARCHAR(100),
ModifiedDate	DATETIME
)
GO

CREATE TABLE Candidates
(
CandidateID	INT	IDENTITY(1,1)	PRIMARY KEY,
IndentNumber VARCHAR(25) REFERENCES Indent(IndentNumber),
FirstName VARCHAR(100),
LastName VARCHAR(100),
Gender	VARCHAR(1),
DOB	DATETIME,
Email VARCHAR(100),
ContactNumber VARCHAR(20),
Skills VARCHAR(200),
CurrentTitle VARCHAR(100),
CurrentCompany  VARCHAR(200),
CurrentLocation  VARCHAR(100),
Certifications VARCHAR(200),
TotalExperience DECIMAL,
Passport BIT,
Visa BIT,
AadhaarNumber varchar(20),
TravelledOnsiteBefore BIT,
Reference1 VARCHAR(100),
Reference1Contact VARCHAR(100),
Reference2 VARCHAR(100),
Reference2Contact VARCHAR(100),
ResumeSourceTypeID INT,
ResumeSourceDetail VARCHAR(300),
CurrentCTC MONEY,
ExpectedCTC MONEY,
CandidateStatusTypeID INT,
Remarks VARCHAR(500),
CreatedBy VARCHAR(200),
CreatedDate		DATETIME,
ModifiedBy	VARCHAR(200),
ModifiedDate	DATETIME

)
GO

CREATE TABLE CandidatesHistory
(
CandidatesHistoryID	INT	IDENTITY(1,1)	PRIMARY KEY,
CandidateID	INT,
IndentNumber VARCHAR(25),
FirstName VARCHAR(100),
LastName VARCHAR(100),
Gender	VARCHAR(1),
DOB	DATETIME,
Email VARCHAR(100),
ContactNumber VARCHAR(20),
Skills VARCHAR(200),
CurrentTitle VARCHAR(100),
CurrentCompany  VARCHAR(200),
CurrentLocation  VARCHAR(100),
Certifications VARCHAR(200),
TotalExperience DECIMAL,
Passport BIT,
Visa BIT,
AadhaarNumber varchar(20),
TravelledOnsiteBefore BIT,
Reference1 VARCHAR(100),
Reference1Contact VARCHAR(100),
Reference2 VARCHAR(100),
Reference2Contact VARCHAR(100),
ResumeSourceTypeID INT,
ResumeSourceDetail VARCHAR(300),
CurrentCTC MONEY,
ExpectedCTC MONEY,
CandidateStatusTypeID INT,
Remarks VARCHAR(500),
CreatedBy VARCHAR(200),
CreatedDate		DATETIME

)
GO

CREATE TABLE Resumes
(
ResumeID INT	IDENTITY(1,1)	PRIMARY KEY,
CandidateID	INT	REFERENCES Candidates(CandidateID),
ResumePath varchar(200),
FileType varchar(10),
CandidatePhoto VARBINARY
)
GO

CREATE TABLE Consultancies
(
ConsultancyID INT	IDENTITY(1,1)	PRIMARY KEY,
ConsultancyName	VARCHAR(100),
Address1 VARCHAR(300),
Address2 VARCHAR(300),
City VARCHAR(50),
State VARCHAR(50),
Country VARCHAR(100),
PostalCode VARCHAR(20),
ContactPerson VARCHAR(100),
ContactNumber VARCHAR(20),
Email VARCHAR(100)
)
GO

CREATE TABLE ActivityLog
(
ActivityLogID INT	IDENTITY(1,1)	PRIMARY KEY,
IndentID INT	REFERENCES Indent(IndentID),
UserID varchar(100),
LogTypeID int,
Header VARCHAR(500), 
Description VARCHAR(MAX),
Comments VARCHAR(MAX),
RecordDate DateTime
)
GO

CREATE TABLE InterviewSchedule
(
InterviewScheduleID INT	IDENTITY(1,1)	PRIMARY KEY,
CandidateID	INT REFERENCES Candidates(CandidateID),
InverviewRound INT,
ScheduledInterviewers VARCHAR(200),
Subject VARCHAR(100),
Description VARCHAR(500),
StartTime datetime,
EndTime datetime,
AttachResume bit,
Status	INT,
CreatedBy VARCHAR(200),
CreatedDate		DATETIME,
ModifiedBy	VARCHAR(200),
ModifiedDate	DATETIME

)
GO

CREATE TABLE InterviewFeedbacks
(
InterviewFeedbacksID INT	IDENTITY(1,1)	PRIMARY KEY,
CandidateID	INT REFERENCES Candidates(CandidateID),
InterviewRound INT,
InterviewerName VARCHAR(100),
PositionFor VARCHAR(100),
ReleventExperience DECIMAL,
ReleventExperienceDiscountReason VARCHAR(400),
TechKnowledgeAreas VARCHAR(100),
TechKnowledgeAreasLevel	VARCHAR(1),
TechKnowledgeAreasComments	VARCHAR(200),
AnalysisAreas VARCHAR(100),
AnalysisAreasLevel	VARCHAR(1),
AnalysisAreasComments	VARCHAR(200),
DesignAreas VARCHAR(100),
DesignAreasLevel	VARCHAR(1),
DesignAreasComments	VARCHAR(200),
CodingAreas VARCHAR(100),
CodingAreasLevel	VARCHAR(1),
CodingAreasComments	VARCHAR(200),
DatabaseAreas VARCHAR(100),
DatabaseAreasLevel	VARCHAR(1),
DatabaseAreasComments	VARCHAR(200),
TestingAreas VARCHAR(100),
TestingAreasLevel	VARCHAR(1),
TestingAreasComments	VARCHAR(200),
ResultOrientationLevel	VARCHAR(1),
ResultOrientationComments	VARCHAR(200),
CommunicationSkillsLevel	VARCHAR(1),
CommunicationSkillsComments	VARCHAR(200),
TeamWorkingLevel	VARCHAR(1),
TeamWorkingComments	VARCHAR(200),
LeadershipCapabilityLevel	VARCHAR(1),
LeadershipCapabilityComments	VARCHAR(200),
AttitudeLevel	VARCHAR(1),
AttitudeComments	VARCHAR(200),
OverallRatingLevel  VARCHAR(1),
OverallRatingComments	VARCHAR(200),
SelectionReason	VARCHAR(200),
PositivesRemarks	VARCHAR(200),
ConcernsGaps	VARCHAR(200),
PositionRecomended 	VARCHAR(200),
PositionSuggested 	VARCHAR(200),
AlternaticeCompetancy	VARCHAR(200),
TrainingsNeededTechnical 	VARCHAR(200),
TrainingNeededBehavioral	VARCHAR(200),
Status	INT,
CreatedBy VARCHAR(200),
CreatedDate		DATETIME,
ModifiedBy	VARCHAR(200),
ModifiedDate	DATETIME

)
GO

CREATE TABLE EmailNotifications
(
EmailNotificationID INT	IDENTITY(1,1)	PRIMARY KEY,
EmailFrom  VARCHAR(100),
EmailTo  VARCHAR(100),
EmailCc  VARCHAR(100),
Subject VARCHAR(100),
BodyHtml VARCHAR(2000),
Status	INT,
RecordDate DATETIME
)
GO

-- Types
-- Project status
DECLARE @TypeClassID INT 
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('1','Project Status','Process statuses', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Potential New Project', 'New projects', 1, getdate())
INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Ongoing', 'Ongoing projects', 1, getdate())


-- Indent reason
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('2','Indent Reason','Indent Reason', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Replacement', 'Replacement', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'New Requirement', 'New Requirement', 1, getdate())


-- Resource location
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('3','Resource Location','Resource Location', 1, getdate())
SELECT @TypeClassID= @@IDENTITY


INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Onsite', 'Onsite', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Offshore', 'Offshore', 1, getdate())

-- Employment type
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('4','Employment Type','Employment Type', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Permanent', 'Permanent', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Consultant', 'Consultant', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '3', 'Contract', 'Contract', 1, getdate())


-- Staffing mode
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('5','Staffing Mode','Staffing Mode', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'External', 'External', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Internal', 'Internal', 1, getdate())

-- Visa type
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('6','Visa Type','Visa Type', 1, getdate())
SELECT @TypeClassID= @@IDENTITY


INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'H1/L1', 'Work Visa', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'B1/B2', 'Business Visa', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '3', 'None', 'None', 1, getdate())

-- Workflow status types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('7','Indent Status','Indent Status', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Save', 'Save', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Submitted', 'Send for Approval', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '3', 'Approved', 'Approved', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '4', 'On Hold', 'On Hold', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '5', 'Rejected', 'Rejected', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '6', 'Cancelled', 'Cancelled', 1, getdate())


-- Candidate status types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('8','Candidate Status','Candidate and resume statuses', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Short Listed', 'Short Listed', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Archived', 'Archived', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '3', 'Not Appeared', 'Did not turned up for interview', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '4', 'In Process', 'Selection process is in progress', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '5', 'Selected', 'Selected in interview', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '6', 'Offered', 'Offer letter issued', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '7', 'On Board', 'On Board', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '8', 'Declined Offer', 'Did not joined after offer made', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '9', 'Rejected', 'Rejected in selection process', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '10', 'On Hold', 'Candidate is on hold', 1, getdate())


-- Activity log types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('9','Activity Log','Activity log types', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'IndentRaised', 'Indent Raised', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'IndentApproved', 'Indent Approved', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '3', 'IndentRejected', 'Indent Rejected or Cancelled', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '4', 'IndentOnHold', 'Indent On Hold', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '5', 'SelectedRound1', 'Selected in Technical Interview Round 1', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '6', 'SelectedRound2', 'Selected in Technical Interview Round 2', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '7', 'RejectedRound1', 'Rejected in Technical Interview Round 1', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '8', 'RejectedRound2', 'Rejected in Technical Interview Round 2', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '9', 'HRSelected', 'Selected in HR Round', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '10', 'HRRejected', 'Rejected in HR Round', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '11', 'OfferMade', 'Offered by HR', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '12', 'OnBoard', 'Joined Company', 1, getdate())



-- Resume source types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('10','Resume Source','Resume sources types', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Direct', 'Direct', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Employee', 'Employee Reference', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '3', 'Vendor', 'Placement agencies', 1, getdate())


-- Skills grade types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
VALUES('11','Skills Grade','Grading for candidate skills', 1, getdate())
SELECT @TypeClassID= @@IDENTITY

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '1', 'Expert', 'Has good knowledge & experience in the relevent technology & functional area. Has capability for problem solving and capability to train/coach others.', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '2', 'Proficient', 'Has good knowledge and application skills in relevent functional & technology area. Has capability to experience to perform task independently.', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '3', 'Good Understanding', 'Has good clarity on fundamentals and relevent experience in application. Would require more time, comprehensive/OJT training, to become fully effective and work independently.', 1, getdate())

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
VALUES(@TypeClassID, '4', 'Beginner', 'Understands fundamentals/basic theorotical knowledge but limited in application ability.', 1, getdate())
GO