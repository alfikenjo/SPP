﻿CREATE TABLE [dbo].[KuesionerDetail]
(
	[ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IDHeader]    UNIQUEIDENTIFIER DEFAULT NULL,
    [Num]         INT,
    [InputType]   VARCHAR (255)    NULL,
    [Label]       VARCHAR (MAX)    NULL,
    [Required]    BIT NULL DEFAULT(0),
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   VARCHAR (200)    NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (200)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
)
