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
		<div class="map" id="map" />
		<!-- <div>
			
			<p id='lat'>Current Lat: {{currentLocation.lat}}</p>
			<p id='lng'>Current lng: {{currentLocation.lng}}</p>
		</div> -->
	</div>
</template>

<script>
import gmapsInit from "@/utils/gmaps";

export default {
	data(){
		return{
			currentLocation:{
				lat:0,
				lng:0
			}
		}
	},
	name: "playdate-map",
	async mounted() {
		//iniialize the google maps api
		const google = await gmapsInit();
		//create a new map
		const map = new google.maps.Map(document.getElementById("map"), {
			//these coords are for downtown Cleveland OH
			center: { lat: 41.505493, lng: -81.681290 },
			zoom: 13,
		});
		//the search box criteria 
		const input = document.getElementById("pac-input");

		const options = {
			componentRestrictions: { country: "us" },
			fields: ["formatted_address", "geometry", "name" ],
			origin: map.getCenter(),
			strictBounds: false,
			types: ["geocode","establishment"],
		};
		//map.controls[google.maps.ControlPosition.TOP_RIGHT].push(card);
		const autocomplete = new google.maps.places.Autocomplete(
			input,
			options
		);
		// Bind the map's bounds (viewport) property to the autocomplete object,
		// so that the autocomplete requests use the current map bounds for the
		// bounds option in the request.
		autocomplete.bindTo("bounds", map);
		const infowindow = new google.maps.InfoWindow();
		const infowindowContent = document.getElementById("infowindow-content");
		infowindow.setContent(infowindowContent);
		const marker = new google.maps.Marker({
			map,
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
				map.fitBounds(place.geometry.viewport);
			} else {
				map.setCenter(place.geometry.location);
				map.setZoom(17);
			}
			marker.setPosition(place.geometry.location);
			//when the marker is changed, lets update our displayed coords in the HTML
			this.currentLocation.lat = place.geometry.location.lat();
			this.currentLocation.lng = place.geometry.location.lng();
			marker.setVisible(true);
			infowindowContent.children["place-name"].textContent = place.name;
			infowindowContent.children["place-address"].textContent =
				place.formatted_address;
			infowindow.open(map, marker);
		});

		
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