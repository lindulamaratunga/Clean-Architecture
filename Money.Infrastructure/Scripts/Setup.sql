-- Database setup script for Currency Converter API
-- This script creates the database and sets up initial data

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CurrencyConverterDb')
BEGIN
    CREATE DATABASE [CurrencyConverterDb]
END
GO

USE [CurrencyConverterDb]
GO

-- The Entity Framework will handle table creation through code-first migrations
-- This script is for reference and manual database operations if needed

-- Verify database creation
SELECT 'CurrencyConverterDb database created successfully' AS Status

-- Note: Tables will be created automatically when the application runs
-- due to context.Database.EnsureCreated() in Program.cs
