CREATE DATABASE ShopChainManagement
GO
USE ShopChainManagement
GO
-- Tạo bảng Customer
CREATE TABLE Customer (
  Id INT IDENTITY PRIMARY KEY,
  Name VARCHAR(50),
  DateOfBirth DATE,
  Email VARCHAR(100)
);
GO
-- Tạo bảng Shop
CREATE TABLE Shop (
  Id INT IDENTITY PRIMARY KEY,
  Name VARCHAR(50),
  Location VARCHAR(100)
);
GO
-- Tạo bảng Product
CREATE TABLE Product (
  Id INT IDENTITY PRIMARY KEY,
  Name VARCHAR(50),
  Price DECIMAL(10,2)
);
GO
-- Tạo bảng Order
CREATE TABLE [Order] (
  Id INT IDENTITY PRIMARY KEY,
  ShopId INT,
  CustomerId INT,
  FOREIGN KEY (ShopId) REFERENCES Shop(Id),
  FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
);
-- Tạo bảng Transaction
CREATE TABLE [Transaction] (
  Id INT IDENTITY PRIMARY KEY,
  OrderId INT,
  ProductId INT,
  Price DECIMAL(10,2),
  Quantity INT,
  Amount DECIMAL(20,2),
  FOREIGN KEY (OrderId) REFERENCES [Order](Id),
  FOREIGN KEY (ProductId) REFERENCES Product(Id)
);