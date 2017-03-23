CREATE TABLE [dbo].[DepartmentRoles] (
    [DepartmentRoleID] INT           IDENTITY (1, 1) NOT NULL,
    [DepartmentID]     INT           NULL,
    [BranchID]         INT           NULL,
    [FunctionHead]     VARCHAR (200) NULL,
    [SVP]              VARCHAR (200) NULL,
    [Active]           BIT           NULL,
    PRIMARY KEY CLUSTERED ([DepartmentRoleID] ASC),
    FOREIGN KEY ([BranchID]) REFERENCES [dbo].[Branches] ([BranchID]),
    FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Departments] ([DepartmentID])
);

