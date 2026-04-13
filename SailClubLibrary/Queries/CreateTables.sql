--DROP DATABASE IF EXISTS SailClubDatabase
--CREATE DATABASE SailClubDatabase 
--USE SailClubDatabase;
--GO
--DROP TABLE IF EXISTS Bookings;
--DROP TABLE IF EXISTS Boats;
--DROP TABLE IF EXISTS Members;
CREATE TABLE Boats(
	ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	SailNumber nvarchar(7) NOT NULL UNIQUE,
	Model nvarchar(10) NOT NULL,
	Draft float(52) NOT NULL,
	Width float(52) NOT NULL,
	Length float(52) NOT NULL,
	YearOfConstruction nvarchar(4) NOT NULL,
	EngineInfo nvarchar(50),
	TheBoatType int,
	Image nvarchar(100) NOT NULL DEFAULT 'DefaultBoat.jpg'
	);

CREATE TABLE Members(
	ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FirstName nvarchar(20) NOT NULL,
	SurName nvarchar(20) NOT NULL,
	PhoneNumber nvarchar(8) NOT NULL UNIQUE,
	Address nvarchar(30) NOT NULL,
	City nvarchar(20) NOT NULL,
	Mail nvarchar(30) NOT NULL,
	TheMemberType int NOT NULL,
	TheMemberRole int NOT NULL,
	Image nvarchar(100) NOT NULL DEFAULT 'Default.jpg',
	Password nvarchar(16) NOT NULL DEFAULT '123'
	);

CREATE TABLE Bookings(
	ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	Destination nvarchar(20) NOT NULL,
	Member_ID int NOT NULL,
	Boat_ID int NOT NULL,
	FOREIGN KEY (Member_ID) REFERENCES Members (ID),
	FOREIGN KEY (Boat_ID) REFERENCES Boats (ID),
	);

	--ALTER TABLE Members
	--ADD CONSTRAINT DEFAULT Member_Image "Male1.jpg" FOR Member_Image;