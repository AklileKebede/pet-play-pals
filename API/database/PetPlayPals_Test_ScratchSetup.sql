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

