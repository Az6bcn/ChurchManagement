--- Add Tenant-------------
PRINT('Adding Demo Tenant')
DECLARE @Currency INT = (SELECT CurrencyId FROM Currencies WHERE Name = 'British Pounds')
INSERT INTO Tenants
(
    [Name],
    [LogoUrl],
    [CurrencyId],
    [IsActive],
    [CreatedAt]
)
VALUES
(
    'Demo Tenant',
    'https://source.unsplash.com/random',
    @Currency,
    1,
    GETDATE()
);

--- Add Members-------------
PRINT('Adding Demo Tenant Members')