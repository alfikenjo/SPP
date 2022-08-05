﻿CREATE TABLE [dbo].[tblT_Tanggapan] (
    [ID]                      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [JenisPengaduan]          VARCHAR (50)     NULL,
    [IDPengaduan]             UNIQUEIDENTIFIER NOT NULL,
    [TipePengirim]            VARCHAR (50)     NULL,
    [Email]                   VARCHAR (200)    NULL,
    [Nama]                    VARCHAR (200)    NULL,
    [Tanggapan]               VARCHAR (MAX)    NULL,
    [FileLampiran]            VARCHAR (200)    NULL,
    [FileLampiran_Ekstension] VARCHAR (50)     NULL,
    [CreatedOn]               DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]               VARCHAR (200)    NULL,
    [IsRead]                  INT              DEFAULT ((0)) NOT NULL,
    [ReadOn]                  DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

