-- disable referential integrity
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' 
GO 

EXEC sp_MSForEachTable 'DELETE FROM ?' 
GO 

-- enable referential integrity again 
EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL' 
GO

-- reset identity seeds
EXEC sp_MSForEachTable 'DBCC CHECKIDENT(''?'', RESEED, 10000)'