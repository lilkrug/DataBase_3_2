go
use Trucking;

create table cityCoord
(
	Id int identity(1,1) constraint TOWN_ID_PK PRIMARY KEY,
	Point geometry
);

insert into cityCoord(Point) 
	values 
	(geometry::STGeomFromText('Point(53.54 27.30)', 0)),
	(geometry::STGeomFromText('Point(52.06 23.42)', 0)),
	(geometry::STGeomFromText('Point(53.42 23.43)', 0)),
	(geometry::STGeomFromText('Point(55.12 30.06)', 0)),
	(geometry::STGeomFromText('Point(52.24 31.00)', 0)),
	(geometry::STGeomFromText('Point(53.54 30.18)', 0));

update  City set City_Addr = 1 where CityName = 'Минск';
update  City set City_Addr = 2 where CityName = 'Брест';
update  City set City_Addr = 3 where CityName = 'Гродно';
update  City set City_Addr = 4 where CityName = 'Витебск';
update  City set City_Addr = 5 where CityName = 'Гомель';
update  City set City_Addr = 6 where CityName = 'Могилев';

go
alter table City
	add City_Addr int constraint CADDR_ID_FK foreign key references cityCoord(Id);


