CREATE TABLE [dbo].[tblT_Dumas_Detail] (
    [ID]                                UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (newid()) NOT NULL,
    [ID_Header]                         UNIQUEIDENTIFIER,  
    [Nama]                              VARCHAR (MAX)    NULL,
    [NomorHandphone]                    VARCHAR (MAX)    NULL,
    [Departemen]                        VARCHAR (MAX)    NULL,
    [Jabatan]                           VARCHAR (MAX)    NULL,    
    [FileIdentitas]                     VARCHAR (255)    NULL,
    [FileIdentitas_Ekstension]          VARCHAR (50)     NULL,   
    [CreatedOn]                         DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreatedBy]                         VARCHAR (MAX)    NULL,
    [UpdatedOn]                         DATETIME         DEFAULT (getdate()) NULL,
    [UpdatedBy]                         VARCHAR (MAX)    NULL, 
    CONSTRAINT [FK_tblT_Dumas_Detail_To_tblT_Dumas] FOREIGN KEY (ID_Header) REFERENCES tblT_Dumas(ID), 
);

