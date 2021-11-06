CREATE TABLE [dbo].[CurrencyTypes] (
    [CurrencyTypeId] INT          IDENTITY (10000, 1) NOT NULL,
    [Name]           VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_CurrencyTypes] PRIMARY KEY CLUSTERED ([CurrencyTypeId] ASC)
);


GO

