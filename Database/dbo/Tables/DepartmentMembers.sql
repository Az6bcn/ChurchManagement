CREATE TABLE [dbo].[DepartmentMembers] (
    [DepartmentId]       INT      NOT NULL,
    [MemberId]           INT      NOT NULL,
    [IsHeadOfDepartment] BIT      CONSTRAINT [DF_DepartmentMembers_IsHeadOfDepartment] DEFAULT ((0)) NOT NULL,
    [DateJoined]         DATETIME NOT NULL,
    [CreatedAt]          DATETIME NOT NULL,
    [DateLeft]           DATETIME NULL,
    [UpdatedAt]          DATETIME NULL,
    [Deleted]            DATETIME NULL,
    CONSTRAINT [PK_DepartmentMembers] PRIMARY KEY NONCLUSTERED ([DepartmentId] ASC, [MemberId] ASC),
    CONSTRAINT [FK_DepartmentMembers_DepartmentId_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([DepartmentId]),
    CONSTRAINT [FK_DepartmentMembers_MemberId_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([MemberId]),
    CONSTRAINT [IX_DepartmentMembers_DepartmentId_MemberId] UNIQUE CLUSTERED ([MemberId] ASC, [DepartmentId] ASC)
);


GO

