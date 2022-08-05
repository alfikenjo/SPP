CREATE TABLE [dbo].[tblM_Referensi] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Type]        VARCHAR (50)  NOT NULL,
    [Value]       VARCHAR (200) NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [isActive]    INT           DEFAULT ((1)) NOT NULL,
    [Created_On]  DATETIME      DEFAULT (getdate()) NULL,
    [Created_By]  VARCHAR (30)  NULL,
    [Updated_On]  DATETIME      DEFAULT (getdate()) NULL,
    [Updated_By]  VARCHAR (30)  NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

