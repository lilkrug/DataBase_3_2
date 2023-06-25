
CREATE TRIGGER OnAfterInsertRowCity
         AFTER INSERT
            ON City
      FOR EACH ROW
BEGIN
    INSERT INTO AUDIT (
                          TableName,
                          ActionName,
                          RowOption,
                          RowNumberChanged
                      )
                      VALUES (
                          "City",
                          "INSERT",
                          "ROW",
                          1
                      );
END;