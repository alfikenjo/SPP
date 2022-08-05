CREATE TABLE [dbo].[tblM_Unit] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Level]       VARCHAR (100)    NULL,
    [Name]        VARCHAR (100)    NULL,
    [Description] VARCHAR (MAX)    NULL,
    [ID_Parent]   UNIQUEIDENTIFIER NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (50)     NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

