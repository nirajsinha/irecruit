CREATE TABLE [dbo].[EmailNotifications] (
    [EmailNotificationID] INT            IDENTITY (1, 1) NOT NULL,
    [EmailFrom]           VARCHAR (100)  NULL,
    [EmailTo]             VARCHAR (100)  NULL,
    [EmailCc]             VARCHAR (100)  NULL,
    [Subject]             VARCHAR (100)  NULL,
    [BodyHtml]            VARCHAR (2000) NULL,
    [Status]              INT            NULL,
    [RecordDate]          DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([EmailNotificationID] ASC)
);

