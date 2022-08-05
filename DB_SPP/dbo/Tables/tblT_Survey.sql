CREATE TABLE [dbo].[tblT_Survey] (
    [ID]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [NamaSurvey]     VARCHAR (MAX)    NULL,
    [JenisPengaduan] VARCHAR (50)     NOT NULL,
    [IDPengaduan]    UNIQUEIDENTIFIER NULL,
    [NamaPengirim]   VARCHAR (50)     NULL,
    [Umur]           INT              NULL,
    [Gender]         VARCHAR (50)     NULL,
    [Pendidikan]     VARCHAR (150)    NULL,
    [Pekerjaan]      VARCHAR (MAX)    NULL,
    [Email]          VARCHAR (200)    NULL,
    [Handphone]      VARCHAR (200)    NULL,
    [Jawaban_1]      VARCHAR (MAX)    NULL,
    [Jawaban_2]      VARCHAR (MAX)    NULL,
    [Jawaban_3]      VARCHAR (MAX)    NULL,
    [Jawaban_4]      VARCHAR (MAX)    NULL,
    [Jawaban_5]      VARCHAR (MAX)    NULL,
    [Jawaban_6]      VARCHAR (MAX)    NULL,
    [Jawaban_7]      VARCHAR (MAX)    NULL,
    [CreatedOn]      DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]      VARCHAR (200)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

