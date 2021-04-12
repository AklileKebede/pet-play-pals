import axios from 'axios';

export default {
	getPets(filterParams) {
		return axios.get('/pets',{params:filterParams});
	},
	getPetById(petId){
		return axios.get(`/pets/${petId}`)
	},
	// todo: make this NOT cache
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