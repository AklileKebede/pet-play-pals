<template>
  <div>
    <form class="editusername" v-on:submit.prevent="updateUsernameByUserId">
    <h1>Edit {{currentUser.username}}</h1>
    <p>
				Username:
				<input type="text" v-model="currentUser.username" />
			</p>
  
    <button type="submit" class="smallGreenButton">Submit</button>
    <router-link
      v-bind:to="{ name: 'profile' }"
      tag="button"
      id="editusername"
      class="smallGreenButton"
      >Cancel</router-link
    >
     </form>
  </div>
</template>


<script>
import EditUsername from "@/services/EditUsername";

export default {
  name: "EditUsername",
  components: {},
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
        updateUsernameByUserId() {
          //this works but the route doesn't change when directed to edit username page
          EditUsername.updateUsernameByUserId(this.$store.state.user.userId, this.$store.state.user)
           .then(
              (response) => {
                  this.user = response.data;//this.user doesn't exist?
                  this.$router.push({ name: "profile" });
              }
          );
    }
  },
};
</script>

<style scoped>
</style>