<template>
  <div class="playdates">
    <h1>Play Dates Search</h1>
    <table class="form">
      <tr>
        <tr>Pet Type:</tr>
        <td class="dropdown">
          <select name="searchPetType" id="petType" class="dropdown-content">
            <option value=""></option>
            <option value="dog">Dog</option>
            <option value="cat">Cat</option>
          </select>
        </td>

        <tr>Personality:</tr>

        <td class="dropdown">
          <select
            name="searchPersonality"
            id="personality"
            class="dropdown-content"
          >
            <option value=""></option>
            <option value="friendly">Friendly</option>
            <option value="Plays Rough">Plays Rough</option>
            <option value="shy">Shy</option>
            <option value="skittish">Skittish</option>
            <option value="highEnergy">High-energy</option>
            <option value="Reactive">Reactive</option>
            <option value="Gentle">Gentle</option>
          </select>
        </td>
        <tr>Location:</tr>
        <td>
          <input
            type="text"
            v-model="location"
            placeholder="zip code"
            class="dropdown-content"
          />
        </td>
        <tr>
          <button type="button" @click="getData" class="green-button">Search</button>
        </tr>
    </table>
    <h2>Search Results</h2>
    <!-- Shows below the list of playdates , TODO need to change to show on a search page-->
    <playdate-list :playdates="playdates"></playdate-list>
  </div>
</template>

<script>
import PlaydateList from "@/components/PlaydateList.vue";
import api from "@/services/AuthService.js";
import "@/cssStyles/style.css"

export default {
  name: "playdates",
  components: {
    "playdate-list": PlaydateList,
  },
  data() {
    return {
      searchPetType: "",
      searchPersonality: "",
      playdates: [],
      location: "",
    };
  },
  methods: {
    getData() {
      // TODO 01: use a service to get data from the server populate the cities array
      // TODO 02: add in the query string parameters
      api
        .getPlaydates(this.searchPetType, this.searchPersonality)
        .then((resp) => {
          this.playdates = resp.data;
        });
    },
  },
  created() {
    this.searchPetType = this.$route.query.petType;
    this.searchPersonality = this.$route.query.personality;
    // If PetType is true then get data
    if (this.searchPetType) {
      this.getData();
    }
  },
};
</script>

<style>
.playdates{
  display: flex;
    flex-direction: column;
    align-items: center;
}

.dropdown:hover .dropdown-content {
  display: block;
}
.dropdown {
  position: relative;
  display: inline-block;
}
.dropdown-content {
  position: relative;
  background-color: whitesmoke;
  color: #0d7685;

  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
}
h1{
  text-align: center;
}
</style>