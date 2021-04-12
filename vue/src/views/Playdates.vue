<template>
	<div id="playdateSearch">
		<div id="searchFunction" class="bubble foggy-gray-bg">
			<h1>Play Dates Search</h1>
			<table class="form">
				<tr></tr>
				<tr>
					Pet Type:
				</tr>
				<td class="dropdown">
					<select
						name="searchPetType"
						id="petType"
						class="dropdown-content"
						v-model="filterParams.petTypeId"
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
				</td>

				<tr>
					Personality:
				</tr>

				<td class="dropdown">
					<!-- <select
            name="searchPersonality"
            id="personality"
            class="dropdown-content"
             v-model="filterParams.personalityIds"
          > -->
					<ul>
						<li
							v-for="(item, key) in $store.state
								.validPersonalities"
							v-bind:key="key"
						>
							{{ item }}
							<input
								type="checkbox"
								v-bind:value="key"
								v-model="filterParams.personalityIds"
							/>
						</li>
					</ul>
				</td>
				<tr>
					Location:
				</tr>
				<td>
					<input
						type="text"
						v-model="filterParams.location"
						placeholder="zip code"
						class="dropdown-content"
					/>
				</td>
				<tr>
					<button type="button" @click="getData" class="green-button">
						Search
					</button>
				</tr>
			</table>
		</div>
		<div id="searchMap" class="bubble foggy-gray-bg">
			<playdate-map></playdate-map>
		</div>
		<div id="searchResults" class="bubble foggy-gray-bg">
			<h2>Search Results</h2>
			<!-- Shows below the list of playdates , TODO need to change to show on a search page-->
		</div>
	</div>
</template>

<script>
// import api from "@/services/AuthService.js";
import PlaydatesService from "@/services/PlaydatesService";
import "@/cssStyles/style.css";
import PlaydateMap from "../components/PlaydateMap.vue";

export default {
	name: "playdates",
	components: { PlaydateMap },
	data() {
		return {
			// searchPetType: "",
			// searchPersonality: "",
			// location: "",
			playdates: {},
			filterParams: { personalityIds: [] },
		};
	},
	methods: {
		getData() {
			// TODO 02: add in the query string parameters
			PlaydatesService.getPlaydates(this.filterParams).then((resp) => {
				this.playdates = resp.data;
			});
		},
		// search() {
		//   PlaydatesService.getPlaydates().then((response) => {
		//     this.playdates = response.data;
		//   });
		// },
	},
	created() {
		// this.searchPetType = this.$route.query.petType;
		// this.searchPersonality = this.$route.query.personality;
		// If PetType is true then get data
		// if (this.searchPetType) {
		this.getData();
		// this.search();
		// }
	},
};
</script>

<style>
.playdates {
	display: flex;
	flex-direction: column;
	align-items: center;
}

.dropdown:hover .dropdown-content {
	display: block;
}
.dropdown {
	position: relative;
	display: inline-block;
}
.dropdown-content {
	position: relative;
	background-color: whitesmoke;
	color: #0d7685;

	min-width: 160px;
	box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
	z-index: 1;
}
h1 {
	text-align: center;
}


#searchFunction {
	grid-area: searchFunction;
}
#searchMap {
	grid-area: searchMap;
}

#searchResults {
	grid-area: searchResults;
}
div#playdateSearch {
	display: grid;
	grid-template-columns: 1fr 1fr;
	grid-template-areas: 
		"searchFunction searchMap"
		"searchResults searchResults";
	grid-gap: 10px;
}
</style>