CREATE TABLE [dbo].[tblM_User] (
    [UserID]              UNIQUEIDENTIFIER CONSTRAINT [DF__tblM_User__UserI__300424B4] DEFAULT (newid()) NOT NULL,
    [Username]            VARCHAR (20)     NULL,
    [PasswordHash]        VARCHAR (MAX)    NULL,
    [Fullname]            VARCHAR (100)    NULL,
    [Email]               VARCHAR (100)    NULL,
    [Mobile]              VARCHAR (50)     NULL,
    [MobileTemp]          VARCHAR (50)     NULL,
    [Address]             VARCHAR (MAX)    NULL,
    [Gender]              NVARCHAR (2)     NULL,
    [Mail_Verification]   INT              CONSTRAINT [DF__tblM_User__Mail___30F848ED] DEFAULT ((0)) NOT NULL,
    [Mobile_Verification] INT              CONSTRAINT [DF_tblM_User_Mobile_Verification] DEFAULT ((0)) NOT NULL,
    [NIP]                 VARCHAR (100)    NULL,
    [Jabatan]             VARCHAR (200)    NULL,
    [ID_Unit]             UNIQUEIDENTIFIER NULL,
    [Img]                 NVARCHAR (100)   NULL,
    [Ekstension]          NVARCHAR (100)   NULL,
    [Notifikasi]          INT              CONSTRAINT [DF__tblM_User__Notif__31EC6D26] DEFAULT ((1)) NOT NULL,
    [isActive]            INT              CONSTRAINT [DF__tblM_User__isAct__32E0915F] DEFAULT ((1)) NULL,
    [LastLoginDate]       DATETIME         NULL,
    [LastPasswordChanged] DATETIME         NULL,
    [CreatedOn]           DATETIME         CONSTRAINT [DF__tblM_User__Creat__33D4B598] DEFAULT (getdate()) NULL,
    [CreatedBy]           VARCHAR (255)    NULL,
    [UpdatedOn]           DATETIME         CONSTRAINT [DF__tblM_User__Updat__34C8D9D1] DEFAULT (getdate()) NULL,
    [UpdatedBy]           VARCHAR (255)    NULL,
    [NIK]                 VARCHAR (20)     NULL,
    [Tempat_Lahir]        VARCHAR (200)    NULL,
    [Tanggal_Lahir]       DATE             NULL,
    [ID_Organisasi]       UNIQUEIDENTIFIER NULL,
    [IsDeleted]           INT              DEFAULT ((0)) NOT NULL,
    [DeletedOn]           DATETIME         NULL,
    [DeletedBy]           VARCHAR (200)    NULL,
    [EmailNotification]   INT              DEFAULT ((1)) NOT NULL,
    [Divisi]              VARCHAR (200)    NULL,
    CONSTRAINT [PK__tblM_Use__1788CCACD6BA7EE7] PRIMARY KEY CLUSTERED ([UserID] ASC)
);





