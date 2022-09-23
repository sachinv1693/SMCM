CREATE TABLE [dbo].[UserLocations] (
    [AreaCode]      BIGINT        DEFAULT ((100)) NOT NULL,
    [Street]        NCHAR (200)   NOT NULL,
    [City]          NCHAR (80)    NOT NULL,
    [State]         NCHAR (80)    NOT NULL,
    [Pincode]       NCHAR (20)    NOT NULL,
    [ApartmentName] NCHAR (50)    NOT NULL,
    [BlockNumber]   NCHAR (20)    NOT NULL,
    [Id]            VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

