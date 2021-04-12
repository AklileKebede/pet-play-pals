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
  data() {
    return {
      user: {},
    };
  },
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
        updateUsernameByUserId() {
                        EditUsername.updateUsernameByUserId(this.$route.params.userId, this.user). then(
                            (response) => {
                                this.user = response.data;
                            }
                        );
    }
  },
};
</script>

<style scoped>
</style>