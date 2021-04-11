<template>
  <div>
    <ul>
      <li> Playdate Id: {{playdate.playdateId}}</li>
      <li> Date: {{playdate.date}}</li>
      <li> Playdate Organizer: {{playdate.userId}}</li>
      <li>Participating Pets: 
         <ul>
          <li v-for="pet in playdate.pets" v-bind:key = "pet">{{pet.petName}}|{{pet.petType}}</li>
        </ul>
      </li>
      <li>Location: {{playdate.location}}</li>
      <li></li>
    </ul>

  </div>
</template>

<script>
export default {
data(){
  return {};
},
name: "playdate-detail",
props: {
    playdates: Object,
},
}
</script>

<style scoped>

</style>