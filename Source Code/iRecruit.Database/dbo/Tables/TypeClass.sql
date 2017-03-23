CREATE TABLE [dbo].[TypeClass] (
    [TypeClassID]  INT          IDENTITY (1, 1) NOT NULL,
    [Code]         VARCHAR (10) NULL,
    [Name]         VARCHAR (30) NULL,
    [Description]  VARCHAR (50) NULL,
    [Active]       BIT          NULL,
    [ModifiedDate] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([TypeClassID] ASC)
);

