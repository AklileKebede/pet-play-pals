<template>
	<div>
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
					<li v-for="pet in playdate.participants" v-bind:key="pet">
						<pet-details v-bind:pet="pet"></pet-details>
					</li>
				</ul>
			</li>
			<li>
				<form
					class="addPetToPlaydate"
					v-on:submit.prevent="updatePlaydate"
				>
					Add your pet to playdate:
					<ul>
						<li
							v-for="(item, key) in $store.state.user"
							v-bind:key="key"
						>
							<label v-bind:for="key">{{ item }}</label>
						</li>
					</ul>
				</form>
			</li>
		</ul>
	</div>
</template>

<script>
import PlaydatesService from "../services/PlaydatesService.js";
import petDetails from '../components/PetDetails'

export default {
	name: "playdate-details",
	components:{petDetails},
	props:{
		playdate : Object
	},
	data() {
		return {
			pet: {},
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
	}
};
</script>

<style>
</style>