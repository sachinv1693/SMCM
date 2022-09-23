CREATE TABLE [dbo].[UserRequests] (
    [UserEmailId] NCHAR (100) NOT NULL,
    [Id]          BIGINT      IDENTITY (1, 1) NOT NULL,
    [Date]        DATETIME    NOT NULL,
    [Type]        NCHAR (50)  NULL,
    [Status]      NCHAR (20)  NULL,
    CONSTRAINT [PK_UserRequests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_UserRequest] FOREIGN KEY ([UserEmailId]) REFERENCES [dbo].[Users] ([EmailAddress])
);

