CREATE TABLE [dbo].[tblT_Banner] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ID_IN]       UNIQUEIDENTIFIER NULL,
    [Lang]        VARCHAR (2)      NULL,
    [Filename]    VARCHAR (255)    NULL,
    [Ekstension]  VARCHAR (50)     NULL,
    [Title]       VARCHAR (100)    NULL,
    [Title_Color] VARCHAR (20)    NULL,
    [SubTitle]    VARCHAR (150)    NULL,
    [SubTitle_Color]    VARCHAR (20)    NULL,
    [LabelTombol] VARCHAR (50)     NULL,
    [Link]        VARCHAR (MAX)    NULL,
    [Status]      VARCHAR (50)     NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   VARCHAR (MAX)    NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (MAX)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);









