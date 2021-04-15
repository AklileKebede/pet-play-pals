<template>
	<div id="petDetailsOuterDiv">
		<div id="petDetails" class="bubble foggy-gray-bg">
			<ul>
				<li>Playdate Id: {{ playdate.playdateId }}</li>
				<li>Playdate Start Time:{{ playdate.startDateTime }}</li>
				<li>Playdate End Time: {{ playdate.endDateTime }}</li>
				<!-- <li>Location: {{ playdate.location.address }}</li> -->
				<li>Playdate Discription:{{ playdate.description }}</li>
				<li>Playdate Organizer: {{ playdate.userName }}</li>
				<li>
					Participating Pets:
					<ul>
						<li
							v-for="pet in playdate.participants"
							v-bind:key="pet.petId"
						>
							<pet-details v-bind:pet="pet"></pet-details>
						</li>
					</ul>
				</li>
			</ul>
		</div>
		<div id="addRemovePets" class="bubble foggy-gray-bg">
			<ul>
				<li>
					<h2>Add your pets to this playdate</h2>
					<div v-for="myPet in myPets" v-bind:key="myPet.petId">
						<pet-details v-bind:pet="myPet"></pet-details>
						<label v-bind:for="`addRemove${myPet.petId}`"
							>add pet to/remove pet from</label
						>
						<input
							type="checkbox"
							v-bind:value="myPet"
							v-model="playdate.participants"
							v-bind:id="`addRemove${myPet.petId}`"
						/>
					</div>
				</li>
			</ul>
		</div>
	</div>
</template>

<script>
import PlaydatesService from "../services/PlaydatesService.js";
import petDetails from "../components/PetDetails";
import PetService from "../services/PetsService";

export default {
	name: "playdate-details",
	components: { petDetails },
	props: {
		playdate: Object,
	},
	data() {
		return {
			myPets: {},
		};
	},
	computed: {
		currentUser() {
			return this.$store.state.user;
		},
	},
	methods: {
		updatePlaydate() {
			PlaydatesService.updatePlaydate(
				this.$route.params.playdateId,
				this.playdate
			).then((resp) => {
				this.playdate = resp.data;
			});
		},
		getPets() {
			PetService.getPets({ userId: this.currentUser.userId }).then(
				(response) => {
					this.myPets = response.data;
				}
			);
		},
		addPet(petToAdd) {
			this.playdate.participants.push(petToAdd);
		},
		removePet(petToRemove) {
			this.playdate.participants.delete(petToRemove);
		},
	},
	created() {
		this.getPets();
	},
};
</script>

<style>
#playdateDetails {
	grid-area: "playdateDetails";
}
#addRemovePets {
	grid-area: "addRemovePets";
}
#petDetailsOuterDiv {
	display: grid;
	grid-gap: 10px;
	grid-template-areas: "addRemovePets playdateDetails";
}
</style>