CREATE TABLE [dbo].[KuesionerValue] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IDPengaduan] UNIQUEIDENTIFIER DEFAULT (NULL) NULL,
    [Title]       VARCHAR (255)    NULL,
    [Num]         INT              NULL,
    [InputType]   VARCHAR (255)    NULL,
    [Label]       VARCHAR (MAX)    NULL,
    [Required]    BIT              DEFAULT ((0)) NULL,
    [Options]     VARCHAR (MAX)    NULL,
    [InputValue]  VARCHAR (MAX)    NULL,
    [CreatedOn]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   VARCHAR (200)    NULL,
    [UpdatedOn]   DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (200)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


