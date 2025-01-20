CREATE TRIGGER [dbo].[Stories_UPDATE] ON [dbo].[Stories]
    AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

    UPDATE S
    SET LastUpdated = GETDATE()
    FROM dbo.Stories AS S
    INNER JOIN INSERTED AS I
        ON S.Id = I.Id
END