CREATE TABLE [dbo].[Consultancies] (
    [ConsultancyID]   INT           IDENTITY (1, 1) NOT NULL,
    [ConsultancyName] VARCHAR (100) NULL,
    [Address1]        VARCHAR (300) NULL,
    [Address2]        VARCHAR (300) NULL,
    [City]            VARCHAR (50)  NULL,
    [State]           VARCHAR (50)  NULL,
    [Country]         VARCHAR (100) NULL,
    [PostalCode]      VARCHAR (20)  NULL,
    [ContactPerson]   VARCHAR (100) NULL,
    [ContactNumber]   VARCHAR (20)  NULL,
    [Email]           VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([ConsultancyID] ASC)
);

