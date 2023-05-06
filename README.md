# Portfolio.BackEnd

# Diagrama Db

![DiagramaDb](https://user-images.githubusercontent.com/30541728/236626990-b7d89a43-e40b-46fb-a5e2-900a785423a8.png)

# QUERY DB

``` sql
create database PortfolioDb
use PortfolioDb
go
create table Users
(
	UserId int identity primary key,
	Name varchar(100) not null,
	Email varchar(100) not null,
	Password varchar(256) not null,
	CreateDate datetime default GetDate()
)
go
Create table WorkExperience
(
	WorkExperienceId int identity primary key,
	CompanyName varchar(100) not null,
	PositionName varchar(100) not null,
	Description varchar(250),
	UserId int FOREIGN KEY (UserId) REFERENCES Users(UserId),
	StartDate datetime not null,
	EndDate datetime,
	CreateDate datetime default GetDate()
)
go
create view View_WorkExperience
as
select WorkExperienceId, CompanyName, PositionName, Description,
UserId, StartDate, EndDate, CreateDate from WorkExperience
go
create procedure Sp_GetWorkExperience
@UserId int
as
begin
	if @UserId > 0
	begin
		select * from View_WorkExperience where UserId = @UserId
	end
	else
	begin
		select * from View_WorkExperience
	end
end
go
create procedure Sp_SetWorkExperience
@WorkExperienceId int,
@CompanyName varchar(100),
@PositionName varchar(100),
@Description varchar(250),
@UserId int,
@StartDate datetime,
@EndDate datetime = null,
@Identity int out
as
begin
	insert into WorkExperience (CompanyName, PositionName, Description, UserId, StartDate, EndDate) 
	values (@CompanyName, @PositionName, @Description, @UserId, @StartDate, @EndDate)
	select @Identity = @@Identity
end
go
create table Levels
(
	LevelId int identity primary key,
	Name varchar(30),
	CreateDate datetime default GetDate()
)
go
create view View_Levels
as
select LevelId, Name, CreateDate from Levels
go
alter procedure Sp_GetLevels
@LevelId int
as
begin
	if @LevelId > 0
	begin
		select * from View_Levels where LevelId = @LevelId
	end
	else
	begin
		select * from View_Levels
	end
end
go
create table Languages 
(
	LanguageId int identity primary key,
	Name varchar(100) not null,
)
go
create view View_Languages
as
select LanguageId, Name  from Languages
go
create procedure Sp_GetLanguages
@LanguageId int
as
begin
	if @LanguageId > 0
	begin
		select * from View_Languages where LanguageId = @LanguageId 
	end
	else
	begin
		select * from View_Languages
	end
end
go
create table Skills
(
	SkillId int identity primary key,
	LanguageId int FOREIGN KEY (LanguageId) REFERENCES Languages(LanguageId),
	LevelId int FOREIGN KEY (LevelId) REFERENCES Levels(LevelId),
	UserId int FOREIGN KEY (UserId) REFERENCES Users(UserId),
	CreateDate datetime default GetDate()
)
go
create table Platforms
(
	PlatformId int identity primary key,
	Name varchar(100) not null,
	CreateDate datetime default GetDate()
)
go
create view View_Platforms
as
select PlatformId, Name, CreateDate from Platforms
go
create procedure Sp_GetPlatforms
@PlatformId int
as
begin
	if @PlatformId > 0
	begin
		select * from View_Platforms where PlatformId = @PlatformId
	end
	else
	begin
		select * from View_Platforms
	end
end
go
create table Proyects
(
	ProyectId int identity primary key,
	Name varchar(100) not null,
	Description varchar(250) null,
	ImageGuidId uniqueidentifier null,
	GithubUrl varchar(500) null,
	DomainUrl varchar(100) null,
	PlatformId int FOREIGN KEY (PlatformId) REFERENCES Platforms(PlatformId) not null,
	UserId int FOREIGN KEY (UserId) REFERENCES Users(UserId),
	CreateDate datetime default GetDate()
)
go
create view View_Proyects
as
select Proyect.ProyectId, Proyect.Name, Proyect.Description, Proyect.ImageGuidId,
Proyect.GithubUrl, Proyect.DomainUrl, Proyect.PlatformId, Platform.Name as PlatformName, Proyect.UserId, Proyect.CreateDate
from Proyects as Proyect
inner join Platforms as Platform on Proyect.PlatformId = Platform.PlatformId
go
create type CreateProyect as Table 
(
	ProyectId int,
	Name varchar(100) not null,
	Description varchar(250) null,
	ImageGuidId uniqueidentifier null,
	GithubUrl varchar(500) null,
	DomainUrl varchar(100) null,
	PlatformId int,
	UserId int
)
go
create procedure Sp_SetProyect
@ProyectId int,
@Name varchar(100),
@Description varchar(250),
@ImageGuidId uniqueidentifier,
@GithubUrl varchar(500),
@DomainUrl varchar(100),
@PlatformId int,
@UserId int,
@Identity int out
as
begin
	insert into Proyects (Name, Description, ImageGuidId, GithubUrl, DomainUrl, PlatformId, UserId)
	values(@Name, @Description, @ImageGuidId, @GithubUrl, @DomainUrl, @PlatformId, @UserId) 
	select @Identity = @@IDENTITY
end

go
create procedure Sp_GetProyects
@UserId int
as
begin
	if @UserId > 0
	begin
		select * from View_Proyects where UserId = @UserId
	end
	else
	begin
		select * from View_Proyects 
	end
end
go
create table ProyectSkills
(
	ProyectSkillId int identity primary key,
	ProyectId int FOREIGN KEY (ProyectId) REFERENCES Proyects(ProyectId) not null,
	SkillId int FOREIGN KEY (SkillId) REFERENCES Skills(SkillId) not null,
	CreateDate datetime default GetDate()
)
go
create view View_ProyectSkills
as
select 
ProyectSkills.ProyectSkillId, ProyectSkills.ProyectId, ProyectSkills.SkillId, ProyectSkills.CreateDate,
Skills.LanguageId, Skills.LevelId,
Language.Name as LanguageName, 
Level.Name as LevelName
from ProyectSkills 
inner join Skills on ProyectSkills.SkillId = Skills.SkillId
inner join Languages as Language on Skills.LanguageId = Language.LanguageId
inner join Levels as Level on Skills.LevelId = Level.LevelId
go
alter procedure Sp_SetProyectSkills
@ProyectSkillId int,
@ProyectId int,
@SkillId int,
@Identity int out
as
begin
	insert into ProyectSkills (ProyectId, SkillId) values (@ProyectId, @SkillId)
	select @Identity = @@Identity
end
go
ALTER procedure Sp_GetProyectSkills
@ProyectId int
as
begin
	if @ProyectId > 0
	begin
		select * from View_ProyectSkills where ProyectId = @ProyectId
	end
	else
	begin
		select * from View_ProyectSkills
	end
end
go
create procedure Sp_GetSkills
@UserId int = 0
as
begin
	if @UserId > 0
	begin
		select * from Skills where UserId = @UserId
	end
	else
	begin
		select * from Skills
	end
end
go
create procedure Sp_SetSkills
@LanguageId int,
@LevelId int,
@UserId int,
@Identity int out
as
begin
	insert into Skills (LanguageId, LevelId, UserId) values (@LanguageId, @LevelId, @UserId)
	select @Identity = @@IDENTITY
end
go
create procedure Sp_AuthUser
@Email varchar(100),
@Password varchar(256)
as
begin
	select * from Users where Email = @Email and Password = @Password
end
go
create procedure Sp_CreateUser
@Name varchar(100),
@Email varchar(100),
@Password varchar(256),
@Identity int out
as
begin
	insert into Users (Name, Email, Password) values (@Name, @Email, @Password)
	select @Identity = @@IDENTITY
end
go
create procedure Sp_GetUsers
@UserId int = 0
as
begin
	select * from Users where UserId = @UserId;
end
```

