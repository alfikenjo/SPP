CREATE TABLE [dbo].[tblT_UserInDelegator] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserID]      UNIQUEIDENTIFIER NOT NULL,
    [DelegatorID] UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (MAX)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_tblT_UserInDelegator_To_tblM_Delegator] FOREIGN KEY ([DelegatorID]) REFERENCES [dbo].[tblM_Delegator] ([ID]),
    CONSTRAINT [FK_tblT_UserInDelegator_To_tblM_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblM_User] ([UserID])
);

