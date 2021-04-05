<template>
  <div id="app">
    <div id="nav">
      <div>
        <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
        <!-- <router-link>Search</router-link>&nbsp;|&nbsp; -->
        <router-link v-bind:to="{ name: 'login' }" v-if="$store.state.token === ''">Login</router-link>
      </div>
      <div v-if="$store.state.token != ''">
      <router-link v-bind:to="{ name: 'profile' }" v-if="$store.state.token != ''">Profile</router-link> <span v-if="$store.state.token != ''">&nbsp; | &nbsp;</span>
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <!-- <router-link>Form</router-link> -->
      </div>

    </div>
    <router-view />
  </div>
</template>
<style scoped>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
  a {
    font-weight: bold;
    color: #2c3e50;
    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>