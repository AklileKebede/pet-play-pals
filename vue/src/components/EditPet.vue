<template>
  <div>
    <form class="editPet">
      <h1>Edit {{ pet.petName }}'s- Information</h1>
      <p>
        Pet Name:
        <input type="text" v-model="pet.petName" />
      </p>

      <li>
        <span>Your Pets Bio:</span>
        <p style="white-space: pre-line">{{ message }}</p>

        <textarea
          v-model="pet.bio"
        ></textarea>
      </li>

      <li>
        Pet Type
        <select name="petType" id="petType" class="dropdown-content" v-model="pet.petType">
          <option value=""></option>
          <option value="Dog">Dog</option>
          <option value="Cat">Cat</option>
        </select>
      </li>

      <li>
        Pet Sex
        <select name="petSex" id="petSex" class="dropdown-content" v-model="pet.sex">
          <option value=""></option>
          <option value="F">Female</option>
          <option value="M">Male</option>
        </select>
      </li>
      <li>
        Breed: <input type="text" v-model="pet.breed" />
      </li>
      <li>
        Color: <input type="text" v-model="pet.color" />
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

    <button type="submit" @click="updatePet" class="smallGreenButton">
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
    getPetById() {
      PetService.getPetById(this.$route.params.petId)
      .then(response=>{
        this.pet=response.data;
      });
    },
    updatePet() {
      PetService.updatePet(this.$route.params.petId, this.pet)
      .then(response=>{
        this.pet=response.data;
      });
	},
  },
  created() {
    this.getPetById();
  },
};
</script>

<style scoped>
</style>