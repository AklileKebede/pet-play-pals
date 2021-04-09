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
				<router-link   v-bind:to="{name: 'EditPet', params:{petId: `${pet.petId}`}}"  tag="button" id="editPet" class="smallGreenButton"
      >Edit Pet</router-link>
			</li>
			<!-- <router-link :to="{path: bus.url + data.item, query: { item: { "id": 1, "bus_number": "xx-xx-xxx", "bus_color": "black" }
    }
}"> -->
		</ul>
		<router-link to="/PetForm" tag="button" id="petForm" class="smallGreenButton"
      >Add a new Pet!</router-link>
	</div>
</template>

<script>
import PetDetails from "../components/PetDetails.vue";
import PetService from "@/services/PetsService";
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
	methods: {
		getPets() {
			PetService.getPetsForUser(this.currentUser.userId).then(
				(response) => {
					this.pets = response.data;
				}
			);
		},
	},
	created() {
		this.getPets();
	},
};
</script>
<style scoped>
.smallGreenButton {
    background: #69c181;
    border-radius: 11px;
    width: 100px;
    height: 25px;
    color: #ffffff;
    display: inline-block;
    font: normal bold 12px/25px "Open Sans", sans-serif;
    text-align: center;
    text-decoration: none;
    margin: 6px;
    border: none;
}
</style>
