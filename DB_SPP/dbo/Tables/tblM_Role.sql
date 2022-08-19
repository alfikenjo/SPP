CREATE TABLE [dbo].[tblM_Role] (
    [ID]          UNIQUEIDENTIFIER CONSTRAINT [DF__tblM_Role__ID__25869641] DEFAULT (newid()) NOT NULL,
    [Name]        VARCHAR (100)    NULL,
    [Description] VARCHAR (MAX)    NULL,
    [Privileges]  VARCHAR (50)     NULL,
    [Status]      INT              CONSTRAINT [DF__tblM_Role__Statu__267ABA7A] DEFAULT ((1)) NULL,
    [CreatedOn]   DATETIME         CONSTRAINT [DF__tblM_Role__Creat__276EDEB3] DEFAULT (getdate()) NULL,
    [CreatedBy]   VARCHAR (MAX)    NULL,
    [UpdatedOn]   DATETIME         CONSTRAINT [DF__tblM_Role__Updat__286302EC] DEFAULT (getdate()) NULL,
    [UpdatedBy]   VARCHAR (MAX)    NULL,
    CONSTRAINT [PK__tblM_Rol__3214EC270266B6D7] PRIMARY KEY CLUSTERED ([ID] ASC)
);



