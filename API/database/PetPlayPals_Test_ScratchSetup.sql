use PetPlayPals_Test
go

--insert a location
insert into locations(name,address,lat,lng) values
	('Tech Elevator','7100 Euclid Ave #140, Cleveland, OH 44103', 41.50383000392251,-81.63868811898969)

select * from locations

--insert a playdate
insert into playdates(date,location_id) values
	('04-20-2021',1)

select * from playdates

	select * from pets
--insert a pet 
insert into pets(pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values ('Ramona', '12-02-2018', 'F', 1, 'Mix', 'Spotted', 'A goofy lass');
insert into pets(pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values ('Maggie', '05-12-2019', 'F', 1, 'Goldendoodle', 'Golden', 'Perfect snuggler'  );
insert into pets(pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values ('Carl', '01-28-2020', 'M', 1, 'Mix', 'Brown and Black', 'Loud boy');
insert into pets(pet_name, birthday, sex, pet_type_id, pet_breed, color, bio) values ('Hanzo', '01-15-2015', 'M', 2, 'Brown Tabby', 'Brown and Black', 'HAMzo is fat');


INSERT INTO users (username, password_hash, salt, user_role) VALUES ('brandon', 'V0lRjxFQxgKeP+/h5IbKqnAPQoU=', 'Rz+Z8yMoqIg=', 'user');
insert into users (username, password_hash, salt, user_role) values ('rosa', 'EBbMAfKO8NXslvRz6GCCVmeV6ig=', 'ybDc/+RXVqs=', 'user');
insert into users (username, password_hash, salt, user_role) values ('paul', 'fp9wBngxMgdo2HIcts7YLBJdhyU=', 'Sqzc1pc53es=', 'user');
insert into users (username, password_hash, salt, user_role)values ('aklile', 'HqkX65RZf4sM+SaOwjcVhDKJgvY=', 'v7N29suiPBA=', 'user');



