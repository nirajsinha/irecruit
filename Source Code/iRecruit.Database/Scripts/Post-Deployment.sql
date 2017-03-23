/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- Types
-- Project status
DECLARE @TypeClassID INT 
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
SELECT '1','Project Status','Process statuses', 1, getdate()
where not exists (select 1 from TypeClass where Code='1')

SELECT @TypeClassID= TypeClassID from TypeClass where Code='1'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
SELECT @TypeClassID, '1', 'Potential New Project', 'New projects', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
SELECT @TypeClassID, '2', 'Ongoing', 'Ongoing projects', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

-- Indent reason
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
SELECT '2','Indent Reason','Indent Reason', 1, getdate()
where not exists (select 1 from TypeClass where Code='2')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='2'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'Replacement', 'Replacement', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'New Requirement', 'New Requirement', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

-- Resource location
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
SELECT '3','Resource Location','Resource Location', 1, getdate()
where not exists (select 1 from TypeClass where Code='3')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='3'


INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'Onsite', 'Onsite', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'Offshore', 'Offshore', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

-- Employment type
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
SELECT '4','Employment Type','Employment Type', 1, getdate()
where not exists (select 1 from TypeClass where Code='4')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='4'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'Permanent', 'Permanent', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')


INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'Consultant', 'Consultant', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '3', 'Contract', 'Contract', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='3')

-- Staffing mode
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
SELECT '5','Staffing Mode','Staffing Mode', 1, getdate()
where not exists (select 1 from TypeClass where Code='5')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='5'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'External', 'External', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'Internal', 'Internal', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

-- Visa type
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
SELECT '6','Visa Type','Visa Type', 1, getdate()
where not exists (select 1 from TypeClass where Code='6')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='6'


INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'H1/L1', 'Work Visa', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'B1/B2', 'Business Visa', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '3', 'None', 'None', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='3')

-- Workflow status types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
select '7','Indent Status','Indent Status', 1, getdate()
where not exists (select 1 from TypeClass where Code='7')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='7'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'Save', 'Save', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'Submitted', 'Send for Approval', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '3', 'Approved', 'Approved', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='3')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '4', 'On Hold', 'On Hold', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='4')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '5', 'Rejected', 'Rejected', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='5')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '6', 'Cancelled', 'Cancelled', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='6')


-- Candidate status types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
select '8','Candidate Status','Candidate and resume statuses', 1, getdate()
where not exists (select 1 from TypeClass where Code='8')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='8'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'Short Listed', 'Short Listed', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'Archived', 'Archived', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '3', 'Not Appeared', 'Did not turned up for interview', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='3')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '4', 'In Process', 'Selection process is in progress', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='4')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '5', 'Selected', 'Selected in interview', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='5')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '6', 'Offered', 'Offer letter issued', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='6')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '7', 'On Board', 'On Board', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='7')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '8', 'Declined Offer', 'Did not joined after offer made', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='8')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '9', 'Rejected', 'Rejected in selection process', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='9')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '10', 'On Hold', 'Candidate is on hold', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='10')

-- Activity log types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
select '9','Activity Log','Activity log types', 1, getdate()
where not exists (select 1 from TypeClass where Code='9')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='9'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'IndentRaised', 'Indent Raised', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'IndentApproved', 'Indent Approved', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '3', 'IndentRejected', 'Indent Rejected or Cancelled', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='3')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '4', 'IndentOnHold', 'Indent On Hold', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='4')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '5', 'SelectedRound1', 'Selected in Technical Interview Round 1', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='5')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '6', 'SelectedRound2', 'Selected in Technical Interview Round 2', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='6')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '7', 'RejectedRound1', 'Rejected in Technical Interview Round 1', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='7')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '8', 'RejectedRound2', 'Rejected in Technical Interview Round 2', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='8')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '9', 'HRSelected', 'Selected in HR Round', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='9')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '10', 'HRRejected', 'Rejected in HR Round', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='10')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '11', 'OfferMade', 'Offered by HR', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='11')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '12', 'OnBoard', 'Joined Company', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='12')



-- Resume source types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
select '10','Resume Source','Resume sources types', 1, getdate()
where not exists (select 1 from TypeClass where Code='10')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='10'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'Direct', 'Direct', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'Employee', 'Employee Reference', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '3', 'Vendor', 'Placement agencies', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='3')


-- Skills grade types
INSERT INTO TypeClass(Code, Name, Description,Active,ModifiedDate)
select '11','Skills Grade','Grading for candidate skills', 1, getdate()
where not exists (select 1 from TypeClass where Code='11')
SELECT @TypeClassID= TypeClassID from TypeClass where Code='11'

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '1', 'Expert', 'Has good knowledge & experience in the relevent technology & functional area. Has capability for problem solving and capability to train/coach others.', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='1')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '2', 'Proficient', 'Has good knowledge and application skills in relevent functional & technology area. Has capability to experience to perform task independently.', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='2')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '3', 'Good Understanding', 'Has good clarity on fundamentals and relevent experience in application. Would require more time, comprehensive/OJT training, to become fully effective and work independently.', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='3')

INSERT INTO Type(TypeClassID, Code, Name, Description,Active,ModifiedDate)
select @TypeClassID, '4', 'Beginner', 'Understands fundamentals/basic theorotical knowledge but limited in application ability.', 1, getdate()
where not exists (select 1 from Type where TypeClassID=@TypeClassID and Code='4')



-- Test data scripts
DECLARE @CompanyID INT 
INSERT INTO Company(Code, Name, Address, City, State, Country, PostalCode, URL)
VALUES('iSpace','iSpace, Inc.','2381 Rosecrans Ave., Suite 110','El Segundo', 'CA', 'USA', '90245','http://www.ispace.com')
SELECT @CompanyID= CompanyID from Company where Code='iSpace'

