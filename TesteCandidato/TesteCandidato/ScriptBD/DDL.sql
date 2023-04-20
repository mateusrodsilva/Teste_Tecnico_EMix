CREATE DATABASE CEP;

USE CEP;
GO;

CREATE TABLE[dbo].[CEP] (
    [Id]          INT primary key IDENTITY(1, 1) NOT NULL,
    [cep]         CHAR(9)       NULL,
    [logradouro] NVARCHAR(500) NULL,
    [complemento] NVARCHAR(500) NULL,
    [bairro] NVARCHAR(500) NULL,
    [localidade] NVARCHAR(500) NULL,
    [uf] CHAR(2)       NULL,
    [unidade] BIGINT NULL,
    [ibge]        INT NULL,
    [gia]         NVARCHAR(500) NULL
);
GO