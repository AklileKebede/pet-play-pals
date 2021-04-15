<template>
  <div id="petDetails" class="petDetails bubble foggy-gray-bg">
    <ul id="petDetailsArea">
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
          <li v-for="personality in pet.personalities" v-bind:key="personality">
            {{ personality }}
          </li>
        </ul>
      </li>
    </ul>
    <div id="imgArea">
      <!--this is for adding the pet images -->
      <img
        class="profilePic bubble"
        v-bind:src="pet.imgUrl"
        alt="pet profile pic"
      />
    </div>
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
.imgArea {
  grid-area: imgArea;
}
.petDetailsArea {
  grid-area: petDetailsArea;
}
.petDetails {
  display: grid;
  grid-template-areas: "imgArea petDetailsArea";
}

.petDetails {
  list-style-type: none;
  padding: 20px;
  background-color: #0d7685;
  margin: 10px;
}

.profilePic {
  max-height: 200px;
  max-width: 300px;
  align-content: center;
  margin: 10px;
}
</style>