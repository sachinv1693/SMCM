CREATE TABLE [dbo].[Bills] (
    [Id]                    BIGINT      IDENTITY (1, 1) NOT NULL,
    [ConsumerEmailId]       NCHAR (100) NOT NULL,
    [Date]                  DATETIME    NOT NULL,
    [SmartMeterId]          BIGINT      NULL,
    [CurrentReadingUnit]    FLOAT (53)  DEFAULT ((0)) NULL,
    [PreviousReadingUnit]   FLOAT (53)  DEFAULT ((0)) NULL,
    [CurrentBillingAmount]  FLOAT (53)  DEFAULT ((0)) NULL,
    [PreviousBillingAmount] FLOAT (53)  DEFAULT ((0)) NULL,
    [CurrentBillingMonth]   NCHAR (4)   NULL,
    [PaymentStatus] NCHAR(30) NULL, 
    [PaymentType] NCHAR(50) NULL,
    [PaymentDate] NCHAR(50) NULL,
    [PaymentState] NCHAR(50) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_Bill_emailId] FOREIGN KEY ([ConsumerEmailId]) REFERENCES [dbo].[Users] ([EmailAddress])
);

