
-- Tenants
/****** Object:  Table [dbo].[Tenants]    Script Date: 01/07/2021 18:00:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tenants](
	[TenantId] [int] IDENTITY(10000,1) NOT NULL,
	[TenantGuidId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[LogoUrl] [varchar](max) NULL,
	[CurrencyId] [int] NOT NULL,
	[TenantStatusId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_Tenants_TenantId] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Tenants_TenantGuidId] UNIQUE NONCLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Tenants_TenantGuidId_Name] UNIQUE NONCLUSTERED 
(
	[TenantGuidId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tenants] ADD  CONSTRAINT [DF_Tenants_TenantGuidId]  DEFAULT (newid()) FOR [TenantGuidId]
GO

ALTER TABLE [dbo].[Tenants] ADD  CONSTRAINT [DF_Tenants_UpdatedAt]  DEFAULT (NULL) FOR [UpdatedAt]
GO

ALTER TABLE [dbo].[Tenants] ADD  CONSTRAINT [DF_Tenants_Deleted]  DEFAULT (NULL) FOR [Deleted]
GO

ALTER TABLE [dbo].[Tenants]  WITH CHECK ADD  CONSTRAINT [FK_Tenants_TenantsStatusId_TenantStatus_TenantStatusId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[TenantStatuses] ([TenantStatusId])
GO

ALTER TABLE [dbo].[Tenants] CHECK CONSTRAINT [FK_Tenants_TenantsStatusId_TenantStatus_TenantStatusId]
GO

ALTER TABLE [dbo].[Tenants]  WITH CHECK ADD  CONSTRAINT [CK_Tenant_Name] CHECK  (([Name] IS NOT NULL))
GO

ALTER TABLE [dbo].[Tenants] CHECK CONSTRAINT [CK_Tenant_Name]
GO

-- TenantStatuses
/****** Object:  Table [dbo].[TenantStatuses]    Script Date: 01/07/2021 15:58:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TenantStatuses](
	[TenantStatusId] [int] IDENTITY(100,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TenantStatuses] PRIMARY KEY CLUSTERED 
(
	[TenantStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- NewComers
/****** Object:  Table [dbo].[NewComers]    Script Date: 01/07/2021 18:29:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewComers](
	[NewComerId] [int] IDENTITY(10000,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Surname] [varchar](200) NOT NULL,
	[DateMonthOfBirth] [varchar](50) NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[DateAttended] [datetime] NOT NULL,
	[ServiceTypeId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_NewComers_NewComerId] PRIMARY KEY CLUSTERED 
(
	[NewComerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[NewComers]  WITH CHECK ADD  CONSTRAINT [FK_NewComers_TenantId_Tenants_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO

ALTER TABLE [dbo].[NewComers] CHECK CONSTRAINT [FK_NewComers_TenantId_Tenants_TenantId]
GO

ALTER TABLE [dbo].[NewComers]  WITH CHECK ADD  CONSTRAINT [CK_NewComers_TenantId_Name_Surname] CHECK  (([TenantId]>(0) AND [Name] IS NOT NULL AND [Surname] IS NOT NULL AND [Gender] IS NOT NULL))
GO

ALTER TABLE [dbo].[NewComers] CHECK CONSTRAINT [CK_NewComers_TenantId_Name_Surname]
GO

-- Members
/****** Object:  Table [dbo].[Members]    Script Date: 01/07/2021 17:41:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Members](
                                [MemberId] [int] IDENTITY(10000,1) NOT NULL,
                                [TenantId] [int] NOT NULL,
                                [Name] [varchar](200) NOT NULL,
                                [Surname] [varchar](200) NOT NULL,
                                [DateMonthOfBirth] [varchar](50) NOT NULL,
                                [Gender] [varchar](10) NOT NULL,
                                [IsWorker] [bit] NOT NULL,
                                [PhoneNumber] [varchar](50) NOT NULL,
                                [CreatedAt] [datetime] NOT NULL,
                                [UpdatedAt] [datetime] NULL,
                                [Deleted] [datetime] NULL,
                                CONSTRAINT [PK_Members_MemberId] PRIMARY KEY CLUSTERED
                                    (
                                     [MemberId] ASC
                                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF_Members_IsWorker]  DEFAULT ((0)) FOR [IsWorker]
GO

ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_TenantId_Tenants_TenantId] FOREIGN KEY([TenantId])
    REFERENCES [dbo].[Tenants] ([TenantId])
GO

ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_TenantId_Tenants_TenantId]
GO

ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [CK_Members_TenantId_Name_Surname_DateMonthOfBirth] CHECK  (([TenantId]>(0) AND [Name] IS NOT NULL AND [Surname] IS NOT NULL AND [DateMonthOfBirth] IS NOT NULL))
GO

ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [CK_Members_TenantId_Name_Surname_DateMonthOfBirth]
GO

-- Ministers
/****** Object:  Table [dbo].[Ministers]    Script Date: 01/07/2021 20:29:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ministers](
	[MinisterId] [int] IDENTITY(10000,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[MinisterTitleId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_Ministers_MinisterId] PRIMARY KEY CLUSTERED 
(
	[MinisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Ministers]  WITH CHECK ADD  CONSTRAINT [FK_Ministers_MemberId_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([MemberId])
GO

ALTER TABLE [dbo].[Ministers] CHECK CONSTRAINT [FK_Ministers_MemberId_Members_MemberId]
GO

ALTER TABLE [dbo].[Ministers]  WITH CHECK ADD  CONSTRAINT [FK_Ministers_TenantId_Tenants_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO

ALTER TABLE [dbo].[Ministers] CHECK CONSTRAINT [FK_Ministers_TenantId_Tenants_TenantId]
GO

ALTER TABLE [dbo].[Ministers]  WITH CHECK ADD  CONSTRAINT [CK_Ministers_TenantId] CHECK  (([TenantId]>(0)))
GO

ALTER TABLE [dbo].[Ministers] CHECK CONSTRAINT [CK_Ministers_TenantId]
GO


-- Departments

/****** Object:  Table [dbo].[Departments]    Script Date: 01/07/2021 16:08:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(10000,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_TenantId_Tenants_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO

ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_TenantId_Tenants_TenantId]
GO

ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [CK_Departments_Name] CHECK  (([Name] IS NOT NULL))
GO

ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [CK_Departments_Name]
GO


--DepartmentMembers
/****** Object:  Table [dbo].[DepartmentMembers]    Script Date: 01/07/2021 17:43:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DepartmentMembers](
	[DepartmentId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[IsHeadOfDepartment] [bit] NOT NULL,
	[DateJoined] [datetime] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[DateLeft] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_DepartmentMembers] PRIMARY KEY NONCLUSTERED 
(
	[DepartmentId] ASC,
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DepartmentMembers_DepartmentId_MemberId] UNIQUE CLUSTERED 
(
	[MemberId] ASC,
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DepartmentMembers] ADD  CONSTRAINT [DF_DepartmentMembers_IsHeadOfDepartment]  DEFAULT ((0)) FOR [IsHeadOfDepartment]
GO

ALTER TABLE [dbo].[DepartmentMembers]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentMembers_DepartmentId_Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
GO

ALTER TABLE [dbo].[DepartmentMembers] CHECK CONSTRAINT [FK_DepartmentMembers_DepartmentId_Departments_DepartmentId]
GO

ALTER TABLE [dbo].[DepartmentMembers]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentMembers_MemberId_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([MemberId])
GO

ALTER TABLE [dbo].[DepartmentMembers] CHECK CONSTRAINT [FK_DepartmentMembers_MemberId_Members_MemberId]
GO

-- Finances
/****** Object:  Table [dbo].[Finances]    Script Date: 01/07/2021 23:06:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Finances](
	[FinanceId] [int] IDENTITY(10000,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[FinanceTypeId] [int] NOT NULL,
	[ServiceTypeId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[Amount] [decimal](19, 3) NOT NULL,
	[GivenDate] [datetime] NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_Finances_FinanceId] PRIMARY KEY CLUSTERED 
(
	[FinanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Finances]  WITH CHECK ADD  CONSTRAINT [FK_Finances_TenantId_Tenants_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO

ALTER TABLE [dbo].[Finances] CHECK CONSTRAINT [FK_Finances_TenantId_Tenants_TenantId]
GO

-- Attendances
/****** Object:  Table [dbo].[Attendances]    Script Date: 01/07/2021 23:26:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Attendances](
	[AttendanceId] [int] IDENTITY(10000,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[ServiceDate] [datetime] NOT NULL,
	[Male] [int] NOT NULL,
	[Female] [int] NOT NULL,
	[NewComers] [int] NOT NULL,
    [ServiceTypeId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[Deleted] [datetime] NULL,
 CONSTRAINT [PK_Attendances_AttendanceId] PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Attendances]  WITH CHECK ADD  CONSTRAINT [FK_Attendances_TenantId_Tenants_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO

ALTER TABLE [dbo].[Attendances] CHECK CONSTRAINT [FK_Attendances_TenantId_Tenants_TenantId]
GO
