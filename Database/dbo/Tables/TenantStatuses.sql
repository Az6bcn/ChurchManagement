CREATE TABLE [dbo].[TenantStatuses] (
    [TenantStatusId] INT           IDENTITY (100, 1) NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TenantStatuses] PRIMARY KEY CLUSTERED ([TenantStatusId] ASC)
);


GO

