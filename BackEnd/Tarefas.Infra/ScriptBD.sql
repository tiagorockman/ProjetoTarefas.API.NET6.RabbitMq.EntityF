IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Tarefa] (
    [Id] uniqueidentifier NOT NULL,
    [Descricao] varchar(1000) NOT NULL,
    [Data] datetime2 NOT NULL,
    [Status] bit NOT NULL,
    CONSTRAINT [PK_Tarefa] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231003162843_InitialCreate', N'7.0.11');
GO

COMMIT;
GO

