<template>
	<div>
		<p>Name: {{pet.name}}</p>
		<p>Type: {{pet.type}}</p>
	</div>
</template>

<script>
export default {
	data(){
		return{
		}
	},
	props:{
		pet: object
	},



}
</script>

<style scoped>
</style>