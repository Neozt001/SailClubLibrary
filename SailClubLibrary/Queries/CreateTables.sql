--DROP DATABASE IF EXISTS SailClubDatabase
--CREATE DATABASE SailClubDatabase 
USE SailClubDatabase;
GO

--DROP TABLE IF EXISTS Boats;
--DROP TABLE IF EXISTS Members;
--DROP TABLE IF EXISTS Bookings;
CREATE TABLE Boats(
	Boat_Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Boat_SailNumber nvarchar(7) NOT NULL UNIQUE,
	Boat_Model int NOT NULL,
	Boat_Draft int NOT NULL,
	Boat_Width int NOT NULL,
	Boat_Length int NOT NULL,
	Boat_YearOfConstruction int NOT NULL,
	Boat_EngineInfo nvarchar(50),
	Boat_TheBoatType int
	);

CREATE TABLE Members(
	Member_Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Member_FirstName nvarchar(20) NOT NULL,
	Member_SurName nvarchar(20) NOT NULL,
	Member_PhoneNumber nvarchar(8),
	Member_Address nvarchar(30) NOT NULL,
	Member_City nvarchar(20) NOT NULL,
	Member_Mail nvarchar(30) NOT NULL,
	Member_TheMemberType int NOT NULL,
	Member_TheMemberRole int NOT NULL,
	);

CREATE TABLE Bookings(
	Booking_Id int IDENTITY(1,1) PRIMARY KEY,
	Booking_StartDate date NOT NULL,
	Booking_EndDate date NOT NULL,
	Booking_Destionation nvarchar(20) NOT NULL,
	Booking_PhoneNumber nvarchar(8) NOT NULL,
	Booking_SailNumber nvarchar(7) NOT NULL,
	FOREIGN KEY (Booking_Id) REFERENCES Members (Member_Id),
	FOREIGN KEY (Booking_SailNumber) REFERENCES Boats (Boat_SailNumber),
	);