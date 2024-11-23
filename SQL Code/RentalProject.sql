CREATE DATABASE RentalProject;
GO
USE RentalProject;
GO
-- Create base tables first
CREATE TABLE Room (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber NVARCHAR(20)
);
GO

CREATE TABLE Resident (
    ResidentID INT PRIMARY KEY IDENTITY(1,1),
    ResidentName NVARCHAR(100)
);
GO

CREATE TABLE Service (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(100),
    ServiceDescription NVARCHAR(MAX),
    Cost DECIMAL(10,2)
);
GO

-- Create dependent tables
CREATE TABLE Reservation (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    ReservationDate DATE,
    StartDate DATE,
    EndDate DATE,
    Status NVARCHAR(50),
    ResidentID INT,
    RoomID INT,
    ResidentName NVARCHAR(100),
    RoomNumber NVARCHAR(20),
    FOREIGN KEY (ResidentID) REFERENCES Resident(ResidentID),
    FOREIGN KEY (RoomID) REFERENCES Room(RoomID)
);
GO

CREATE TABLE Request (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    RequestDate DATE,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50),
    ResidentID INT,
    ServiceID INT,
    ResidentName NVARCHAR(100),
    ServiceName NVARCHAR(100),
    FOREIGN KEY (ResidentID) REFERENCES Resident(ResidentID),
    FOREIGN KEY (ServiceID) REFERENCES Service(ServiceID)
);
GO

CREATE TABLE Rent (
    RentID INT PRIMARY KEY IDENTITY(1,1),
    StartDate DATE,
    EndDate DATE,
    RentAmount DECIMAL(10,2),
    ResidentID INT,
    RoomID INT,
    ResidentName NVARCHAR(100),
    RoomNumber NVARCHAR(20),
    FOREIGN KEY (ResidentID) REFERENCES Resident(ResidentID),
    FOREIGN KEY (RoomID) REFERENCES Room(RoomID)
);
GO

-- Create triggers (each in its own batch)
CREATE TRIGGER trg_UpdateReservationDenormalizedData
ON Reservation
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE r
    SET 
        r.ResidentName = res.ResidentName,
        r.RoomNumber = rm.RoomNumber
    FROM Reservation r
    JOIN Resident res ON r.ResidentID = res.ResidentID
    JOIN Room rm ON r.RoomID = rm.RoomID
    WHERE r.ReservationID IN (SELECT ReservationID FROM inserted);
END;
GO

CREATE TRIGGER trg_UpdateRequestDenormalizedData
ON Request
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE r
    SET 
        r.ResidentName = res.ResidentName,
        r.ServiceName = s.ServiceName
    FROM Request r
    JOIN Resident res ON r.ResidentID = res.ResidentID
    JOIN Service s ON r.ServiceID = s.ServiceID
    WHERE r.RequestID IN (SELECT RequestID FROM inserted);
END;
GO

CREATE TRIGGER trg_UpdateRentDenormalizedData
ON Rent
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE r
    SET 
        r.ResidentName = res.ResidentName,
        r.RoomNumber = rm.RoomNumber
    FROM Rent r
    JOIN Resident res ON r.ResidentID = res.ResidentID
    JOIN Room rm ON r.RoomID = rm.RoomID
    WHERE r.RentID IN (SELECT RentID FROM inserted);
END;
GO

-- Create indexes
CREATE INDEX IX_Reservation_Dates ON Reservation(ReservationDate, StartDate, EndDate);
CREATE INDEX IX_Request_Date ON Request(RequestDate);
CREATE INDEX IX_Rent_Dates ON Rent(StartDate, EndDate);
CREATE INDEX IX_Resident_Name ON Resident(ResidentName);
CREATE INDEX IX_Room_Number ON Room(RoomNumber);
CREATE INDEX IX_Service_Name ON Service(ServiceName);