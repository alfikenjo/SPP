CREATE TABLE [dbo].[tblM_AuthParameter] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]        VARCHAR (100)    NULL,
    [Description] VARCHAR (MAX)    NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (50)     NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

