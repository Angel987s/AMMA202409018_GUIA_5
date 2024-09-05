CREATE DATABASE dbAMMA202409
GO

-- Utilizar la base de datos CRMDB
USE dbAMMA202409
GO

drop table Products

sp_help 'Products'


ALTER TABLE Products
DROP COLUMN Id;

ALTER TABLE Products
ADD Id INT IDENTITY(1,1) PRIMARY KEY;

select * from Products
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreAMMA NVARCHAR(100) NOT NULL,
    DescripcionAMMA NVARCHAR(MAX),
    PrecioAMMA DECIMAL(18,2) NOT NULL
);