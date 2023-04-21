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
                <v-col cols="12" md="12">
                  <v-text-field
                    v-model="form.placeName"
                    :disabled="true"
                    :label="$t('PRIVILEGE.ADD_EDIT.PLACE_NAME')"
                    required
                  ></v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.firstName"
                    :disabled="form.loading"
                    :counter="450"
                    :label="$t('PRIVILEGE.ADD_EDIT.FIRST_NAME')"
                    :rules="form.firstNameRules"
                    required
                  >
                  </v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.lastName"
                    :disabled="form.loading"
                    :counter="450"
                    :label="$t('PRIVILEGE.ADD_EDIT.LAST_NAME')"
                    :rules="form.lastNameRules"
                    required
                  >
                  </v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.email"
                    :disabled="form.loading"
                    :counter="256"
                    :label="$t('PRIVILEGE.ADD_EDIT.EMAIL')"
                    :rules="form.emailRules"
                    type="email"
                    required
                  >
                  </v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.password"
                    :disabled="form.loading"
                    :counter="20"
                    :append-icon="form.showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                    :rules="form.passwordRules"
                    :type="form.showPassword ? 'text' : 'password'"
                    :label="$t('PRIVILEGE.ADD_EDIT.PASSWORD')"
                    @click:append="form.showPassword = !form.showPassword"
                    autocomplete="new-password"
                    required
                  >
                  </v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.confirmPassword"
                    :disabled="form.loading"
                    :counter="20"
                    :append-icon="
                      form.showConfirmPassword ? 'mdi-eye' : 'mdi-eye-off'
                    "
                    :rules="form.confirmPasswordRules"
                    :type="form.showConfirmPassword ? 'text' : 'password'"
                    :label="$t('PRIVILEGE.ADD_EDIT.COMFIRM_PASSWORD')"
                    @click:append="
                      form.showConfirmPassword = !form.showConfirmPassword
                    "
                    required
                  >
                  </v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.phoneNumber"
                    :disabled="form.loading"
                    :counter="50"
                    :label="$t('PRIVILEGE.ADD_EDIT.PHONE_NUMBER')"
                    :rules="form.phoneNumberRules"
                    
                  >
                  </v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-radio-group
                    v-model="form.gender"
                    :disabled="form.loading"
                    :label="$t('PRIVILEGE.ADD_EDIT.GENDER')"
                    :rules="form.genderRules"
                    required
                  >
                    <v-radio label="Male" value="M"></v-radio>
                    <v-radio label="Female" value="F"></v-radio>
                  </v-radio-group>
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
                    {{ $t("PRIVILEGE.ADD_EDIT.SUCCESS_DIALOG_HEADER") }}
                  </v-card-title>
                  <v-card-text>
                    {{ $t("PRIVILEGE.ADD_EDIT.ADD_CUSTUMER_SUCCESS") }}
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="green darken-1" text @click="closeDialog">{{
                      $t("SHARED.CLOSE_BUTTON")
                    }}</v-btn>
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
                    {{ $t("SHARED.PLEASE_WAIT") }}
                  </v-card-title>
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
        errors: [],
        placeId: this.$route.params.id,
        placeName: "",
        showPassword: false,
        showConfirmPassword: false,
        email: "",
        firstName: "",
        lastName: "",
        phoneNumber: "",
        password: "",
        confirmPassword: "",
        gender : "",
        emailRules: [
          (v) => !!v || this.$t("PRIVILEGE.ADD_EDIT.EMAIL_VALIDATION"),
          (v) =>
            (v && v.length <= 256) || this.$t("PRIVILEGE.ADD_EDIT.EMAIL_COUNTER"),
          (v) =>
            (v && /.+@.+\..+/.test(v)) || this.$t("PRIVILEGE.ADD_EDIT.EMAIL_FORMAT"),
        ],
        firstNameRules: [
          (v) => !!v || this.$t("PRIVILEGE.ADD_EDIT.FIRST_NAME_VALIDATION"),
          (v) =>
            (v && v.length <= 450) ||
            this.$t("PRIVILEGE.ADD_EDIT.FIRST_NAME_COUNTER"),
        ],
        lastNameRules: [
          (v) => !!v || this.$t("PRIVILEGE.ADD_EDIT.LAST_NAME_VALIDATION"),
          (v) =>
            (v && v.length <= 450) ||
            this.$t("PRIVILEGE.ADD_EDIT.LAST_NAME_COUNTER"),
        ],
        passwordRules: [
          (v) => !!v || this.$t("PRIVILEGE.ADD_EDIT.PASSWORD_VALIDATION"),
          (v) =>
            (v && v.length <= 20) || this.$t("PRIVILEGE.ADD_EDIT.PASSWORD_COUNTER"),
          (v) =>
            (v && v.length >= 6) ||
            this.$t("PRIVILEGE.ADD_EDIT.PASSWORD_COUNTER_LT"),
        ],
        confirmPasswordRules: [
          (v) => !!v || this.$t("PRIVILEGE.ADD_EDIT.COMFIRM_PASSWORD_VALIDATION"),
          (v) =>
            (v && v.length <= 20) ||
            this.$t("PRIVILEGE.ADD_EDIT.COMFIRM_PASSWORD_COUNTER"),
          (v) =>
            (v && v == this.form.password) ||
            this.$t("PRIVILEGE.ADD_EDIT.COMFIRM_PASSWORD_EQ"),
          (v) =>
            (v && v.length >= 6) ||
            this.$t("PRIVILEGE.ADD_EDIT.COMFIRM_PASSWORD_COUNTER_LT"),
        ],
        phoneNumberRules: [
          (v) =>
            !v ||
            (v && v.length <= 50) ||
            this.$t("PRIVILEGE.ADD_EDIT.PHONE_NUMBER_COUNTER"),
        ],
        genderRules :[
            (v) => !!v || this.$t("PRIVILEGE.ADD_EDIT.GENDER_VALIDATION")
        ]
      },
    };
  },
  computed: {
    title() {
      return this.$t("PRIVILEGE._PRIVILEGE.ADD_CUSTUMER");
    },
  },
  methods: {
    submitForm() {
      var chk = this.$refs.form.validate();
      if (chk) {
        this.postDataToApi();
      }
    },
    resetForm() {
        this.form.firstName = "";
        this.form.lastName = "";
        this.form.email = "";
        this.form.phoneNumber = "";
        this.form.password = "";
        this.form.confirmPassword = "";
        this.form.gender = "";
    },
    postDataToApi() {
      this.form.loading = true;
      this.form.errors = [];

      ApiService.setHeader();
      ApiService.post("/Api/Privilege/AddCustomer", {
        PlaceId: this.$route.params.id,
        FirstName : this.form.firstName,
        LastName : this.form.lastName,
        Email : this.form.email,
        Password : this.form.password,
        ConfirmPassword : this.form.confirmPassword,
        PhoneNumber : this.form.phoneNumber,
        Gender : this.form.gender
      })
        .then(({ data }) => {
          if (data.status) {
            // close dialog
            this.form.dialog = true;
          } else {
            this.form.dialog = false;
            this.form.loading = false;
            this.form.errors.push(data.message);
            this.$vuetify.goTo(this.$refs.alert, {
              duration: 1000,
              easing: "easeInOutCubic",
              offset: -20,
            });
          }
        })
        .catch(({ response }) => {
          if (response.data) {
            this.form.errors.push(response.data.message);
          } else {
            this.form.errors.push("Unknow error occurs");
          }
          this.$vuetify.goTo(this.$refs.alert, {
            duration: 1000,
            easing: "easeInOutCubic",
            offset: -20,
          });
          this.form.dialog = false;
          this.form.loading = false;
        });
    },
    closeDialog() {
      // not close but redirect to search page
      this.$router.push({
        name: "searchPrivilege",
        param: this.$route.params.id,
      });
    },
    getPlaceApi(id) {
      this.form.loading = true;
      return new Promise(() => {
        ApiService.setHeader();
        ApiService.get("/Api/Place/Name", id)
          .then(({ data }) => {
            this.form.placeName = data.name_TH + " (" + data.name_EN + ")";
          })
          .finally(() => {
            this.form.loading = false;
          });
      });
    },


  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      {
        title: this.$t("PRIVILEGE._PRIVILEGE.SECTION"),
        route: "/place/searchPrivilege/" + this.$route.params.id,
      },
      { title: this.$t("PRIVILEGE._PRIVILEGE.ADD_CUSTUMER") },
    ]);

    this.getPlaceApi(this.$route.params.id);
  },
  watch: {

  },
};
</script>

<style lang="scss" scoped></style>
