use sports_facility
CREATE OR ALTER PROCEDURE addTrainer
   @trainer_id int, @trainer_name nvarchar(255),
    @experience NVARCHAR (255),
    @category INT
AS
BEGIN
INSERT INTO trainer(trainer_id,trainer_name, experience, category) 
VALUES(@trainer_id, @trainer_name, @experience,@category)
END;

CREATE OR ALTER PROCEDURE addHall
   @hall_id INT ,
	@facility_id INT ,
	@trainer_id int,
	@coating_type NVARCHAR(255)
AS
BEGIN
INSERT INTO hall (hall_id ,facility_id , trainer_id,coating_type ) 
VALUES(@hall_id  ,
	@facility_id ,
	@trainer_id ,
	@coating_type)
END;


CREATE OR ALTER PROCEDURE addSportsKind
   @sports_kind_id INT ,
	@sports_kind_name NVARCHAR (255) 
AS
BEGIN
INSERT INTO sports_kind(sports_kind_id ,sports_kind_name  ) 
VALUES( @sports_kind_id  ,
	@sports_kind_name)
END;

CREATE OR ALTER PROCEDURE addFacility
    @facility_id INT ,
	@facility_name  NVARCHAR (255) ,
	@adress NVARCHAR (255) ,
	@founding_date DATE ,
	@sports_kind_id INT
AS
BEGIN
INSERT INTO facility(
	facility_id ,
	facility_name  ,
	adress,
	founding_date  ,
	sports_kind_id) 
VALUES(  @facility_id  ,
	@facility_name  ,
	@adress,
	@founding_date  ,
	@sports_kind_id )
END;

-----------------------

create or alter view InfoHalls as select hall.hall_id,facility.facility_name, trainer.trainer_name , hall.coating_type from hall join facility on hall.facility_id= facility.facility_id
join trainer on hall.trainer_id=trainer.trainer_id;

select * from InfoHalls;


create or alter view  FacilityInfo as select facility.facility_name,sports_kind.sports_kind_name,facility.founding_date from facility 
join sports_kind on facility.sports_kind_id=sports_kind.sports_kind_id;

select * from FacilityInfo;

----------------------
CREATE TABLE NewTrainer 
(
    id INT IDENTITY(1,1) PRIMARY KEY,
    trainer_id INT NOT NULL,
    operation NVARCHAR(200) NOT NULL,
	createAt DATETIME NOT NULL DEFAULT GETDATE(),
);
go
create trigger TrainerInsert on trainer after insert
as 
insert into newTrainer (trainer_id,operation) select trainer_id,'insert' from inserted
go

go
create trigger TrainerDeleted on trainer after delete
as 
insert into newTrainer (trainer_id,operation) select trainer_id,'delete' from deleted
go

delete trainer where trainer.trainer_id=6
 drop trigger TrainerUpdate
go
create trigger TrainerUpdat on trainer after update
as 
insert into newTrainer (trainer_id,operation) select trainer_id,'update' from inserted
go

update trainer set trainer_name='Egorchik'  where trainer_id=6

exec addTrainer  7,'Tata','5 years',2;

select * from NewTrainer;

-----------------------------
CREATE OR ALTER PROCEDURE FindHall
  @facility_name NVARCHAR(200)
AS
BEGIN
select hall.hall_id,sports_kind.sports_kind_name,trainer.trainer_name from hall 
join facility on hall.facility_id=facility.facility_id
join sports_kind on sports_kind.sports_kind_id=facility.sports_kind_id 
join trainer on trainer.trainer_id=hall.hall_id
where facility.facility_name=@facility_name
END;

exec FindHall 'Dinamo';

CREATE OR ALTER PROCEDURE FindTrainerHalls
  @trainer_name nvarchar(200)
AS
BEGIN
select trainer.trainer_name ,facility.facility_name, hall.hall_id,sports_kind.sports_kind_name from hall 
join facility on hall.facility_id=facility.facility_id
join sports_kind on sports_kind.sports_kind_id=facility.sports_kind_id 
join trainer on trainer.trainer_id=hall.trainer_id
where trainer.trainer_name=@trainer_name
END;

exec FindTrainerHalls 'Alla';


CREATE OR ALTER PROCEDURE FindFacilityFound
  @date_before date,
  @date_after date
AS
BEGIN
select facility.facility_name,sports_kind.sports_kind_name,facility.founding_date from  facility
join sports_kind on sports_kind.sports_kind_id=facility.sports_kind_id 
where facility.founding_date between @date_before and @date_after
END;


exec FindFacilityFound '09-09-2010','01-01-2020';

----------------------------------
CREATE or ALTER FUNCTION  HallsCount 
(@kind nvarchar(255))
RETURNS int
AS
BEGIN 
declare @result int;
    select @result=count( hall.hall_id)   from  facility
	join sports_kind on facility.sports_kind_id=sports_kind.sports_kind_id
	join hall on hall.facility_id=facility.facility_id where sports_kind.sports_kind_name=@kind;
    RETURN @result
END;

select dbo.HallsCount('football')

CREATE or ALTER FUNCTION  facilityCount 
( @date_before date,
  @date_after date)
RETURNS int
AS
BEGIN 
declare @result int;
    select @result=count( facility.facility_name) from  facility
join sports_kind on sports_kind.sports_kind_id=facility.sports_kind_id 
where facility.founding_date between @date_before and @date_after
    RETURN @result
END;

select dbo.facilityCount('09-09-2010','01-01-2020')

CREATE or ALTER FUNCTION  MinCategory 
()
RETURNS int
AS
BEGIN 
declare @result int;
    select @result=min( trainer.trainer_id)  from  trainer
	;
    RETURN @result
END;


select dbo.MinCategory();

create nonclustered index IX_Facility on Facility (founding_date);
create nonclustered index IX_Trainer_categ on Trainer (category) WHERE category>1;
create nonclustered index IX_Hall on Hall (coating_type);

create index ix_Facility_foundation
on dbo.facility(founding_date)
include(adress,facility_name)


go
Create or Alter Procedure DeleteSportsKind
		@kind_id int
AS
Begin
	DELETE sports_kind where sports_kind.sports_kind_id = @kind_id;
End;
go 

go
Create or Alter Procedure DeleteFacility
		@facility_id int
AS
Begin
	DELETE facility where facility.facility_id = @facility_id;
End;
go
go
Create or Alter Procedure DeleteTrainer
		@trainer_id int
AS
Begin
	DELETE trainer where trainer.trainer_id = @trainer_id;
End;
go
-------------------

Create OR ALTER Procedure UpdateTrainer
		@trainer_id int, 
		@trainer_name varchar
AS
Begin
	UPDATE trainer 
	SET  trainer_name = @trainer_name
	where trainer_id = @trainer_id
End;


Create OR ALTER Procedure UpdateSportsKind
		@sports_kind_id int, 
		@sports_kind_name varchar
AS
Begin
	UPDATE sports_kind 
	SET  sports_kind_name = @sports_kind_name
	where sports_kind_id = @sports_kind_id
End;



Create OR ALTER Procedure UpdateFacility
		@facility_id int, 
		@adress varchar
AS
Begin
	UPDATE facility 
	SET  adress = @adress
	where facility_id = @facility_id
End;