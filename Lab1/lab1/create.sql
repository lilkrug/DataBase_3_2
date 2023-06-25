drop table facility;
drop table sports_kind;
drop table hall;
drop table coach;

use sports_facility;
--объект
CREATE TABLE facility (    
	facility_id INT   PRIMARY KEY,
	facility_name  NVARCHAR (255) NOT NULL,
	adress NVARCHAR (255) NOT NULL,
	founding_date DATE NOT NULL,
	sports_kind_id INT NOT NULL ,
	FOREIGN KEY (sports_kind_id) 
        REFERENCES sports_kind (sports_kind_id) 
        ON DELETE CASCADE ON UPDATE CASCADE
);
--спортивный вид
CREATE TABLE sports_kind (
	sports_kind_id INT   PRIMARY KEY,
	sports_kind_name NVARCHAR (255) NOT NULL
);

--зал
CREATE TABLE hall(
	hall_id INT  PRIMARY KEY,
	facility_id INT ,
	trainer_id int,
	coating_type NVARCHAR (255) ,
	FOREIGN KEY (facility_id) 
        REFERENCES facility (facility_id) 
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (trainer_id) 
        REFERENCES trainer (trainer_id)  ON DELETE CASCADE ON UPDATE CASCADE

);
--тренер
CREATE TABLE trainer(
	trainer_id INT  PRIMARY KEY,
	trainer_name  NVARCHAR (255) not null,
	experience NVARCHAR (255) NOT NULL,
	category INT NOT NULL
);

	

select * from sports_kind;
select * from trainer;
select * from facility;
select * from hall;