CREATE TABLE [dbo].[Users] (
    [Id]           CHAR (36)      NOT NULL,
    [UserName]     NVARCHAR(50)   NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

