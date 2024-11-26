USE RentalProject;
GO

-- Get all rents with related data
CREATE PROCEDURE sp_GetAllRents
AS
BEGIN
    SELECT 
        r.RentID,
        r.StartDate,
        r.EndDate,
        r.RentAmount,
        r.ResidentID,
        r.RoomID,
        res.ResidentName,
        rm.RoomNumber
    FROM Rent r
    INNER JOIN Resident res ON r.ResidentID = res.ResidentID
    INNER JOIN Room rm ON r.RoomID = rm.RoomID
    ORDER BY r.RentID DESC;
END;
GO

-- Search rents
CREATE PROCEDURE sp_SearchRents
    @SearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT 
        r.RentID,
        r.StartDate,
        r.EndDate,
        r.RentAmount,
        r.ResidentID,
        r.RoomID,
        res.ResidentName,
        rm.RoomNumber
    FROM Rent r
    INNER JOIN Resident res ON r.ResidentID = res.ResidentID
    INNER JOIN Room rm ON r.RoomID = rm.RoomID
    WHERE res.ResidentName LIKE @SearchTerm + '%'
    ORDER BY r.RentID DESC;
END;
GO

-- Get resident name by ID
CREATE PROCEDURE sp_GetResidentNameById
    @ResidentID INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Resident WHERE ResidentID = @ResidentID)
    BEGIN
        THROW 50001, 'Resident not found', 1;
        RETURN;
    END

    SELECT ResidentName
    FROM Resident
    WHERE ResidentID = @ResidentID;
END;
GO

-- Get room number by ID
CREATE PROCEDURE sp_GetRoomNumberById
    @RoomID INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Room WHERE RoomID = @RoomID)
    BEGIN
        THROW 50002, 'Room not found', 1;
        RETURN;
    END

    SELECT RoomNumber
    FROM Room
    WHERE RoomID = @RoomID;
END;
GO

-- Insert rent with validation
CREATE PROCEDURE sp_InsertRent
    @StartDate DATE,
    @EndDate DATE,
    @RentAmount DECIMAL(10,2),
    @ResidentID INT,
    @RoomID INT
AS
BEGIN
    -- Validate resident exists
    IF NOT EXISTS (SELECT 1 FROM Resident WHERE ResidentID = @ResidentID)
    BEGIN
        THROW 50001, 'Resident not found', 1;
        RETURN;
    END

    -- Validate room exists
    IF NOT EXISTS (SELECT 1 FROM Room WHERE RoomID = @RoomID)
    BEGIN
        THROW 50002, 'Room not found', 1;
        RETURN;
    END

    -- Validate dates
    IF @EndDate < @StartDate
    BEGIN
        THROW 50003, 'End date cannot be earlier than start date', 1;
        RETURN;
    END

    -- Validate rent amount
    IF @RentAmount <= 0
    BEGIN
        THROW 50004, 'Rent amount must be greater than zero', 1;
        RETURN;
    END

    -- Check for overlapping rent periods
    IF EXISTS (
        SELECT 1 
        FROM Rent 
        WHERE RoomID = @RoomID 
        AND (
            (@StartDate BETWEEN StartDate AND EndDate)
            OR (@EndDate BETWEEN StartDate AND EndDate)
            OR (StartDate BETWEEN @StartDate AND @EndDate)
        )
    )
    BEGIN
        THROW 50005, 'Room is already rented during this period', 1;
        RETURN;
    END

    -- Insert the rent record
    INSERT INTO Rent (
        StartDate,
        EndDate,
        RentAmount,
        ResidentID,
        RoomID
    )
    VALUES (
        @StartDate,
        @EndDate,
        @RentAmount,
        @ResidentID,
        @RoomID
    );

    SELECT SCOPE_IDENTITY() AS RentID;
END;
GO

-- Update rent with validation
CREATE PROCEDURE sp_UpdateRent
    @RentID INT,
    @StartDate DATE,
    @EndDate DATE,
    @RentAmount DECIMAL(10,2)
AS
BEGIN
    -- Validate rent exists
    IF NOT EXISTS (SELECT 1 FROM Rent WHERE RentID = @RentID)
    BEGIN
        THROW 50006, 'Rent record not found', 1;
        RETURN;
    END

    -- Validate dates
    IF @EndDate < @StartDate
    BEGIN
        THROW 50003, 'End date cannot be earlier than start date', 1;
        RETURN;
    END

    -- Validate rent amount
    IF @RentAmount <= 0
    BEGIN
        THROW 50004, 'Rent amount must be greater than zero', 1;
        RETURN;
    END

    -- Get the RoomID for the current rent record
    DECLARE @CurrentRoomID INT;
    SELECT @CurrentRoomID = RoomID FROM Rent WHERE RentID = @RentID;

    -- Check for overlapping rent periods (excluding current record)
    IF EXISTS (
        SELECT 1 
        FROM Rent 
        WHERE RoomID = @CurrentRoomID 
        AND RentID != @RentID
        AND (
            (@StartDate BETWEEN StartDate AND EndDate)
            OR (@EndDate BETWEEN StartDate AND EndDate)
            OR (StartDate BETWEEN @StartDate AND @EndDate)
        )
    )
    BEGIN
        THROW 50005, 'Room is already rented during this period', 1;
        RETURN;
    END

    -- Update the rent record
    UPDATE Rent
    SET 
        StartDate = @StartDate,
        EndDate = @EndDate,
        RentAmount = @RentAmount
    WHERE RentID = @RentID;
END;
GO

-- Delete rent
CREATE PROCEDURE sp_DeleteRent
    @RentID INT
AS
BEGIN
    -- Validate rent exists
    IF NOT EXISTS (SELECT 1 FROM Rent WHERE RentID = @RentID)
    BEGIN
        THROW 50006, 'Rent record not found', 1;
        RETURN;
    END

    -- Check if this is an active rent
    IF EXISTS (
        SELECT 1 
        FROM Rent 
        WHERE RentID = @RentID 
        AND GETDATE() BETWEEN StartDate AND EndDate
    )
    BEGIN
        THROW 50007, 'Cannot delete an active rent record', 1;
        RETURN;
    END

    -- Delete the rent record
    DELETE FROM Rent
    WHERE RentID = @RentID;
END;
GO