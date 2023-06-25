use sports_facility

exec addSportsKind  1,'football';
exec addSportsKind  2,'athletics';
exec addSportsKind  3,'gymnastics';
exec addSportsKind  4,'basketball';

select * from sports_kind;

exec addTrainer  1,'Allena','5 years',1;
exec addTrainer  2,'Alla','5 years',1;
exec addTrainer  3,'Karina','2 years',3;
exec addTrainer  4,'Gleb','4 years',1;
exec addTrainer  5,'Igor','5 years',1;
exec addTrainer  6,'Egor','3 years',2;


select * from trainer;

exec addFacility  1,'Dinamo','Makaenka,12','10-12-2018',1;
exec addFacility  2,'Arena','Gorkogo,58','11-09-2010',2;
exec addFacility  3,'Stadion','Gercena,65','07-07-2022',3;
exec addFacility  4,'Lebyazhik','Titova,47','06-01-2008',4;
exec addFacility  5,'Sports club','Moskovskaya,19','05-10-2016',1;

select * from facility;

exec addHall 1,1,5,'Grass';
exec addHall 2,1,4,'Grass';
exec addHall 3,1,2,'Taraflex';
exec addHall 4,1,5,'Carpet';
exec addHall 5,1,1,'Grass';

exec addHall 6,2,4,'Grass';
exec addHall 7,2,2,'Carpet';
exec addHall 8,2,1,'Taraflex';
exec addHall 9,2,4,'Grass';
exec addHall 10,2,5,'Grass';

exec addHall 11,3,1,'Taraflex';
exec addHall 12,3,2,'Grass';
exec addHall 13,3,3,'Taraflex';
exec addHall 14,3,4,'Carpet';
exec addHall 15,3,5,'Grass';

exec addHall 16,4,1,'Grass';
exec addHall 17,4,2,'Taraflex';
exec addHall 18,4,5,'Grass';
exec addHall 19,4,5,'Taraflex';
exec addHall 20,4,4,'Grass';

exec addHall 21,5,1,'Grass';
exec addHall 22,5,5,'Taraflex';
exec addHall 23,5,4,'Carpet';
exec addHall 24,5,3,'Taraflex';
exec addHall 25,5,2,'Grass';

SELECT * FROM HALL









