<template>
  <div>
    <form class="editPet" v-on:submit.prevent="updatePet">
      <h1>Edit {{ pet.petName }}'s- Information</h1>
      <p>
        Pet Name:
        <input type="text" v-model="pet.petName" />
      </p>

      <li>
        <span>Your Pets Bio:</span>

        <textarea v-model="pet.bio"></textarea>
      </li>

      <li>
        Pet Type
        <select
          name="petType"
          id="petType"
          class="dropdown-content"
          v-model="pet.petTypeId"
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
        Pet Sex
        <select
          name="petSex"
          id="petSex"
          class="dropdown-content"
          v-model="pet.sex"
        >
          <option value=""></option>
          <option value="F">Female</option>
          <option value="M">Male</option>
        </select>
      </li>
      <li>Breed: <input type="text" v-model="pet.breed" /></li>
      <li>Color: <input type="text" v-model="pet.color" /></li>

      <li>
        Personality
        <ul>
          <li
            v-for="(item, key) in $store.state.validPersonalities"
            v-bind:key="key"
          >
            {{ item }}
            <input
              type="checkbox"
              v-bind:value="key"
              v-model="pet.personalityIds"
            />
          </li>
        </ul>
      </li>
      <li>
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

      <button type="submit" class="smallGreenButton">Submit</button>
      <router-link
        v-bind:to="{ name: 'profile' }"
        tag="button"
        id="petForm"
        class="smallGreenButton"
        >Cancel</router-link
      >
    </form>
  </div>
</template>

<script>
// import PetDetails from "../components/PetDetails.vue";
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import PetService from "@/services/PetsService";
export default {
  name: "EditPet",
  components: { vueDropzone: vue2Dropzone },
  data() {
    return {
      pet: {},
      allPersonalities: {},
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
    getPetById() {
      PetService.getPetById(this.$route.params.petId).then((response) => {
        this.pet = response.data;
      });
    },
    updatePet() {
      PetService.updatePet(this.$route.params.petId, this.pet).then(
        (response) => {
          this.pet = response.data;
        }
      );
      this.$router.push({ name: "profile" });
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
      const uploadedImgUrl = response.secure_url; // store the url for the uploaded image
      this.pet.imgUrl = uploadedImgUrl;
      this.addNewPetPhoto(this.pet);
      this.$emit("image-upload", uploadedImgUrl); // fire custom event with image url in case someone cares
    },
    // This method is for images
    addNewPetPhoto(pet) {
      PetService.addNewPetPhoto(pet).then((response) => {
        console.log(response);
      });
    },
  },
  created() {
    this.getPetById();
  },
};
</script>

<style scoped>
</style>