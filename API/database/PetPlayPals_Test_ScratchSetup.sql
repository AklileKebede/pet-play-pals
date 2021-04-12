use PetPlayPals_Test
go

--insert a location
insert into locations(name,address,lat,lng) values
	('Tech Elevator','7100 Euclid Ave #140, Cleveland, OH 44103', 41.50383000392251,-81.63868811898969),
	('Canine Meadow Dog Park', '9038 Euclid Chardon Rd, Kirtland, OH 44094', 41.578580, -81.319240),
	('affoGATO Cat Cafe', '761 Starkweather Ave, Cleveland, OH 44113', 41.477340, -81.682290)

select * from locations

--creating users from web app
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('brandon', 'V0lRjxFQxgKeP+/h5IbKqnAPQoU=', 'Rz+Z8yMoqIg=', 'user');
insert into users (username, password_hash, salt, user_role) values ('rosa', 'EBbMAfKO8NXslvRz6GCCVmeV6ig=', 'ybDc/+RXVqs=', 'user');
insert into users (username, password_hash, salt, user_role) values ('paul', 'fp9wBngxMgdo2HIcts7YLBJdhyU=', 'Sqzc1pc53es=', 'user');
insert into users (username, password_hash, salt, user_role) values ('aklile', 'HqkX65RZf4sM+SaOwjcVhDKJgvY=', 'v7N29suiPBA=', 'user');

select * from users

--insert a playdate
insert into playdates(date,user_id,location_id) values
	('04-20-2021',5,1),
	('05-12-2021',4,2),
	('11-11-2021',3,3)


select * from fullPlaydates




--insert a pet 
insert into pets(user_id, pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values (4,'Ramona', '12-02-2018', 'F', 1, 'Mix', 'Spotted', 'A goofy lass');
insert into pets(user_id, pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values (5,'Maggie', '05-12-2019', 'F', 1, 'Goldendoodle', 'Golden', 'Perfect snuggler'  );
insert into pets(user_id, pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values (6,'Carl', '01-28-2020', 'M', 1, 'Mix', 'Brown and Black', 'Loud boy');
insert into pets(user_id, pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values (3,'Hanzo', '01-15-2015', 'M', 2, 'Brown Tabby', 'Brown and Black', 'HAMzo is fat');
insert into pets(user_id, pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values (3,'Pippin', '03-07-2019', 'M', 2, 'American Shorthair', 'White and Gray', 'Catch me if you can');

select * from pets



--connecting pets to play dates
insert into playdate_pet(playdate_id, pet_id) Values(1,3);
insert into playdate_pet(playdate_id, pet_id) Values(1,1);
insert into playdate_pet(playdate_id, pet_id) Values(2,4);
insert into playdate_pet(playdate_id, pet_id) Values(2,2);
insert into playdate_pet(playdate_id, pet_id) Values(3,4);
insert into playdate_pet(playdate_id, pet_id) Values(3,5);

select * from playdate_pet

--populating pet personality
insert into personality_pet (personality_id, pet_id) values (6, 1);
insert into personality_pet (personality_id, pet_id) values (1, 1);
insert into personality_pet (personality_id, pet_id) values (1, 3);
insert into personality_pet (personality_id, pet_id) values (5, 3);
insert into personality_pet (personality_id, pet_id) values (2, 3);
insert into personality_pet (personality_id, pet_id) values (1, 4);
insert into personality_pet (personality_id, pet_id) values (1, 2);
insert into personality_pet (personality_id, pet_id) values (7, 2);
insert into personality_pet (personality_id, pet_id) values (1, 5);



