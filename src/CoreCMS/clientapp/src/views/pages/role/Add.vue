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
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.name"
                    :disabled="form.loading"
                    :counter="256"
                    :label="$t('ROLE.ADD_EDIT.NAME')"
                    :rules="form.nameRules"
                    required
                  ></v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-autocomplete
                    v-model="form.userType"
                    :disabled="form.loading"
                    :items="form.userTypeItems"
                    :loading="form.userTypeLoading"
                    :search-input.sync="form.userTypeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('ROLE.ADD_EDIT.USER_TYPE')"
                    :rules="form.userTypeRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>

                <v-col cols="12" md="6">
                  <v-textarea
                    v-model="form.description"
                    :disabled="form.loading"
                    :counter="450"
                    :label="$t('ROLE.ADD_EDIT.DESCRIPTION')"
                    :rules="form.descriptionRules"
                    required
                  ></v-textarea>
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
                      $t("ROLE.ADD_EDIT.SUCCESS_DIALOG_HEADER")
                    }}</v-card-title
                  >
                  <v-card-text>
                    {{ $t("ROLE.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
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
        userType: null,
        userTypeSearch: "",
        userTypeLoading: false,
        userTypeItems: [],
        //pictureFile: [],
        userTypeRules: [
          (v) => !!v || this.$t("ROLE.ADD_EDIT.USER_TYPE_VALIDATION"),
        ],
        nameRules: [
          (v) => !!v || this.$t("ROLE.ADD_EDIT.NAME_VALIDATION"),
          (v) =>
            (v && v.length <= 450) || this.$t("ROLE.ADD_EDIT.NAME_COUNTER"),
          (v) => /[a-z_]+$/.test(v) || this.$t("ROLE.ADD_EDIT.NAME_FORMAT"),
        ],
        descriptionRules: [
          (v) => !!v || this.$t("ROLE.ADD_EDIT.DESCRIPTION_VALIDATION"),
          (v) =>
            (v && v.length <= 450) ||
            this.$t("ROLE.ADD_EDIT.DESCRIPTION_COUNTER"),
        ],
        errors: [],
        nameTH: "",
        nameEN: "",
        inActiveStatus: true,
        picture: null,
        pictureType: ["image/png"],
      },
    };
  },
  computed: {
    title() {
      return this.$t("MENU.ROLE.ADD");
    },
  },
  watch: {
    "form.userTypeSearch": {
      handler() {
        this.getUserTypeFromApi().then((data) => {
          this.form.userTypeItems = data;
        });
      },
    },
  },
  methods: {
    submitForm() {
      //var self = this;
      var chk = this.$refs.form.validate();
      if (chk) {
        this.postDataToApi();
      }
    },
    resetForm() {
      this.$refs.form.reset();
    },
    getUserTypeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/UserType/UserTypeOptions", {
          query: this.form.userTypeSearch,
          //isall:true,
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
      //var self = this;
      this.form.loading = true;
      this.form.errors = [];

      ApiService.setHeader();
      ApiService.post("/Api/Role/Add", {
        Name: this.form.name,
        Description: this.form.description,
        UserTypeId: this.form.userType.value,
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
      this.$router.push({ name: "searchRole" });
    },
    lowercase() {
      console.log("this.form.name.toLowercase()");
      this.form.name = this.form.name.toLowerCase();
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("MENU.ROLE.SECTION"), route: "/role" },
      { title: this.$t("MENU.ROLE.ADD") },
    ]);
  },
};
</script>

<style lang="scss" scoped></style>
