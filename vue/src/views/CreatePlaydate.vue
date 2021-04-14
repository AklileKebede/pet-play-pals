<template>
		<div>
		<form id="creatPlaydate" v-on:submit.prevent="submitForm">
			<h1>Create New Playdate</h1>
			<ul>
				<li>
					<!-- Need to adjust so the playdate connects petId -->
					<label for="petName">Playdate created for:{{Playdate.petName}}</label>
				</li>
				<li>
					<label for="description">Playdate description:</label>
				</li>
				<li>
					<textarea
						id="description"
						v-model="Playdate.description"
						placeholder="Playdate Description"
						style="width: -webkit-fill-available"
					></textarea>
				</li>
			</ul>

			<li>
				Pet Type Allowed:
				<select
					name="petType"
					id="petType"
					class="dropdown-content"
					v-model="playdate.petTypeId"
				>
					<option value=""></option>
					<option v-for="(item, key) in $store.state.validPetTypes" v-bind:key="key" v-bind:value="key">{{item}}</option>
				</select>
			</li>
			<li>
				<label for="dateToPlay">Date</label>

				<input
					type="date"
					id="dateToPlay"
					v-model="playdate.startDate"
					placeholder="today"
				/>
			</li>
			<li>
				<label for="EndDate">Date</label>

				<input
					type="date"
					id="endDate"
					v-model="playdate.endDate"
					placeholder="today"
				/>
			</li>

			<li>
				<label for="petSex">Pet Sex</label>
				<select
					name="petSex"
					id="petSex"
					v-model="pet.sex"
					class="dropdown-content"
				>
					<option value=""></option>
					<option value="F">Female</option>
					<option value="M">Male</option>
				</select>
			</li>
			<li>
				<label for="breed">Breed: </label>
				<input
					type="text"
					id="breed"
					v-model="pet.breed"
					placeholder="Breed"
				/>
			</li>
			<li>
				<label for="color">Color: </label>
				<input
					type="text"
					id="color"
					v-model="pet.color"
					placeholder="Color"
				/>
			</li>
			<li>
				Personality:
				<ul>
					<li
						v-for="(item, key) in $store.state.validPersonalities"
						v-bind:key="key"
					>
						<label v-bind:for="key">{{ item }}</label>
						<input
							v-bind:id="key"
							type="checkbox"
							v-bind:value="key"
							v-model="pet.personalityIds"
						/>
					</li>
				</ul>
			</li>
			<button type="submit" class="smallGreenButton">Submit</button>
		</form>

		<router-link
			to="/home/profile"
			tag="button"
			id="petForm"
			class="smallGreenButton"
			>Cancel</router-link
		>
	</div>
		<!-- make me a new playdate
	location
	map
	playdate organizer
	time
	whoch pet
	pet types acceptable
	delet
	-->
</template>

<script>
import PlaydatesService from "../services/PlaydatesService.js"
export default {
	name: "createPlaydate",
	components:{},
	data(){
		return{
			playdate:{},
			allPersonalities:{},
			
		};
	},
	computed: {
		currentUser() {
			return this.$store.state.user;
		},
	},
	methods:{
		submitForm(){
			PlaydatesService.addNewPlaydate(this.playdate)
			.then(()=>{
				this.$router.push({name:""})
			});
		},
	},
	created(){

	},
};
</script>

<style scoped>
</style>