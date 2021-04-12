--given a playdate ID, get all the pets that are participating in that playdate
select pp.playdate_id,p.* from playdate_pet as pp join fullPets as p on pp.pet_id = p.pet_id where playdate_id = 1


--filter

declare @userId as int=5;
select distinct fullPlaydates.*, fullPets.pet_type_id  from
	fullPlaydates join playdate_pet as pp on fullPlaydates.playdate_id = pp.playdate_id
	join fullPets on pp.pet_id = fullPets.pet_id where ( (pet_type_id in ({0})) or (-1 in ({0})));


select * from playdateIdsAndPetTypeIds

select * from fullPlaydates where (playdate_id in (select playdate_id from playdateIdsAndPetTypeIds where( (pet_type_id in (1,2)) or (-1 in (1,2)) ) ))