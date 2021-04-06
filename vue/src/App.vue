<template>
  <div id="app">
    <header>
      <div class="left-nav">
        <router-link id="home-nav" v-bind:to="{ name: 'home' }"
          ><img src="logo.png" alt="Play Pals Logo"
        /></router-link>
      </div>
      <div class="right-nav">
        <router-link
          v-bind:to="{ name: 'login' }"
          v-if="$store.state.token === ''"
          >Login</router-link
        >
        <div v-if="$store.state.token != ''">
          <router-link
            v-bind:to="{ name: 'profile' }"
            v-if="$store.state.token != ''"
            >Profile</router-link
          >
          <span v-if="$store.state.token != ''">&nbsp; | &nbsp;</span>
          <router-link
            v-bind:to="{ name: 'logout' }"
            v-if="$store.state.token != ''"
            >Logout</router-link
          >
        </div>
      </div>
    </header>
    <router-view />
    <footer>&copy; Pet Play Pals</footer>
  </div>
</template>
<style scoped>
header {
  display: flex;
}
.left-nav img {
  height: 1.5em;
}
.right-nav img{
  height: 1.5em;
}

header {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  display: flex;
  justify-content: space-between;
}
#app{
zoom: 2
}
</style>