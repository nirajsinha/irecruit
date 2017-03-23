CREATE TABLE [dbo].[ActivityLog] (
    [ActivityLogID] INT           IDENTITY (1, 1) NOT NULL,
    [IndentID]      INT           NULL,
    [UserID]        VARCHAR (100) NULL,
    [LogTypeID]     INT           NULL,
    [Header]        VARCHAR (500) NULL,
    [Description]   VARCHAR (MAX) NULL,
    [Comments]      VARCHAR (MAX) NULL,
    [RecordDate]    DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ActivityLogID] ASC),
    FOREIGN KEY ([IndentID]) REFERENCES [dbo].[Indent] ([IndentID])
);

