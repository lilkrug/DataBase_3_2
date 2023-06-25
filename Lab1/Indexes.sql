use Trucking;

--City Table indexes
CREATE UNIQUE NONCLUSTERED INDEX index_CityNameUL ON City(CityName);
CREATE UNIQUE NONCLUSTERED INDEX index_CityNameULAsc ON City(CityName Asc);
CREATE UNIQUE NONCLUSTERED INDEX index_CityNameULDesc ON City(CityName Desc);

--Route Table indexes
CREATE UNIQUE NONCLUSTERED INDEX index_RouteNameUL ON Route(RouteName);
CREATE UNIQUE NONCLUSTERED INDEX index_RouteNameULAsc ON Route(RouteName Asc);
CREATE UNIQUE NONCLUSTERED INDEX index_RouteNameULDesc ON Route(RouteName Desc);

--ServiceType Table indexes
CREATE UNIQUE NONCLUSTERED INDEX index_ServiceTypeUL ON ServiceType(ServiceName);
CREATE UNIQUE NONCLUSTERED INDEX index_ServiceTypeULAsc ON ServiceType(ServiceName Asc);
CREATE UNIQUE NONCLUSTERED INDEX index_ServiceTypeULDesc ON ServiceType(ServiceName Desc);

--ServiceType Table indexes
CREATE UNIQUE NONCLUSTERED INDEX index_CustomerCN ON Customer(CustomerName);
CREATE UNIQUE NONCLUSTERED INDEX index_CustomerCNAsc ON Customer(CustomerName Asc);
CREATE UNIQUE NONCLUSTERED INDEX index_CustomerCNDesc ON Customer(CustomerName Desc);

--Order Table indexes
CREATE NONCLUSTERED INDEX index_OrderCN ON [Order](CustomerName);
CREATE NONCLUSTERED INDEX index_OrderSId ON [Order](ServiceId);
CREATE NONCLUSTERED INDEX index_OrderOE ON [Order](OrderDate);
CREATE NONCLUSTERED INDEX index_OrderOD ON [Order](OrderExec);

CREATE NONCLUSTERED INDEX index_OrderCNAsc ON [Order](CustomerName asc);
CREATE NONCLUSTERED INDEX index_OrderSIdAsc ON [Order](ServiceId asc);
CREATE NONCLUSTERED INDEX index_OrderOEAsc ON [Order](OrderDate asc);
CREATE NONCLUSTERED INDEX index_OrderODAsc ON [Order](OrderExec asc);

CREATE NONCLUSTERED INDEX index_OrderCNDesc ON [Order](CustomerName desc);
CREATE NONCLUSTERED INDEX index_OrderSIdDesc ON [Order](ServiceId desc);
CREATE NONCLUSTERED INDEX index_OrderOEDesc ON [Order](OrderDate desc);
CREATE NONCLUSTERED INDEX index_OrderODDesc ON [Order](OrderExec desc);

--Service Table indexes
CREATE NONCLUSTERED INDEX index_ServiceST ON Service(ServiceType);
CREATE NONCLUSTERED INDEX index_ServiceRN ON Service(RouteName);

CREATE NONCLUSTERED INDEX index_ServiceSTAsc ON Service(ServiceType asc);
CREATE NONCLUSTERED INDEX index_ServiceRNAsc ON Service(RouteName asc);

CREATE NONCLUSTERED INDEX index_ServiceSTDesc ON Service(ServiceType desc);
CREATE NONCLUSTERED INDEX index_ServiceRNDesc ON Service(RouteName desc);