--given a playdate ID, get all the pets that are participating in that playdate
select pp.playdate_id,p.* from playdate_pet as pp join fullPets as p on pp.pet_id = p.pet_id where playdate_id = 1


--filter

declare @userId as int=5;
select distinct fullPlaydates.*, fullPets.pet_type_id  from
	fullPlaydates join playdate_pet as pp on fullPlaydates.playdate_id = pp.playdate_id
	join fullPets on pp.pet_id = fullPets.pet_id where ( (pet_type_id in ({0})) or (-1 in ({0})));


select * from playdateIdsAndPetTypeIds
declare @disallowedPetTypeID as int=(select pet_type_id from pet_types where pet_type_name = 'Cat');

declare @disallowedPetTypeID as int=-1;
--select playdate_id from playdateIdsAndPetTypeIds where( (pet_type_id  in (@disallowedPetTypeID)))

select fullPlaydates.*,pet_types.pet_type_name from	fullPlaydates
	full join playdateIdsAndPetTypeIds as pdpt on fullPlaydates.playdate_id = pdpt.pet_type_id
	join pet_types on pdpt.pet_type_id = pet_types.pet_type_id
	where (fullPlayDates.playdate_id not in (select playdate_id from playdateIdsAndPetTypeIds where( (pet_type_id  in (@disallowedPetTypeID))) )
)

select * from fullPlaydates