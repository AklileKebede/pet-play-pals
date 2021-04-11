<template>
	<div class="profile">
		<h1>Profile</h1>
		<div id="profileDetails">
			<div id="userDetails" class="bubble foggy-gray-bg">
				<h2>My Info</h2>
				<!-- <p>These are your profile details:</p> -->
				<p>User Name: {{ currentUser.username }}</p>
			</div>
			<!-- <p>User Id: {{currentUser.userId}}</p> -->
			<!-- <p>Role: {{currentUser.role}}</p> -->
			<div id="petList" class="bubble foggy-gray-bg">
				<h2>My Pets</h2>
				<ul>
					<li v-for="pet in pets" v-bind:key="pet.id">
						<pet-details v-bind:pet="pet"></pet-details>
						<router-link
							v-bind:to="{
								name: 'EditPet',
								params: { petId: `${pet.petId}` },
							}"
							tag="button"
							id="editPet"
							class="smallGreenButton"
							>Edit Pet</router-link
						>
					</li>
					<!-- <router-link :to="{path: bus.url + data.item, query: { item: { "id": 1, "bus_number": "xx-xx-xxx", "bus_color": "black" }
    }
}"> -->
				</ul>
				<router-link
					to="/PetForm"
					tag="button"
					id="petForm"
					class="smallGreenButton"
					>Add a new Pet!</router-link
				>
			</div>
			<div id="playdateList" class="bubble foggy-gray-bg">
				<h2>My Playdates</h2>
			</div>
		</div>
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
#userDetails{
	grid-area: userDetails;
}
#playdateList{
	grid-area: playdateList;
}
#petList{
	grid-area: petList;
}
div#profileDetails{
	display:grid;
	grid-template-columns: 1fr 1fr 1fr;
	grid-template-areas: 
	"userDetails petList playdateList";
	grid-gap: 10px;
}
</style>
