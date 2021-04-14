<template>
  <div>
    <form id="creatPlaydate" v-on:submit.prevent="submitForm">
      <h1>Create New Playdate</h1>
      <li>
        Pet Type Allowed:
        <ul>
          <li
            v-for="(item, key) in $store.state.validPetTypes"
            v-bind:key="key"
            v-bind:value="key"
          >
            {{ item }}
            <select v-model="playdate.petTypesPermitted[key]">
              <option value="True">Allowed</option>
              <option value="null">-</option>
              <option value="False">Disallowed</option>
            </select>
          </li>
        </ul>
      </li>
      <li>
        Personality:
        <ul>
          <li
            v-for="(item, key) in $store.state.validPersonalities"
            v-bind:key="key"
          >
            {{ item }}
            <select v-model="playdate.personalitiesPermitted[key]">
              <option value="True">Allowed</option>
              <option value="null">-</option>
              <option value="False">Disallowed</option>
            </select>
          </li>
        </ul>
      </li>
      <li>
        Location:
        <map-searchbox></map-searchbox>
      </li>
      <button type="submit" class="smallGreenButton">Submit</button>
    </form>
    <router-link
      to="/home/profile"
      tag="button"
      id="creatPlaydate"
      class="smallGreenButton"
      >Cancel</router-link
    >
    <div id="searchMap" class="bubble foggy-gray-bg">
      <!-- <playdate-map></playdate-map> -->
      <div class="map" id="map" />
    </div>
  </div>

  <!-- make me a new playdate
	location
	map
	playdate organizer
	time
	whoch pet
	pet types acceptable
	delet
	-->
</template>

<script>
import PlaydatesService from "../services/PlaydatesService.js";
import MapSearchbox from "../components/MapSearchBox.vue";
export default {
  name: "createPlaydate",
  components: { MapSearchbox },
  data() {
    return {
      playdate: { petTypesPermitted: {}, personalitiesPermitted: {} },
    };
  },
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
    submitForm() {
      PlaydatesService.addNewPlaydate(this.playdate).then(() => {
        this.$router.push({ name: "" });
      });
    },
  },
  created() {},
};
</script>

<style scoped>
input:invalid + span:after {
  content: "✖";
  padding-left: 5px;
}

input:valid + span:after {
  content: "✓";
  padding-left: 5px;
}
</style>