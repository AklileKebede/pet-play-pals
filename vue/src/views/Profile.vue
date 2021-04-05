<template>
  <div class="profile">
    <h1>Profile</h1>
    <p>These are your profile details:</p>
    <p>User Name: {{currentUser.username}}</p>
    <p>User Id: {{currentUser.userId}}</p>
    <p>Role: {{currentUser.role}}</p>
  </div>
</template>

<script>
export default {
  name: "profile",
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
};
</script>
