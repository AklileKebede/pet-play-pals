<template>
  <body class="container">
    <h1 id="h1">Pet Play Pals!</h1>
    <h2 id="h2">Finding your bestfriend's bestfriend</h2>
    <router-link to="/playdates" tag="button" id="search"
      >Search Playdates</router-link>

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
#search{
background:    #69c181;
border-radius: 11px;
width:         auto;
height:        50px;
color:         #ffffff;
display:       inline-block;
font:          normal bold 26px/50px "Open Sans", sans-serif;
text-align:    center;
text-decoration: none;
border: none;
margin: 6px;
}
/*Assigning each area to a grid location*/

#h1 {
  grid-area: title;
}
#h2 {
  grid-area: slogan;
}
/* #search {
  grid-area: search;
} */

.container {
  display: grid;
  justify-items: center;
  grid-template-areas:
    "title"
    "slogan"
    "search";
}

</style>
