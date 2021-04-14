<template>
  <div id="createPlaydate">
    <form id="playdateForm" v-on:submit.prevent="submitForm">
      <h1>Create New Playdate</h1>
      <li>
        <label for="startDatetime">Playdate Start Time:</label>
        <input
          v-model="playdate.startDatetime"
          id="startDatetime"
          type="datetime-local"
          name="startDatetime"
          required
        />
        <span class="validity"></span>
      </li>
      <li>
        <label for="endDatetime">Playdate End Time:</label>
        <input
          v-model="playdate.endDatetime"
          id="endDatetime"
          type="datetime-local"
          name="endDatetime"
          required
        />
        <span class="validity"></span>
      </li>
      <li>
        <label for="description">Playdate Discription:</label>
        <textarea
          id="description"
          v-model="playdate.description"
          placeholder="Please add playdate description (including but not limited to arrival details, changes to playdate, activity details, and etc."
          style="width: -webkit-fill-available"
        ></textarea>
      </li>
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
      <button
        type="submit"
        class="smallGreenButton"
        onclick="compareDateTime()"
      >
        Submit
      </button>
      <router-link
        to="/home/profile"
        tag="button"
        id="creatPlaydate"
        class="smallGreenButton"
        >Cancel</router-link
      >
    </form>

    <div id="searchMap" class="bubble foggy-gray-bg">
      <div class="map" id="map" />
    </div>
  </div>

  <!-- make me a new playdate
	location +
	map +
	playdate organizer
	time 
	whoch pet
	pet types acceptable +
	delete
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
      playdate: {
        startDatetime: {},
        endDatetime: {},
        petTypesPermitted: {},
        personalitiesPermitted: {},
        location: {},
      },
    };
  },
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
    submitForm() {
      this.stripNullKeys();
      this.playdate.location = this.$store.state.currentMapMarker;
      PlaydatesService.addNewPlaydate(this.playdate).then(() => {
        this.$router.push({ name: "profile" });
      });
    },
    stripNullKeys() {
      for (let dict of [
        this.playdate.petTypesPermitted,
        this.playdate.personalitiesPermitted,
      ]) {
        for (let key in dict) {
          if (dict[key] == "null") {
            delete dict[key];
          }
        }
      }
    },
    compareDateTime() {
      let startTime = this.playdate.startDatetime;
      let endTime = this.playdate.endDatetime;
      if (startTime > endTime) {
        alert(
          "Playdate start time is greater then end time, please make sure playdate ends after it starts"
        );
      }
      if (startTime === endTime) {
        alert(
          "Playdate start Time is equal to end time, please make sure playdate ends after it starts"
        );
      }
      console.log(startTime);
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

#playdateForm {
  grid-area: playdateForm;
}
#searchMap {
  grid-area: searchMap;
}

div#createPlaydate {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas:
    "playdateForm searchMap"
    "playdateForm searchMap";
  grid-gap: 10px;
}
</style>