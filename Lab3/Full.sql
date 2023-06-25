--����� ����� CLR - CLR (Common Language Runtime) � ������������ ����� ���������� � ����������� ������, �� ������� �����������  ��� ����������,
--���������� � ����� .NET. �������� ����������� ��������� VES �� �������� Microsoft.
--�������� � ���� JIT-���������� (Just-In-Time).
--��� ����� ������ - ���� ����� ������ � ����������� ���� � �������� ����
--��� ����� dll -����������� ������������ ���������� ����������� ������������ ������������� ���������� ������������ ������������.
--����� ����� �� clr ������ ���������  --��� ���������� ������������������

--CREATE ASSEMBLY FROM .. -- ��� ���� ������� ���������� assembly

Use Trucking
sp_configure 'clr enabled',1 --�������� ��������� CLR
go
alter database [Trucking] set trustworthy on

create type route
external name KruglikLab.Route -- ������� ���

drop type route

declare @mytask as route
set @mytask = 'okay	, 2'
print @mytask.ToString()									--��������� ������������ ��� ������, Route Parse �������� ��� ��������

CREATE PROCEDURE spSendEmail
@receiver nvarchar(30)
AS    
EXTERNAL NAME KruglikLab.[KruglikLab.StoredProcedures].SendEmailUsingCLR

drop procedure spsendemail

exec spSendEmail 'lilkrug2003@gmail.com' --����� ��������� �������� ��� ����� ����������
