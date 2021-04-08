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
			<li v-for="pet in pets" v-bind:key="pet.id">
				<pet-details v-bind:pet="pet"></pet-details>
			</li>
		</ul>
	</div>
</template>

<script>
import PetDetails from "../components/PetDetails.vue";
import PetService from "@/services/PetsService"
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
		this.pets = PetService.getPetsForUser(this.currentUser.userId)
	},
};
</script>
