--given a playdate ID, get all the pets that are participating in that playdate
select pp.playdate_id,p.* from playdate_pet as pp join fullPets as p on pp.pet_id = p.pet_id where playdate_id = 1


--filter

declare @userId as int=5;
declare @petTypeIds as 
select * from fullPlaydates where (@userId = -1 OR user_id = @userId);