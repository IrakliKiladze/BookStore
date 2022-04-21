﻿CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [Author] NVARCHAR(50) NULL, 
    [ISBN] NVARCHAR(50) NULL, 
    [Genre] NVARCHAR(50) NULL, 
    [Count] INT NULL 
)