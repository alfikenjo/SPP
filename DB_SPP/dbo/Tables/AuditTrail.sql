CREATE TABLE [dbo].[AuditTrail] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Username]    VARCHAR (50)     NULL,
    [Menu]        VARCHAR (MAX)    NULL,
    [Halaman]     VARCHAR (MAX)    NULL,
    [Item]        VARCHAR (MAX)    NULL,
    [Action]      VARCHAR (200)    NULL,
    [Description] VARCHAR (MAX)    NULL,
    [Datetime]    DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

