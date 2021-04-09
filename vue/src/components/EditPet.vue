<template>
  <div>
    <form class="editPet">
      <h1>Edit {{ petName }}- Information</h1>
      <p>
        Pet Name:
        <input type="text" v-model="petInfo" placeholder="Pet Name" />
      </p>

      <li>
        <span>Your Pets Bio:</span>
        <p style="white-space: pre-line">{{ message }}</p>

        <textarea
          v-model="message"
          placeholder="Tell us about your pet!"
        ></textarea>
      </li>

      <li>
        Pet Type
        <select name="petType" id="petType" class="dropdown-content">
          <option value=""></option>
          <option value="dog">Dog</option>
          <option value="cat">Cat</option>
        </select>
      </li>

      <li>
        Pet Sex
        <select name="petSex" id="petSex" class="dropdown-content">
          <option value=""></option>
          <option value="female">Female</option>
          <option value="male">Male</option>
        </select>
      </li>
      <li>
        Breed: <input type="text" v-model="location" placeholder="Breed" />
      </li>
      <li>
        Color: <input type="text" v-model="location" placeholder="Color" />
      </li>

      <li>Personality</li>
      <li>
        Friendly
        <input
          type="checkbox"
          value="Friendly"
          id="Friendly"
          v-model="petPersonality"
        />
      </li>
      <li>
        Plays Rough
        <input
          type="checkbox"
          value="Plays Rough"
          id="PlaysRough"
          v-model="petPersonality"
        />
      </li>
      <li>
        Shy
        <input type="checkbox" value="Shy" id="Shy" v-model="petPersonality" />
      </li>
      <li>
        Skittish
        <input
          type="checkbox"
          value="Skittish"
          id="Skittish"
          v-model="petPersonality"
        />
      </li>
      <li>
        High-energy
        <input
          type="checkbox"
          value="High-energy"
          id="High-energy"
          v-model="petPersonality"
        />
      </li>
      <li>
        Reactive<input
          type="checkbox"
          value="Reactive"
          id="Reactive"
          v-model="petPersonality"
        />
      </li>
      <li>
        Gentle<input
          type="checkbox"
          value="Gentle"
          id="Gentle"
          v-model="petPersonality"
        />
      </li>
    </form>

    <button type="button" @click="getData" class="smallGreenButton">
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
// import PetDetails from "../components/PetDetails.vue";
import PetService from "@/services/PetsService";
export default {
  name: "EditPet",
  components: {},
  data() {
    return {
      pet: {},
    };
  },
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
    getPets() {
      PetService.getPetsForUser(this.currentUser.userId).then((response) => {
        this.pets = response.data;
      });
    },
    getPetsForUser() {},
    getPetById() {},
  },
  created() {
    this.getPets();
  },
};
</script>

<style scoped>
</style>