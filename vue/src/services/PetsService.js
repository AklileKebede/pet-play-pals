import axios from 'axios';

export default {
	getAllPets() {
		return axios.get('/pets');
	},
	getPetById(petId){
		return axios.get(`/pets/${petId}`)
	},
	// todo: make this NOT cache
	getPetsForUser(userId){
		return axios.get(`/pets?userId=${userId}`)
	},
	getAllPersonalities(){
		return axios.get('/pets/personalities')
	},
	getAllPetTypes(){
		return axios.get('/pets/types')
	},
	updatePet(petId, pet) {
		return axios.put(`/pets/${petId}`,pet)
	},
	addNewPet(pet) {
		return axios.post("/pets", pet);
	},



}