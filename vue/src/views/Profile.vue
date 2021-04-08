<template>
	<div class="profile">
		<h1>Profile</h1>
		<!-- <p>These are your profile details:</p> -->
		<p>User Name: {{ currentUser.username }}</p>

		<p>My Address: {{}}</p>
		<!-- <p>User Id: {{currentUser.userId}}</p> -->
		<!-- <p>Role: {{currentUser.role}}</p> -->
		<h2>My Pets</h2>
		<ul>
			<li v-for="pet in myPets" v-bind:key="pet.id">
				<pet-details></pet-details>
			</li>
		</ul>
	</div>
</template>

<script>
import PetDetails from "../components/PetDetails.vue";
import AuthService from "../services/AuthService";
export default {
	components: { PetDetails },
	name: "profile",
	data() {
		return {
			pets: {},
		};
	},
	computed: {
		currentUser() {
			return this.$store.state.user;
		},
	},
	methods: {},
	created() {
		this.pets = AuthService.getAllPets();
	},
};
</script>
