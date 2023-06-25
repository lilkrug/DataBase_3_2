--City Table indexes
CREATE UNIQUE INDEX index_CityNameUL ON City(CityName);
CREATE INDEX index_CityNameAsc ON City(CityName ASC);
CREATE INDEX index_CityNameDesc ON City(CityName DESC);

--Route Table indexes
CREATE UNIQUE INDEX index_RouteNameUL ON Route(RouteName);
CREATE INDEX index_RouteNameAsc ON Route(RouteName ASC);
CREATE INDEX index_RouteNameDesc ON Route(RouteName DESC);

--ServiceType Table indexes
CREATE UNIQUE INDEX index_ServiceTypeUL ON ServiceType(ServiceName);
CREATE INDEX index_ServiceTypeAsc ON ServiceType(ServiceName ASC);
CREATE INDEX index_ServiceTypeDesc ON ServiceType(ServiceName DESC);
