<template>
	<div id="petDetails" class="petDetails bubble foggy-gray-bg">
		<ul>
			<li>Name: {{ pet.petName }}</li>
			<li>Bio: {{ pet.bio }}</li>
			<li>Pet Type: {{ pet.petType }}</li>
			<li>Sex: {{ pet.sex }}</li>
			<li>Breed: {{ pet.breed }}</li>
			<li>Color: {{ pet.color }}</li>
			<li>
				Personality:
				<ul>
					<li
						v-for="personality in pet.personalities"
						v-bind:key="personality"
					>
						{{ personality }}
					</li>
				</ul>
			</li>
			<!--this is for adding the pet images -->
			<li>
				<img
					class="profilePic"
					v-bind:src="pet.imgUrl"
					alt="pet profile pic"
				/>
			</li>
		</ul>
	</div>
</template>

<script>
export default {
	name: "pet-details",
	data() {
		return {};
	},
	props: {
		pet: Object,
	},
};
</script>

<style scoped>

.profilePic {
	max-height: 200px;
	max-width: 200px;
}
</style>