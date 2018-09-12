

SET XACT_ABORT ON

BEGIN TRANSACTION QUICKDBD


CREATE TABLE [Customer] (
    [Id] uniqueidentifier DEFAULT NEWID(),
    [Role] Tinyint  NOT NULL ,
    [Name] varchar(20)  NOT NULL ,
    [Address1] varchar(50) NOT NULL ,
    [Address2] varchar(20)  NULL ,
    [Address3] varchar(20)  NULL ,
    [Email] varchar(20) unique NOT NULL ,
    [Password] varchar(20)  NOT NULL ,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED (
        [Id] ASC
    ),
    CONSTRAINT [UK_Customer_Email] UNIQUE (
        [Email]
    )
)

CREATE TABLE [Order] (
    [Id] uniqueidentifier DEFAULT NEWID(),
    [CustomerId] uniqueidentifier  NOT NULL ,
    [TotalAmount] decimal  NOT NULL ,
    [OrderStatus] varchar(20)  NOT NULL ,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
)

CREATE TABLE [OrderLine] (
    [Id] uniqueidentifier DEFAULT NEWID(),
    [OrderId] uniqueidentifier  NOT NULL ,
    [ProductId] uniqueidentifier  NOT NULL ,
    [Quantity] int  NOT NULL ,
    CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
)

CREATE TABLE [Product] (
    [Id] uniqueidentifier DEFAULT NEWID(),
    [Name] varchar(200)  NOT NULL ,
    [Price] decimal  NOT NULL ,
    [Description] varchar(1000)  NOT NULL ,
	[ImageURL] varchar(200)  NULL ,
    [CategoryId] uniqueidentifier  NOT NULL ,
    [TotalQuantitySale] int  NOT NULL ,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED (
        [Id] ASC
    ),
    CONSTRAINT [UK_Product_Name] UNIQUE (
        [Name]
    )
)

CREATE TABLE [ProductVariant] (
    [Id] uniqueidentifier DEFAULT NEWID(),
    [Name] varchar(200)  NOT NULL ,
    [ProductId] uniqueidentifier  NOT NULL ,
    CONSTRAINT [PK_ProductVariant] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )   
)

CREATE TABLE [ProductCategory] (
    [Id] uniqueidentifier DEFAULT NEWID(),
    [Name] varchar(200)  NOT NULL ,
    [TotalSaleQuantity] int  NOT NULL ,
    CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED (
        [Id] ASC
    ),
    CONSTRAINT [UK_ProductCategory_Name] UNIQUE (
        [Name]
    )
)

CREATE TABLE [CartLine] (
    [Id] uniqueidentifier DEFAULT NEWID(),
	[Quantity] int NOT NULL ,
    [ProductId] uniqueidentifier  NOT NULL ,
    [CustomerId] uniqueidentifier  NOT NULL ,   
    CONSTRAINT [PK_CartLine] PRIMARY KEY CLUSTERED (
        [ProductId] ASC,[CustomerId] ASC
    )
)


CREATE TABLE [TopProducts] (
    [Id] uniqueidentifier DEFAULT NEWID(),
    [TotalSale] int  NOT NULL ,
    [ProductId] uniqueidentifier  NOT NULL ,
    [ProductCategoryId] uniqueidentifier  NOT NULL ,
    CONSTRAINT [PK_TopProducts] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
)



ALTER TABLE [Order] WITH CHECK ADD CONSTRAINT [FK_Order_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [Customer] ([Id])

ALTER TABLE [Order] CHECK CONSTRAINT [FK_Order_CustomerId]

ALTER TABLE [OrderLine] WITH CHECK ADD CONSTRAINT [FK_OrderLine_OrderId] FOREIGN KEY([OrderId])
REFERENCES [Order] ([Id])

ALTER TABLE [OrderLine] CHECK CONSTRAINT [FK_OrderLine_OrderId]

ALTER TABLE [OrderLine] WITH CHECK ADD CONSTRAINT [FK_OrderLine_ProductId] FOREIGN KEY([ProductId])
REFERENCES [Product] ([Id])

ALTER TABLE [OrderLine] CHECK CONSTRAINT [FK_OrderLine_ProductId]

ALTER TABLE [Product] WITH CHECK ADD CONSTRAINT [FK_Product_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [ProductCategory] ([Id])

ALTER TABLE [Product] CHECK CONSTRAINT [FK_Product_CategoryId]

ALTER TABLE [ProductVariant] WITH CHECK ADD CONSTRAINT [FK_ProductVariant_ProductId] FOREIGN KEY([ProductId])
REFERENCES [Product] ([Id])

ALTER TABLE [ProductVariant] CHECK CONSTRAINT [FK_ProductVariant_ProductId]

ALTER TABLE [CartLine] WITH CHECK ADD CONSTRAINT [FK_CartLine_ProductId] FOREIGN KEY([ProductId])
REFERENCES [Product] ([Id])

ALTER TABLE [CartLine] CHECK CONSTRAINT [FK_CartLine_ProductId]

ALTER TABLE [CartLine] WITH CHECK ADD CONSTRAINT [FK_CartLine_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [Customer] ([Id])

ALTER TABLE [CartLine] CHECK CONSTRAINT [FK_CartLine_CustomerId]

ALTER TABLE [TopProducts] WITH CHECK ADD CONSTRAINT [FK_TopProducts_ProductId] FOREIGN KEY([ProductId])
REFERENCES [Product] ([Id])

ALTER TABLE [TopProducts] CHECK CONSTRAINT [FK_TopProducts_ProductId]

ALTER TABLE [TopProducts] WITH CHECK ADD CONSTRAINT [FK_TopProducts_ProductCategoryId] FOREIGN KEY([ProductCategoryId])
REFERENCES [ProductCategory] ([Id])

ALTER TABLE [TopProducts] CHECK CONSTRAINT [FK_TopProducts_ProductCategoryId]

CREATE INDEX [Idx_Customer_Name]
ON [Customer] ([Name])

COMMIT TRANSACTION QUICKDBD