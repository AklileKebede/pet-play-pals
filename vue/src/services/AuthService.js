import axios from 'axios';
import axiosRetry from 'axios-retry'



const http = axios.create(
	{// Base URL of every endpoint that we will be calling
		baseURL: process.env.VUE_APP_REMOTE_API
	});

axiosRetry(http, {
	retries: 15,
	retryCondition: true
});
export default {

	login(user) {
		return axios.post('/login', user)
	},

	register(user) {
		return axios.post('/register', user)
	},

	getPlaydates(petType, personality) {
		let url = '/playdates'
		if (petType) {
			url += `?petType=${petType}`;
			if (personality) {
				url += `&personality=${personality}`
			}
		}
		return http.get(url)
	},


}
