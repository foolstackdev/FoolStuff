
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/23/2017 20:40:31
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

IF OBJECT_ID(N'[dbo].[FK_TesoreriaToUser_Tesoreria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Tesoreria] DROP CONSTRAINT [FK_TesoreriaToUser_Tesoreria];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoToUser_Tesoreria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Tesoreria] DROP CONSTRAINT [FK_UserInfoToUser_Tesoreria];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
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

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [Name] nvarchar(50)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [Phone] nvarchar(20)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL,
    [Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Tesoreria'
CREATE TABLE [dbo].[Tesoreria] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DataOperazione] datetimeoffset  NOT NULL,
    [Operazione] nvarchar(20)  NOT NULL,
    [Totale] decimal(18,0)  NOT NULL,
    [Note] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'User_Tesoreria'
CREATE TABLE [dbo].[User_Tesoreria] (
    [Versamento] decimal(18,0)  NOT NULL,
    [TesoreriaId] int  NOT NULL,
    [UserInfoId] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tesoreria'
ALTER TABLE [dbo].[Tesoreria]
ADD CONSTRAINT [PK_Tesoreria]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TesoreriaId], [UserInfoId] in table 'User_Tesoreria'
ALTER TABLE [dbo].[User_Tesoreria]
ADD CONSTRAINT [PK_User_Tesoreria]
    PRIMARY KEY CLUSTERED ([TesoreriaId], [UserInfoId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TesoreriaId] in table 'User_Tesoreria'
ALTER TABLE [dbo].[User_Tesoreria]
ADD CONSTRAINT [FK_TesoreriaToUser_Tesoreria]
    FOREIGN KEY ([TesoreriaId])
    REFERENCES [dbo].[Tesoreria]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserInfoId] in table 'User_Tesoreria'
ALTER TABLE [dbo].[User_Tesoreria]
ADD CONSTRAINT [FK_UserInfoToUser_Tesoreria]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoToUser_Tesoreria'
CREATE INDEX [IX_FK_UserInfoToUser_Tesoreria]
ON [dbo].[User_Tesoreria]
    ([UserInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------