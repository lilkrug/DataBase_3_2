CREATE DATABASE Trucking;
use Trucking;
DROP DATABASE Trucking;


CREATE TABLE ServiceType
(
Id int constraint PK_SERVICE_TYPE primary key (Id) identity(1,1),
ServiceName varchar(50) NOT NULL UNIQUE
)

CREATE TABLE City
(
Id int constraint PK_CITY primary key(Id) identity(1,1),
CityName varchar(100) NOT NULL UNIQUE
)

CREATE TABLE Route
(
Id int constraint PK_ROUTE primary key(Id) identity(1,1),
RouteName varchar(100) NOT NULL UNIQUE,
Distance decimal(5,1) NOT NULL,
DeparturePoint varchar(100) NOT NULL constraint FK_CITYD_ROUTE foreign key (DeparturePoint) references City(CityName),
ArrivalPoint varchar(100) NOT NULL constraint FK_CITYA_ROUTE foreign key (ArrivalPoint) references City(CityName)
)

CREATE TABLE Customer
(
Id int constraint PK_CUSTOMER primary key(Id) identity(1,1),
CustomerName varchar(300) UNIQUE NOT NULL
)

CREATE TABLE Service
(
Id int constraint PK_SERVICE primary key(Id) identity(1,1),
ServiceType varchar(50) NOT NULL constraint FK_SERVICE_SERVICETYPE foreign key (ServiceType) references ServiceType(ServiceName),
RouteName varchar(100) NOT NULL constraint FK_SERVICE_ROUTE foreign key (RouteName) references Route(RouteName),
)

CREATE TABLE [Order]
(
Id int constraint PK_Order primary key(Id) identity(1,1),
CustomerName varchar(300) NOT NULL constraint FK_ORDER_CUSTOMER foreign key (CustomerName) references Customer(CustomerName),
ServiceId int NOT NULL constraint FK_ORDER_SERVICE foreign key (ServiceId) references Service(Id),
OrderDate datetime NOT NULL,
OrderExec datetime NOT NULL
)