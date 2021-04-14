<template>
  <div class="profile">
    <h1>Profile</h1>
    <div id="profileDetails">
      <div id="userDetails" class="bubble foggy-gray-bg">
        <h2>My Info</h2>
        <!-- <p>These are your profile details:</p> -->
        <p>Username: {{ currentUser.username }}</p>
        <p>
          <router-link
            v-bind:to="{
              name: 'EditUsername',
            }"
            tag="button"
            id="editusername"
            class="smallGreenButton"
            >Edit Username</router-link
          >
        </p>
      </div>
      <!-- <p>User Id: {{currentUser.userId}}</p> -->
      <!-- <p>Role: {{currentUser.role}}</p> -->
      <div id="petList" class="bubble foggy-gray-bg">
        <h2>My Pets</h2>
        <ul>
          <li v-for="pet in pets" v-bind:key="pet.id">
            <pet-details v-bind:pet="pet"></pet-details>
            <router-link
              v-bind:to="{
                name: 'EditPet',
                params: { petId: `${pet.petId}` },
              }"
              tag="button"
              id="editPet"
              class="smallGreenButton"
              >Edit Pet</router-link
            >
            <!-- eslint-disable -->
            <!-- This disables annoying eslink warning messages in the html       -->
            <!-- This is the dropzone component that will give a place to drop the image to be uploaded -->
            <!-- there are two custom events the component listens for:                                 -->
            <!--       the vdropzone-sending event which is fired when dropzone is sending an image     -->
            <!--       the vdropzone-success event which is fired when dropzone upload is successful    -->
            <vue-dropzone
              id="dropzone"
              class="mt-3"
              v-bind:options="dropzoneOptions"
              v-on:vdropzone-sending="addFormData"
              v-on:vdropzone-success="getSuccess"
              :useCustomDropzoneOptions="true"
            ></vue-dropzone>
          </li>
        </ul>
        <router-link
          to="/PetForm"
          tag="button"
          id="petForm"
          class="smallGreenButton"
          >Add a new Pet!</router-link
        >
      </div>
      <div id="playdateList" class="bubble foggy-gray-bg">
        <h2>My Playdates</h2>
        <ul>
          <li v-for="playdate in playdates" v-bind:key="playdate.id">
            <playdate-details v-bind:playdate="playdate"></playdate-details>
            <!-- <router-link
							v-bind:to="{
								name: 'EditPlaydate',
								params: {  },
							}"
							tag="button"
							id="editPlaydate"
							class="smallGreenButton"
							>Edit Playdate</router-link
						> -->
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import PetDetails from "../components/PetDetails.vue";
import PetService from "@/services/PetsService";
import PlaydatesService from "@/services/PlaydatesService";
import PlaydateDetails from "@/components/PlaydateDetails.vue";
/* eslint-disable */
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";

export default {
  components: { PetDetails, PlaydateDetails, vueDropzone: vue2Dropzone },
  name: "profile",
  data() {
    return {
      pets: {},
      playdates: {},
      //-------------------------------------------------------------------------------------
      // TODO: substitute your actual Cloudinary cloud-name where indicated in the URL
      //-------------------------------------------------------------------------------------
      dropzoneOptions: {
        url: "https://api.cloudinary.com/v1_1/ddmt8rec2/image/upload",
        thumbnailWidth: 250,
        thumbnailHeight: 250,
        maxFilesize: 2.0,
        acceptedFiles: ".jpg, .jpeg, .png, .gif",
        uploadMultiple: false,
        addRemoveLinks: true,
        dictDefaultMessage:
          "Drop files here to upload. </br> Alternatively, click to select a file for upload.",
      },
    };
  },
  computed: {
    currentUser() {
      return this.$store.state.user;
    },
  },
  methods: {
    getPets() {
      PetService.getPets({ userId: this.currentUser.userId }).then(
        (response) => {
          this.pets = response.data;
        }
      );
    },
    getPlaydates() {
      PlaydatesService.getPlaydates({ userId: this.currentUser.userId }).then(
        (response) => {
          this.playdates = response.data;
        }
      );
    },

    /******************************************************************************************
     * The addFormData method is called when vdropzone-sending event is fired
     * it adds additional headers to the request
     ******************************************************************************************/
    //--------------------------------------------------------------------------------------------
    // TODO: substitute your actual Cloudinary api-key where indicated in the following code
    // TODO: substitute your actual Cloudinary upload preset where indicated in the following code
    //----------------------------------------------------------------------------==---------------
    addFormData(file, xhr, formData) {
      formData.append("api_key", "212213545218932"); // substitute your api key
      formData.append("upload_preset", "plziirta"); // substitute your upload preset
      formData.append("timestamp", (Date.now() / 1000) | 0);
      formData.append("tags", "vue-app");
    },
    /******************************************************************************************
     * The getSuccess method is called when vdropzone-success event is fired
     ******************************************************************************************/
    getSuccess(file, response) {
      const imgUrl = response.secure_url; // store the url for the uploaded image
      this.$emit("image-upload", imgUrl); // fire custom event with image url in case someone cares
    },
    // This method is for images 
    addNewPetPhoto(){
      PetService.addNewPetPhoto({userId: this.currentUser.userId}). then(
        (response) => {
          this.pets = response.data;
        }
      )
    },

  },
  created() {
    this.getPets();
    this.getPlaydates();
  },
};
</script>
<style scoped>
#userDetails {
  grid-area: userDetails;
}
#playdateList {
  grid-area: playdateList;
}
#petList {
  grid-area: petList;
}
div#profileDetails {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas: "userDetails petList playdateList";
  grid-gap: 10px;
}
</style>
