CREATE TABLE [dbo].[Features] (
    [FeatureID]   INT           IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (50)  NULL,
    [CompanyID]   INT           NULL,
    [Description] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([FeatureID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID])
);

