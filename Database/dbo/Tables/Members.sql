CREATE TABLE [dbo].[Members] (
    [MemberId]         INT           IDENTITY (10000, 1) NOT NULL,
    [TenantId]         INT           NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Surname]          VARCHAR (200) NOT NULL,
    [DateMonthOfBirth] VARCHAR (50)  NOT NULL,
    [IsWorker]         BIT           CONSTRAINT [DF_Members_IsWorker] DEFAULT ((0)) NOT NULL,
    [PhoneNumber]      VARCHAR (50)  NOT NULL,
    [CreatedAt]        DATETIME      NOT NULL,
    [UpdatedAt]        DATETIME      NULL,
    [Deleted]          DATETIME      NULL,
    [Gender]           VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_Members_MemberId] PRIMARY KEY CLUSTERED ([MemberId] ASC),
    CONSTRAINT [CK_Members_TenantId_Name_Surname_DateMonthOfBirth] CHECK ([TenantId]>(0) AND [Name] IS NOT NULL AND [Surname] IS NOT NULL AND [DateMonthOfBirth] IS NOT NULL),
    CONSTRAINT [FK_Members_TenantId_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[Tenants] ([TenantId])
);


GO

