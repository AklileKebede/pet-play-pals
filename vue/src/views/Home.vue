<template>
<div>
  <h1>
  Pet Play Pals! 
  </h1>
<h2>
Finding your bestfriend's bestfriend
 </h2>
 <p>
  short descriptions
 </p>
<router-link to="/playdates" tag="button">Search for PlayDates in your Area!</router-link>
</div>

</template>

<script>
export default {
  name: "home",
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
};
</script>
