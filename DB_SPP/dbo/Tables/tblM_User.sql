CREATE TABLE [dbo].[tblM_User] (
    [UserID]              UNIQUEIDENTIFIER DEFAULT (newid()) PRIMARY KEY NOT NULL,
    [PasswordHash]        VARCHAR (MAX)    NULL,
    [Fullname]            VARCHAR (MAX)    NULL,
    [Email]               VARCHAR (MAX)    NULL,
    [Mobile]              VARCHAR (MAX)     NULL,
    [MobileTemp]          VARCHAR (MAX)     NULL,
    [Address]             VARCHAR (MAX)    NULL,
    [Gender]              NVARCHAR (2)     NULL,
    [Mail_Verification]   INT              DEFAULT ((0)) NOT NULL,
    [Mobile_Verification] INT              DEFAULT ((0)) NOT NULL,
    [NIP]                 VARCHAR (MAX)    NULL,
    [Jabatan]             VARCHAR (MAX)    NULL,
    [Divisi]              VARCHAR (MAX)    NULL,    
    [Img]                 NVARCHAR (100)   NULL,
    [Ekstension]          NVARCHAR (100)   NULL,
    [Notifikasi]          INT              DEFAULT ((1)) NOT NULL,
    [EmailNotification]   INT              DEFAULT ((1)) NOT NULL,
    [isActive]            INT              DEFAULT ((1)) NULL,
    [LastPasswordChanged] DATETIME         NULL,
    [CreatedOn]           DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]           VARCHAR (MAX)    NULL,
    [UpdatedOn]           DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]           VARCHAR (MAX)    NULL,
    [IsDeleted]           INT              DEFAULT ((0)) NOT NULL,
    [DeletedOn]           DATETIME         NULL,
    [DeletedBy]           VARCHAR (MAX)    NULL,
    
    
);





