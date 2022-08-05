CREATE TABLE [dbo].[tblT_UserInRole] (
    [ID]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserID]    UNIQUEIDENTIFIER NOT NULL,
    [RoleID]    UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn] DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy] VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_tblT_UserInRole_To_tblM_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[tblM_Role] ([ID]),
    CONSTRAINT [FK_tblT_UserInRole_To_tblM_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblM_User] ([UserID])
);

