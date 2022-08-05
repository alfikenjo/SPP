CREATE TABLE [dbo].[tblT_File_Evidence] (
    [ID]                                UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (newid()) NOT NULL,
    [ID_Header]                         UNIQUEIDENTIFIER,      
    [FileEvidence]                      VARCHAR (255)    NULL,
    [FileEvidence_Ekstension]           VARCHAR (50)     NULL,
    [CreatedOn]                         DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]                         VARCHAR (200)    NULL,
    [UpdatedOn]                         DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]                         VARCHAR (200)    NULL
);

