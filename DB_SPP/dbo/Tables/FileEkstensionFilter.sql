CREATE TABLE [dbo].[FileEkstensionFilter]
(
	[ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL PRIMARY KEY,
    [Name]        VARCHAR (100)    NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (MAX)    NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (MAX)    NULL,
)
