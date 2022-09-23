CREATE TABLE [dbo].[Users] (
    [Id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [Role]              NCHAR (20)    NOT NULL,
    [EmailAddress]      NCHAR (100)   NOT NULL,
    [Password]          NCHAR (50)    NOT NULL,
    [FirstName]         NCHAR (50)    NOT NULL,
    [LastName]          NCHAR (50)    NOT NULL,
    [MobileNumber]      NCHAR (11)    NOT NULL,
    [LanguageSelected]  NCHAR (30)    NULL,
    [HasLoggedIn]       BIT           NULL,
    [CreatedAt]         DATETIME      NULL,
    [SessionTimerCount] INT           NULL,
    [SmartMeterId]      BIGINT        NULL,
    [LocationId]        VARCHAR (255) NOT NULL,
    [UserCategory]      NCHAR (20)    NULL,
    PRIMARY KEY CLUSTERED ([EmailAddress] ASC),
    CONSTRAINT [Fk_UserLocation] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[UserLocations] ([Id])
);

