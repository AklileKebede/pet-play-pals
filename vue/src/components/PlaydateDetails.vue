<template>
  <div>
    <ul>
      <li>Playdate Id: {{ playdate.playdateId }}</li>
      <li>Playdate Start Time:{{ playdate.startDatetime }}</li>
      <li>Playdate End Time: {{ playdate.endDatetime }}</li>
      <li>Location: {{ playdate.location.address }}</li>
      <li>Playdate Discription:{{ playdate.description }}</li>
      <li>Playdate Organizer: {{ playdate.userName }}</li>
      <li>
        Participating Pets:
        <ul>
          <li v-for="pet in playdate.participants" v-bind:key="pet">
            <pet-details v-bind:pet="pet"></pet-details>
          </li>
        </ul>
      </li>
      <li>
        <form class="addPetToPlaydate" v-on:submit.prevent="updatePlaydate">
          Add your pet to playdate:
          <ul>
            <li v-for="(item, key) in $store.state.user" v-bind:key="key">
              <label v-bind:for="key">{{ item }}</label>
            </li>
          </ul>
        </form>
      </li>
    </ul>
  </div>
</template>

<script>
// import PetService from "@/services/PetsService";
import PlaydatesService from "../services/PlaydatesService.js";

export default {
  name: "playdate-details",
  components: {},
  props: {
    playdate: Object,
  },
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
    getPlaydateById() {
      PlaydatesService.getPlaydateById(this.$route.params.playdateId).then(
        (resp) => {
          this.playdate = resp.data;
        }
      );
    },
    updatePlaydate() {
      PlaydatesService.updatePlaydate(
        this.$route.params.playdateId,
        this.playdate
      )
      .then((resp) => {
        this.playdate = resp.data;
      });
    },
  },
};
</script>

<style>
</style>