CREATE TABLE [dbo].[tblT_User_Login] (
    [ID]                   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserID]               UNIQUEIDENTIFIER NULL,
    [isActive]             INT              DEFAULT ((0)) NULL,
    [Last_Login_DateTime]  DATETIME         NULL,
    [Last_Logout_DateTime] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

