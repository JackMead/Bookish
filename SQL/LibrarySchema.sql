-- Delete and recreate the CatShelter database

IF EXISTS ( SELECT * FROM master..sysdatabases WHERE name = 'Bookish' ) BEGIN
  USE master
  DROP DATABASE Bookish
END
GO

CREATE DATABASE Bookish
GO

USE Bookish
GO

-- Create the tables

CREATE TABLE Users (
	Id nvarchar(50) NOT NULL PRIMARY KEY,
	Password nvarchar(20) NOT NULL
)
GO

CREATE TABLE Books (
	Author nvarchar(max) NOT NULL,
	Title nvarchar(max) NOT NULL,
	ISBN int NOT NULL,
	CopyNumber int NOT NULL,
	UserId nvarchar(50) NULL,
	DueDate date NULL,
	PRIMARY KEY (ISBN, CopyNumber)
)
GO

INSERT INTO Users(Id, Password) VALUES ('jack','password')
INSERT INTO Books(Author, Title, ISBN, CopyNumber) VALUES('Stephen King','The Dark Tower',102341,1)
INSERT INTO Books(Author, Title, ISBN, CopyNumber) VALUES('Stephen King','The Dark Tower',102341,2)
INSERT INTO Books(Author, Title, ISBN, CopyNumber) VALUES('Stephen King','The Dark Tower',102341,3)
INSERT INTO Books(Author, Title, ISBN, CopyNumber) VALUES('Stephen King','The Dark Tower',102341,4)
INSERT INTO Books(Author, Title, ISBN, CopyNumber) VALUES('J.K.Rowling','Harry Potter',1041,1)
INSERT INTO Books(Author, Title, ISBN, CopyNumber, UserId) VALUES('J.K.Rowling','Harry Potter',1041,2,'jack')

SELECT * FROM Books