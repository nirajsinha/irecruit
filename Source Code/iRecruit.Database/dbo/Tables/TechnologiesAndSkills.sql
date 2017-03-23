CREATE TABLE [dbo].[TechnologiesAndSkills] (
    [TechnologyAndSkillID] INT           IDENTITY (1, 1) NOT NULL,
    [Code]                 VARCHAR (50)  NULL,
    [CompanyID]            INT           NULL,
    [Name]                 VARCHAR (250) NULL,
    [SkillType]            INT           DEFAULT ((1)) NULL,
    [Active]               BIT           NULL,
    PRIMARY KEY CLUSTERED ([TechnologyAndSkillID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);

