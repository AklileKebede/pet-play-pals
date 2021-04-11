<template>
	<div id="app" class="darkmode">
		<header class="bubble">
			<router-link
				id="home-nav"
				v-bind:to="{ name: 'home' }"
				class="green-button"
				>Pet Play Pals</router-link
			>
			<div id="right-header">
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
						>Profile</router-link
					>
					<router-link
						class="green-button"
						v-bind:to="{ name: 'logout' }"
						>Logout</router-link
					>
				</div>
			</div>
		</header>
		<aside class="bubble foggy-gray-bg">
			<div>
				<router-link v-bind:to="{ name: 'playdates' }" id="search" class="green-button">Search Playdates</router-link>
				<div v-if="$store.state.token != ''">
					<router-link v-bind:to="{ name: '???' }" id="forum" class="green-button">Forum</router-link>
					<router-link v-bind:to="{ name: '???' }" id="schedule" class="green-button">Schedule Playdate</router-link>
				</div>
			</div>
		</aside>
		<main class="bubble foggy-gray-bg">
			<router-view class="main" />
		</main>
		<footer class="bubble foggy-gray-bg">&copy; Pet Play Pals</footer>
	</div>
</template>
<script>
import "./cssStyles/style.css";
import "./cssStyles/header.css";
import "./cssStyles/leftBar.css";
export default {
	created() {
		this.$store.commit("INITIALIZE_LOCAL_STATIC_DB_CACHE");
	},
};
</script>




<style scoped>
header {
	grid-area: header;
}
aside {
	grid-area: aside;
}
main {
	grid-area: main;
}
footer {
	grid-area: footer;
}

#app {
	display: grid;
	grid-template-columns: 1fr 2fr 2fr;
	grid-template-areas:
		"header header header"
		"aside main main"
		"footer footer footer";
	grid-gap: 10px;
	background-color: 0d7685;
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