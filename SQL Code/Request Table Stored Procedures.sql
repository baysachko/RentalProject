USE RentalProject;
GO

-- Get all requests with related data
CREATE PROCEDURE sp_GetAllRequests
AS
BEGIN
    SELECT 
        r.RequestID,
        r.RequestDate,
        r.Description,
        r.Status,
        r.ResidentID,
        r.ServiceID,
        res.ResidentName,
        s.ServiceName
    FROM Request r
    INNER JOIN Resident res ON r.ResidentID = res.ResidentID
    INNER JOIN Service s ON r.ServiceID = s.ServiceID
    ORDER BY r.RequestID DESC;
END;
GO

-- Get request by ID
CREATE PROCEDURE sp_GetRequestById
    @RequestID INT
AS
BEGIN
    SELECT 
        r.RequestID,
        r.RequestDate,
        r.Description,
        r.Status,
        r.ResidentID,
        r.ServiceID,
        res.ResidentName,
        s.ServiceName
    FROM Request r
    INNER JOIN Resident res ON r.ResidentID = res.ResidentID
    INNER JOIN Service s ON r.ServiceID = s.ServiceID
    WHERE r.RequestID = @RequestID;
END;
GO

-- Search requests
CREATE PROCEDURE sp_SearchRequests
    @SearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT 
        r.RequestID,
        r.RequestDate,
        r.Description,
        r.Status,
        r.ResidentID,
        r.ServiceID,
        res.ResidentName,
        s.ServiceName
    FROM Request r
    INNER JOIN Resident res ON r.ResidentID = res.ResidentID
    INNER JOIN Service s ON r.ServiceID = s.ServiceID
    WHERE res.ResidentName LIKE @SearchTerm + '%'
    OR r.Description LIKE @SearchTerm + '%'
    OR r.Status LIKE @SearchTerm + '%'
    OR s.ServiceName LIKE @SearchTerm + '%'
    ORDER BY r.RequestID DESC;
END;
GO

-- Get resident by ID (helper)
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

-- Get service by ID (helper)
CREATE PROCEDURE sp_GetServiceNameById
    @ServiceID INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @ServiceID)
    BEGIN
        THROW 50002, 'Service not found', 1;
        RETURN;
    END

    SELECT ServiceName
    FROM Service
    WHERE ServiceID = @ServiceID;
END;
GO

-- Insert request
CREATE PROCEDURE sp_InsertRequest
    @RequestDate DATE,
    @Description NVARCHAR(MAX),
    @Status NVARCHAR(50),
    @ResidentID INT,
    @ServiceID INT
AS
BEGIN
    -- Validate inputs
    IF NOT EXISTS (SELECT 1 FROM Resident WHERE ResidentID = @ResidentID)
    BEGIN
        THROW 50001, 'Resident not found', 1;
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @ServiceID)
    BEGIN
        THROW 50002, 'Service not found', 1;
        RETURN;
    END

    IF @Status NOT IN ('Pending', 'Confirmed', 'Completed', 'Cancelled')
    BEGIN
        THROW 50003, 'Invalid status value', 1;
        RETURN;
    END

    -- Insert the request
    INSERT INTO Request (
        RequestDate,
        Description,
        Status,
        ResidentID,
        ServiceID
    )
    VALUES (
        @RequestDate,
        @Description,
        @Status,
        @ResidentID,
        @ServiceID
    );

    SELECT SCOPE_IDENTITY() AS RequestID;
END;
GO

-- Update request
CREATE PROCEDURE sp_UpdateRequest
    @RequestID INT,
    @RequestDate DATE,
    @Description NVARCHAR(MAX),
    @Status NVARCHAR(50),
    @ResidentID INT,
    @ServiceID INT
AS
BEGIN
    -- Validate request exists
    IF NOT EXISTS (SELECT 1 FROM Request WHERE RequestID = @RequestID)
    BEGIN
        THROW 50004, 'Request not found', 1;
        RETURN;
    END

    -- Validate inputs
    IF NOT EXISTS (SELECT 1 FROM Resident WHERE ResidentID = @ResidentID)
    BEGIN
        THROW 50001, 'Resident not found', 1;
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @ServiceID)
    BEGIN
        THROW 50002, 'Service not found', 1;
        RETURN;
    END

    IF @Status NOT IN ('Pending', 'Confirmed', 'Completed', 'Cancelled')
    BEGIN
        THROW 50003, 'Invalid status value', 1;
        RETURN;
    END

    -- Update the request
    UPDATE Request
    SET 
        RequestDate = @RequestDate,
        Description = @Description,
        Status = @Status,
        ResidentID = @ResidentID,
        ServiceID = @ServiceID
    WHERE RequestID = @RequestID;
END;
GO

-- Delete request
CREATE PROCEDURE sp_DeleteRequest
    @RequestID INT
AS
BEGIN
    -- Validate request exists
    IF NOT EXISTS (SELECT 1 FROM Request WHERE RequestID = @RequestID)
    BEGIN
        THROW 50004, 'Request not found', 1;
        RETURN;
    END

    -- Check if request can be deleted (example: don't delete completed requests)
    IF EXISTS (SELECT 1 FROM Request WHERE RequestID = @RequestID AND Status = 'Completed')
    BEGIN
        THROW 50005, 'Cannot delete completed requests', 1;
        RETURN;
    END

    -- Delete the request
    DELETE FROM Request
    WHERE RequestID = @RequestID;
END;
GO