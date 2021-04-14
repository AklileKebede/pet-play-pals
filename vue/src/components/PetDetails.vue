<template>
	<div class="petDetails bubble">
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
					class="profilePic bubble"
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
.petDetails{
    height: fit-content;
    width: fit-content;
    list-style-type: none;
    margin: 0;
    padding: 0;
    padding: 20px;
    background-color: #0D7685;
}
.profilePic {
	max-height: 200px;
	max-width: 200px;
  margin: 10px;
}
ul{
  list-style: none;
}

</style>