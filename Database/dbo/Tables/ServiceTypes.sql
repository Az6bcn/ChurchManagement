CREATE TABLE [dbo].[ServiceTypes] (
    [ServiceTypeId] INT          IDENTITY (10000, 1) NOT NULL,
    [Name]          VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ServiceTypes] PRIMARY KEY CLUSTERED ([ServiceTypeId] ASC)
);


GO

