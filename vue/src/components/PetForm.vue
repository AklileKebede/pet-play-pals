<template>
  <div>
    <form id="PetForm" v-on:submit.prevent="submitForm">
      <h1>Register Your Pet</h1>
      <ul>
        <li>
          <label for="petName">Pet Name:</label>
          <input
            type="text"
            id="petName"
            v-model="pet.petName"
            placeholder="Pet Name"
          />
        </li>
        <li>
          <label for="bio">Your Pets Bio:</label>
        </li>
        <li>
          <textarea
            id="bio"
            v-model="pet.bio"
            placeholder="Pet Bio"
            style="width: -webkit-fill-available"
          ></textarea>
        </li>
      </ul>

      <li>
        <label for="petType">Pet Type</label>

        <select
          name="petType"
          id="petType"
          v-model="pet.petType"
          class="dropdown-content"
        >
          <option value=""></option>
          <option value="dog">Dog</option>
          <option value="cat">Cat</option>
        </select>
      </li>

      <li>
        <label for="petSex">Pet Sex</label>
        <select
          name="petSex"
          id="petSex"
          v-model="pet.petSex"
          class="dropdown-content"
        >
          <option value=""></option>
          <option value="female">Female</option>
          <option value="male">Male</option>
        </select>
      </li>
      <li>
        <label for="breed">Breed: </label>
        <input type="text" id="breed" v-model="pet.breed" placeholder="Breed" />
      </li>
      <li>
        <label for="color">Color: </label>
        <input type="text" id="color" v-model="pet.color" placeholder="Color" />
      </li>
      <li>
        Personality:
        <ul>
          <li v-for="(item, key) in allPersonalities" v-bind:key="key">
            <label v-bind:for="key">{{ item }}</label>
            <input
              v-bind:id="key"
              type="checkbox"
              v-bind:value="key"
              v-model="pet.personalityIds"
            />
          </li>
        </ul>
      </li>
    </form>
    <button type="submit" class="smallGreenButton">
      Submit
    </button>
    <router-link
      to="/home/profile"
      tag="button"
      id="petForm"
      class="smallGreenButton"
      >Cancel</router-link
    >
  </div>
</template>

<script>
import PetService from "@/services/PetsService";
export default {
  name: "PetForm",
  props: {},
  data() {
    return {
      pet: {
        petName: "",
        bio: "",
        petType: "",
        sex: "",
        breed: "",
        color: "",
        petPersonality: [],
      },
      allPersonalities: {},
    };
  },
  methods: {
    getPersonalities() {
      PetService.getAllPersonalities().then((response) => {
        this.allPersonalities = response.data;
      });
    },
    submitForm() {
      // this.isSubmitting = true;
      // const newPet = {
      //   petId: Number(this.petId),
      //   petName: this.pet.petName,
      //   bio: this.pet.bio,
      //   petType: this.pet.petType,
      //   sex: this.pet.sex,
      //   breed:this.pet.breed,
      //   color:this.pet.color,
      //   petPersonality:
      // };

      PetService.addNewPet(this.pet).then(() => {
        this.$router.push({name: "profile"});
      });
    },
  },
  created() {
    this.getPersonalities();
  },
};
</script>

<style scoped>
</style>