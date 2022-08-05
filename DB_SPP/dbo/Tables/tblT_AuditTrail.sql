CREATE TABLE [dbo].[tblT_AuditTrail] (
    [ID]           UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [username]     VARCHAR (30)     NULL,
    [PC_name]      VARCHAR (100)    NULL,
    [IP_address]   VARCHAR (30)     NULL,
    [MAC_address]  VARCHAR (30)     NULL,
    [item]         VARCHAR (200)    NULL,
    [url]          VARCHAR (150)    NULL,
    [action]       VARCHAR (50)     NULL,
    [description]  VARCHAR (MAX)    NULL,
    [datetime]     DATETIME         DEFAULT (getdate()) NULL,
    [respond_type] NCHAR (10)       NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

