<template>
  <body class="container">
    <h1 id="h1">Pet Play Pals!</h1>
    <p id="description">
      Pet Play Pals is a social networking service for your best friend!
      <br />
      Pet Play Pals is a place to make real connections, <br />
      for both pets and their owners while meeting up for playdates, walks, or
      hangouts.
    </p>
    <h2 id="h2">Finding your bestfriend's bestfriend</h2>
    <router-link to="/playdates" tag="button" id="search"
      >Search Playdates</router-link
    >
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
#search {
  background: #69c181;
  border-radius: 11px;
  width: auto;
  height: 50px;
  color: #ffffff;
  display: inline-block;
  font: normal bold 26px/50px "Open Sans", sans-serif;
  text-align: center;
  text-decoration: none;
  border: none;
  margin: 6px;
}
/*Assigning each area to a grid location*/

#h1 {
  grid-area: title;
  font-size: 50px;
}
#h2 {
  grid-area: slogan;
  font-size: 40px;
}
#search {
  grid-area: search;
}
#description {
  grid-area: description;
  font-size: 25px;
    color: #02262b;
    font-weight: bolder;
  
}

.container {
  height: 80vh;
  display: grid;
  justify-items: center;
  grid-template-areas:
    "title"
    "slogan"
    "description"
    "search";
  text-align: center;
}
body {
  color: #0d7685;
  background-image: url("../../img/download.png");
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: cover;
  border-radius: 11px;
}
</style>
