Use Truck;
Drop Database Truck;

Create table Client
(
	Id int constraint PK_CLIENT primary key (Id) identity(1,1),
	Name varchar(50) NOT NULL,
	Contact int NOT NULL,
	Address varchar(50) UNIQUE,
	Delivery varchar(50) UNIQUE
)

Create table Good
(
	Id int constraint PK_GOOD primary key (Id) identity(1,1),
	Description varchar(50) NOT NULL,
	Weight int NOT NULL,
	Volume int NOT NULL,
	Address varchar(50) NOT NULL constraint FK_CLIENT_GOOD foreign key (Address) references Client(Address),
	Delivery varchar(50) NOT NULL constraint FK_CLIENTS_GOOD foreign key (Delivery) references Client(Delivery)
)

Create table Transportation
(
	Id int constraint PK_TRANSPORTATION primary key (Id) identity(1,1),
	Brand varchar(50) NOT NULL,
	Model varchar(50) NOT NULL,
	License varchar(50) NOT NULL,
	Capacity int NOT NULL
)
 
Create table [Order]
(
	Id int constraint PK_ORDER primary key (Id) identity(1,1),
	ClientID int NOT NULL constraint FK_ORDER_CLIENT foreign key (ClientId) references Client(Id),
	GoodId int NOT NULL constraint FK_ORDER_GOOD foreign key (GoodId) references Good(Id),
	TransportationID int NOT NULL constraint FK_ORDER_TRANSPORTATION foreign key (TransportationId) references Transportation(Id),
	Status varchar(50) NOT NULL
)

INSERT into Client values
('John',12345,'New York','Boston'),('Jane',98765,'LA','San Francisco')

INSERT into Good values
('Boxes',500,5,'New York','Boston'),('Pallets',1000,10,'LA','San Francisco')

INSERT into Transportation values
('Ford','f-150','ABC123',1000),('Volvo','FH-16','XYZ789',5000)

INSERT into [Order] values
(1,1,1,'In transit'),(2,2,2,'Pending')

select * from Client

select * from Good

select * from Transportation

select * from [Order]