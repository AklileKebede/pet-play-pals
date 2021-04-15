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
        Pet Type
        <select
          name="petType"
          id="petType"
          class="dropdown-content"
          v-model="pet.petTypeId"
        >
          <option value=""></option>
          <option
            v-for="(item, key) in $store.state.validPetTypes"
            v-bind:key="key"
            v-bind:value="key"
          >
            {{ item }}
          </option>
        </select>
      </li>
      <li>
        <label for="birthday">Birthday</label>

        <input
          type="date"
          id="birthday"
          v-model="pet.birthday"
          placeholder="today"
        />
      </li>

      <li>
        <label for="petSex">Pet Sex</label>
        <select
          name="petSex"
          id="petSex"
          v-model="pet.sex"
          class="dropdown-content"
        >
          <option value=""></option>
          <option value="F">Female</option>
          <option value="M">Male</option>
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
          <li
            v-for="(item, key) in $store.state.validPersonalities"
            v-bind:key="key"
          >
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
      <button type="submit" class="smallGreenButton">Submit</button>
    </form>

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
  components: {},
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
        personalityIds: [],
      },
      allPersonalities: {},
    };
  },
  methods: {
    submitForm() {
      PetService.addNewPet(this.pet).then(() => {
        this.$router.push({ name: "profile" });
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