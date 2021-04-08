<template>
  <div>
    <form id="PetForm">
      <h1>Register Your Pet</h1>
      <p> Pet Name: 
        <input type="text" v-model="location" placeholder="Pet Name" />
      </p>
      <li>Bio: <input type="text" v-model="location" placeholder="Bio" /></li>
      <li>Pet Type: <input type="text" v-model="location" placeholder="Pet Type" /></li>
      <li>Sex: <input type="text" v-model="location" placeholder="Sex" /></li>
      <li>Breed: <input type="text" v-model="location" placeholder="Breed" /></li>
      <li>Color: <input type="text" v-model="location" placeholder="Color" /></li>
      <!-- /*TODO add check box for personality. using info from the database*/ -->
      <!-- TODO find a job in the backend -->
      <li>Personality: <input type="text" v-model="location" placeholder="Personality" /></li>
    </form>
  </div>
</template>

<script>
import axios from 'axios';
export default {
    name: "PetForm",
    props: {},
    data(){
        return{
            pet: {
                petId:0,
                petName: "",
                bio:"",
                petType:"",
                sex:"",
                breed:"",
                color:"",
                personality:""
            }
        };
    },
    methods:{
        addPet(){
            // use the service to Add the pet on the server (Post)
            
        }
    },
    created(){ //post from form to database.
      const petForm = {title: "Pet Form Update"};
      axios.post("BASE_URL/home/profile/", petForm).then(response => this.petId = response.data.petId)

    }
};
</script>

<style scoped>
</style>