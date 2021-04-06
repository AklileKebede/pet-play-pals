<template>
  <body class="container">
    <h1 id="h1">Pet Play Pals!</h1>
    <h2 id="h2">Finding your bestfriend's bestfriend</h2>
    <router-link to="/playdates" tag="button" id="search"
      >Search for PlayDates in your Area!</router-link>

    <p id="description">short descriptions</p>
  </body>
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
<style scoped>
/*Assigning each area to a grid location*/
#h1 {
  grid-area: title;
}
#h2 {
  grid-area: slogan;
}
#search {
  grid-area: search;
}

.container {
  display: grid;
  justify-items: center;
  background-color: #f2f3f4;
  grid-template-areas:
    "title"
    "slogan"
    "search";
}
/* div.container{
  background-color: #F2F3F4;
} */
#search {
  width: fit-content;
  background-color: red;
}
</style>
