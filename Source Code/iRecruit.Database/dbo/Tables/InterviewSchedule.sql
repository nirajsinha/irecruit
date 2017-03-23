CREATE TABLE [dbo].[InterviewSchedule] (
    [InterviewScheduleID]   INT           IDENTITY (1, 1) NOT NULL,
    [CandidateID]           INT           NULL,
    [InverviewRound]        INT           NULL,
    [ScheduledInterviewers] VARCHAR (200) NULL,
    [Subject]               VARCHAR (100) NULL,
    [Description]           VARCHAR (500) NULL,
    [StartTime]             DATETIME      NULL,
    [EndTime]               DATETIME      NULL,
    [AttachResume]          BIT           NULL,
    [Status]                INT           NULL,
    [CreatedBy]             VARCHAR (200) NULL,
    [CreatedDate]           DATETIME      NULL,
    [ModifiedBy]            VARCHAR (200) NULL,
    [ModifiedDate]          DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([InterviewScheduleID] ASC),
    FOREIGN KEY ([CandidateID]) REFERENCES [dbo].[Candidates] ([CandidateID])
);

