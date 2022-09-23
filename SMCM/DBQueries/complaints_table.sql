CREATE TABLE [dbo].[Complaints] (
    [ConsumerEmailId] NCHAR (100)   NOT NULL,
    [Id]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [Date]            DATETIME  NOT NULL,
    [Type]            VARCHAR (50)  NULL,
    [Status]          VARCHAR (20)  NULL,
    [Message]         VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_Complaints_emailId] FOREIGN KEY ([ConsumerEmailId]) REFERENCES [dbo].[Users] ([EmailAddress])
);

