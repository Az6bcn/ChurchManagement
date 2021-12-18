CREATE TABLE [dbo].[NewComers] (
    [NewComerId]       INT           IDENTITY (10000, 1) NOT NULL,
    [TenantId]         INT           NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Surname]          VARCHAR (200) NOT NULL,
    [DateMonthOfBirth] VARCHAR (50)  NOT NULL,
    [Gender]           VARCHAR (10)  NOT NULL,
    [PhoneNumber]      VARCHAR (50)  NOT NULL,
    [DateAttended]     DATETIME      NOT NULL,
    [ServiceTypeId]    INT           NOT NULL,
    [CreatedAt]        DATETIME      NOT NULL,
    [UpdatedAt]        DATETIME      NULL,
    [Deleted]          DATETIME      NULL,
    CONSTRAINT [PK_NewComers_NewComerId] PRIMARY KEY CLUSTERED ([NewComerId] ASC),
    CONSTRAINT [CK_NewComers_TenantId_Name_Surname] CHECK ([TenantId]>(0) AND [Name] IS NOT NULL AND [Surname] IS NOT NULL AND [Gender] IS NOT NULL),
    CONSTRAINT [FK_NewComers_TenantId_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenants] ([TenantId])
);


GO

