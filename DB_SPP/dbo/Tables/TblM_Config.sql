CREATE TABLE [dbo].[TblM_Config] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Request_OTP] VARCHAR (150) NULL,
    [Submit_OTP]  VARCHAR (150) NULL,
    [CreatedOn]   DATETIME      NULL,
    [CreatedBy]   VARCHAR (255) NULL,
    [UpdateOn]    DATETIME      NULL,
    [UpdateBy]    VARCHAR (255) NULL
);

