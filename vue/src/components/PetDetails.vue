<template>
  <div class="petDetails">
    <ul>
      <li>Name: {{ pet.petName }}</li>
      <li>Bio: {{ pet.bio }}</li>
      <li>Pet Type: {{ pet.petType }}</li>
      <li>Sex: {{ pet.sex }}</li>
      <li>Breed: {{ pet.breed }}</li>
      <li>Color: {{ pet.color }}</li>
      <li>Personality: {{ pet.personality }}</li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  props: {
    pet: Object,
  },
};
</script>

<style scoped>
.petDetails {
  background-color: rgb(80, 66, 19);
  color: burlywood;
}

</style>