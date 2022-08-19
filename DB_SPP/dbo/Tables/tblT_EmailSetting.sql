CREATE TABLE [dbo].[tblT_EmailSetting]
(
	[ID]                                UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ID_IN]                             UNIQUEIDENTIFIER NULL,
    [Tipe]                              VARCHAR (255)    NULL,
    [Lang]                              VARCHAR (2)      NULL,
    [Subject]                           VARCHAR (MAX)    NULL,
    [Konten]                            VARCHAR (MAX)    NULL,   
    [DelegatorName]                     VARCHAR (MAX)    NULL,
    [Email]                             VARCHAR (MAX)    NULL,
    [EmailPelapor]                      VARCHAR (MAX)    NULL,
    [Fullname]                          VARCHAR (MAX)    NULL,
    [IDValue]                           VARCHAR (MAX)    NULL,
    [New_User_Verification_ID]          VARCHAR (MAX)    NULL,
    [NewRandomPassword]                 VARCHAR (MAX)    NULL,
    [Nomor]                             VARCHAR (MAX)    NULL,
    [Roles]                             VARCHAR (MAX)    NULL,
    [Status]                            VARCHAR (MAX)    NULL,
    [TanggalKirim]                      VARCHAR (MAX)    NULL,
    [New_User_Password_Forgotten_ID]    VARCHAR (MAX)    NULL,

    [CreatedOn]                         DATETIME          DEFAULT (getdate()) NOT NULL,
    [CreatedBy]                         VARCHAR (MAX)     NULL,
    [UpdatedOn]                         DATETIME          DEFAULT (getdate()) NULL,
    [UpdatedBy]                         VARCHAR (MAX)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
)
