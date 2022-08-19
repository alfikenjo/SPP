CREATE TABLE [dbo].[NotificationSetting] (
    [ID]                    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [SMTPAddress]           VARCHAR (MAX)    NULL,
    [SMTPPort]              VARCHAR (MAX)    NULL,
    [EmailAddress]          VARCHAR (MAX)    NULL,
    [Password]              VARCHAR (MAX)    NULL,
    [SenderName]            VARCHAR (MAX)    NULL,
    [EnableSSL]             BIT              NULL,
    [UseDefaultCredentials] BIT              NULL,
    [UseAsync]              BIT              NULL,
    [EnableServices]        BIT              NULL,
    [NewUser]               BIT              NULL,
    [NewRoleAssignment]     BIT              NULL,
    [UserPasswordReset]     BIT              NULL,
    [NewClient]             BIT              NULL,
    [Messaging]             BIT              NULL,
    [QuotaExceeded]         BIT              NULL,
    [ReminderServices]      BIT              NULL,
    [CreatedOn]             DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]             VARCHAR (MAX)    NULL,
    [UpdatedOn]             DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]             VARCHAR (MAX)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


