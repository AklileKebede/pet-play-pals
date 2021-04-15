<template>
  <div class="profile">
    <h1>Profile</h1>
    <div id="profileDetails">
      <div id="userDetails" class="bubble foggy-gray-bg">
        <h2>My Info</h2>
        <!-- <p>These are your profile details:</p> -->
        <p>Username: {{ currentUser.username }}</p>
        <p>
          <router-link
            v-bind:to="{
              name: 'EditUsername',
            }"
            tag="button"
            id="editusername"
            class="smallGreenButton"
            >Edit Username</router-link
          >
        </p>
      </div>
      <!-- <p>User Id: {{currentUser.userId}}</p> -->
      <!-- <p>Role: {{currentUser.role}}</p> -->
      <div id="petList" class="bubble foggy-gray-bg">
        <h2>My Pets</h2>
        <ul>
          <li v-for="pet in pets" v-bind:key="pet.id">
            <pet-details v-bind:pet="pet"></pet-details>
            <router-link
              v-bind:to="{
                name: 'EditPet',
                params: { petId: `${pet.petId}` },
              }"
              tag="button"
              id="editPet"
              class="smallGreenButton"
              >Edit Pet</router-link
            >
          </li>
        </ul>
        <router-link
          to="/PetForm"
          tag="button"
          id="petForm"
          class="smallGreenButton"
          >Add a new Pet!</router-link
        >
      </div>
      <div id="playdateList" class="bubble foggy-gray-bg">
        <h2>My Playdates</h2>
        <p>Playdates organized by me:</p>
		<div id="myPlayDates">
			<playdate-preview v-for="playdate in playdates" v-bind:key="playdate.id" v-bind:playdate="playdate"></playdate-preview>
		</div>
      </div>
    </div>
  </div>
</template>

<script>
import PetDetails from "../components/PetDetails.vue";
import PetService from "@/services/PetsService";
import PlaydatesService from "@/services/PlaydatesService";
import PlaydatePreview from "@/components/PlaydatePreview.vue";

export default {
  components: { PetDetails, PlaydatePreview },
  name: "profile",
  data() {
    return {
      pets: {},
      playdates: {},
    };
  },
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
    getPets() {
      PetService.getPets({ userId: this.currentUser.userId }).then(
        (response) => {
          this.pets = response.data;
        }
      );
    },
    getPlaydates() {
      PlaydatesService.getPlaydates({ userId: this.currentUser.userId }).then(
        (response) => {
          this.playdates = response.data;
        }
      );
    },
  },
  created() {
    this.getPets();
    this.getPlaydates();
  },
};
</script>
<style scoped>
#userDetails {
  grid-area: userDetails;
}
#playdateList {
  grid-area: playdateList;
}
#petList {
  grid-area: petList;
}
div#profileDetails {
  display: grid;
  /* grid-template-columns: 1fr 1fr 1fr; */
  grid-template-areas:
    "userDetails userDetails"
    "petList playdateList";
  grid-gap: 10px;
}
ul {
  list-style-type: none;
  margin: auto;
  list-style-position: outside;
}div#myPlayDates {
	display: flex;
	flex-direction: column;
	gap: 10px;
}
</style>
