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
                    :label="$t('COMPANY.ADD_EDIT.NAME_TH')"
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
                    :label="$t('COMPANY.ADD_EDIT.NAME_EN')"
                    :rules="form.nameRulesEN"
                    required
                  ></v-text-field>
                </v-col>
                  <v-col
                  cols="12"
                  md="4"
                >
                  <v-autocomplete
                    v-model="form.owner"
                    :disabled="form.loading"
                    :items="form.ownerItems"
                    :loading="form.ownerLoading"
                    :search-input.sync="form.ownerSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('COMPANY.SEARCH.OWNER')"
                    :rules="form.ownerRules"
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
                    :label="$t('COMPANY.ADD_EDIT.IN_ACTIVE_STATUS')"
                    required
                  ></v-checkbox>
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
                      $t("COMPANY.ADD_EDIT.SUCCESS_DIALOG_HEADER")
                    }}</v-card-title
                  >
                  <v-card-text>
                      {{ $t("COMPANY.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
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
        owner:null,
        ownerSearch: "",
        ownerLoading: false,
        ownerItems:[],
        ownerRules:[
          (v) => !!v || this.$t("COMPANY.ADD_EDIT.OWNER_VALIDATION"),
        ],
        nameRulesTH: [
          (v) => !!v || this.$t("COMPANY.ADD_EDIT.NAME_TH_VALIDATION"),
          (v)  => (v && v.length <= 450) || this.$t("COMPANY.ADD_EDIT.NAME_COUNTER"),
        ],
        nameRulesEN: [
          (v)  => !!v || this.$t("COMPANY.ADD_EDIT.NAME_EN_VALIDATION"),
          (v)  => (v && v.length <= 450) || this.$t("COMPANY.ADD_EDIT.NAME_COUNTER"),
        ],
        errors: [],
        nameTH: "",
        nameEN: "",
        inActiveStatus: true,
      }
    }
  },
  computed: {
    title() {
      return this.$t("COMPANY._COMPANY.ADD");
    },
  },
  watch: {
    "form.ownerSearch": {
      handler() {
        this.getOwnerFromApi().then((data) => {
          this.form.ownerItems = data;
        });
      }
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
      this.$refs.form.reset();
    },
    getOwnerFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/User/Select2User", {
          search: this.form.ownerSearch,
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
      this.form.loading = true;
      this.form.errors = [];

      ApiService.setHeader();
      ApiService.post("/Api/Company/Add", 
      {
        Name_TH: this.form.nameTH,
        Name_EN: this.form.nameEN,
        OwnerId:this.form.owner?this.form.owner.value:null,
        InActiveStatus: !this.form.inActiveStatus,   
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
      this.$router.push({ name: "searchCompany" });
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("COMPANY._COMPANY.SECTION"), route: "/Company" },
      { title: this.$t("COMPANY._COMPANY.ADD") },
    ]);
  },
};
</script>

<style lang="scss" scoped></style>
