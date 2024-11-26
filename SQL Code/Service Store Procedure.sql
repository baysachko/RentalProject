USE RentalProject;
GO

-- Get all services
CREATE PROCEDURE sp_GetAllServices
AS
BEGIN
    SELECT 
        ServiceID,
        ServiceName,
        ServiceDescription,
        Cost
    FROM Service
    ORDER BY ServiceID DESC;
END;
GO

-- Get service by ID
CREATE PROCEDURE sp_GetServiceById
    @ServiceID INT
AS
BEGIN
    SELECT 
        ServiceID,
        ServiceName,
        ServiceDescription,
        Cost
    FROM Service
    WHERE ServiceID = @ServiceID;
END;
GO

-- Search services
CREATE PROCEDURE sp_SearchServices
    @SearchTerm NVARCHAR(100)
AS
BEGIN
    SELECT 
        ServiceID,
        ServiceName,
        ServiceDescription,
        Cost
    FROM Service
    WHERE ServiceName LIKE @SearchTerm + '%'
    OR ServiceDescription LIKE @SearchTerm + '%'
    OR CAST(Cost AS NVARCHAR) LIKE @SearchTerm + '%'
    ORDER BY ServiceID DESC;
END;
GO

-- Insert service
CREATE PROCEDURE sp_InsertService
    @ServiceName NVARCHAR(100),
    @ServiceDescription NVARCHAR(MAX),
    @Cost DECIMAL(10,2)
AS
BEGIN
    -- Validate service name is not empty
    IF LTRIM(RTRIM(@ServiceName)) = ''
    BEGIN
        THROW 50001, 'Service name cannot be empty', 1;
        RETURN;
    END

    -- Validate cost is not negative
    IF @Cost < 0
    BEGIN
        THROW 50002, 'Cost cannot be negative', 1;
        RETURN;
    END

    -- Check for duplicate service name
    IF EXISTS (SELECT 1 FROM Service WHERE ServiceName = @ServiceName)
    BEGIN
        THROW 50003, 'Service name already exists', 1;
        RETURN;
    END

    INSERT INTO Service (
        ServiceName,
        ServiceDescription,
        Cost
    )
    VALUES (
        @ServiceName,
        @ServiceDescription,
        @Cost
    );

    SELECT SCOPE_IDENTITY() AS ServiceID;
END;
GO

-- Update service
CREATE PROCEDURE sp_UpdateService
    @ServiceID INT,
    @ServiceName NVARCHAR(100),
    @ServiceDescription NVARCHAR(MAX),
    @Cost DECIMAL(10,2)
AS
BEGIN
    -- Validate service exists
    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @ServiceID)
    BEGIN
        THROW 50004, 'Service not found', 1;
        RETURN;
    END

    -- Validate service name is not empty
    IF LTRIM(RTRIM(@ServiceName)) = ''
    BEGIN
        THROW 50001, 'Service name cannot be empty', 1;
        RETURN;
    END

    -- Validate cost is not negative
    IF @Cost < 0
    BEGIN
        THROW 50002, 'Cost cannot be negative', 1;
        RETURN;
    END

    -- Check for duplicate service name (excluding current service)
    IF EXISTS (SELECT 1 FROM Service WHERE ServiceName = @ServiceName AND ServiceID != @ServiceID)
    BEGIN
        THROW 50003, 'Service name already exists', 1;
        RETURN;
    END

    UPDATE Service
    SET 
        ServiceName = @ServiceName,
        ServiceDescription = @ServiceDescription,
        Cost = @Cost
    WHERE ServiceID = @ServiceID;
END;
GO

-- Delete service
CREATE PROCEDURE sp_DeleteService
    @ServiceID INT
AS
BEGIN
    -- Validate service exists
    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @ServiceID)
    BEGIN
        THROW 50004, 'Service not found', 1;
        RETURN;
    END

    -- Check for existing requests using this service
    IF EXISTS (SELECT 1 FROM Request WHERE ServiceID = @ServiceID)
    BEGIN
        THROW 50005, 'Cannot delete service because it is referenced in requests', 1;
        RETURN;
    END

    DELETE FROM Service
    WHERE ServiceID = @ServiceID;
END;
GO