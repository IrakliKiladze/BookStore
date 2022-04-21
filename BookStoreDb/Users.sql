CREATE TABLE [dbo].[Users] (
    [Id]       INT            NOT NULL IDENTITY,
    [FirstName]     NVARCHAR (50)  NULL,
    [LastName] NVARCHAR (50) NULL,
    [UserName] NVARCHAR(50) NULL, 
    [Password] NVARCHAR(500) NULL, 
    [UserType] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

