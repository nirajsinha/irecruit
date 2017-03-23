CREATE TABLE [dbo].[Users] (
    [UserID]         VARCHAR (100)  NULL,
    [CompanyID]      INT            NULL,
    [Name]           VARCHAR (250)  NULL,
    [Title]          VARCHAR (250)  NULL,
    [Email]          VARCHAR (250)  NULL,
    [Branches]       VARCHAR (250)  NULL,
    [AccessFeatures] VARCHAR (1000) NULL,
    [Photo]          VARCHAR (MAX)  NULL,
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);

