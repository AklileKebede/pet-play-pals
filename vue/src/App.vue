<template>
  <div id="app">
    <header>
      <div class="left-nav">
        <router-link
          id="home-nav"
          v-bind:to="{ name: 'home' }"
          class="green-button"
          >Pet Play Pals</router-link
        >
      </div>
      <div class="right-nav">
        <router-link
          class="green-button"
          v-bind:to="{ name: 'login' }"
          v-if="$store.state.token === ''"
          >Login</router-link
        >
        <div v-if="$store.state.token != ''">
          <router-link
            class="green-button"
            v-bind:to="{ name: 'profile' }"
            v-if="$store.state.token != ''"
            >Profile</router-link
          >
          <router-link
            class="green-button"
            v-bind:to="{ name: 'logout' }"
            v-if="$store.state.token != ''"
            >Logout</router-link
          >
        </div>
      </div>
    </header>
    <aside>
      <ul>
        <router-link to="/playdates" tag="button" id="search" class="green-button"
      >Search Playdates</router-link>
        <div v-if="$store.state.token != ''">
          <p>Forum</p>
          <p>Schedule Playdate</p>
        </div>
        </ul>
    </aside>
    <body>
      <router-view class="main" />
    </body>
    <footer>&copy; Pet Play Pals</footer>
  </div>
</template>


<style scoped>
header {
  grid-area: header;
}
aside {
  grid-area: aside;
}
body {
  grid-area: body;
}
footer {
  grid-area: footer;
}

#app {
  display: grid;
  grid-template-areas:
    'header header header'
    'asid body body'
    'aside footer footer';
  grid-gap: 20px;
  background-color: 0d7685;
  padding: 20px;
}
ul {
    padding: 0;
}
.green-button {
  background: #69c181;
  border-radius: 11px;
  width: 200px;
  height: 50px;
  color: #ffffff;
  display: inline-block;
  font: normal bold 26px/50px "Open Sans", sans-serif;
  text-align: center;
  text-decoration: none;
  margin: 6px;
  border: none;
}


header {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  display: flex;
  justify-content: space-between;
}
</style>