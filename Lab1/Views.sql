use Trucking;

--FirstView
go
Create view RouteServiceTypeService AS
select Route.RouteName AS RouteName,
	   Service.RouteName AS ServiceRouteName,
	   ServiceType.ServiceName AS ServiceName
From Route Inner join Service on Route.RouteName = Service.RouteName
inner join ServiceType on Service.ServiceType = ServiceType.ServiceName

Select * from RouteServiceTypeService;

--SecondView
go
create view CityRoutesDeparture AS
	SELECT CityName, COUNT(Route.RouteName) 'Кол-во маршрутов'
	From City left outer join Route on Route.DeparturePoint = CityName group by CityName;

Select * from CityRoutesDeparture;

--ThirdView
go 
create view RouteView AS
select RouteName AS Route,Distance,DeparturePoint,ArrivalPoint
From Route

Select * from RouteView;