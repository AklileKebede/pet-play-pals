import axios from 'axios';

const http = axios.create(
	{// Base URL of every endpoint that we will be calling
		baseURL: process.env.VUE_APP_REMOTE_API
	});
export default {
	getAllPets() {
		return axios.get('/pets');
	},
	getPetById(petId){
		return axios.get(`/pets/${petId}`)
	},
	getPetsForUser(userId){
		return axios.get(`/pets?userId=${userId}`)
	}

}