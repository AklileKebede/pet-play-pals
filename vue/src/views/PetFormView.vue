<template>
  <div>
  <!-- <form id="PetForm">
      <h1>Example h1: Register Your Pet</h1>
      <p> Pet Name: 
        <input type="text" v-model="location" placeholder="Pet Name" />
      </p>
      <li>Bio: <input type="text" v-model="location" placeholder="Bio" /></li>
      <li>Pet Type: <input type="text" v-model="location" placeholder="Pet Type" /></li>
      <li>Sex: <input type="text" v-model="location" placeholder="Sex" /></li>
      <li>Breed: <input type="text" v-model="location" placeholder="Breed" /></li>
      <li>Color: <input type="text" v-model="location" placeholder="Color" /></li>
      <li>Personality: <input type="text" v-model="location" placeholder="Personality" /></li>
    </form> -->

  </div>
</template>

<script>
 import PetForm from "@/components/PetForm.vue";
// import api from "@/services/AuthService.js";
// import axios from 'axios';

export default {
    name: "PetForm",
    components: {PetForm},
    data(){
        return{
           
        };
    },
    methods:{
        addPet(){
            // use the service to Add the pet on the server (Post)
            
        }
    },
    // created(){ //post from form to database.
    //   const petForm = {title: "Pet Form Update"};
    //   axios.post("BASE_URL/home/profile/", petForm).then(response => this.petId = response.data.petId)

    // }
};
</script>

<style scoped>
</style>