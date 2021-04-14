<template>
  <div>
    <form id="creatPlaydate" v-on:submit.prevent="submitForm">
      <h1>Create New Playdate</h1>

      <p>create playdate</p>
      <!--      <tr>
          Pet Type:
        </tr>
        <td class="dropdown">
          <ul>
            <li
              v-for="(item, key) in $store.state.validPetTypes"
              v-bind:key="key"
              v-bind:value="key"
            >
              {{ item }}
              <input
                type="checkbox"
                v-bind:value="key"
                v-model="filterParams.petTypeIds"
              />
            </li>
          </ul>
        </td> -->
      <li>
        Pet Type Allowed:
        <select
          name="petType"
          id="petType"
          class="dropdown-content"
          v-model="playdate.petTypeId"
        >
          <option value=""></option>
          <option
            v-for="(item, key) in $store.state.validPetTypes"
            v-bind:key="key"
            v-bind:value="key"
          >
            {{ item }}
          </option>
        </select>
      </li>
      <li>
        Personality:
        <ul>
          <li
            v-for="(item, key) in $store.state.validPersonalities"
            v-bind:key="key"
          >
            <label v-bind:for="key">{{ item }}</label>
            <input
              v-bind:id="key"
              type="checkbox"
              v-bind:value="key"
              v-model="playdate.personalityIds"
            />
          </li>
        </ul>
      </li>
      <button type="submit" class="smallGreenButton">Submit</button>
    </form>
    <router-link
      to="/home/profile"
      tag="button"
      id="creatPlaydate"
      class="smallGreenButton"
      >Cancel</router-link
    >
  </div>

  <!-- <li>
					<label for="description">Playdate description:</label>
				</li>
				<li>
					<textarea
						id="description"
						v-model="Playdate.description"
						placeholder="Playdate Description"
						style="width: -webkit-fill-available"
					></textarea>
				</li>
			</ul>

			
			<li>
				<label for="startDate">Start Time:</label>

				<input
					type="datetime-local"
					id="startDate"
					min="today"
					v-model="playdate.startDate"
					placeholder="today"
				/>
				<span class="validity"></span>
			</li>
			<li>
				<label for="endDate">End Time:</label>

				<input
					type="datetime-local"
					id="endDate"
					v-model="playdate.endDate"
					placeholder="today"
				/>
			</li>

			<li>
				<label for="petSex">Pet Sex</label>
				<select
					name="petSex"
					id="petSex"
					v-model="pet.sex"
					class="dropdown-content"
				>
					<option value=""></option>
					<option value="F">Female</option>
					<option value="M">Male</option>
				</select>
			</li>
			<li>
				<label for="breed">Breed: </label>
				<input
					type="text"
					id="breed"
					v-model="pet.breed"
					placeholder="Breed"
				/>
			</li>
			<li>
				<label for="color">Color: </label>
				<input
					type="text"
					id="color"
					v-model="pet.color"
					placeholder="Color"
				/>
			</li>
	
		</form>
	</div> -->
  <!-- make me a new playdate
	location
	map
	playdate organizer
	time
	whoch pet
	pet types acceptable
	delet
	-->
</template>

<script>
import PlaydatesService from "../services/PlaydatesService.js";
export default {
  name: "createPlaydate",
  components: {},
  data() {
    return {
      playdate: {},
      allPersonalities: {},
    };
  },
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
    submitForm() {
      PlaydatesService.addNewPlaydate(this.playdate).then(() => {
        this.$router.push({ name: "" });
      });
    },
  },
  created() {},
};
</script>

<style scoped>
input:invalid + span:after {
  content: "✖";
  padding-left: 5px;
}

input:valid + span:after {
  content: "✓";
  padding-left: 5px;
}
</style>