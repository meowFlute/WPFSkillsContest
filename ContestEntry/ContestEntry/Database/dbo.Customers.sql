CREATE TABLE [dbo].[Customers] (
    [CustomerID] UNIQUEIDENTIFIER NOT NULL,
    [FirstName]  NVARCHAR (50)    NOT NULL,
    [LastName]   NVARCHAR (50)    NOT NULL,
    [Email]      NVARCHAR (200)   NOT NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

