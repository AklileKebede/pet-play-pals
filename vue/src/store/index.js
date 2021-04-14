import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import PetsService from '../services/PetsService'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if (currentToken != null) {
	axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;

}

export default new Vuex.Store({
	state: {
		token: currentToken || '',
		user: currentUser || {},
		validPersonalities: {},
		validPetTypes: {},
		currentMapMarker: {},

	},
	mutations: {
		SET_AUTH_TOKEN(state, token) {
			state.token = token;
			localStorage.setItem('token', token);
			axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
		},
		SET_USER(state, user) {
			state.user = user;
			localStorage.setItem('user', JSON.stringify(user));
		},
		LOGOUT(state) {
			localStorage.removeItem('token');
			localStorage.removeItem('user');
			state.token = '';
			state.user = {};
			axios.defaults.headers.common = {};
		},
		INITIALIZE_LOCAL_STATIC_DB_CACHE(state) {
			PetsService.getAllPersonalities().then((response) => {state.validPersonalities = response.data;}).catch((error) => {alert(`Error getting pet personalities:${error}. Is the backend running?`)});
			PetsService.getAllPetTypes().then((response) => {state.validPetTypes = response.data;}).catch((error) => {alert(`Error getting pet types:${error}. Is the backend running?`)});
		},
		SET_CURRENT_MAP_MARKER(state,latLanLocation) {
			state.currentMapMarker = latLanLocation;

		}
	},
})
