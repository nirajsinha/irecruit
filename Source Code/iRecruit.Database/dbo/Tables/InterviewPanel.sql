CREATE TABLE [dbo].[InterviewPanel] (
    [InterviewPanelID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (250) NULL,
    [CompanyID]        INT           NULL,
    [Departments]      VARCHAR (200) NULL,
    [Technologies]     VARCHAR (200) NULL,
    [Level]            VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([InterviewPanelID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);

