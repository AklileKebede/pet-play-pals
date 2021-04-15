<template>

	<div id="petDetails" class="petDetails bubble foggy-gray-bg">

		<ul>
			<li>ID: {{ pet.petId }}</li>
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
				<img
					class="profilePic bubble"
					v-bind:src="pet.imgUrl"
					alt="pet profile pic"
				/>
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
.petDetails{
    list-style-type: none;
    padding: 20px;
    background-color: #0D7685;
	margin: 10px;
}

.profilePic {
	max-height: 200px;
	max-width: 300px;
  align-content: center;
  margin: 10px;
}
ul{
  width: 50%;
  margin: auto;
  
}

</style>