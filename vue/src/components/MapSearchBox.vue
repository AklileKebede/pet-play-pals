<template>
	<div>
		<input
			id="pac-input"
			class="controls"
			type="text"
			placeholder="Search Box"
		/>
		<div id="infowindow-content">
			<span id="place-name" class="title"></span><br />
			<span id="place-address"></span>
		</div>
		<!-- <div class="map" id="map" /> --> 
		<!-- <div>
			
			<p id='lat'>Current Lat: {{currentLocation.lat}}</p>
			<p id='lng'>Current lng: {{currentLocation.lng}}</p>
		</div> -->
	</div>
</template>

<script>
import gmapsInit from "@/utils/gmaps";
import PlaydatesService from "@/services/PlaydatesService";

export default {
	data() {
		return {
			currentLocation: {
				lat: 0,
				lng: 0,
			},
			playdates: [],
			map: {},
			markers: [],
			google: {},
		};
	},
	name: "playdate-map",
	methods: {
		getPlaydates() {
		},
		async initMap() {
			//iniialize the google maps api
			this.google = await gmapsInit();
			let google = this.google;
			//create a new map
			this.map = new google.maps.Map(document.getElementById("map"), {
				//these coords are for downtown Cleveland OH
				center: { lat: 41.505493, lng: -81.68129 },
				zoom: 13,
			});
			//the search box criteria
			const input = document.getElementById("pac-input");

			const options = {
				componentRestrictions: { country: "us" },
				fields: ["formatted_address", "geometry", "name"],
				origin: this.map.getCenter(),
				strictBounds: false,
				types: ["geocode", "establishment"],
			};
			//map.controls[google.maps.ControlPosition.TOP_RIGHT].push(card);
			const autocomplete = new google.maps.places.Autocomplete(
				input,
				options
			);
			// Bind the map's bounds (viewport) property to the autocomplete object,
			// so that the autocomplete requests use the current map bounds for the
			// bounds option in the request.
			autocomplete.bindTo("bounds", this.map);
			const infowindow = new google.maps.InfoWindow();
			const infowindowContent = document.getElementById(
				"infowindow-content"
			);
			infowindow.setContent(infowindowContent);
			const marker = new google.maps.Marker({
				map: this.map,
				anchorPoint: new google.maps.Point(0, -29),
			});
			autocomplete.addListener("place_changed", () => {
				infowindow.close();
				marker.setVisible(false);
				const place = autocomplete.getPlace();

				if (!place.geometry || !place.geometry.location) {
					// User entered the name of a Place that was not suggested and
					// pressed the Enter key, or the Place Details request failed.
					window.alert(
						"No details available for input: '" + place.name + "'"
					);
					return;
				}

				// If the place has a geometry, then present it on a map.
				if (place.geometry.viewport) {
					this.map.fitBounds(place.geometry.viewport);
				} else {
					this.map.setCenter(place.geometry.location);
					this.map.setZoom(17);
				}
				marker.setPosition(place.geometry.location);
				//when the marker is changed, lets update our displayed coords in the HTML
				this.$store.commit("SET_CURRENT_MAP_MARKER",{lat:place.geometry.location.lat(),lng:place.geometry.location.lng()},address:);
				this.currentLocation.lat = place.geometry.location.lat();
				this.currentLocation.lng = place.geometry.location.lng();
				marker.setVisible(true);
				infowindowContent.children["place-name"].textContent =
					place.name;
				infowindowContent.children["place-address"].textContent =
					place.formatted_address;
				infowindow.open(this.map, marker);

			});
		},
		addMarkers() {
			PlaydatesService.getPlaydates({}).then((response) => {
				this.playdates = response.data;
				for (let playdate of this.playdates) {
					this.markers.push(
						new this.google.maps.Marker({
							position:playdate.location,
							label: playdate.location.name,
							map: this.map,
						})
					);
				}
			});
		},
        
	},
	created() {
		this.initMap();
		//todo: load the playdates to display. thius dont work so good atm
		//this.addMarkers();

		//on map load, we need to clear out the current map marker from the store
		this.$store.commit("SET_CURRENT_MAP_MARKER",{});
	},
};
</script>

<style>
html,
body {
	margin: 100;
	padding: 0;
}

.map {
	width: 100%;
	height: 50vh;
}
</style>