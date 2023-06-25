--3.	Продемонстрируйте заимствование прав для любой процедуры в базе данных.
USE TRANS;

CREATE LOGIN AlexOne with PASSWORD = 'AlexOne';
CREATE LOGIN AlexTwo with PASSWORD = 'AlexTwo';
CREATE USER AlexOne for LOGIN AlexOne;
CREATE USER AlexTwo for LOGIN AlexTwo;

USE TRANS;
EXEC sp_addrolemember 'db_datareader', 'AlexOne';
EXEC sp_addrolemember 'db_ddladmin', 'AlexOne';
DENY SELECT on TRANS.dbo.City TO AlexTwo;

GO
CREATE or ALTER PROCEDURE us_proc_GetTableTrans
WITH EXECUTE AS 'AlexOne'
AS
SELECT * FROM TRANS.dbo.City;

ALTER AUTHORIZATION ON  us_proc_GetTableTrans TO AlexOne;

GRANT EXECUTE ON us_proc_GetTableTrans to AlexTwo;

SETUSER 'AlexTwo';
SELECT * FROM TRANS.dbo.City;
EXEC us_proc_GetTableTrans;

--4.Создать для экземпляра SQL Server объект аудита. 
--5.Задать для серверного аудита необходимые спецификации.
--6.Запустить серверный аудит, продемонстрировать журнал аудита.

USE MASTER;

CREATE SERVER AUDIT TableTransAudit
TO FILE
(
 FILEPATH = 'D:\ALEX\STUDY\6SEM_3COURSE\Программирование и безопасность баз данных web-приложений\Готовые лабораторные\Lab 10',
 MAXSIZE = 100 MB
)
WITH (QUEUE_DELAY = 1000, ON_FAILURE = CONTINUE);

alter server audit TableTransAudit with ( state = on );

select * from fn_get_audit_file( 'D:\ALEX\STUDY\6SEM_3COURSE\Программирование и безопасность баз данных web-приложений\Готовые лабораторные\Lab 10\TableTransAudit_4F715012-85DF-4995-B8CD-0A9F9B2A54B2_0_132935368672480000.sqlaudit', null, null ) order by event_time desc,sequence_number

--7.Создать необходимые объекты аудита.
--8.Задать для аудита необходимые спецификации.
--9.Запустить аудит БД, продемонстрировать журнал аудита
USE TRANS  
GO    
CREATE DATABASE AUDIT SPECIFICATION Specification_TableTransAudit
FOR SERVER AUDIT TableTransAudit
add ( select on object::[dbo].City by [public]);

select * from dbo.City;
alter database AUDIT SPECIFICATION Specification_TableTransAudit with (state = on);

--10.Остановить аудит БД и сервера
GO
alter server audit TableTransAudit with ( state = off );
alter database AUDIT SPECIFICATION Specification_TableTransAudit with (state = off);

--11.Создать для экземпляра SQL Server ассиметричный ключ шифрования.
USE TRANS
GO
CREATE ASYMMETRIC KEY LabKey   
    WITH ALGORITHM = RSA_2048   
    ENCRYPTION BY PASSWORD = 'LabKey';   
--12.	Зашифровать и расшифровать данные при помощи этого ключа.

GO
USE TRANS
CREATE TABLE CryptoData
(
	Id int PRIMARY KEY IDENTITY,
	PersonalData varchar(max)
)

GO
INSERT INTO CryptoData(PersonalData)
VALUES (ENCRYPTBYASYMKEY( AsymKey_ID('LabKey') , N'1111-2222-3333-4444'))

GO
SELECT * FROM CryptoData

GO
SELECT Id, CONVERT(nvarchar(max),  DecryptByAsymKey( AsymKey_Id('LabKey'), PersonalData, N'LabKey'))   
,DecryptByAsymKey( AsymKey_Id('LabKey'), PersonalData, N'LabKey')
AS DecryptedData   
FROM CryptoData
  

--13.	Создать для экземпляра SQL Server сертификат.
GO
USE TRANS
create certificate SampleCert
encryption by password = N'123456789'
with subject = N'Creation Target',
Expiry_DATE = N'01/05/2022';

--14.	Зашифровать и расшифровать данные при помощи этого сертификата.
GO
USE TRANS
INSERT INTO CryptoData values(EncryptByCert(Cert_ID('SampleCert'), N'Секретные данные'));

GO
SELECT * FROM CryptoData

GO
SELECT Id,(Convert(Nvarchar(100), DecryptByCert(Cert_ID('SampleCert'), PersonalData, N'123456789'))) FROM CryptoData;



--15.	Создать для экземпляра SQL Server симметричный ключ шифрования данных.
--drop Symmetric key SKey
GO
USE TRANS
create Symmetric key SKey
WITH ALGORITHM = AES_256  
ENCRYPTION BY PASSWORD = '123456789';

Open symmetric key SKey
Decryption by password = '123456789'

create Symmetric key SData
with Algorithm =  AES_256
encryption by symmetric key SKey;

Open symmetric key SData 
Decryption by symmetric key SKey;

create Master key encryption by password = N'Password';

-- 16.	Зашифровать и расшифровать данные при помощи этого ключа.
GO
USE TRANS
INSERT INTO CryptoData VALUES (ENCRYPTBYKEY( Key_GUID('SData') , N'Secret Data'))

GO
SELECT * FROM CryptoData

GO
SELECT Id, CONVERT(nvarchar(max),  DecryptByKey( PersonalData)) AS DecryptedData  FROM CryptoData
GO 

--17.	Продемонстрировать прозрачное шифрование базы данных.
USE master;  

GO  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Password';  

go
CREATE CERTIFICATE MyServerCert WITH SUBJECT = 'My  Certificate';  

go  
USE TRANS

GO  
CREATE DATABASE ENCRYPTION KEY  
WITH ALGORITHM = AES_128  
ENCRYPTION BY SERVER CERTIFICATE MyServerCert;  

GO  
ALTER DATABASE TRANS  
SET ENCRYPTION ON;  
  

--18.	Продемонстрировать применение хэширования.
GO
select HASHBYTES('SHA1', 'Hash example');

--19.	Продемонстрировать применение Электронной подписи(ЭЦП) при помощи сертификата.
GO
select SignByCert(Cert_Id( 'SampleCert' ),'Secrect Info', N'123456789')

--20.	Сделать резервную копию необходимых ключей и сертификатов.
Backup certificate SampleCert
to File = N'D:\ALEX\STUDY\6SEM_3COURSE\Программирование и безопасность баз данных web-приложений\Готовые лабораторные\Lab 10\BackupSampleCert.cer'
with private key (
File = N'D:\ALEX\STUDY\6SEM_3COURSE\Программирование и безопасность баз данных web-приложений\Готовые лабораторные\Lab 10\BackupSampleCert.pvk',
Encryption by password = N'123456789',
Decryption by password = N'123456789');





