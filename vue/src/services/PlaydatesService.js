import axios from 'axios';

export default {
	getPlaydates(filterParams){
		return axios.get("/playdates", {params:filterParams})
	},
	updatePlaydate(playdateId, playdate) {
		return axios.put(`/playdates/${playdateId}`, playdate)
	},
	addNewPlaydate(playdate) {
		return axios.post("/playdates", playdate);
	},
}