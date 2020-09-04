IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EmployeeSystem')
CREATE DATABASE EmployeeSystem;
GO
USE EmployeeSystem
--dropping tables
IF OBJECT_ID('tblEmployees') IS NOT NULL 
DROP TABLE tblEmployees;

IF OBJECT_ID('tblSectors') IS NOT NULL 
DROP TABLE tblSectors;

IF OBJECT_ID('tblGenders') IS NOT NULL 
DROP TABLE tblGenders;

IF OBJECT_ID('tblLocations') IS NOT NULL 
DROP TABLE tblLocations;

IF OBJECT_ID('vwEmployees') IS NOT NULL 
DROP VIEW vwEmployees

--creating tables
--locations
CREATE TABLE tblLocations(
	locationId INT PRIMARY KEY IDENTITY(1,1),
	country VARCHAR(25) NOT NULL,
	city VARCHAR(25) NOT NULL,
	street VARCHAR(30) NOT NULL,
);

--sector
CREATE TABLE tblSectors(
	sectorId INT PRIMARY KEY IDENTITY(1,1),
	sectorName VARCHAR(40)
);


--gender
CREATE TABLE tblGenders(
	genderId INT PRIMARY KEY IDENTITY(1,1),
	genderName VARCHAR(10)
	);

--employees
CREATE TABLE tblEmployees(
	employeeId INT PRIMARY KEY IDENTITY(1,1),
	fullname VARCHAR(30) NOT NULL,
	IdentityCardNumber CHAR(9) NOT NULL,
	jmbg CHAR(13) NOT NULL,
	phone VARCHAR(15) NOT NULL,
	dateOfBirth DATE not null,
	managerId INT,
	--foreign keys
	locationId INT FOREIGN KEY REFERENCES  tblLocations(locationId) ON DELETE SET NULL,
	genderId INT FOREIGN KEY REFERENCES tblGenders(genderId) ON DELETE SET NULL,
	sectorId INT FOREIGN KEY REFERENCES tblSectors(sectorId) ON DELETE SET NULL,
	);

INSERT INTO tblGenders(genderName)
VALUES ('male'),('female'),('other');

INSERT INTO tblSectors(sectorName)
VALUES ('Marketing Department'),('Logistics Department'), ('Sales Department'),('Finance Department');

GO
CREATE VIEW vwEmployees
as
select e.employeeId, e.fullname, e.IdentityCardNumber, e.jmbg, genderName, e.dateOfBirth, e.phone,
CONCAT(l.street, ',', l.city, ',', l.country) AS location, sectorName, m.fullname AS manager
from tblEmployees e
inner join tblLocations l
on e.locationId = l.locationId
inner join tblGenders g
on g.genderId = e.genderId
inner join tblSectors s
on e.sectorId = s.sectorId
left join tblEmployees m
on e.managerId = m.employeeId;