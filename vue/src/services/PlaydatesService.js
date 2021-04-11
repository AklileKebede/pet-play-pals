import axios from 'axios';

export default{
getAllPlaydates(){
    return axios.get("/playdates")
},
getPlaydatesForUser(userId){
    return axios.get(`/playdates?userId=${userId}`)
},
updatePlaydate(playdateId, playdate) {
	return axios.put(`/playdates/${playdateId}`, playdateId)
	},
addNewPlaydate(playdate) {
		return axios.post("/playdates", playdate);
	},








}