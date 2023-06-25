alter database open;

--DROP TABLESPACE LOB_LAB;

CREATE TABLESPACE LOB_LAB
  DATAFILE 'LOB_LABB.dbf'
  SIZE 30M
  AUTOEXTEND ON NEXT 10M
  MAXSIZE 200M
  EXTENT MANAGEMENT LOCAL;

CREATE USER C##lob_lab_user IDENTIFIED BY 123456789
  DEFAULT TABLESPACE LOB_LAB
  ACCOUNT UNLOCK;
  
GRANT CREATE SESSION,
      CREATE TABLE,
      CREATE VIEW,
      CREATE PROCEDURE TO C##lob_lab_user;
      
GRANT CREATE ANY DIRECTORY,
      DROP ANY DIRECTORY TO C##lob_lab_user;
      
ALTER USER C##lob_lab_user QUOTA 100M ON LOB_LAB;

-- C##lob_lab_user
--DROP DIRECTORY BFILE_DIR;
CREATE DIRECTORY BFILE_DIR as 'Y:\Lab8';
GRANT READ ON DIRECTORY BFILE_DIR TO C##lob_lab_user;
--DROP TABLE lob_table;
CREATE TABLE lob_table (
  id   NUMBER(5)  PRIMARY KEY,
  JPG BLOB NOT NULL
)

SELECT * FROM lob_table;

DECLARE
  l_blob BLOB; 
  v_src_loc  BFILE;
  v_amount   BINARY_INTEGER;
BEGIN
  v_src_loc := BFILENAME('BFILE_DIR', 'Image.jpg');
  INSERT INTO lob_table  
  VALUES (1, empty_blob()) RETURN JPG INTO l_blob; 
  DBMS_LOB.FILEOPEN(v_src_loc, DBMS_LOB.LOB_READONLY);
  v_amount := DBMS_LOB.GETLENGTH(v_src_loc);
  DBMS_LOB.LOADFROMFILE(l_blob, v_src_loc, v_amount);
  DBMS_LOB.FILECLOSE(v_src_loc);
  COMMIT;
END;
-----
--DROP TABLE bfile_table;
CREATE TABLE bfile_table (
 name VARCHAR(255),
 doc BFILE 
)

delete bfile_table where NAME = 'docx';
INSERT INTO bfile_table VALUES ( 'docx', BFILENAME( 'BFILE_DIR', 'Document.docx' ) );
SELECT * FROM bfile_table;

INSERT INTO bfile_table VALUES ( 'jpg', BFILENAME( 'BFILE_DIR', 'Image.jpg' ) );
SELECT * FROM bfile_table;

DECLARE
  l_blob BLOB; 
  v_src_loc  BFILE;
  v_amount   BINARY_INTEGER;
BEGIN
  v_src_loc := BFILENAME('BFILE_DIR', 'Document.docx');
  INSERT INTO bfile_table  
  VALUES ('pdf', empty_blob()) RETURNING doc INTO l_blob;
  DBMS_LOB.FILEOPEN(v_src_loc, DBMS_LOB.LOB_READONLY);
  v_amount := DBMS_LOB.GETLENGTH(v_src_loc);
  DBMS_LOB.LOADFROMFILE(l_blob, v_src_loc, v_amount);
  DBMS_LOB.FILECLOSE(v_src_loc);
  COMMIT;
END;
