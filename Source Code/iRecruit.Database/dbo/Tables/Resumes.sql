CREATE TABLE [dbo].[Resumes] (
    [ResumeID]       INT           IDENTITY (1, 1) NOT NULL,
    [CandidateID]    INT           NULL,
    [ResumePath]     VARCHAR (200) NULL,
    [FileType]       VARCHAR (10)  NULL,
    [CandidatePhoto] VARBINARY (1) NULL,
    PRIMARY KEY CLUSTERED ([ResumeID] ASC),
    FOREIGN KEY ([CandidateID]) REFERENCES [dbo].[Candidates] ([CandidateID])
);

