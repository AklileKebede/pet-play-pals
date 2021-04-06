<template>
  <div class="playdates">
    <h1>Play Dates</h1>
    <table class="form">
      <tr>
        <td>Pet Type:</td>
        <td class="dropdown">
          <select name="searchPetType" id="petType" class="dropdown-content">
            <option value="dog">Dog</option>
            <option value="cat">Cat</option>
          </select>
        </td>

        <td>Personality:</td>
        
        <td class="dropdown">
          <select
            name="searchPersonality"
            id="personality"
            class="dropdown-content"
          >
            <option value=""></option>
            <option value="friendly">Friendly</option>
            <option value="aggressive">Aggressive</option>
            <option value="shy">Shy</option>
            <option value="skittish">Skittish</option>
            <option value="highEnergy">High-energy</option>
          </select>
        </td>
        <td>Location:</td>
        <td>
          <input type="text" v-model="location" placeholder="zip code" class="dropdown-content"/>
        </td>
        <td>
          <button type="button" @click="getData">Search</button>
        </td>
      </tr>
    </table>
    <!-- Shows below the list of playdates , TODO need to change to show on a search page-->
    <playdate-list :playdates="playdates"></playdate-list>
  </div>
</template>

<script>
import PlaydateList from "@/components/PlaydateList.vue";
import api from "../services/AuthService.js";

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

<style scoped>
.dropdown:hover .dropdown-content {
  display: block;
}
.dropdown {
  position: relative;
  display: inline-block;
}
.dropdown-content {
  position: relative;
  background-color: #d7dff8;
  color:darkslateblue;

  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
}

</style>