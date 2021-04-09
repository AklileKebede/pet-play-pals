import axios from 'axios';

const http = axios.create(
	{// Base URL of every endpoint that we will be calling
		baseURL: process.env.VUE_APP_REMOTE_API
	});
export default {
	getAllPets() {
		return http.get('/pets');
	},
	getPetById(petId){
		return http.get(`/pets/${petId}`)
	},
	getPetsForUser(userId){
		return http.get(`/pets?userId=${userId}`)
	},
	getAllPersonalities(){
		return http.get('/pets/personalities')
	},
	getPetType(){
		return http.get('/types')
	},
	updatePet(petId, pet) {
	return http.put(`/pets/${petId}`,pet)
	},



}