CREATE TABLE [dbo].[Departments] (
    [DepartmentID] INT           IDENTITY (1, 1) NOT NULL,
    [CompanyID]    INT           NULL,
    [Code]         VARCHAR (50)  NULL,
    [Name]         VARCHAR (200) NULL,
    [Active]       BIT           NULL,
    [ModifiedDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([DepartmentID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);

