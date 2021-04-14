<template>
  <div>
    <ul>
      <li>Playdate Id: {{ playdate.playdateId }}</li>
      <li>Start Date: {{ playdate.startDateTime }}</li>
      <li>End Date: {{ playdate.endDateTime }}</li>
      <li>Location: {{ playdate.location.address }}</li>
      <li>Playdate Organizer: {{ playdate.userName }}</li>
    </ul>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {};
  },
  name: "playdate-preview",
  props: {
    playdate: Object,
  },
};
</script>

<style scoped>
</style>