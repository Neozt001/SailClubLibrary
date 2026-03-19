--CREATE DATABASE SailClubDatabase 

CREATE TABLE Boats(
	Boat_Id int NOT NULL,
	Boat_SailNumber nvarchar(7) NOT NULL PRIMARY KEY,
	Boat_Model int NOT NULL,
	Boat_Draft int NOT NULL,
	Boat_Width int NOT NULL,
	Boat_Length int NOT NULL,
	Boat_YearOfConstruction int NOT NULL,
	Boat_EngineInfo nvarchar(50),
	Boat_TheBoatType_Id int
	);

CREATE TABLE Members(
	Member_Id int,
	Member_FirstName nvarchar(20),
	Member_SurName nvarchar(20),
	Member_PhoneNumber nvarchar(8) PRIMARY KEY,
	Member_Address nvarchar(30),
	Member_City nvarchar(20),
	Member_Mail nvarchar(30),
	Member_TheMemberType_Id int,
	Member_TheMemberRole_Id int,
	);

CREATE TABLE Bookings(
	Booking_Id int,
	Booking_StartDate date,
	Booking_EndDate date,
	Booking_Destionation nvarchar(20),
	Booking_Member_PhoneNumber nvarchar(8),
	Booking_Boat_SailNumber nvarchar(7),
	);