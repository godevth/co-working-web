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
                    :label="$t('TERM_AND_CONDITION.ADD_EDIT.PLACE_NAME')"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.name"
                    :disabled="form.loading"
                    :counter="100"
                    :label="$t('TERM_AND_CONDITION.ADD_EDIT.NAME')"
                    :rules="form.nameRules"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="12">
                  <label>{{ $t("TERM_AND_CONDITION.ADD_EDIT.TERM_TH") }}</label>
                  <tinymce
                    id="termTH"
                    :other_options="form.tinyOptions"
                    v-model="form.termTH"
                  ></tinymce>
                </v-col>
                <v-col cols="12" md="12">
                  <label>{{ $t("TERM_AND_CONDITION.ADD_EDIT.TERM_EN") }}</label>
                  <tinymce
                    id="termEN"
                    :other_options="form.tinyOptions"
                    v-model="form.termEN"
                  ></tinymce>
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
                      $t("TERM_AND_CONDITION.ADD_EDIT.SUCCESS_DIALOG_HEADER")
                    }}
                  </v-card-title>
                  <v-card-text>
                    {{
                      $t("TERM_AND_CONDITION.ADD_EDIT.EDIT_SUCCESS_DIALOG_TEXT")
                    }}
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
import tinymce from "vue-tinymce-editor";

export default {
  components: {
    KTPortlet,
    tinymce,
  },
  data() {
    return {
      form: {
        valid: true,
        dialog: false,
        loading: false,
        errors: [],
        termId: null,
        placeId: null,
        placeName: "",
        tinyOptions: {
          height: 500,
        },
        termTH: null,
        termEN: null,
        nameRules: [
          (v) => !!v || this.$t("THEME.ADD_EDIT.NAME_VALIDATION"),
          (v) =>
            (v && v.length <= 100) || this.$t("THEME.ADD_EDIT.NAME_COUNTER"),
        ],
      },
    };
  },
  computed: {
    title() {
      return this.$t("TERM_AND_CONDITION._TERM_AND_CONDITION.ADD");
    },
  },
  methods: {
    submitForm() {
      var chk = this.$refs.form.validate();
      this.form.errors = [];
      if (!this.form.termTH) {
        this.form.errors.push(
          this.$t("TERM_AND_CONDITION.ADD_EDIT.TERM_TH_VALIDATION")
        );
      }

      if (!this.form.termEN) {
        this.form.errors.push(
          this.$t("TERM_AND_CONDITION.ADD_EDIT.TERM_EN_VALIDATION")
        );
      }

      if (this.form.errors.length > 0) {
        this.$vuetify.goTo(this.$refs.alert, {
          duration: 1000,
          easing: "easeInOutCubic",
          offset: -20,
        });
        chk = false;
      }

      if (chk) {
        this.postDataToApi();
      }
    },
    resetForm() {
      this.form.name = null;
      this.form.termTH = null;
      this.form.termEN = null;
      //this.$refs.form.reset();
    },
    postDataToApi() {
      this.form.loading = true;
      this.form.errors = [];

      ApiService.setHeader();
      ApiService.put("/Api/TermAndCondition/Edit", {
        TermId: this.form.termId,
        Name: this.form.name,
        TermTH: this.form.termTH,
        TermEN: this.form.termEN,
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
      this.$route.params.id = this.form.placeId;
      this.$router.push({
        name: "searchTermAndCondition",
        param: this.$route.params.id,
      });
    },
    getDataFromApi(id) {
      this.form.loading = true;
      return new Promise(() => {
        ApiService.setHeader();
        ApiService.get("/Api/TermAndCondition", id)
          .then(({ data }) => {
            this.form.placeId = data.placeId;
            this.form.placeName = data.placeName;
            this.form.name = data.name;
            this.form.termTH = data.termTH;
            this.form.termEN = data.termEN;

            //console.log(this.form.placeId);
          })
          .finally(() => {
            this.form.loading = false;
          });
      });
    },
  },
  watch: {
    "form.placeId"(newValue) {
      this.$store.dispatch(SET_BREADCRUMB, [
        {
          title: this.$t("TERM_AND_CONDITION._TERM_AND_CONDITION.SECTION"),
          route: "/place/searchTermAndCondition/" + newValue,
        },
        { title: this.$t("TERM_AND_CONDITION._TERM_AND_CONDITION.EDIT") },
      ]);
    },
  },
  mounted() {
    this.form.termId = this.$route.params.id;
    this.getDataFromApi(this.form.termId);
  },
};
</script>

<style lang="scss" scoped></style>
