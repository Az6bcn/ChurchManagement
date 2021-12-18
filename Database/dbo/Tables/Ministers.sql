CREATE TABLE [dbo].[Ministers] (
    [MinisterId]      INT      IDENTITY (10000, 1) NOT NULL,
    [MemberId]        INT      NOT NULL,
    [MinisterTitleId] INT      NOT NULL,
    [TenantId]        INT      NOT NULL,
    [CreatedAt]       DATETIME NOT NULL,
    [UpdatedAt]       DATETIME NULL,
    [Deleted]         DATETIME NULL,
    CONSTRAINT [PK_Ministers_MinisterId] PRIMARY KEY CLUSTERED ([MinisterId] ASC),
    CONSTRAINT [CK_Ministers_TenantId] CHECK ([TenantId]>(0)),
    CONSTRAINT [FK_Ministers_MemberId_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([MemberId]),
    CONSTRAINT [FK_Ministers_TenantId_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenants] ([TenantId])
);


GO

