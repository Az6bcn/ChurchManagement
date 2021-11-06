CREATE TABLE [dbo].[FinanceTypes] (
    [FinanceTypeId] INT          IDENTITY (10000, 1) NOT NULL,
    [Name]          VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_FinanceTypes] PRIMARY KEY CLUSTERED ([FinanceTypeId] ASC)
);


GO

