﻿CREATE TABLE [dbo].[tblT_User_Password_Forgotten] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserID]      UNIQUEIDENTIFIER NOT NULL,
    [Email]       VARCHAR (100)    NULL,
    [Mail_Status] VARCHAR (150)    NULL,
    [Status]      VARCHAR (150)    NULL,
    [Remarks]     VARCHAR (MAX)    NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (50)     NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_tblT_User_Password_Forgotten_To_tblM_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblM_User] ([UserID])
);

