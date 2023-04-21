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
                <v-col cols="6" md="6">
                  <v-text-field
                    v-model="form.placeName"
                    :disabled="true"
                    :label="$t('PRIVILEGE.ADD_EDIT.PLACE_NAME')"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="6">
                  <v-autocomplete
                    v-model="form.users"
                    :items="form.usersItems"
                    :loading="form.usersLoading"
                    :search-input.sync="form.usersSearch"
                    :rules="form.usersRules"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PRIVILEGE.ADD_EDIT.USER')"
                    return-object
                    multiple
                  >
                    <template v-slot:selection="data">
                      <v-chip
                        v-bind="data.attrs"
                        :input-value="data.selected"
                        close
                        @click="data.select"
                        @click:close="removeUser(data.item)"
                      >
                        {{ data.item.text }}
                      </v-chip>
                    </template>
                  </v-autocomplete>
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
                    {{ $t("PRIVILEGE.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
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
        usersSearch: "",
        usersLoading: false,
        usersItems: [],
        usersRules: [
          (v) =>
            !!(v && v.length) || this.$t("PRIVILEGE.ADD_EDIT.USER_VALIDATION"),
        ],
      },
    };
  },
  computed: {
    title() {
      return this.$t("PRIVILEGE._PRIVILEGE.ADD");
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
      this.form.users = [];
      //this.$refs.form.reset();
    },
    postDataToApi() {
      this.form.loading = true;
      this.form.errors = [];

      var uItems = [];
      this.form.users.forEach((element) => {
        //console.log(element);
        uItems.push(element.value);
      });

      ApiService.setHeader();
      ApiService.post("/Api/Privilege/Add", {
        PlaceId: this.$route.params.id,
        Users: uItems,
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
    removeUser(item) {
      const index = this.form.users.indexOf(item);
      if (index >= 0) this.form.users.splice(index, 1);
    },
    getUserFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/User/Select2UserPrivilege", {
          search: this.form.usersSearch,
          placeId: this.$route.params.id,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.usersLoading = false;
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
      { title: this.$t("PRIVILEGE._PRIVILEGE.ADD") },
    ]);

    this.getPlaceApi(this.$route.params.id);
  },
  watch: {
    "form.usersSearch": {
      handler() {
        this.getUserFromApi().then((data) => {
          this.form.usersItems = data;
        });
      },
    },
  },
};
</script>

<style lang="scss" scoped></style>
