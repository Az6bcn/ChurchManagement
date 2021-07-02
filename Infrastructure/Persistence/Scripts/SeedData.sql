
--- Add Tenant Statuses-------------
PRINT('Adding Tenant Statuses')

INSERT INTO [ChurchManagement].[dbo].[TenantStatus]
VALUES
    ('Active'),
    ('On Hold'),
    ('Suspended'),
    ('Pending'),
    ('Cancelled')
GO

--- Add Tenant-------------
PRINT('Adding Demo Tenant')
DECLARE @Currency INT = (SELECT CurrencyId
FROM [ChurchManagement].[dbo].[Currencies]
WHERE Name = 'British Pounds')
DECLARE @CurrencyNaira INT = (SELECT CurrencyId
FROM [ChurchManagement].[dbo].[Currencies]
WHERE Name = 'Naira');
DECLARE @CurrencyDollars INT = (SELECT CurrencyId
FROM [ChurchManagement].[dbo].[Currencies]
WHERE Name = 'US Dollars');
DECLARE @ActiveTenantStatusId INT = (SELECT TenantStatusId
FROM [ChurchManagement].[dbo].[TenantStatus]
WHERE Name = 'Active');
DECLARE @OnHoldTenantStatusId INT = (SELECT TenantStatusId
FROM [ChurchManagement].[dbo].[TenantStatus]
WHERE Name = 'On Hold');

INSERT INTO [ChurchManagement].[dbo].[Tenants]
    (
    [Name],
    [LogoUrl],
    [CurrencyId],
    [TenantStatusId],
    [CreatedAt]
    )
VALUES
    (
        'Demo Tenant',
        'https://source.unsplash.com/random',
        @Currency,
        @ActiveTenantStatusId,
        GETDATE()
),
    (
        'Our Church',
        'https://source.unsplash.com/random',
        @CurrencyNaira,
        @OnHoldTenantStatusId,
        GETDATE()
),
    (
        'Open Church Ministry Of God For Redeemed Souls',
        'https://source.unsplash.com/random',
        @CurrencyDollars,
        @ActiveTenantStatusId,
        GETDATE()
);

GO

--- Add Departments-------------
PRINT('Adding Demo Tenant Departments')

INSERT INTO [ChurchManagement].[dbo].[Departments]
    (
    Name,
    CreatedAt
    )
VALUES
    ('Ushering', GETDATE()),
    ('Technical', GETDATE()),
    ('Minstering', GETDATE()),
    ('Choir', GETDATE()),
    ('Prayer Warrior', GETDATE()),
    ('Welfare', GETDATE()),
    ('Sunday School', GETDATE()),
    ('Security', GETDATE()),
    ('Santuary Keepers', GETDATE()),
    ('Treasury', GETDATE()),
    ('Hospitality', GETDATE()),
    ('Multimedia', GETDATE()),
    ('Choir', GETDATE()),
    ('Envagelisim', GETDATE()),
    ('Sanitation', GETDATE()),
    ('Children', GETDATE()),
    ('Drama', GETDATE()),
    ('Visitation/Follow Up', GETDATE()),
    ('Protocaol', GETDATE()),
    ('Editorial', GETDATE()),
    ('Counselling', GETDATE()),
    ('Youth', GETDATE()),
    ('Teens', GETDATE()),
    ('Singles', GETDATE()),
    ('Workers in training', GETDATE()),
    ('Elders', GETDATE())

  GO

--- Add Members-------------
PRINT('Adding Demo Tenant Members')
INSERT INTO [ChurchManagement].[dbo].[Members]
    (
    TenantId,
    Name,
    Surname,
    DateAndMonthOfBirth,
    Gender,
    IsWorker,
    PhoneNumber,
    CreatedAt
    )
