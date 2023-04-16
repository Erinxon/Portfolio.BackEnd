# Portfolio.BackEnd

## Scaffold-DbContext
Scaffold-DbContext "Server=LAPTOP-9LCNL3MM;Database=PortfolioDb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer  -Force -ContextDir "C:\Users\Erinxon\Desktop\PortfolioProject\Portfolio.BackEnd\PortfolioProject\Infratructure\Persistence" -OutputDir "C:\Users\Erinxon\Desktop\PortfolioProject\Portfolio.BackEnd\PortfolioProject\Domain\Entities" -Namespace "Domain.Entities" -ContextNamespace "Infrastructure.Persistence" 

## Query SQL
``` sql
create database PortfolioDb
go
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
	UserId int FOREIGN KEY (UserId) REFERENCES Users(UserId),
	StartDate datetime not null,
	EndDate datetime,
	CreateDate datetime default GetDate()
)
go
create table Levels
(
	LevelId int identity primary key,
	Name varchar(30),
	CreateDate datetime default GetDate()
)
go
create table Languages 
(
	LanguageId int identity primary key,
	Name varchar(100) not null,
)
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
create table ProyectSkills
(
	ProyectSkillId int primary key,
	ProyectId int FOREIGN KEY (ProyectId) REFERENCES Proyects(ProyectId) not null,
	SkillId int FOREIGN KEY (SkillId) REFERENCES Skills(SkillId) not null UNIQUE,
	CreateDate datetime default GetDate()
)
go
create procedure Sp_GetSkills
@SkillId int = 0
as
begin
	if @SkillId > 0
	begin
		select * from Skills where SkillId = @SkillId
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

```
