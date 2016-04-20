﻿/*
Deployment script for C:\USERS\SCOTT\DOCUMENTS\WPFSKILLSCONTEST\CONTESTENTRY\CONTESTENTRY\SAMPLEDATABASE.MDF

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "C:\USERS\SCOTT\DOCUMENTS\WPFSKILLSCONTEST\CONTESTENTRY\CONTESTENTRY\SAMPLEDATABASE.MDF"
:setvar DefaultFilePrefix "C_\USERS\SCOTT\DOCUMENTS\WPFSKILLSCONTEST\CONTESTENTRY\CONTESTENTRY\SAMPLEDATABASE.MDF_"
:setvar DefaultDataPath "C:\Users\Scott\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\Scott\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
/*
The type for column CustomerID in table [dbo].[Customers] is currently  INT NOT NULL but is being changed to  UNIQUEIDENTIFIER NOT NULL. There is no implicit or explicit conversion.
*/
GO
PRINT N'Starting rebuilding table [dbo].[Customers]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Customers] (
    [CustomerID] UNIQUEIDENTIFIER NOT NULL,
    [FirstName]  NVARCHAR (50)    NOT NULL,
    [LastName]   NVARCHAR (50)    NOT NULL,
    [Email]      NVARCHAR (200)   NOT NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Customers])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Customers] ([CustomerID], [FirstName], [LastName], [Email])
        SELECT   [CustomerID],
                 [FirstName],
                 [LastName],
                 [Email]
        FROM     [dbo].[Customers]
        ORDER BY [CustomerID] ASC;
    END

DROP TABLE [dbo].[Customers];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Customers]', N'Customers';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Table]...';


GO
CREATE TABLE [dbo].[Table] (
    [Id]         INT              NOT NULL,
    [CustomerId] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'The transacted portion of the database update succeeded.'
COMMIT TRANSACTION
END
ELSE PRINT N'The transacted portion of the database update failed.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Update complete.';


GO
