<template>
  <div>
    <form id="PetForm">
      <h1>Register Your Pet</h1>
      <p> Pet Name: 
        <input type="text" v-model="petInfo" placeholder="Pet Name" />
      </p>
      
      <li><span>Your Pets Bio:</span>
<p style="white-space: pre-line;">{{ message }}</p>
<br>
  <textarea v-model="message" placeholder="Tell us about your pet!"></textarea></li>
      <li>Dog <input type="checkbox" value="Dog" v-model="petTypeCheck" place  /></li>
      <!-- <label for="Dog">Dog</label> -->
      <li>Cat <input type="checkbox" value="Cat" v-model="petTypeCheck"  /></li>
      <li>Sex: <input type="text" v-model="location" placeholder="Sex" /></li>
      <li>Breed: <input type="text" v-model="location" placeholder="Breed" /></li>
      <li>Color: <input type="text" v-model="location" placeholder="Color" /></li>
    
      <li>Personality</li>
      <li>Friendly <input type="checkbox" value="Friendly" id="Friendly" v-model="petPersonality"  /></li>
      <li>Plays Rough <input type="checkbox" value="Plays Rough" id="PlaysRough" v-model="petPersonality"  /></li>
      <li>Shy <input type="checkbox" value="Shy" id="Shy" v-model="petPersonality"  /></li>
      <li>Skittish <input type="checkbox" value="Skittish" id="Skittish" v-model="petPersonality"  /></li>
      <li>High-energy <input type="checkbox" value="High-energy" id="High-energy" v-model="petPersonality"  /></li>
      <li>Reactive<input type="checkbox" value="Reactive" id="Reactive" v-model="petPersonality"  /></li>
      <li>Gentle<input type="checkbox" value="Gentle" id="Gentle" v-model="petPersonality"  /></li>
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
                petTypeCheck:[],
                sex:"",
                breed:"",
                color:"",
                petPersonality:[]
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