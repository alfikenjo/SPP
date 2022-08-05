CREATE TABLE [dbo].[KuesionerDetailOptions]
(
	[ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IDHeader]    UNIQUEIDENTIFIER DEFAULT NULL,
    [Num]         INT NULL,
    [Options]     VARCHAR (MAX)    NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   VARCHAR (200)    NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (200)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
)
