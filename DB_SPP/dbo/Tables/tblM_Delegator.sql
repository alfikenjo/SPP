CREATE TABLE [dbo].[tblM_Delegator] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]        VARCHAR (255)    NULL,
    [Description] VARCHAR (MAX)    NULL,
    [isActive]    INT              DEFAULT ((1)) NOT NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (MAX)     NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (MAX)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

