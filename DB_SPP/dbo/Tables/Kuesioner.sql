CREATE TABLE [dbo].[Kuesioner]
(
	[ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Title]       VARCHAR (255)    NULL,
    [Status]      VARCHAR (50)     NULL,
    [StartDate]   DATE             DEFAULT NULL,
    [EndDate]     DATE             DEFAULT NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   VARCHAR (MAX)    NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (MAX)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
)
