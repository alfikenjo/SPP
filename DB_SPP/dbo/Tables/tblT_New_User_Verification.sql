CREATE TABLE [dbo].[tblT_New_User_Verification] (
    [ID]                       UNIQUEIDENTIFIER CONSTRAINT [DF__tblT_New_Use__ID__0A9D95DB] DEFAULT (newid()) NOT NULL,
    [UserID]                   UNIQUEIDENTIFIER NOT NULL,
    [Email]                    VARCHAR (255)    NULL,
    [Verification_Mail_Status] VARCHAR (150)    NULL,
    [Status]                   VARCHAR (150)    NULL,
    [Remarks]                  VARCHAR (MAX)    NULL,
    [CreatedOn]                DATETIME         CONSTRAINT [DF__tblT_New___Creat__0B91BA14] DEFAULT (getdate()) NULL,
    [CreatedBy]                VARCHAR (255)    NULL,
    [UpdatedOn]                DATETIME         CONSTRAINT [DF__tblT_New___Updat__0C85DE4D] DEFAULT (getdate()) NULL,
    [UpdatedBy]                VARCHAR (255)    NULL,
    CONSTRAINT [PK__tblT_New__3214EC2733A4AC7C] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_tblT_New_User_Verification_To_tblM_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblM_User] ([UserID])
);

