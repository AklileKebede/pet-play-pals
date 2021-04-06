USE master
GO

--drop database if it exists
IF DB_ID('PetPlayPals_Test') IS NOT NULL
BEGIN
	ALTER DATABASE PetPlayPals_Test SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE PetPlayPals_Test;
END

CREATE DATABASE PetPlayPals_Test
GO

USE PetPlayPals_Test
GO

--create tables

--locations table
create table locations (
	location_id int identity(1,1) not null,
	name varchar(60) null,
	address varchar(80) null,
	lat numeric(10,6) not null,
	lng numeric(10,6) not null,

	CONSTRAINT PK_location PRIMARY KEY (location_id)
)

--user table
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	location_id int null,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

-- ######## pet stuff ############

--pet_type table
create table pet_types(
	pet_type_id int identity(1,1) NOT NULL,
	pet_type_name varchar(10) NOT NULL,

	constraint PK_pet_types primary key(pet_type_id)
)

-- populate the pet types table
insert into pet_types(pet_type_name) values 
	('Dog'),
	('Cat')

--pet_personality table
create table personalities(
	personality_id int identity(1,1) not null,
	personality_name varchar(20) not null

	constraint PK_pet_personality primary key(personality_id)
)
--populate the pet personality table
insert into personalities(personality_name) values 
	('Friendly'),
	('Aggressive'),
	('Shy'),
	('Skittish'),
	('High-energy')

--pets table
create table pets(
	pet_id int identity(1,1) NOT NULL,
	pet_name varchar(20) NOT NULL,
	birthday Date NOT NULL,
	sex char NOT NULL,
	pet_type_id int not null,
	pet_breed varchar(20),
	color varchar(20) null,
	bio varchar(300),


	constraint PK_pet primary key (pet_id),
	constraint FK_pet_type_id foreign key (pet_type_id) references pet_types (pet_type_id),
	constraint CHK_sex check (sex in ('M','F'))
)

--pet pictures
create table pet_images(
	pet_image_id int identity(1,1) not null,
	pet_image_data varbinary(max)not null,
	pet_id int not null,
	constraint PK_pet_images primary key (pet_image_id),
	constraint FK_pet_images_pet_id foreign key (pet_id) references pets (pet_id)
)

--owner_pet relator table
create table user_pet(
	user_id int not null,
	pet_id int not null,
	constraint FK_user_pet_user_id foreign key (user_id) references users (user_id),
	constraint FK_user_pet_pet_id foreign key (pet_id) references pets (pet_id),
)

--personality_pet relator table
create table personality_pet(
	pet_id int not null,
	personality_id int not null,
	constraint FK_personality_pet_pet_id foreign key (pet_id) references pets (pet_id),
	constraint FK_personality_pet_personality_id foreign key (personality_id) references personalities (personality_id),
)

-- ####### playdate stuff #########
create table playdates(
	playdate_id int identity(1,1) not null,
	date Date not null,
	location_id int not null,

	constraint PK_playdate primary key (playdate_id),
	constraint FK_playdate_location foreign key (location_id) references locations (location_id)
)

--playdate_pet relator table
create table playdate_pet(
	playdate_id int not null,
	pet_id int not null,
	constraint FK_playdate_id foreign key (playdate_id) references playdates (playdate_id),
	constraint FK_playdate_pet_pet_id foreign key (pet_id) references pets (pet_id)
)
--

GO