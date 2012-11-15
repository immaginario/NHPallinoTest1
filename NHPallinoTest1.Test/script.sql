
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ShopOrderItems_ShopOrders]') AND parent_object_id = OBJECT_ID('ShopOrderItems'))
alter table ShopOrderItems  drop constraint FK_ShopOrderItems_ShopOrders


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ShopOrders_Shops]') AND parent_object_id = OBJECT_ID('ShopOrders'))
alter table ShopOrders  drop constraint FK_ShopOrders_Shops


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ShopOrders_Customers]') AND parent_object_id = OBJECT_ID('ShopOrders'))
alter table ShopOrders  drop constraint FK_ShopOrders_Customers


    if exists (select * from dbo.sysobjects where id = object_id(N'Customers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Customers

    if exists (select * from dbo.sysobjects where id = object_id(N'Shops') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Shops

    if exists (select * from dbo.sysobjects where id = object_id(N'ShopOrderItems') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ShopOrderItems

    if exists (select * from dbo.sysobjects where id = object_id(N'ShopOrders') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ShopOrders

    create table Customers (
        Id INT IDENTITY NOT NULL,
       Name NVARCHAR(255) not null,
       Surname NVARCHAR(255) not null,
       Address NVARCHAR(255) not null,
       Created DATETIME not null,
       primary key (Id)
    )

    create table Shops (
        Id INT IDENTITY NOT NULL,
       Name NVARCHAR(255) not null,
       primary key (Id)
    )

    create table ShopOrderItems (
        Id INT IDENTITY NOT NULL,
       Price DECIMAL(19,5) not null,
       ProductName NVARCHAR(255) not null,
       Quantity INT not null,
       ShopOrder_id INT not null,
       primary key (Id)
    )

    create table ShopOrders (
        Id INT IDENTITY NOT NULL,
       Created DATETIME not null,
       Shop_id INT not null,
       Customer_id INT not null,
       primary key (Id)
    )

    alter table ShopOrderItems 
        add constraint FK_ShopOrderItems_ShopOrders 
        foreign key (ShopOrder_id) 
        references ShopOrders

    alter table ShopOrders 
        add constraint FK_ShopOrders_Shops 
        foreign key (Shop_id) 
        references Shops

    alter table ShopOrders 
        add constraint FK_ShopOrders_Customers 
        foreign key (Customer_id) 
        references Customers
