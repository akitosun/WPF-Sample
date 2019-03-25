CREATE TABLE [dbo].[Users] (
    [UserName]     NVARCHAR(50)   NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserName])
);

