--██████████████████	Task 1	██████████████████
go
use Trucking;

CREATE TABLE Geo  
(
    Level hierarchyid NOT NULL,  
    Location nvarchar(30) NOT NULL,  
    LocationType nvarchar(9) NULL
);

insert into Geo values(hierarchyid::GetRoot(), 'Europe', 'Continent'); 
insert into Geo values(hierarchyid::GetRoot(), 'SouthAmerica', 'Continent'); 
select * from Geo;

go
declare @Id hierarchyid  
select @Id = MAX(Level) from Geo where Level.GetAncestor(1) = hierarchyid::GetRoot(); 
insert into Geo values(hierarchyid::GetRoot().GetDescendant(@id, null), 'France', 'Country');

go
declare @Id hierarchyid  
select @Id = MAX(Level) from Geo where Level.GetAncestor(1) = hierarchyid::GetRoot() ; 
insert into Geo values(hierarchyid::GetRoot().GetDescendant(@id, null), 'Belarus', 'Country');

 
go
declare @phId hierarchyid
select @phId = (SELECT Level FROM Geo WHERE Location = 'Belarus');
declare @Id hierarchyid
select @Id = MAX(Level) from Geo where Level.GetAncestor(1) = @phId;
insert into Geo values( @phId.GetDescendant(@id, null), 'Minsk', 'City');

go
declare @phId hierarchyid
select @phId = (SELECT Level FROM Geo WHERE Location = 'France');
declare @Id hierarchyid
select @Id = MAX(Level) from Geo where Level.GetAncestor(1) = @phId;
insert into Geo values( @phId.GetDescendant(@id, null), 'Paris', 'City');

select Level.ToString() [Связи], Level.GetLevel() [Уровень], * from Geo; 

--██████████████████	Task 2	██████████████████
GO  
CREATE or Alter PROCEDURE getRoot(@level int)    
AS   
BEGIN  
   select Level.ToString()[Связи], * from Geo where Level.GetLevel() = @level;
END;

GO  
exec getRoot 0;
exec getRoot 1;
exec getRoot 2;

--██████████████████	Task 3	██████████████████
GO  
CREATE or Alter PROCEDURE addChildNode(@LocationParent nvarchar(30) ,@LocationChild nvarchar(30) ,@LocationType nvarchar(9))   
AS   
BEGIN  

declare @Level hierarchyid
declare @RLevel hierarchyid
select @RLevel = (SELECT Level FROM Geo WHERE Location = @LocationParent);

select @Level = MAX(Level) from Geo where Level.GetAncestor(1) = @RLevel

insert into Geo values( @RLevel.GetDescendant(@Level, null),@LocationChild,@LocationType);
END;  

GO  
exec addChildNode 'Belarus', 'Mogilev', 'City';
select * from Geo; 

--██████████████████	Task 4	██████████████████
go
CREATE or alter PROCEDURE moveRoot(@moveableNode nvarchar(30), @newRoot nvarchar(30) )
AS  
BEGIN  
	DECLARE @nold hierarchyid, @nnew hierarchyid;
	SELECT @nold = level FROM geo WHERE Location = @moveableNode ;  
  
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE  
	BEGIN TRANSACTION  
	SELECT @nnew = level FROM geo WHERE Location = @newRoot ; 
  
	SELECT @nnew = @nnew.GetDescendant(max(level), NULL) FROM geo WHERE level.GetAncestor(1)=@nnew ; 
	UPDATE geo SET level = level.GetReparentedValue(@nold, @nnew) WHERE level.IsDescendantOf(@nold) = 1 ;   
	commit;
END ;  

GO  
select level.ToString(), level.GetLevel(), * from geo;
exec moveRoot 'Belarus','SouthAmerica';
select level.ToString(), level.GetLevel(), * from geo;

truncate table geo;