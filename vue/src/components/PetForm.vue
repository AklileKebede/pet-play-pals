<template>
  <div>
    <form id="PetForm" v-on:submit.prevent="submitForm">
      <h1>Register Your Pet</h1>
      <ul>
        <li>
          <label for="petName">Pet Name:</label>
          <input
            type="text"
            id="petName"
            v-model="pet.petName"
            placeholder="Pet Name"
          />
        </li>
        <li>
          <label for="bio">Your Pets Bio:</label>
        </li>
        <li>
          <textarea
            id="bio"
            v-model="pet.bio"
            placeholder="Pet Bio"
            style="width: -webkit-fill-available"
          ></textarea>
        </li>
      </ul>

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
        <label for="birthday">Birthday</label>

        <input
          type="date"
          id="birthday"
          v-model="pet.birthday"
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
        <input type="text" id="breed" v-model="pet.breed" placeholder="Breed" />
      </li>
      <li>
        <label for="color">Color: </label>
        <input type="text" id="color" v-model="pet.color" placeholder="Color" />
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
              v-model="pet.personalityIds"
            />
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
        </ul>
      </li>
      <button type="submit" class="smallGreenButton">Submit</button>
    </form>

    <router-link
      to="/home/profile"
      tag="button"
      id="petForm"
      class="smallGreenButton"
      >Cancel</router-link
    >
  </div>
</template>

<script>
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import PetService from "@/services/PetsService";
export default {
  components: { vueDropzone: vue2Dropzone },
  name: "PetForm",
  props: {},
  data() {
    return {
      pet: {
        petName: "",
        bio: "",
        petType: "",
        sex: "",
        breed: "",
        color: "",
        petPersonality: [],
        personalityIds: [],
      },
      allPersonalities: {},
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
  methods: {
    submitForm() {
      PetService.addNewPet(this.pet).then(() => {
        this.$router.push({ name: "profile" });
      });
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
    this.getPersonalities();
  },
};
</script>

<style scoped>
</style>