CREATE TABLE [dbo].[Tenants] (
    [TenantId]       INT              IDENTITY (10000, 1) NOT NULL,
    [TenantGuidId]   UNIQUEIDENTIFIER CONSTRAINT [DF_Tenants_TenantGuidId] DEFAULT (newid()) NOT NULL,
    [Name]           VARCHAR (255)    NOT NULL,
    [LogoUrl]        VARCHAR (MAX)    NULL,
    [CurrencyId]     INT              NOT NULL,
    [TenantStatusId] INT              NOT NULL,
    [CreatedAt]      DATETIME         NOT NULL,
    [UpdatedAt]      DATETIME         CONSTRAINT [DF_Tenants_UpdatedAt] DEFAULT (NULL) NULL,
    [Deleted]        DATETIME         CONSTRAINT [DF_Tenants_Deleted] DEFAULT (NULL) NULL,
    CONSTRAINT [PK_Tenants_TenantId] PRIMARY KEY CLUSTERED ([TenantId] ASC),
    CONSTRAINT [CK_Tenant_Name] CHECK ([Name] IS NOT NULL),
    CONSTRAINT [UQ_Tenants_TenantGuidId] UNIQUE NONCLUSTERED ([TenantId] ASC),
    CONSTRAINT [UQ_Tenants_TenantGuidId_Name] UNIQUE NONCLUSTERED ([TenantGuidId] ASC, [Name] ASC)
);


GO

