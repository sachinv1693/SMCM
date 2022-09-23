CREATE TABLE [dbo].[Vendors] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NULL,
    [Address]       VARCHAR (200) NULL,
    [ContactNumber] VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

