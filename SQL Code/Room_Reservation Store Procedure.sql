USE RentalProject;
GO

-- Get all reservations with related data
CREATE PROCEDURE sp_GetAllReservations
AS
BEGIN
    SELECT 
        r.ReservationID,
        r.ReservationDate,
        r.StartDate,
        r.EndDate,
        r.Status,
        r.ResidentID,
        r.RoomID,
        res.ResidentName,
        rm.RoomNumber
    FROM Reservation r
    LEFT JOIN Resident res ON r.ResidentID = res.ResidentID
    LEFT JOIN Room rm ON r.RoomID = rm.RoomID
    ORDER BY r.ReservationID DESC;
END;
GO

-- Get reservation by ID
CREATE PROCEDURE sp_GetReservationById
    @ReservationID INT
AS
BEGIN
    SELECT 
        r.ReservationID,
        r.ReservationDate,
        r.StartDate,
        r.EndDate,
        r.Status,
        r.ResidentID,
        r.RoomID,
        res.ResidentName,
        rm.RoomNumber
    FROM Reservation r
    LEFT JOIN Resident res ON r.ResidentID = res.ResidentID
    LEFT JOIN Room rm ON r.RoomID = rm.RoomID
    WHERE r.ReservationID = @ReservationID;
END;
GO

-- Insert new reservation with validation
CREATE PROCEDURE sp_InsertReservation
    @ReservationDate DATE,
    @StartDate DATE,
    @EndDate DATE,
    @Status NVARCHAR(50),
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
    IF @StartDate > @EndDate
    BEGIN
        THROW 50003, 'Start date cannot be after end date', 1;
        RETURN;
    END

    -- Check for overlapping reservations
    IF EXISTS (
        SELECT 1 
        FROM Reservation 
        WHERE RoomID = @RoomID 
        AND Status != 'Cancelled'
        AND (
            (@StartDate BETWEEN StartDate AND EndDate)
            OR (@EndDate BETWEEN StartDate AND EndDate)
            OR (StartDate BETWEEN @StartDate AND @EndDate)
        )
    )
    BEGIN
        THROW 50004, 'Room is already reserved for this period', 1;
        RETURN;
    END

    -- Insert the reservation
    INSERT INTO Reservation (
        ReservationDate,
        StartDate,
        EndDate,
        Status,
        ResidentID,
        RoomID
    )
    VALUES (
        @ReservationDate,
        @StartDate,
        @EndDate,
        @Status,
        @ResidentID,
        @RoomID
    );
    
    -- Return the inserted ID
    SELECT SCOPE_IDENTITY() AS ReservationID;
END;
GO

-- Update existing reservation with validation
CREATE PROCEDURE sp_UpdateReservation
    @ReservationID INT,
    @ReservationDate DATE,
    @StartDate DATE,
    @EndDate DATE,
    @Status NVARCHAR(50),
    @ResidentID INT,
    @RoomID INT
AS
BEGIN
    -- Check if reservation exists
    IF NOT EXISTS (SELECT 1 FROM Reservation WHERE ReservationID = @ReservationID)
    BEGIN
        THROW 50005, 'Reservation not found', 1;
        RETURN;
    END

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
    IF @StartDate > @EndDate
    BEGIN
        THROW 50003, 'Start date cannot be after end date', 1;
        RETURN;
    END

    -- Check for overlapping reservations (excluding current reservation)
    IF EXISTS (
        SELECT 1 
        FROM Reservation 
        WHERE RoomID = @RoomID 
        AND ReservationID != @ReservationID
        AND Status != 'Cancelled'
        AND (
            (@StartDate BETWEEN StartDate AND EndDate)
            OR (@EndDate BETWEEN StartDate AND EndDate)
            OR (StartDate BETWEEN @StartDate AND @EndDate)
        )
    )
    BEGIN
        THROW 50004, 'Room is already reserved for this period', 1;
        RETURN;
    END

    -- Update the reservation
    UPDATE Reservation
    SET 
        ReservationDate = @ReservationDate,
        StartDate = @StartDate,
        EndDate = @EndDate,
        Status = @Status,
        ResidentID = @ResidentID,
        RoomID = @RoomID
    WHERE ReservationID = @ReservationID;
END;
GO

-- Delete reservation with validation
CREATE PROCEDURE sp_DeleteReservation
    @ReservationID INT
AS
BEGIN
    -- Check if reservation exists
    IF NOT EXISTS (SELECT 1 FROM Reservation WHERE ReservationID = @ReservationID)
    BEGIN
        THROW 50005, 'Reservation not found', 1;
        RETURN;
    END

    -- Check if the reservation can be deleted (you might want to add more business rules here)
    IF EXISTS (
        SELECT 1 
        FROM Reservation 
        WHERE ReservationID = @ReservationID 
        AND Status = 'Confirmed'
        AND StartDate <= GETDATE()
        AND EndDate >= GETDATE()
    )
    BEGIN
        THROW 50006, 'Cannot delete an active confirmed reservation', 1;
        RETURN;
    END

    -- Delete the reservation
    DELETE FROM Reservation
    WHERE ReservationID = @ReservationID;
END;
GO

-- Helper stored procedures for lookups
CREATE PROCEDURE sp_GetResidentById
    @ResidentID INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Resident WHERE ResidentID = @ResidentID)
    BEGIN
        THROW 50001, 'Resident not found', 1;
        RETURN;
    END

    SELECT 
        r.ResidentName,
        ISNULL((
            SELECT TOP 1 RoomID 
            FROM Rent 
            WHERE ResidentID = @ResidentID 
            ORDER BY StartDate DESC
        ), '') as RoomID
    FROM Resident r
    WHERE r.ResidentID = @ResidentID;
END;
GO

CREATE PROCEDURE sp_GetRoomById
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