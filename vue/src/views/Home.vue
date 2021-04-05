<template>
<div>
  Home Page
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
