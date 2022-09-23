CREATE TABLE [dbo].[SmartMeters] (
    [Id]                  BIGINT     IDENTITY (1, 1) NOT NULL,
    [Status]              NCHAR (20) NULL,
    [CurrentMonthReading] FLOAT (53) NULL,
    [VendorId]            INT        NULL,
    [PurchaseDate]        DATETIME   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_VendorID] FOREIGN KEY ([VendorId]) REFERENCES [dbo].[Vendors] ([Id])
);

