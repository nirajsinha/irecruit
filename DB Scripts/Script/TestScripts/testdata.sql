USE iRecruit
GO

DECLARE @CompanyID INT 
INSERT INTO Company(Code, Name, Address, City, State, Country, PostalCode, URL)
VALUES('iSpace','iSpace, Inc.','2381 Rosecrans Ave., Suite 110','El Segundo', 'CA', 'USA', '90245','http://www.ispace.com')
SELECT @CompanyID= @@IDENTITY

-- Features
INSERT INTO Features(Code, CompanyID, Description) VALUES('Indent',@CompanyID,'Indent')
INSERT INTO Features(Code, CompanyID, Description) VALUES('Interviews',@CompanyID,'Interviews')
INSERT INTO Features(Code, CompanyID, Description) VALUES('ResumeManagement',@CompanyID,'Uploading, modifying, deleting resumes')
INSERT INTO Features(Code, CompanyID, Description) VALUES('Administrator',@CompanyID,'Configuring all master data')

INSERT INTO Users(UserID, CompanyID, Name, Branches, AccessFeatures) VALUES('nirajs',@CompanyID,'Niraj Sinha','ODC,OC','Administrator')
INSERT INTO Users(UserID, CompanyID, Name, Email) VALUES('HR',@CompanyID,'HR', 'niraj.sinha@ispace.com')
-- branches
INSERT INTO Branches(CompanyID, Code, Name, Address, City, State, Country, PostalCode, URL)
VALUES(@CompanyID, 'OC','iSpace, Inc.','2100 Main Street, Suite 210','Irvine', 'CA', 'USA', '92614','http://www.ispace.com')

INSERT INTO Branches(CompanyID, Code, Name, Address, City, State, Country, PostalCode, URL)
VALUES(@CompanyID, 'ODC','Eliptico IT Solutions Pvt. Ltd.','C - Block , 3rd Floor, Wing A (1) Cybergateway, Hi-Tech City, Madhapur','Hyderabad', 'Telangana', 'India', '','http://www.ispace.com')

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
VALUES(@CompanyID, 'IT','IT Services', 1, getdate())

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
VALUES(@CompanyID, 'BPO','BPO', 1, getdate())

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
VALUES(@CompanyID, 'Admin','Admin', 1, getdate())

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
VALUES(@CompanyID, 'FIN','Finance', 1, getdate())

INSERT INTO Departments(CompanyID, Code, Name, Active, ModifiedDate)
VALUES(@CompanyID, 'HR','Human Resources', 1, getdate())


-- TechnologiesAndSkills
INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('WCF', @CompanyID, 'WCF', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('MVC', @CompanyID, 'MVC', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('WebAPI', @CompanyID, 'WebAPI', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('AngularJS', @CompanyID, 'AngularJS', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('Java', @CompanyID, 'Java', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('Struts', @CompanyID, 'Struts', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('JBPM', @CompanyID, 'JBPM', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('OOP', @CompanyID, 'OOP', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('OOAD', @CompanyID, 'OOAD', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('Design Patterns', @CompanyID, 'Design Patterns', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('UML', @CompanyID, 'UML', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('Sharepoint', @CompanyID, 'Sharepoint', 1, 1)

INSERT INTO TechnologiesAndSkills(Code, CompanyID, Name, SkillType, Active)
VALUES('Negotiation', @CompanyID, 'Negotiation', 2, 1)

-- InterviewPanel
INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
VALUES('Niraj Sinha', @CompanyID, 'IT, BPO, Admin, FIN','OOAD, OOP, WCF, WebAPI, MVC', 1)

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
VALUES('Pradeep Kumar Gavuji', @CompanyID, 'IT','WCF, WebAPI, MVC', 1)

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
VALUES('Gnanasekhar Eatakuri', @CompanyID, 'IT','WebAPI, MVC', 1)

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
VALUES('Ramakrishna Bodi', @CompanyID, 'IT, BPO, Admin, FIN','OOAD, OOP, WCF, WebAPI, MVC', 2)

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
VALUES('Sreenivasa Rao Uyyurru', @CompanyID, 'IT','MVC', 2)

INSERT INTO InterviewPanel(Name, CompanyID, Departments, Technologies, Level)
VALUES('Naveen Krishnamsetti', @CompanyID, 'HR','Negotiation', 1)



