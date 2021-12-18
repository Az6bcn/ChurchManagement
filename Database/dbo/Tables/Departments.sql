CREATE TABLE [dbo].[Departments] (
    [DepartmentId] INT           IDENTITY (10000, 1) NOT NULL,
    [TenantId]     INT           NOT NULL,
    [Name]         VARCHAR (200) NOT NULL,
    [CreatedAt]    DATETIME      NOT NULL,
    [UpdatedAt]    DATETIME      NULL,
    [Deleted]      DATETIME      NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([DepartmentId] ASC),
    CONSTRAINT [CK_Departments_Name] CHECK ([Name] IS NOT NULL),
    CONSTRAINT [FK_Departments_TenantId_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenants] ([TenantId])
);


GO

