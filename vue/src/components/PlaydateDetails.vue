<template>
	<div>
		<ul>
			<li>Playdate Id: {{ playdate.playdateId }}</li>
			<li>Date: {{ playdate.date }}</li>
			<li>Playdate Organizer: {{ playdate.userId }}</li>
			<li>
				Participating Pets:
				<ul>
					<li v-for="pet in playdate.participants" v-bind:key="pet">
						<pet-details v-bind:pet="pet"></pet-details>
					</li>
				</ul>
			</li>
			<li>Location: {{ playdate.location.address }}</li>
		</ul>
	</div>
</template>

<script>
export default {
	name: "playdate-details",
	components: { PetDetails },
	data() {
		return {};
	},
	props: {
		playdate: Object,
	},
};
</script>

<style>
</style>