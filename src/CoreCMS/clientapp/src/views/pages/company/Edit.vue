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
                    :readonly="!form.isEdit"
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
                    :readonly="!form.isEdit"
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
                    :readonly="!form.isEdit"
                    :rules="form.ownerRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <div id="companyProfile" class="col-xl-12 table-responsive">
                                    <table class="table table-sm table-striped table-bordered table-hover table-checkable"
                                           style="min-width:2000px;">
                                        <thead>
                                            <tr>
                                                <th class="text-center" style="width: 2%;">ลำดับ</th>
                                                <th class="text-center" style="width: 8%;">แอดมิน</th>
                                                <th class="text-center" style="width: 10%;">สถานที่</th>
                                                <th class="text-center" style="width: 5%;">การจัดการ</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-if="cpItems == null || cpItems.length == 0">
                                                <td colspan="5" class="text-muted text-center">คลิก + เพื่อเพิ่มรายการใหม่</td>
                                            </tr>
                                            <template v-for="(item, i) in cpItems">
                                                <cp-item-row ref="fItems"
                                                             :key="item"
                                                             :rowIndex="i"
                                                             :item="item"
                                                             :isEdit="true"
                                                             @removeItem="onRemoveItemClick"></cp-item-row>
                                            </template>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="11">
                                                    <button type="button"
                                                            class="btn btn-sm btn-brand"
                                                            @click.prevent="onAddItemClick">
                                                        <i class="fa fa-plus"></i> เพิ่มรายการใหม่
                                                    </button>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
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
                      {{ $t("COMPANY.ADD_EDIT.EDIT_SUCCESS_DIALOG_TEXT") }}
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
import cpItemRow from "./ProfileRow";

export default {
  components: {
    KTPortlet,
    cpItemRow
  },
  data() {
    return {
      cpItems:[],
      id:null,
      form: {
        valid: true,
        dialog: false,
        loading: false,
        isEdit:false,
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
    };
  },
  computed: {
    title() {
      return this.$t("COMPANY._COMPANY.EDIT");
    }
  },
  methods: {
    submitForm() {
      var chk = this.$refs.form.validate();
      this.form.errors = [];


        if (chk) {
          this.postDataToApi();
        }
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
    resetForm() {
      this.$refs.form.reset();
    },
                onAddItemClick() {
                // if (
                //   !$(this.$refs.form)
                //     .validate()
                //     .form()
                // ) {
                //   return false;
                // }

                this.cpItems.push({
                  cpItemsId: null,
                  admin: null,
                  place: null,
                });
            },
            onRemoveItemClick(index) {
                var self = this;
                self.cpItems.splice(index, 1);
            },
    postDataToApi() {
       var self = this;
      this.form.loading = true;
      this.form.errors = [];

       var pItem = [];
                self.cpItems.forEach(element => {
                  console.log(element);
                    pItem.push({
                        Id:element.cpItemsId,
                        AdminId: element.admin.value,
                        PlaceId: element.place.value,
                    });
                });
      ApiService.setHeader();
      ApiService.put("/Api/Company/Edit", {
        Id:this.$route.params.id,
        Name_TH: this.form.nameTH,
        Name_EN: this.form.nameEN,
        OwnerId:this.form.owner?this.form.owner.value:null,
        InActiveStatus: !this.form.inActiveStatus,   
        Profile:pItem
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
              offset: -20
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
            offset: -20
          });
          this.form.dialog = false;
          this.form.loading = false;
        });
    },
    closeDialog() {
      // not close but redirect to search page
      this.$router.push({ name: "searchCompany" });
    },
    getDataFromApi(id) {
       let self = this;
      this.form.loading = true;
      return new Promise(() => {
        ApiService.setHeader();
        ApiService.get("/Api/Company", id)
          .then(({ data }) => {
            console.log(data);
            this.id = data.data.id;
            this.form.nameTH = data.data.name_TH;
            this.form.nameEN = data.data.name_EN;
            this.form.owner = {value:data.data.owner};
            this.form.inActiveStatus = !data.data.inActiveStatus;
            this.form.isEdit = data.data.isEdit;
            if (data.data.profiles.length > 0) {
                                for (let i = 0; i < data.data.profiles.length; i++) {
                                    var pfitem = data.data.profiles[i];
                                    var a = {
                                        value: pfitem.adminId
                                    }
                                    var p = {
                                        value: pfitem.placeId
                                    }
                                    this.cpItems.push({
                                        cpItemsId: pfitem.companyProfileId,
                                        admin: a,
                                        place: p,
                                    });
                                    setTimeout(function () {
                                        self.$refs.fItems[i].adminItem={
                                            value:data.data.profiles[i].adminId
                                        };
                                        self.$refs.fItems[i].placeItem={
                                            value:data.data.profiles[i].placeId
                                        };
                                    }, 300);
                                }
                            }
          })
          .finally(() => {
            this.form.loading = false;
          });
      });
    }
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("COMPANY._COMPANY.SECTION"), route: "/Company" },
      { title: this.$t("COMPANY._COMPANY.EDIT") }
    ]);

    this.getDataFromApi(this.$route.params.id);
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
};
</script>

<style lang="scss" scoped></style>