-- Features
INSERT INTO Features(Code, CompanyID, Description) 
select 'Indent',@CompanyID,'Indent'
where not exists (select 1 from Features where Code='Indent')

INSERT INTO Features(Code, CompanyID, Description) 
select 'Interviews',@CompanyID,'Interviews'
where not exists (select 1 from Features where Code='Interviews')

INSERT INTO Features(Code, CompanyID, Description) 
select 'ResumeManagement',@CompanyID,'Uploading, modifying, deleting resumes'
where not exists (select 1 from Features where Code='ResumeManagement')

INSERT INTO Features(Code, CompanyID, Description) 
select 'Administrator',@CompanyID,'Configuring all master data'
where not exists (select 1 from Features where Code='Administrator') 

-- Users
INSERT INTO Users(UserID, CompanyID, Name, Branches, AccessFeatures) 
select 'nirajs',@CompanyID,'Niraj Sinha','ODC,OC','Administrator'
where not exists (select 1 from Users where UserID='nirajs')

INSERT INTO Users(UserID, CompanyID, Name, Email) 
select 'HR',@CompanyID,'HR', 'niraj.sinha@ispace.com'
where not exists (select 1 from Users where UserID='HR')

-- branches
INSERT INTO Branches(CompanyID, Code, Name, Address, City, State, Country, PostalCode, URL)
select @CompanyID, 'OC','iSpace, Inc.','2100 Main Street, Suite 210','Irvine', 'CA', 'USA', '92614','http://www.ispace.com'
where not exists (select 1 from Branches where Code='OC')

INSERT INTO Branches(CompanyID, Code, Name, Address, City, State, Country, PostalCode, URL)
select  @CompanyID, 'ODC','Eliptico IT Solutions Pvt. Ltd.','C - Block , 3rd Floor, Wing A (1) Cybergateway, Hi-Tech City, Madhapur','Hyderabad', 'Telangana', 'India', '','http://www.ispace.com'
where not exists (select 1 from Branches where Code='ODC')

-- Departments
INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
select @CompanyID, 'IT','IT Services', 1, getdate()
where not exists (select 1 from Departments where Code='IT')

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
select @CompanyID, 'BPO','BPO', 1, getdate()
where not exists (select 1 from Departments where Code='BPO')

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
select @CompanyID, 'Admin','Admin', 1, getdate()
where not exists (select 1 from Departments where Code='Admin')

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
select @CompanyID, 'FIN','Finance', 1, getdate()
where not exists (select 1 from Departments where Code='FIN')

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
select @CompanyID, 'HR','Human Resources', 1, getdate()
where not exists (select 1 from Departments where Code='HR')


-- TechnologiesAndSkills
INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'WCF', @CompanyID, 'WCF', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='WCF')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'MVC', @CompanyID, 'MVC', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='MVC')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'WebAPI', @CompanyID, 'WebAPI', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='WebAPI')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'AngularJS', @CompanyID, 'AngularJS', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='AngularJS')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'Java', @CompanyID, 'Java', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='Java')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'Struts', @CompanyID, 'Struts', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='Struts')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'JBPM', @CompanyID, 'JBPM', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='JBPM')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'OOP', @CompanyID, 'OOP', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='OOP')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'OOAD', @CompanyID, 'OOAD', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='OOAD')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'Design Patterns', @CompanyID, 'Design Patterns', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='Design Patterns')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'UML', @CompanyID, 'UML', 1, 1
where not exists (select 1 from Features where Code='UML')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'Sharepoint', @CompanyID, 'Sharepoint', 1, 1
where not exists (select 1 from TechnologiesAndSkills where Code='Sharepoint')

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
select 'Negotiation', @CompanyID, 'Negotiation', 2, 1
where not exists (select 1 from TechnologiesAndSkills where Code='Negotiation')

-- InterviewPanel
INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
select 'Niraj Sinha', @CompanyID, 'IT, BPO, Admin, FIN','OOAD, OOP, WCF, WebAPI, MVC', 1
where not exists (select 1 from InterviewPanel where Name='Niraj Sinha')

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
select 'Pradeep Kumar Gavuji', @CompanyID, 'IT','WCF, WebAPI, MVC', 1
where not exists (select 1 from InterviewPanel where Name='Pradeep Kumar Gavuji')

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
select 'Gnanasekhar Eatakuri', @CompanyID, 'IT','WebAPI, MVC', 1
where not exists (select 1 from InterviewPanel where Name='Gnanasekhar Eatakuri')

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
select 'Ramakrishna Bodi', @CompanyID, 'IT, BPO, Admin, FIN','OOAD, OOP, WCF, WebAPI, MVC', 2
where not exists (select 1 from InterviewPanel where Name='Ramakrishna Bodi')

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
select 'Sreenivasa Rao Uyyurru', @CompanyID, 'IT','MVC', 2
where not exists (select 1 from InterviewPanel where Name='Sreenivasa Rao Uyyurru')

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
select 'Naveen Krishnamsetti', @CompanyID, 'HR','Negotiation', 1
where not exists (select 1 from InterviewPanel where Name='Naveen Krishnamsetti')

-- Consultancies
INSERT INTO Consultancies(ConsultancyName,Address1,Address2,City,State,Country,PostalCode,ContactPerson,ContactNumber,Email)
select 'Pyramid Consultancy Services', 'Ameerpet','','Hyderabad','Telangana','India','500016','Sunil','999955555','sunil@gmail.com'
where not exists (select 1 from Consultancies where ConsultancyName='Pyramid Consultancy Services')

