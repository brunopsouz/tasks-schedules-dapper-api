/********************************************************************
Author			: Bruno Souza (https://github.com/brunopsouz)
Creation Date	: 13/09/2025
Description		: Script to create Database and Tables.
Script Version	: 1.0
********************************************************************/

-- Criar a Database
CREATE DATABASE TaskNoteManager;
GO

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Position VARCHAR(20) NOT NULL,
    UserType VARCHAR(20) NULL,
    UserIdentifier UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()
);
