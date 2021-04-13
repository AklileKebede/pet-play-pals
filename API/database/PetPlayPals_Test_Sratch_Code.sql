--given a playdate ID, get all the pets that are participating in that playdate
select pp.playdate_id,p.* from playdate_pet as pp join fullPets as p on pp.pet_id = p.pet_id where playdate_id = 1


--filter


declare @permittedPersonalites as int=-1;

--filter on allowed personality types
select distinct playdate.playdate_id from playdate join playdate_personality_permitted as ppp on playdate.playdate_id = ppp.playdate_id where (((personality_id_is_permitted = 1) and (personality_id in (@permittedPetTypes))) or (-1 in (@permittedPetTypes)))


--filter on allowed pet types
declare @permittedPetTypes as int=2

select distinct playdate.playdate_id from playdate join playdate_pet_type_permitted as ppp on playdate.playdate_id = ppp.playdate_id where (((pet_type_id_is_permitted = 1) and (pet_type_id in ({0}))) or (-1 in ({0})))