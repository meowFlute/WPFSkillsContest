CREATE TABLE [dbo].[Orders] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [CustomerID]  UNIQUEIDENTIFIER NOT NULL,
    [OrderDate]   DATETIME2 (7)    NOT NULL,
    [OrderStatus] INT              NOT NULL,
    [ItemsTotal]  DECIMAL (18, 4)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);

