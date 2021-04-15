<template>
	<div>
		<playdate-details v-bind:playdate="playdate"></playdate-details>
	</div>
</template>

<script>
import PlaydateDetails from '@/components/PlaydateDetails.vue'
import PlaydatesService from '../services/PlaydatesService'
export default {
	components: { PlaydateDetails },
	data(){
		return{
			playdate:{}
		}
	},
	methods:{
		getPlaydate(){
			PlaydatesService.getPlaydateById(this.$route.params.playdateId).then((response) => {this.playdate = response.data})
		}
	},
	created(){
		this.getPlaydate();
	}

}
</script>

<style>

</style>