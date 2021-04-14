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
					<ul>
						<li
							v-for="(item, key) in $store.state.validPetTypes"
							v-bind:key="key"
							v-bind:value="key"
						>
							{{ item }}
							<input
								type="checkbox"
								v-bind:value="key"
								v-model="filterParams.petTypesPermitted[key]"
							/>
						</li>
					</ul>
				</td>
				<tr>
					Personality:
				</tr>

				<td class="dropdown">
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
				<tr>
					<td>
						within
						<input
							type="number"
							v-model.number="filterParams.searchRadius"
						/>
						km
					</td>
				</tr>
				<tr>
					<td>of:<map-searchbox></map-searchbox></td>
				</tr>
				<tr>
					<button type="button" @click="getData" class="green-button">
						Search
					</button>
				</tr>
			</table>
		</div>
		<div id="searchMap" class="bubble foggy-gray-bg">
			<!-- <playdate-map></playdate-map> -->
			<div class="map" id="map" />
		</div>
		<div id="searchResults" class="bubble foggy-gray-bg">
			<h2>Search Results</h2>
			<ul>
				<li v-for="playdate in playdates" v-bind:key="playdate.id">
					<playdate-details v-bind:playdate="playdate"></playdate-details>
				</li>

			</ul>

			<!-- Shows below the list of playdates , TODO need to change to show on a search page-->
		</div>
	</div>
</template>

<script>
// import api from "@/services/AuthService.js";
import PlaydatesService from "@/services/PlaydatesService";
import "@/cssStyles/style.css";
// import PlaydateMap from "../components/PlaydateMap.vue";
import MapSearchbox from "../components/MapSearchBox.vue";
import PlaydateDetails from '../components/PlaydateDetails.vue';

export default {
	name: "playdates",
	components: { MapSearchbox, PlaydateDetails },
	data() {
		return {
			playdates: {},
			filterParams: { personalitiesPermitted: {}, petTypesPermitted: {} },
		};
	},
	methods: {
		getData() {
			this.playdates = {}
			this.filterParams.searchCenter = this.$store.state.currentMapMarker;
			PlaydatesService.getPlaydates(this.filterParams).then((resp) => {
				this.playdates = resp.data;
			});
		},
	},
	created() {
		// this.searchPetType = this.$route.query.petType;
		// this.searchPersonality = this.$route.query.personality;
		this.getData();
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