use Trucking;
/*▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ PROCEDURES ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬*/

go 
Create procedure TOrder 
AS
begin
	SELECT * FROM [ORDER];
end;

go 
Create procedure TCity
AS
begin
	SELECT * FROM City;
end;

go 
Create procedure TRoute
AS
begin
	SELECT * FROM Route;
end;

go 
Create procedure TCustomer 
AS
begin
	SELECT * FROM Customer;
end;

go 
Create procedure TServiceType 
AS
begin
	SELECT * FROM ServiceType ;
end;

go 
Create procedure TServices 
AS
begin
	SELECT * FROM Service ;
end;

EXECUTE TOrder;
EXECUTE TCity;
EXECUTE TRoute;
EXECUTE TCustomer;
EXECUTE TServiceType;
EXECUTE TServices;

drop procedure TServices;
/*▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ FUNCTIONS ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬*/

go
Create FUNCTION TGetOrdersQByCompany(@companyName varchar(300)) returns int
as
begin
	return (SELECT COUNT(*) FROM [ORDER] WHERE CustomerName = @companyName);
end;


go
Create FUNCTION TGetOrdersByDate(@start datetime,@end datetime) returns table
as
	return (SELECT * FROM [Order] WHERE OrderDate between @start and @end);

go
Create FUNCTION TGetRelevantRoutesByCompany(@companyName varchar(300)) returns table
as
	return (SELECT C.RouteName 'Название пути',isnull(Count(B.Id),'0') 'Количество поездок' FROM [Service] A inner join [Order] B 
	on A.Id = B.ServiceId left outer join Route C on A.RouteName = C.RouteName 
	where B.CustomerName = @companyName group by C.RouteName );


go
SELECT CustomerName 'Название заказчика', dbo.TGetOrdersQByCompany(CustomerName) 'Количество заказов' from Customer;
SELECT * FROM TGetOrdersByDate('2019/01/01','2020/01/01');
SELECT * FROM TGetRelevantRoutesByCompany('ТурБай');

/*▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ TRIGGERS ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬*/
CREATE TABLE TR_AUDIT
(
ID int identity,
STMT varchar(20) check (STMT in ('INS','DEL','UPD')),
TRNAME varchar(50),
CC varchar(800)
)
select * from TR_AUDIT;
go
create trigger TR_ORDER  on [ORDER] after INSERT, DELETE, UPDATE  
as 
declare @ins int = (select count(*) from inserted),
         @del int = (select count(*) from deleted); 
if  @ins > 0 and  @del = 0  
begin 
     print 'Событие: INSERT';
     insert into TR_AUDIT values ('INS','TR_ORDER_INS','CustomerName:' + rtrim((SELECT CustomerName from INSERTED)) + '. ServiceId:' + rtrim((SELECT ServiceId from INSERTED))
													 + '. OrderDate: ' + rtrim((SELECT OrderDate from INSERTED))+
													'. OrderExec:' + rtrim((SELECT OrderExec from INSERTED)));
end; 
else		  	 
if @ins = 0 and  @del > 0  
begin 
    print 'Событие: DELETE';
   insert into TR_AUDIT values ('DEL','TR_ORDER_DEL','CustomerName:' + rtrim((SELECT CustomerName from DELETED)) + '. ServiceId:' + rtrim((SELECT ServiceId from DELETED))
													+ '. OrderDate: ' + rtrim((SELECT OrderDate from DELETED))+
													'. OrderExec:' + rtrim((SELECT OrderExec from DELETED)));
end; 
else	  
if @ins > 0 and  @del > 0  
begin 
    print 'Событие: UPDATE'; 
   insert into TR_AUDIT values ('UPD','TR_ORDER_UPD','CustomerName:' + rtrim((SELECT CustomerName from INSERTED)) + '. ServiceId:' + rtrim((SELECT ServiceId from INSERTED))
													+ '. OrderDate: ' + rtrim((SELECT OrderDate from INSERTED))+
													'. OrderExec:' + rtrim((SELECT OrderExec from INSERTED)));
end;  
return;

go
create trigger TR_ROUTE  on ROUTE after INSERT, DELETE, UPDATE  
as 
declare @ins int = (select count(*) from inserted),
         @del int = (select count(*) from deleted); 
if  @ins > 0 and  @del = 0  
begin 
     print 'Событие: INSERT';
     insert into TR_AUDIT values ('INS','TR_ROUTE_INS','RouteName:' + rtrim((SELECT RouteName from INSERTED)));
end; 
else		  	 
if @ins = 0 and  @del > 0  
begin 
    print 'Событие: DELETE';
   insert into TR_AUDIT values ('DEL','TR_ROUTE_DEL','RouteName:' + rtrim((SELECT RouteName from DELETED)));
end; 
else	  
if @ins > 0 and  @del > 0  
begin 
    print 'Событие: UPDATE'; 
   insert into TR_AUDIT values ('UPD','TR_ROUTE_UPD','RouteName:' + rtrim((SELECT RouteName from INSERTED)));
end;  
return;

go
create trigger TR_CUSTOMER  on Customer after INSERT, DELETE, UPDATE  
as 
declare @ins int = (select count(*) from inserted),
         @del int = (select count(*) from deleted); 
if  @ins > 0 and  @del = 0  
begin 
     print 'Событие: INSERT';
     insert into TR_AUDIT values ('INS','TR_CUSTOMER_INS','CustomerName:' + rtrim((SELECT CustomerName from INSERTED)));
end; 
else		  	 
if @ins = 0 and  @del > 0  
begin 
    print 'Событие: DELETE';
   insert into TR_AUDIT values ('DEL','TR_CUSTOMER_DEL','CustomerName:' + rtrim((SELECT CustomerName from DELETED)));
end; 
else	  
if @ins > 0 and  @del > 0  
begin 
    print 'Событие: UPDATE'; 
   insert into TR_AUDIT values ('UPD','TR_CUSTOMER_UPD','CustomerName:' + rtrim((SELECT CustomerName from INSERTED)));
end;  
return;