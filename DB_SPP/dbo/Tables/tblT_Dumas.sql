CREATE TABLE [dbo].[tblT_Dumas] (
    [ID]                                UNIQUEIDENTIFIER CONSTRAINT [DF__tblT_Dumas__ID__3B75D760] DEFAULT (newid()) NOT NULL,
    [Sumber]                            VARCHAR (200)    NULL,
    [Nomor]                             VARCHAR (200)    NULL,
    [Email]                             VARCHAR (MAX)    NULL,
    [Handphone]                         VARCHAR (MAX)    NULL,
    [TempatKejadian]                    VARCHAR (MAX)    NULL,
    [WaktuKejadian]                     DATETIME         NULL,
    [Kronologi]                         VARCHAR (MAX)    NULL,
    [Status]                            VARCHAR (50)     NULL,  
    [PenyaluranBy]                      VARCHAR (MAX)    NULL,
    [PenyaluranDate]                    DATETIME         NULL,
    [TindakLanjutBy]                    VARCHAR (MAX)    NULL,
    [TindakLanjutDate]                  DATETIME         NULL,
    [ResponBy]                          VARCHAR (MAX)    NULL,
    [ResponDate]                        DATETIME         NULL,
    [CreatedOn]                         DATETIME         CONSTRAINT [DF__tblT_Duma__Creat__3C69FB99] DEFAULT (getdate()) NOT NULL,
    [CreatedBy]                         VARCHAR (MAX)    NULL,
    [UpdatedOn]                         DATETIME         CONSTRAINT [DF__tblT_Duma__Updat__3D5E1FD2] DEFAULT (getdate()) NULL,
    [UpdatedBy]                         VARCHAR (MAX)    NULL,
    [ProsesBy]                          VARCHAR (MAX)    NULL,
    [ProsesDate]                        DATETIME         NULL,
    [Jenis_Pelanggaran]                 VARCHAR (MAX)    NULL,
    [Keterangan_Penyaluran]             VARCHAR (MAX)    NULL,
    [Keterangan_Penyaluran_Filename]    VARCHAR (255)    NULL,
    [Keterangan_Penyaluran_Ekstension]  VARCHAR (50)     NULL,
    [Keterangan_Pemeriksaan]            VARCHAR (MAX)    NULL,
    [Keterangan_Pemeriksaan_Filename]   VARCHAR (255)    NULL,
    [Keterangan_Pemeriksaan_Ekstension] VARCHAR (50)     NULL,
    [Keterangan_Konfirmasi]             VARCHAR (MAX)    NULL,
    [Keterangan_Konfirmasi_Filename]    VARCHAR (255)    NULL,
    [Keterangan_Konfirmasi_Ekstension]  VARCHAR (50)     NULL,
    [Keterangan_Respon]                 VARCHAR (MAX)    NULL,
    [Keterangan_Respon_Filename]        VARCHAR (255)    NULL,
    [Keterangan_Respon_Ekstension]      VARCHAR (50)     NULL, 
    [DelegatorID]                       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK__tblT_Dum__3214EC2722148FF4] PRIMARY KEY CLUSTERED ([ID] ASC)
);





