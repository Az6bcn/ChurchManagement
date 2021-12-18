CREATE TABLE [dbo].[Finances] (
    [FinanceId]     INT             IDENTITY (10000, 1) NOT NULL,
    [TenantId]      INT             NOT NULL,
    [FinanceTypeId] INT             NOT NULL,
    [ServiceTypeId] INT             NOT NULL,
    [CurrencyId]    INT             NOT NULL,
    [Amount]        DECIMAL (19, 3) NOT NULL,
    [GivenDate]     DATE            NOT NULL,
    [Description]   VARCHAR (MAX)   NULL,
    [CreatedAt]     DATETIME        NOT NULL,
    [UpdatedAt]     DATETIME        NULL,
    [Deleted]       DATETIME        NULL,
    CONSTRAINT [PK_Finances_FinanceId] PRIMARY KEY CLUSTERED ([FinanceId] ASC),
    CONSTRAINT [FK_Finances_TenantId_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenants] ([TenantId])
);


GO

