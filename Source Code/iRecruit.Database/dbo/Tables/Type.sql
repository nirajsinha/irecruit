CREATE TABLE [dbo].[Type] (
    [TypeID]       INT           IDENTITY (1, 1) NOT NULL,
    [TypeClassID]  INT           NULL,
    [Code]         VARCHAR (10)  NULL,
    [Name]         VARCHAR (50)  NULL,
    [Description]  VARCHAR (500) NULL,
    [Active]       BIT           NULL,
    [ModifiedDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([TypeID] ASC),
    FOREIGN KEY ([TypeClassID]) REFERENCES [dbo].[TypeClass] ([TypeClassID])
);

