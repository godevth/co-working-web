<template>
  <div>
    <b-alert
      :show="form.errors && form.errors.length > 0"
      variant="light"
      class="alert red lighten-4"
      ref="alert" 
    >
      <div class="alert-icon">
        <i class="flaticon-warning kt-font-danger"></i>
      </div>
      <div class="alert-text">
        <div v-for="err in form.errors" :key="err">
          {{ err }}
        </div>
      </div>
    </b-alert>
    <div class="row">
      <div class="col-md-12">
        <KTPortlet v-bind:title="title">
          <template v-slot:body>
            <v-form 
              ref="form" 
              @submit.prevent="submitForm"
              v-model="form.valid"
              lazy-validation
            >
              <v-row>
                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    v-model="form.nameTH"
                    :disabled="form.loading"
                    :counter="50"
                    :label="$t('FACILITY.ADD_EDIT.NAME_TH')"
                    :rules="form.nameRulesTH"
                    required
                  ></v-text-field>
                </v-col>
                <v-col
                  cols="12"
                  md="4"
                >
                  <v-text-field
                    v-model="form.nameEN"
                    :disabled="form.loading"
                    :counter="50"
                    :label="$t('FACILITY.ADD_EDIT.NAME_EN')"
                    :rules="form.nameRulesEN"
                    required
                  ></v-text-field>
                </v-col>
                  <v-col
                  cols="12"
                  md="4"
                >
                  <v-autocomplete
                    v-model="form.facilityType"
                    :disabled="form.loading"
                    :items="form.facilityTypeItems"
                    :loading="form.facilityTypeLoading"
                    :search-input.sync="form.facilityTypeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('FACILITY.SEARCH.FACILITY_TYPE')"
                    :rules="form.facilityTypeRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col
                  cols="12"
                  md="12"
                >
                  <v-checkbox
                    v-model="form.inActiveStatus"
                    :disabled="form.loading"
                    :label="$t('FACILITY.ADD_EDIT.IN_ACTIVE_STATUS')"
                    required
                  ></v-checkbox>
                </v-col>

                <v-col cols="6" md="6">
                  <v-file-input accept="image/*"
                    show-size
                    v-model="form.picture"
                    :disabled="form.loading"
                    :label="$t('FACILITY.ADD_EDIT.ICON')"
                    :rules="form.pictureRules"
                    :hint="$t('FACILITY.ADD_EDIT.PICTURE_HINT')"
                    persistent-hint
                    multiple
                    @change="addFile()"
                    truncate-length="50"></v-file-input>
                </v-col>
              </v-row>
              <v-row>
                <div class="col-12">
                  <v-btn
                    :disabled="!form.valid || form.loading"
                    color="success"
                    class="mr-4"
                    tile
                    type="submit"
                  >
                    <v-icon left>la-save</v-icon>
                    {{ $t("SHARED.SAVE_BUTTON") }}
                  </v-btn>
                  <v-btn
                    :disabled="form.loading"
                    color="default"
                    class="mr-4"
                    type="reset"
                    tile
                    @click.prevent="resetForm"
                  >
                    <v-icon left>mdi-eraser</v-icon>
                    {{ $t("SHARED.RESET_BUTTON") }}
                  </v-btn>
                </div>
              </v-row>

              <v-dialog v-model="form.dialog" persistent max-width="300">
                <v-card>
                  <v-card-title class="headline">
                    {{
                      $t("FACILITY.ADD_EDIT.SUCCESS_DIALOG_HEADER")
                    }}</v-card-title
                  >
                  <v-card-text>
                      {{ $t("FACILITY.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                      color="green darken-1"
                      text
                      @click="closeDialog"
                      >{{ $t("SHARED.CLOSE_BUTTON") }}</v-btn
                    >
                  </v-card-actions>
                </v-card>
              </v-dialog>

              <v-dialog
                v-model="form.loading"
                persistent
                hide-overlay
                width="300"
              >
                <v-card>
                  <v-card-title class="headline">
                    {{ $t("SHARED.PLEASE_WAIT") }}</v-card-title
                  >
                  <v-card-text>
                    <p>
                      {{ $t("SHARED.PROCESSING") }}
                    </p>
                    <v-progress-linear
                      indeterminate
                      color="amber lighten-3"
                      class="mb-3"
                    ></v-progress-linear>
                  </v-card-text>
                </v-card>
              </v-dialog>
            </v-form>
          </template>
        </KTPortlet>
      </div>
    </div>
  </div>
</template>

<script>
import ApiService from "@/common/api.service";
import { SET_BREADCRUMB } from "@/store/breadcrumbs.module";
import KTPortlet from "@/views/partials/content/Portlet.vue";

export default {
  components: {
    KTPortlet,
  },
  data() {
    return {
      form: {
        valid: true,
        dialog: false,
        loading: false,
        facilityType:null,
        facilityTypeSearch: "",
        facilityTypeLoading: false,
        facilityTypeItems:[],
        pictureFile: [],
        facilityTypeRules:[
          (v) => !!v || this.$t("FACILITY.ADD_EDIT.FACILITY_TYPE_VALIDATE"),
        ],
        nameRulesTH: [
          (v) => !!v || this.$t("FACILITY.ADD_EDIT.NAME_TH_VALIDATION"),
          (v)  => (v && v.length <= 450) || this.$t("FACILITY.ADD_EDIT.NAME_COUNTER"),
        ],
        nameRulesEN: [
          (v)  => !!v || this.$t("FACILITY.ADD_EDIT.NAME_EN_VALIDATION"),
          (v)  => (v && v.length <= 450) || this.$t("FACILITY.ADD_EDIT.NAME_COUNTER"),
        ],
        pictureRules: [
          (v) => !!v || this.$t("FACILITY.ADD_EDIT.PICTURE_VALIDATION"),
        ],
        errors: [],
        nameTH: "",
        nameEN: "",
        inActiveStatus: true,
        picture: null,
        pictureType: ["image/png"],
      }
    }
  },
  computed: {
    title() {
      return this.$t("FACILITY._FACILITY.ADD");
    },
  },
  watch: {
    "form.facilityTypeSearch": {
      handler() {
        this.getFacilityTypeFromApi().then((data) => {
          this.form.facilityTypeItems = data;
        });
      }
    },
  },
  methods: {
    submitForm() {
      var self = this;
      var chk = this.$refs.form.validate();
      if (chk) {
        for (let i = 0; i < self.form.picture.length; i++) 
            {
              if (self.form.picture[0] && !self.form.pictureType.includes(self.form.picture[0].type)) 
              {
                self.form.errors.push("กรุณาเลือกรูป Format.png เท่านั้น");
              }
              else{
                this.getFileBase64(self.form.picture[i]).then(file => {
                        console.log(file);
                        let mb = 2 * 43 * 43;
                        if (file.size > mb)
                        {
                          self.form.errors.push("กรุณาเลือกรูปที่มีขนาดน้อยกว่าหรือเท่ากับ 3 Kb");
                        }
                        else
                        {
                          self.form.pictureFile[i].Base64s = file.base64;
                          self.form.pictureFile[i].ContentTypes = file.contentType;
                          this.postDataToApi();
                        }
                    });
              }
            }
      }
    },
    addFile(){
      this.form.pictureFile = [];
      this.form.picture.forEach(element =>{
          this.getFileBase64(element).then(file => {
              this.form.pictureFile.push({
                  Base64s:file.base64,
                  ContentTypes:file.contentType 
               });
          });
      })
    },
    getFileBase64(file) {
      return new Promise((resolve) => {
        let reader = new FileReader();
        reader.onload = () => {
          resolve({
            url: reader.result,
            base64: reader.result.split(",")[1],
            contentType: file.name.split(".")[1],
            size: file.size,
            });
          };
          reader.readAsDataURL(file);
        });
    },
    resetForm() {
      this.$refs.form.reset();
    },
    getFacilityTypeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/FacilityType/Select2FacilityType", {
          search: this.form.facilityTypeSearch,
          isall:true,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roleLoading = false;
          });
      });
    },
    postDataToApi() {
      var self = this;
      this.form.loading = true;
      this.form.errors = [];

      var pftems = [];
      self.form.pictureFile.forEach(element => {
          console.log(element);
          pftems.push({
            Base64s:element.Base64s,
            ContentTypes:element.ContentTypes
          });
      });
      ApiService.setHeader();
      ApiService.post("/Api/Facility/Add", 
      {
        Name_TH: this.form.nameTH,
        Name_EN: this.form.nameEN,
        FacilityTypeId:this.form.facilityType.value,
        InActiveStatus: !this.form.inActiveStatus,
        Pictures: pftems,   
      })
      .then(({ data }) => {
        if (data.status) {
          // close dialog
          this.form.dialog = true;
        } else {
          this.form.dialog = false;
          this.form.loading = false;
          this.form.errors.push(data.message);
          this.$vuetify.goTo(this.$refs.alert,{duration : 1000, easing: 'easeInOutCubic', offset: -20});
        }
      })
      .catch(({ response }) => {
        if (response.data) {
          this.form.errors.push(response.data.message);
        } else {
          this.form.errors.push("Unknow error occurs");
        }
        this.$vuetify.goTo(this.$refs.alert,{duration : 1000, easing: 'easeInOutCubic', offset: -20});
        this.form.dialog = false;
        this.form.loading = false;
      });
    },
    closeDialog() {
      // not close but redirect to search page
      this.$router.push({ name: "searchFacility" });
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("FACILITY._FACILITY.SECTION"), route: "/Facility" },
      { title: this.$t("FACILITY._FACILITY.ADD") },
    ]);
  },
};
</script>

<style lang="scss" scoped></style>
