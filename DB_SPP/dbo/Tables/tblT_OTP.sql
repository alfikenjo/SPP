CREATE TABLE [dbo].[tblT_OTP] (
    [ID]         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserID]     UNIQUEIDENTIFIER NOT NULL,
    [Mobile]     VARCHAR (MAX)    NULL,
    [OTP]        VARCHAR (100)    NULL,
    [SMS_Status] VARCHAR (150)    NULL,
    [Status]     VARCHAR (150)    NULL,
    [Remarks]    VARCHAR (MAX)    NULL,
    [CreatedOn]  DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]  VARCHAR (MAX)     NULL,
    [UpdatedOn]  DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]  VARCHAR (MAX)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_tblT_OTP_To_tblM_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblM_User] ([UserID])
);

