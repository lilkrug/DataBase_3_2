CREATE VIEW RouteServiceTypeService AS
SELECT Route.RouteName AS RouteName,
	   Service.RouteName AS ServiceRouteName,
	   ServiceType.ServiceName AS ServiceName
FROM Route 
INNER JOIN Service ON Route.RouteName = Service.RouteName
INNER JOIN ServiceType ON Service.ServiceType = ServiceType.ServiceName;

SELECT * FROM RouteServiceTypeService;

create procedure 

--SecondView
CREATE VIEW CityRoutesDeparture AS
	SELECT CityName, COUNT(Route.RouteName) AS 'Кол-во маршрутов'
	FROM City 
	LEFT OUTER JOIN Route ON Route.DeparturePoint = CityName 
	GROUP BY CityName;

SELECT * FROM CityRoutesDeparture;

--ThirdView
CREATE VIEW RouteView AS
SELECT RouteName AS Route, Distance, DeparturePoint, ArrivalPoint
FROM Route;

SELECT * FROM RouteView;