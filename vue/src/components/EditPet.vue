<template>
	<div>
		<form class="editPet" v-on:submit.prevent="updatePet">
			<h1>Edit {{ pet.petName }}'s- Information</h1>
			<p>
				Pet Name:
				<input type="text" v-model="pet.petName" />
			</p>

			<li>
				<span>Your Pets Bio:</span>

				<textarea v-model="pet.bio"></textarea>
			</li>

			<li>
				Pet Type
				<select
					name="petType"
					id="petType"
					class="dropdown-content"
					v-model="pet.petTypeId"
				>
					<option value=""></option>
					<option
						v-for="(item, key) in $store.state.validPetTypes"
						v-bind:key="key"
						v-bind:value="key"
					>
						{{ item }}
					</option>
				</select>
			</li>

			<li>
				Pet Sex
				<select
					name="petSex"
					id="petSex"
					class="dropdown-content"
					v-model="pet.sex"
				>
					<option value=""></option>
					<option value="F">Female</option>
					<option value="M">Male</option>
				</select>
			</li>
			<li>Breed: <input type="text" v-model="pet.breed" /></li>
			<li>Color: <input type="text" v-model="pet.color" /></li>

			<li>
				Personality
				<ul>
					<li
						v-for="(item, key) in $store.state.validPersonalities"
						v-bind:key="key"
					>
						{{ item }}
						<input
							type="checkbox"
							v-bind:value="key"
							v-model="pet.personalityIds"
						/>
					</li>
				</ul>
			</li>

			<button type="submit" class="smallGreenButton">Submit</button>
			<router-link
				v-bind:to="{ name: 'profile' }"
				tag="button"
				id="petForm"
				class="smallGreenButton"
				>Cancel</router-link
			>
		</form>
	</div>
</template>

<script>
// import PetDetails from "../components/PetDetails.vue";
import PetService from "@/services/PetsService";
export default {
	name: "EditPet",
	components: {},
	data() {
		return {
			pet: {},
			allPersonalities: {},
		};
	},
	computed: {
		currentUser() {
			return this.$store.state.user;
		},
	},
	methods: {
		getPetById() {
			PetService.getPetById(this.$route.params.petId).then((response) => {
				this.pet = response.data;
			});
		},
		updatePet() {
			PetService.updatePet(this.$route.params.petId, this.pet).then(
				(response) => {
					this.pet = response.data;
				}
			);
			this.$router.push({ name: "profile" });
		},
	},
	created() {
		this.getPetById();
	},
};
</script>

<style scoped>
</style>