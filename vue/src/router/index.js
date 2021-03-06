import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import Logout from '@/views/Logout.vue'
import Register from '@/views/Register.vue'
import store from '@/store/index'
import Profile from '@/views/Profile.vue'
import Playdates from '@/views/Playdates.vue'
import CreatePlaydate from '@/views/CreatePlaydate.vue'
import PlaydateMapView from '@/views/PlaydateMapView.vue'
import PetForm from '@/components/PetForm.vue'
import EditPet from '@/components/EditPet.vue'
import EditUsername from '@/components/EditUsername.vue'
import playdateDetailsView from '../views/PlaydateDetailsView'


Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
	mode: 'history',
	base: process.env.BASE_URL,
	routes: [
		{
			path: '/playdates',
			name: 'playdates',
			component: Playdates,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: '/playdates/:playdateId',
			name: 'playdateDetails',
			component: playdateDetailsView,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: '/createPlaydate',
			name: 'createPlaydate',
			component: CreatePlaydate,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: '/',
			name: 'home',
			component: Home,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: '/home/profile/',
			name: 'profile',
			component: Profile,
			meta: {
				requiresAuth: true
			}
		},
		{
			path: "/login",
			name: "login",
			component: Login,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: "/logout",
			name: "logout",
			component: Logout,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: "/register",
			name: "register",
			component: Register,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: "/map",
			name: "map",
			component: PlaydateMapView,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: "/PetForm",
			name: "PetForm",
			component: PetForm,
			meta: {
				requiresAuth: false
			}
		},
		{
			path: "/EditPet/:petId",
			name: "EditPet",
			component: EditPet,
			meta: {
				requiresAuth: false
			}
		},
		{
			path:"/EditUsername/:userId",
			name: "EditUsername",
			component: EditUsername,
			meta: {
				requiresAuth: false
			}
		},
	]
})

router.beforeEach((to, from, next) => {
	// Determine if the route requires Authentication
	const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

	// If it does and they are not logged in, send the user to "/login"
	if (requiresAuth && store.state.token === '') {
		next("/login");
	} else {
		// Else let them go to their next destination
		next();
	}
});

export default router;