VALUES
    -- tenant 1
    (1, 'Azeez', 'Odumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Timothy', 'Samuel', '20 Apr', 'Male', 1, 0774333361, GETDATE()),
    (1, 'Ogawa', 'Chuckwu', '1 Jan', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Bunmi', 'Borokini', '4 May', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azdfdfdeez', 'Odumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azfdfeez', 'Odumosu', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'tujty', 'Odumosu', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'myuuy', 'Odumosu', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'Azcxceez', 'Odumosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azeez', 'Odumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azxcxeez', 'Odumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azeez', 'Odumosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azeez', 'Odumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'gff', 'Odumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'cvcvcvcv', 'Odumosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'vcvyj', 'Odyyjyumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, '35rfge', 'Odumhgosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azeez', 'Odumjhjosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azexez', 'ggt', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'yjyj', 'Odumffosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'gbgn', 'ddv', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'r3r', 'vdvd', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'dw', 'dvd', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'w', 'vdvd', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'wdw', 'sds', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azeez', 'vc', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azxcxeez', 'frfe', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'dvd', 'hyjhyoiutgefsu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azcxceez', 'iol8', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azxceez', 'tht', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'vdv', 'ththtgeg', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'dvd', 'hjjurgr', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'vd', 'efhttht', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'xvx', 'rthyhj', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, '6y667Odu', 'mosu', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'xcxc', 'rgyfefe', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'frrg', 'ef5tyt', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'rgh', 'rgrhtht', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azgrgrgeez', 'ythjghty', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'kjuhng', 'fgff', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    -- tenant 2 'Male',
    (1, 'Aze77ez', 'kj78k8t', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'dsds', 'dfferfe', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'efervrgr', 'gthth', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'cxcxc', 'hjhjh', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'dwd', 'grgr', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'Azervez', 'uykukur', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azeez', 'cxcxcx', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'Azfgeez', 'yjyj', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Az5efsez', 'hththtyjy', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azhfgheez', 'Odumcxgrgecxcosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azeekiz', 'cxxcx', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'fefeefe', 'ukhtt', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azjmjmjmeez', 'Odumcxcxosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'vcxdfet', 'uikiukiuk', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'ytrhth', 'sjsdfs', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'erfere', 'Odfefefefumosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azyjyjyjeez', 'gtrtef', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'dfdff', 'Odugvmocxcxcsu', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'rrtrt', 'erer', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azeez', 'yjOdhgsu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azrtyteez', 'cxc', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azergrrez', 'Odumfosu', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'Azrtreez', 'Odumo57y56su', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'Azenez', 'ngn', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azssxscxseez', 'Odumliklisu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azergrgnnz', 'Odumorgdfgsu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azeegfgtz', 'Oduv mosu', '16 Mar', 'Male', 0, 07703768361, GETDATE()),
    (1, 'Azetezy', 'Odumiuiuosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azbyneez', 'Oduvvmosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azfgfeez', 'Oduuiyimosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azrgeez', 'Odvgumosu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Azgbeez', 'Oduuiumosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Afgfzeez', 'Odumoshwewjhu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'rAvdvdzeez', 'Odumereosu', '16 Mar', 'Female', 0, 07703768361, GETDATE()),
    (1, 'Aze6u6ez', 'Odumoo;osu', '16 Mar', 'Male', 1, 07703768361, GETDATE()),
    (1, 'Addddvvzeez', 'Odrty5umosu', '16 Mar', 'Female', 1, 07703768361, GETDATE()),
    (1, 'Azvdeez', 'Odumgfg6osu', '16 Mar', 'Male', 1, 07703768361, GETDATE())

    GO

--- Add Finance-------------
PRINT('Adding Demo Tenant Finance')
DECLARE @Currency INT = (SELECT CurrencyId
FROM [ChurchManagement].[dbo].[Currencies]
WHERE Name = 'British Pounds')
DECLARE @ThanksgivingFianceTypeId INT = (SELECT FinanceTypeId
FROM [ChurchManagement].[dbo].[FinanceTypes]
WHERE Name = 'Thanksgiving')
DECLARE @ThanksgivingServiceTypeId INT = (SELECT ServiceTypeId
FROM [ChurchManagement].[dbo].[ServiceTypes]
WHERE Name = 'Thanksgiving')
DECLARE @OfferingFianceTypeId INT = (SELECT FinanceTypeId
FROM [ChurchManagement].[dbo].[FinanceTypes]
WHERE Name = 'Offering')
DECLARE @TitheFinanceTypeId INT = (SELECT FinanceTypeId
FROM [ChurchManagement].[dbo].[FinanceTypes]
WHERE Name = 'Tithe')
DECLARE @SundayServiceTypeId INT = (SELECT ServiceTypeId
FROM [ChurchManagement].[dbo].[ServiceTypes]
WHERE Name = 'Sunday Service')
DECLARE @DemoTenantId INT = (SELECT TenantId
FROM [ChurchManagement].[dbo].[Tenants]
WHERE Name = 'Demo tenant')

INSERT INTO [ChurchManagement].[dbo].[Finances]
    (
    TenantId,
    FinanceTypeId,
    ServiceTypeId,
    CurrencyId,
    Amount,
    ServiceDate,
    Description,
    CreatedAt
    )
VALUES
    -- tenant 1
    -- Thanksgiving Offering
    (@DemoTenantId, @ThanksgivingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '500.00'), CONVERT(DATETIME, '01/01/2021', 103), 'First thanksgiving offering of the year', CONVERT(DATETIME, '01/01/2021', 103)),
    (@DemoTenantId, @ThanksgivingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '150.00'), CONVERT(DATETIME, '07/02/2021', 103), 'Thanksgiving offering Febuary', CONVERT(DATETIME, '07/02/2021', 103)),
    (@DemoTenantId, @ThanksgivingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '350.00'), CONVERT(DATETIME, '07/03/2021', 103), 'Thanksgiving offering March', CONVERT(DATETIME, '07/03/2021', 103)),
    (@DemoTenantId, @ThanksgivingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '95.00'),  CONVERT(DATETIME, '04/04/2021', 103), 'Thanksgiving offering April', CONVERT(DATETIME, '04/04/2021', 103)),
    (@DemoTenantId, @ThanksgivingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '209.00'), CONVERT(DATETIME, '02/05/2021', 103), 'Thanksgiving offering May', CONVERT(DATETIME, '03/05/2021', 103)),
    -- Offering (Thanksgiving Sunday)
    (@DemoTenantId, @OfferingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '180.05'), CONVERT(DATETIME, '01/01/2021', 103), 'First offering of the year', CONVERT(DATETIME, '01/01/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '93.70'),  CONVERT(DATETIME, '07/02/2021', 103), 'First offering of the year', CONVERT(DATETIME, '07/02/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '78.99'),  CONVERT(DATETIME, '07/03/2021', 103), 'First offering of the year', CONVERT(DATETIME, '07/03/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '58.89'),  CONVERT(DATETIME, '04/04/2021', 103), 'First offering of the year', CONVERT(DATETIME, '04/04/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @ThanksgivingServiceTypeId, @Currency, CONVERT(DECIMAL, '101.05'), CONVERT(DATETIME, '02/05/2021', 103), 'First offering of the year', CONVERT(DATETIME, '03/05/2021', 103)),

    --Offering Sunday Services
    -- Jan
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '85.00'), CONVERT(DATETIME, '01/01/2021', 103), 'Offering', CONVERT(DATETIME, '01/01/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '113.09'), CONVERT(DATETIME, '10/01/2021', 103), 'Offering', CONVERT(DATETIME, '10/01/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '67.78'), CONVERT(DATETIME, '17/01/2021', 103), 'Offering', CONVERT(DATETIME, '17/01/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '79.14'), CONVERT(DATETIME, '24/01/2021', 103), 'Offering', CONVERT(DATETIME, '24/01/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '43.78'), CONVERT(DATETIME, '31/01/2021', 103), 'Offering', CONVERT(DATETIME, '31/01/2021', 103)),
    --Feb
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '89.00'), CONVERT(DATETIME, '07/02/2021', 103), 'Offering', CONVERT(DATETIME, '07/02/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '123.00'),CONVERT(DATETIME, '14/02/2021', 103), 'Offering', CONVERT(DATETIME, '14/02/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '78.00'), CONVERT(DATETIME, '21/02/2021', 103), 'Offering', CONVERT(DATETIME, '21/02/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '34.00'), CONVERT(DATETIME, '28/02/2021', 103), 'Offering', CONVERT(DATETIME, '28/02/2021', 103)),
    --Mar
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '78.34'), CONVERT(DATETIME, '07/03/2021', 103), 'Offering', CONVERT(DATETIME, '07/03/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '23.99'), CONVERT(DATETIME, '14/03/2021', 103), 'Offering', CONVERT(DATETIME, '14/03/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '104.96'),CONVERT(DATETIME, '21/03/2021', 103), 'Offering', CONVERT(DATETIME, '21/03/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '116.98'),CONVERT(DATETIME, '28/03/2021', 103), 'Offering', CONVERT(DATETIME, '28/03/2021', 103)),
    --Apr
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '87.00'), CONVERT(DATETIME, '04/04/2021', 103), 'Offering', CONVERT(DATETIME, '04/04/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '190.00'),CONVERT(DATETIME, '11/04/2021', 103), 'Offering', CONVERT(DATETIME, '11/04/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '37.99'), CONVERT(DATETIME, '18/04/2021', 103), 'Offering', CONVERT(DATETIME, '18/04/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '67.70'), CONVERT(DATETIME, '25/04/2021', 103), 'Offering', CONVERT(DATETIME, '25/04/2021', 103)),
    --May
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '67.00'), CONVERT(DATETIME, '02/05/2021', 103), 'Offering', CONVERT(DATETIME, '02/05/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '89.00'), CONVERT(DATETIME, '09/05/2021', 103), 'Offering', CONVERT(DATETIME, '09/05/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '450.00'),CONVERT(DATETIME, '16/05/2021', 103), 'Offering', CONVERT(DATETIME, '16/05/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '34.08'), CONVERT(DATETIME, '23/05/2021', 103), 'Offering', CONVERT(DATETIME, '23/05/2021', 103)),
    (@DemoTenantId, @OfferingFianceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '315.99'),CONVERT(DATETIME, '30/05/2021', 103), 'Offering', CONVERT(DATETIME, '30/05/2021', 103)),

    -- Tithe 
    --Jan
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '200.00'), CONVERT(DATETIME, '01/01/2021', 103), 'First tithe offering of the year', CONVERT(DATETIME, '01/01/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '400.00'), CONVERT(DATETIME, '01/01/2021', 103), 'Tithe', CONVERT(DATETIME, '01/01/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '700.00'), CONVERT(DATETIME, '10/01/2021', 103), 'Tithe', CONVERT(DATETIME, '10/01/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '600.00'), CONVERT(DATETIME, '17/01/2021', 103), 'Tithe', CONVERT(DATETIME, '17/01/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '95.00'),  CONVERT(DATETIME, '24/01/2021', 103), 'Tithe', CONVERT(DATETIME, '24/01/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '209.00'), CONVERT(DATETIME, '31/01/2021', 103), 'Tithe', CONVERT(DATETIME, '31/01/2021', 103)),
    --Feb
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '300.00'), CONVERT(DATETIME, '07/02/2021', 103), 'Tithe', CONVERT(DATETIME, '07/02/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '190.00'), CONVERT(DATETIME, '14/02/2021', 103), 'Tithe', CONVERT(DATETIME, '14/02/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '350.00'), CONVERT(DATETIME, '21/02/2021', 103), 'Tithe', CONVERT(DATETIME, '21/02/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '50.00'),  CONVERT(DATETIME, '28/02/2021', 103),  'Tithe',CONVERT(DATETIME, '28/02/2021', 103)),
    --Mar
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '500.00'), CONVERT(DATETIME, '07/03/2021', 103), 'Tithe', CONVERT(DATETIME, '07/03/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '150.00'), CONVERT(DATETIME, '14/03/2021', 103), 'Tithe', CONVERT(DATETIME, '14/03/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '350.00'), CONVERT(DATETIME, '21/03/2021', 103), 'Tithe', CONVERT(DATETIME, '21/03/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '350.00'), CONVERT(DATETIME, '28/03/2021', 103),  'Tithe',CONVERT(DATETIME, '28/03/2021', 103)),
    --Apr
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '380.00'), CONVERT(DATETIME, '04/04/2021', 103), 'Tithe', CONVERT(DATETIME, '04/04/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '150.00'), CONVERT(DATETIME, '11/04/2021', 103), 'Tithe', CONVERT(DATETIME, '11/04/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '450.00'), CONVERT(DATETIME, '18/04/2021', 103), 'Tithe', CONVERT(DATETIME, '18/04/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '670.00'), CONVERT(DATETIME, '25/04/2021', 103),  'Tithe',CONVERT(DATETIME, '25/04/2021', 103)),
    --May
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '230.00'), CONVERT(DATETIME, '02/05/2021', 103), 'Tithe', CONVERT(DATETIME, '02/05/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '150.00'), CONVERT(DATETIME, '09/05/2021', 103), 'Tithe', CONVERT(DATETIME, '09/05/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '350.00'), CONVERT(DATETIME, '16/05/2021', 103), 'Tithe', CONVERT(DATETIME, '16/05/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '900.00'), CONVERT(DATETIME, '23/05/2021', 103),  'Tithe',CONVERT(DATETIME, '23/05/2021', 103)),
    (@DemoTenantId, @TitheFinanceTypeId, @SundayServiceTypeId, @Currency, CONVERT(DECIMAL, '209.79'), CONVERT(DATETIME, '30/05/2021', 103), 'Tithe', CONVERT(DATETIME, '30/05/2021', 103))
   
   GO

   --- Add Ministers-------------
DECLARE @DeaconId INT = (SELECT MinisterTitleId
FROM [ChurchManagement].[dbo].[MinisterTitle]
WHERE Name = 'Deacon')
DECLARE @DeaconessId INT = (SELECT MinisterTitleId
FROM [ChurchManagement].[dbo].[MinisterTitle]
WHERE Name = 'Deaconess')
DECLARE @PastorId INT = (SELECT MinisterTitleId
FROM [ChurchManagement].[dbo].[MinisterTitle]
WHERE Name = 'Pastor')
DECLARE @AssistantPastorId INT = (SELECT MinisterTitleId
FROM [ChurchManagement].[dbo].[MinisterTitle]
WHERE Name = 'Pastor')

PRINT('Adding Demo Tenant Ministers')
INSERT INTO [ChurchManagement].[dbo].[Ministers]
    (
       [MemberId]
      ,[MinisterTitleId]
      ,[TenantId]
      ,[CreatedAt]
    )
VALUES
    -- tenant 1
    (2, @PastorId, 1, GETDATE()),
    (1, @DeaconId, 1, GETDATE()),
    (4, @DeaconessId, 1, GETDATE()),
    (112, @AssistantPastorId, 1, GETDATE())

    GO


   --- Add New Comers-------------
DECLARE @SundayServiceTypeId INT = (SELECT ServiceTypeId
FROM [ChurchManagement].[dbo].[ServiceTypes]
WHERE Name = 'Sunday Service')

DECLARE @ThanksgivingServiceTypeId INT = (SELECT ServiceTypeId
FROM [ChurchManagement].[dbo].[ServiceTypes]
WHERE Name = 'Thanksgiving')

PRINT('Adding Demo Tenant Ministers')
INSERT INTO [ChurchManagement].[dbo].[NewComers]
    (
       [TenantId]
      ,[Name]
      ,[Surname]
      ,[DateAndMonthOfBirth]
      ,[Gender]
      ,[PhoneNumber]
      ,[DateAttended]
      ,[ServiceTypeId]
      ,[CreatedAt]
    )
VALUES
    -- tenant 1
   (1, 'Olukemi', 'Sorogun', CONVERT(DATETIME, '01/01/1978', 103), 'Female', '07789097689', CONVERT(DATETIME, '30/05/2021', 103), @ThanksgivingServiceTypeId, GETDATE()),
   (1, 'Abidemi', 'Rogers', CONVERT(DATETIME, '01/01/1978', 103), 'Female', '07789097689', CONVERT(DATETIME, '30/05/2021', 103), @ThanksgivingServiceTypeId, GETDATE()),
   (1, 'Jack', 'Middleton', CONVERT(DATETIME, '01/01/1978', 103), 'Female', '07789097689', CONVERT(DATETIME, '30/05/2021', 103), @ThanksgivingServiceTypeId, GETDATE()),
   (1, 'Ramon', 'Jimennez', CONVERT(DATETIME, '01/01/1978', 103), 'Female', '07789097689', CONVERT(DATETIME, '30/05/2021', 103), @ThanksgivingServiceTypeId, GETDATE()),
   (1, 'Sergio', 'Alubinmo', CONVERT(DATETIME, '01/01/1978', 103), 'Female', '07789097689', CONVERT(DATETIME, '30/05/2021', 103), @ThanksgivingServiceTypeId, GETDATE())

    GO

--- Add Attendance-------------
INSERT INTO [ChurchManagement].[dbo].[Attendance]
(
    TenantId,
    ServiceDate,
    Male,
    Female,
    Children,
    NewComers,
    CreatedAt
)
VALUES
(1, CONVERT(DATETIME, '01/01/2021', 103), 80, 97, 50, 13, GETDATE()),
(1, CONVERT(DATETIME, '10/01/2021', 103), 56, 67, 23, 13, GETDATE()),
(1, CONVERT(DATETIME, '17/01/2021', 103), 18, 20, 20, 13, GETDATE()),
(1, CONVERT(DATETIME, '24/01/2021', 103), 29, 23, 15, 13, GETDATE()),
(1, CONVERT(DATETIME, '31/01/2021', 103), 13, 34, 35, 13, GETDATE()),
(1, CONVERT(DATETIME, '07/02/2021', 103), 68, 56, 78, 13, GETDATE()),
(1, CONVERT(DATETIME, '14/02/2021', 103), 25, 45, 50, 13, GETDATE()),
(1, CONVERT(DATETIME, '21/02/2021', 103), 36, 17, 23, 13, GETDATE()),
(1, CONVERT(DATETIME, '28/02/2021', 103), 23, 12, 54, 13, GETDATE()),
(1, CONVERT(DATETIME, '07/03/2021', 103), 10, 15, 43, 13, GETDATE()),
(1, CONVERT(DATETIME, '14/03/2021', 103), 43, 25, 28, 13, GETDATE()),
(1, CONVERT(DATETIME, '21/03/2021', 103), 78, 16, 30, 13, GETDATE()),
(1, CONVERT(DATETIME, '28/03/2021', 103), 28, 19, 27, 13, GETDATE()),
(1, CONVERT(DATETIME, '04/04/2021', 103), 32, 37, 21, 13, GETDATE()),
(1, CONVERT(DATETIME, '11/04/2021', 103), 50, 67, 30, 13, GETDATE()),
(1, CONVERT(DATETIME, '18/04/2021', 103), 31, 13, 50, 13, GETDATE()),
(1, CONVERT(DATETIME, '25/04/2021', 103), 29, 45, 34, 13, GETDATE()),
(1, CONVERT(DATETIME, '02/05/2021', 103), 8,  17, 23, 0, GETDATE()),
(1, CONVERT(DATETIME, '09/05/2021', 103), 45, 58, 20, 0, GETDATE()),
(1, CONVERT(DATETIME, '16/05/2021', 103), 23, 60, 18, 0, GETDATE()),
(1, CONVERT(DATETIME, '23/05/2021', 103), 51, 10, 19, 0, GETDATE()),
(1, CONVERT(DATETIME, '30/05/2021', 103), 17, 45, 20, 0, GETDATE())

GO
