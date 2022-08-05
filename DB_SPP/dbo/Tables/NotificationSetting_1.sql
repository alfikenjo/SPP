CREATE TABLE [dbo].[NotificationSetting] (
    [ID]                    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [SMTPAddress]           VARCHAR (200)    NULL,
    [SMTPPort]              VARCHAR (200)    NULL,
    [EmailAddress]          VARCHAR (200)    NULL,
    [Password]              VARCHAR (200)    NULL,
    [SenderName]            VARCHAR (200)    NULL,
    [EnableSSL]             BIT              NULL,
    [UseDefaultCredentials] BIT              NULL,
    [UseAsync]              BIT              NULL,
    [EnableServices]        BIT              NULL,
    [NewUser]               BIT              NULL,
    [NewRoleAssignment]     BIT              NULL,
    [UserPasswordReset]     BIT              NULL,
    [Messaging]             BIT              NULL,
    [ReminderServices]      BIT              NULL,
    [CreatedOn]             DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]             VARCHAR (255)    NULL,
    [UpdatedOn]             DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]             VARCHAR (255)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

