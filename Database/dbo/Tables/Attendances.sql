CREATE TABLE [dbo].[Attendances] (
    [AttendanceId]  INT      IDENTITY (10000, 1) NOT NULL,
    [TenantId]      INT      NOT NULL,
    [ServiceDate]   DATE     NOT NULL,
    [Male]          INT      NOT NULL,
    [Female]        INT      NOT NULL,
    [NewComers]     INT      NOT NULL,
    [CreatedAt]     DATETIME NOT NULL,
    [UpdatedAt]     DATETIME NULL,
    [Deleted]       DATETIME NULL,
    [ServiceTypeId] INT      NOT NULL,
    [Children]      INT      NOT NULL,
    CONSTRAINT [PK_Attendances_AttendanceId] PRIMARY KEY CLUSTERED ([AttendanceId] ASC),
    CONSTRAINT [FK_Attendances_TenantId_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenants] ([TenantId])
);


GO

