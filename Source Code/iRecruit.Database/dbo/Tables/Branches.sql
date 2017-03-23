CREATE TABLE [dbo].[Branches] (
    [BranchID]   INT           IDENTITY (1, 1) NOT NULL,
    [CompanyID]  INT           NULL,
    [Code]       VARCHAR (100) NULL,
    [Name]       VARCHAR (100) NULL,
    [Address]    VARCHAR (250) NULL,
    [City]       VARCHAR (100) NULL,
    [State]      VARCHAR (75)  NULL,
    [Country]    VARCHAR (100) NULL,
    [PostalCode] VARCHAR (10)  NULL,
    [Email]      VARCHAR (100) NULL,
    [Phone]      VARCHAR (10)  NULL,
    [Fax]        VARCHAR (10)  NULL,
    [URL]        VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([BranchID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);

