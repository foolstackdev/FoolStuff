
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/22/2017 14:13:15
-- Generated from EDMX file: D:\GITREPO\FoolStuff\FoolStaffDataAccess\FoolStaffDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FoolStaffDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserToUser_Tesoreria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Tesoreria] DROP CONSTRAINT [FK_UserToUser_Tesoreria];
GO
IF OBJECT_ID(N'[dbo].[FK_TesoreriaToUser_Tesoreria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Tesoreria] DROP CONSTRAINT [FK_TesoreriaToUser_Tesoreria];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Tesoreria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tesoreria];
GO
IF OBJECT_ID(N'[dbo].[User_Tesoreria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_Tesoreria];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [Phone] nvarchar(20)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Tesoreria'
CREATE TABLE [dbo].[Tesoreria] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DataOperazione] datetime  NOT NULL,
    [Operazione] nvarchar(20)  NOT NULL,
    [Totale] decimal(18,0)  NOT NULL,
    [Note] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'User_Tesoreria'
CREATE TABLE [dbo].[User_Tesoreria] (
    [Versamento] decimal(18,0)  NOT NULL,
    [UserId] int  NOT NULL,
    [TesoreriaId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tesoreria'
ALTER TABLE [dbo].[Tesoreria]
ADD CONSTRAINT [PK_Tesoreria]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId], [TesoreriaId] in table 'User_Tesoreria'
ALTER TABLE [dbo].[User_Tesoreria]
ADD CONSTRAINT [PK_User_Tesoreria]
    PRIMARY KEY CLUSTERED ([UserId], [TesoreriaId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'User_Tesoreria'
ALTER TABLE [dbo].[User_Tesoreria]
ADD CONSTRAINT [FK_UserToUser_Tesoreria]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TesoreriaId] in table 'User_Tesoreria'
ALTER TABLE [dbo].[User_Tesoreria]
ADD CONSTRAINT [FK_TesoreriaToUser_Tesoreria]
    FOREIGN KEY ([TesoreriaId])
    REFERENCES [dbo].[Tesoreria]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TesoreriaToUser_Tesoreria'
CREATE INDEX [IX_FK_TesoreriaToUser_Tesoreria]
ON [dbo].[User_Tesoreria]
    ([TesoreriaId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------