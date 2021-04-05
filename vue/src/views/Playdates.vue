<template>
  <div class="playdates">
      <h1>Play Dates</h1>
      <table class="form">
          <tr>
              <td>Pet Type:</td>
              <td>
                  <input type="text" v-model="searchPetType" placeholder="Pet Type"/>
              </td>
              <td>Personality:</td>
              <td>
                  <input type="text" v-model="searchPersonality" placeholder="Personality"/>
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
import PlaydateList from '@/components/PlaydateList.vue'
import api from '../services/AuthService.js'

export default {
    name: "playdates",
    components: {
        "playdate-list": PlaydateList
    },
data(){
    return {
        searchPetType: "",
        searchPersonality: "",
        playdates: []
    };
},
methods :{
    getData(){
        // TODO 01: use a service to get data from the server populate the cities array
        // TODO 02: add in the query string parameters
        api.getPlaydates(this.searchPetType, this.searchPersonality)
        .then(
            (resp) =>{
                this.playdates=resp.data;
            }
        )
    }
},
created(){
    this.searchPetType = this.$route.query.petType;
    this.searchPersonality = this.$route.query.personality;
    // If PetType is true then get data
    if (this.searchPetType){
        this.getData();
    }
}
};
</script>

<style scoped>

</style>