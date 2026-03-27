USE SailClubDatabase;
GO

INSERT INTO Boats
VALUES ('16-1111', 1, 20, 10, 30, 1980, 'Good', 1),
('16-2222', 2, 15, 15, 25, 1990, 'Ok', 2),
('16-3333', 3, 25, 15, 35, 2000, 'Bad', 1);

--INSERT INTO MemberTypes
--VALUES (1, 'Junior'),
--(2, 'Adult'),
--(3, 'Senior');

--INSERT INTO MemberRoles
--VALUES (1, 'Admin'),
--(2, 'Member'),
--(3, 'Chairman');

USE SailClubDatabase;
GO
INSERT INTO Members
VALUES ('Poul', 'Hansen', '10101010', 'Gade 123', 'København', 'Mail01@gmail.dk', 1, 1, 'Default.jpg'),
('Per', 'Hansen', '20202020', 'Gade 123', 'København', 'Mail02@gmail.dk', 2, 2, 'Default.jpg' );

INSERT INTO Bookings
VALUES ('2020-01-01', '2020-02-02', 'Destination A', '10101010', '16-1111');
INSERT INTO Bookings 
VALUES ('2020-01-01', '2020-02-02', 'Destination A', '10101010', '16-1111');
INSERT INTO Bookings 
VALUES (GETDATE(), '2020-02-02', 'Destination A', '10101010', '16-1111');