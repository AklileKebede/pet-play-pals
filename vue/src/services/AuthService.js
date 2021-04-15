import axios from 'axios';



const http = axios.create(
	{// Base URL of every endpoint that we will be calling
		baseURL: process.env.VUE_APP_REMOTE_API,

	});

// // Add a request interceptor
// axios.interceptors.request.use(function (config) {
// 	// Do something before request is sent
// 	return config;
// }, function (error) {
// 	// Do something with request error
// 	console.log("request error");
// 	return Promise.reject(error);
// });

// // Add a response interceptor
// axios.interceptors.response.use(function (response) {
// 	// Do something with response data
// 	console.log("response error");
// 	return response;
// }, function (error) {
// 	// Do something with response error
// 	return Promise.reject(error);
// });

//add interceptor
axios.interceptors.response.use((response) => response, (error) => {

	if (!error.response) {
		//todo: this is super hacky but maybe works
		//it just keeps reloading the page until it works!
		location.reload(true);

	} else {
		console.error(error)
	}
});

axios.interceptors.request.use((request) => request, (error) => {
	console.log("send error");
	console.error(error)
})

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
