<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <KTPortlet v-bind:title="title">
          <template v-slot:body>
            <v-form ref="form" @submit.prevent="submitForm">
              <div class="row">
                <div class="col-9">
                  <v-text-field
                    v-model="form.searchKeyword"
                    :disabled="datatable.loading"
                    :label="$t('SHARED.KEYWORD')"
                    :hint="$t('LOCATION_TPYE.SEARCH.KEYWORD_HINT')"
                    prepend-icon="mdi-file-document-box-search-outline"
                  ></v-text-field>
                </div>
                <div class="col-3">
                  <v-autocomplete
                    v-model="form.inActiveStatus"
                    :disabled="datatable.loading"
                    :items="form.inActiveStatusItems"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.SEARCH.IN_ACTIVE_STATUS')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <v-btn
                    :disabled="datatable.loading"
                    color="success"
                    class="mr-4"
                    tile
                    type="submit"
                  >
                    <v-icon v-if="!datatable.loading" left
                      >mdi-database-search</v-icon
                    >
                    <v-icon v-if="datatable.loading" left
                      >fa fa-spinner fa-spin</v-icon
                    >
                    {{ $t("SHARED.SEARCH_BUTTON") }}
                  </v-btn>
                  <v-btn
                    :disabled="datatable.loading"
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
              </div>
            </v-form>

            <v-divider class="my-4"></v-divider>


            <v-data-table
              :headers="datatable.headers"
              :items="datatable.items"
              :loading="datatable.loading"
              :options.sync="datatable.options"
              :server-items-length="datatable.total"
              :footer-props="{
                'items-per-page-options': [30, 50, 100],
              }"
              multi-sort
            >
              <template v-slot:item.inActiveStatus="{ item }">
                <!-- <v-btn
                  :color="getInActiveStatusColor(item.inActiveStatus,item.isApproveCreate,item.isApproveDelete)"
                  class="mr-4"
                  tile
                  block
                >
                  {{ getInActiveStatusText(item.inActiveStatus,item.isApproveCreate,item.isApproveDelete) }}
                </v-btn> -->
                <p  >
                    {{ getInActiveStatusText(item.inActiveStatus,item.isApproveCreate,item.isApproveDelete) }}
                </p>
              </template>

              <template v-slot:item.id="{ item }">
               
               <v-btn
                  color="success"
                  class="mr-4"
                  tile
                  @click="approvePlace(item.id)"
                  v-if="!item.isApproveCreate"
                >
                  {{ $t("APPROVE_PLACE.SEARCH.APPROVE") }}
                </v-btn>
                <v-btn
                  color="success"
                  class="mr-4"
                  tile
                  @click="approveDelete(item.id)"
                  v-if="item.isApproveDelete"
                >
                  {{ $t("APPROVE_PLACE.SEARCH.APPROVE") }}
                </v-btn>

                <v-btn
                  color="error"
                  class="mr-4"
                  tile
                  v-if="!item.isApproveCreate"
                  @click="notApprovePlace(item.id)"
                >
                  {{ $t("APPROVE_PLACE.SEARCH.NOTAPPROVE") }}
                </v-btn>
                <v-btn
                  color="error"
                  class="mr-4"
                  tile
                  v-if="item.isApproveDelete"
                  @click="notApproveDelete(item.id)"
                >
                  {{ $t("APPROVE_PLACE.SEARCH.NOTAPPROVE") }}
                </v-btn>
              </template>
            </v-data-table>

            <v-dialog v-model="datatable.loading" persistent width="300">
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

            <v-dialog
              v-model="datatable.actionDialog"
              persistent
              :max-width="datatable.dialogWidth"
            >
            <v-form ref="dialog">
              <v-card>
                <v-card-title class="headline">
                  {{ $t("SHARED.APPROVE_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.actionDialogText }}
                  <div v-if="datatable.dialogCreate">
                    <v-row>
                      <v-col
                        cols="12"
                        md="12"
                      >
                        <v-text-field
                          v-model="form.subMerchantId"
                          :label="$t('APPROVE_PLACE.SEARCH.SUBMERCHANT')"
                          :rules="form.subMerchantIdRules"
                          required
                        ></v-text-field>
                      </v-col>
                      
                      <!-- <v-col
                        cols="12"
                        md="12"
                      >
                        <v-text-field
                          v-model="form.GP"
                          :label="$t('APPROVE_PLACE.SEARCH.GP')"
                          :rules="form.GPRules"
                          required
                        ></v-text-field>
                      </v-col> -->

                      <v-col cols="12"
                                       md="12">
                                    <vuetify-money v-model="form.GP"
                                                   v-bind:label="$t('APPROVE_PLACE.SEARCH.GP')"
                                                   v-bind:readonly="false"
                                                   v-bind:disabled="false"
                                                   v-bind:clearable="true"
                                                   v-bind:valueWhenIsEmpty="valueWhenIsEmptyCapacity"
                                                   v-bind:options="optionsCapacity"
                                                   v-bind:properties="propertiesCapacity"
                                                   :valueOptions="valueOptionsCapacity"
                                                   v-on:CustomMinEvent="form.GP = $event"
                                                   v-on:CustomMaxEvent="form.GP = $event"
                                                   :rules="form.GPRules"
                                                   required
                                                  />
                                                  
                                </v-col>
                    </v-row>
                    
                  </div>
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="closeApprovePlace()"
                  >
                    {{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmApprove()"
                  >
                    {{ $t("SHARED.CONFIRM_BUTTON") }}
                  </v-btn>
                </v-card-actions>
              </v-card>
              </v-form>
            </v-dialog>

             <v-dialog
              v-model="datatable.actionDialogNot"
              persistent
              max-width="300"
            >
              <v-card>
                <v-card-title class="headline">
                  {{ $t("SHARED.NOT_APPROVE_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                 {{datatable.actionDialogNotText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="datatable.actionDialogNot = false"
                  >
                    {{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmNotApprove()"
                  >
                    {{ $t("SHARED.CONFIRM_BUTTON") }}
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog v-model="datatable.dialog" persistent max-width="300">
              <v-card>
                <v-card-title class="headline">
                  {{ datatable.dialogHeader }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.dialogText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="green darken-1" text @click="closeDialog">{{
                    $t("SHARED.CLOSE_BUTTON")
                  }}</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </template>
        </KTPortlet>
      </div>
    </div>
  </div>
</template>

<script>
import ApiService from "@/common/api.service";
import { SET_BREADCRUMB } from "@/store/breadcrumbs.module";
import VuetifyMoney from "../VuetifyMoney.vue";
import KTPortlet from "@/views/partials/content/Portlet.vue";

export default {
  components: {
    KTPortlet,
    "vuetify-money": VuetifyMoney
  },
  data() {
    return {
      form: {
        searchKeyword: "",
        inActiveStatus: null,
        subMerchantId: "",
        GP:null,
        subMerchantIdRules: [
          (v) => !!v || this.$t("APPROVE_PLACE.SEARCH.SUBMERCHANT_VALIDATION"),
        ],
        GPRules: [
          (v) => !!v || this.$t("APPROVE_PLACE.SEARCH.GP_VALIDATION"),
        ],
        inActiveStatusItems: [
          {
            text: this.$t("SYS_VARIABLE.WAIT_APPROVE_CREATE"),
            value: false,
          },
          {
            text: this.$t("SYS_VARIABLE.WAIT_PPROVE_DELETE"),
            value: true,
          },
        ],
      },
      valueWhenIsEmptyCapacity: "", 
      valueOptionsCapacity: {
          min: 0,
          minEvent: "CustomMinEvent",
          max: 100,
          maxEvent: "CustomMaxEvent"
      },
      optionsCapacity: {
          locale: "en-US",
          prefix: "",
          suffix: "",
          length: 5,
          precision: 2
      },
      propertiesCapacity: {
          hint:"กรอกจำนวน 1-100"
      },
      datatable: {
        headers: [
          {
            text: this.$t("APPROVE_PLACE.SEARCH.NAME_TH"),
            value: "placeNameTH",
            align: "center",
          },
          {
            text: this.$t("APPROVE_PLACE.SEARCH.NAME_EN"),
            value: "placeNameEN",
            align: "center",
          },
        {
            text: this.$t("APPROVE_PLACE.SEARCH.ADDRESS"),
            value: "address",
            align: "center",
          },
          {
            text: this.$t("APPROVE_PLACE.SEARCH.AMPHER"),
            value: "ampherNameTH",
            align: "center",
          },
          {
            text: this.$t("APPROVE_PLACE.SEARCH.PROVINCE"),
            value: "provinceNameTH",
            align: "center",
          },
          {
            text: this.$t("APPROVE_PLACE.SEARCH.IN_ACTIVE_STATUS"),
            value: "inActiveStatus",
            align: "center",
            width: "10%",
            
          },
          {
            text: this.$t("APPROVE_PLACE.SEARCH.ACTION"),
            value: "id",
            align: "center",
            sortable: false,
            width: "20%",
          },
        ],
        items: [],
        total: 0,
        loading: true,
        options: {
          sortBy: ["placeNameTH"],
          sortDesc: [false],
          itemsPerPage: 30,
        },
        actionDialog: false,
        actionDialogId: null,
        actionDialogHeader :null,
        actionDialogText:null,

        dialogCreate :false,
        dialogWidth :300,
        dialog: false,
        dialogText: null,
        dialogHeader: null,

        actionDialogNot: false,
        actionDialogNotId: null,
        actionDialogNotText:null,
      },
    };
  },
  methods: {
    submitForm() {
      this.getDataFromApi().then((data) => {
        this.datatable.total = data.total;
        this.datatable.items = data.items;
      });
    },
    resetForm() {
      this.$refs.form.reset();
      this.submitForm();
    },
    getDataFromApi() {
      this.datatable.loading = true;
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Place/SearchApprove", {
          GroupBy: this.datatable.options.groupBy,
          GroupDesc: this.datatable.options.groupDesc,
          ItemsPerPage: this.datatable.options.itemsPerPage,
          Page: this.datatable.options.page,
          SortBy: this.datatable.options.sortBy,
          SortDesc: this.datatable.options.sortDesc,
          SearchKeyword: this.form.searchKeyword,
          ApproveStatus: this.form.inActiveStatus
            ? this.form.inActiveStatus.value
            : null,
        })
          .then(({ data }) => {
            //console.log(data);
            resolve({
              items: data.items,
              total: data.total,
            });
          })
          .finally(() => {
            this.datatable.loading = false;
          });
      });
    },
    getInActiveStatusColor(inActiveStatus,isApproveCreate,isApproveDelete) {

      if (inActiveStatus && isApproveCreate && !isApproveDelete) 
      {
        return "error";
      }
      else if (!inActiveStatus && isApproveCreate && !isApproveDelete)
      {
        return "success";
      }
      else if (!isApproveCreate && !isApproveDelete)
      {
        return "primary";
      }
      else if (isApproveDelete)
      {
        return "warning";
      }
    },
    getInActiveStatusText(inActiveStatus,isApproveCreate,isApproveDelete) {
      if (inActiveStatus && isApproveCreate && !isApproveDelete) 
      {
        return this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_NO")
      }
      else if (!inActiveStatus && isApproveCreate && !isApproveDelete)
      {
        return this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_YES")
      }
      else if (!isApproveCreate && !isApproveDelete)
      {
        return this.$t("SYS_VARIABLE.WAIT_APPROVE_CREATE")
      }
      else if (isApproveDelete)
      {
        return this.$t("SYS_VARIABLE.WAIT_PPROVE_DELETE")
      }
    },

    closeDialog() {
      this.datatable.dialog = false;
      this.datatable.dialogText = null;
      this.datatable.dialogHeader = null;
      this.submitForm();
    },
    approvePlace(id) {
      this.datatable.actionDialog = true;
      this.datatable.dialogCreate = true;
      this.datatable.dialogWidth = 600;
      this.datatable.actionDialogText  = this.$t("SHARED.CONFIRM_APPROVE_PLACE_DIALOG_TEXT");
      this.datatable.actionDialogId = id;
    },
    closeApprovePlace()
    {
      this.datatable.actionDialog = false
      this.$refs.dialog.reset();
    },
    approveDelete(id) {
      this.datatable.actionDialog = true;
      this.datatable.dialogCreate = false;
      this.datatable.dialogWidth = 300;
      this.datatable.actionDialogText  = this.$t("SHARED.CONFIRM_APPROVE_DELECT_DIALOG_TEXT");
      this.datatable.actionDialogId = id;
    },
    confirmApprove() {
      if (this.$refs.dialog.validate()) {

      this.datatable.dialogHeader = this.$t(
        "SHARED.APPROVE_RESULT_DIALOG_HEADER"
      );
      var data  = this.datatable.items.filter(o => o.id == this.datatable.actionDialogId);
      var placeId = data[0].id;
      var approveStatus = data[0].isApproveCreate == true ? true : data[0].isApproveCreate
      ApiService.setHeader();
      ApiService.post("/Api/Place/Approve", {
        Id: placeId,
        ApproveStatus: approveStatus,
        SubMerchantId: this.form.subMerchantId,
        GP : this.form.GP
      })
        .then(({ data }) => {
          //console.log(data);
          this.datatable.dialog = true;
          this.datatable.actionDialog = false;
          this.datatable.actionDialogId = null;
          if (data.message) {
            this.datatable.dialogText = data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        })
        .catch(({ response }) => {
          //console.log(response);
          this.datatable.dialog = true;
          if (response.data) {
            this.datatable.dialogText = response.data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        });

      }
    },
    notApprovePlace(id) {
      this.datatable.actionDialogNot = true;
      this.datatable.actionDialogNotId = id;
      this.datatable.actionDialogNotText  = this.$t("SHARED.CONFIRM_NOT_APPROVE_PLACE_DIALOG_TEXT");
    },
    notApproveDelete(id) {
      this.datatable.actionDialogNot = true;
      this.datatable.actionDialogNotId = id;
      this.datatable.actionDialogNotText  = this.$t("SHARED.CONFIRM_NOT_APPROVE_DELETE_PLACE_DIALOG_TEXT");
    },
    confirmNotApprove() {
      this.datatable.dialogHeader = this.$t(
        "SHARED.NOT_APPROVE_RESULT_DIALOG_HEADER"
      );
      var data  = this.datatable.items.filter(o => o.id == this.datatable.actionDialogNotId);
      var placeId = data[0].id;
      var approveStatus = data[0].isApproveCreate == true ? true : data[0].isApproveCreate
      ApiService.setHeader();
      ApiService.post("/Api/Place/NotApprove", {
        Id: placeId,
        ApproveStatus: approveStatus,
      })
        .then(({ data }) => {
          //console.log(data);
          this.datatable.dialog = true;
          if (data.message) {
            this.datatable.dialogText = data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        })
        .catch(({ response }) => {
          //console.log(response);
          this.datatable.dialog = true;
          if (response.data) {
            this.datatable.dialogText = response.data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        });

      this.datatable.actionDialogNotId = null;
      this.datatable.actionDialogNot = false;
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      {
        title: this.$t("APPROVE_PLACE.SEARCH.SECTION"),
        route: "/place",
      },
      { title: this.$t("APPROVE_PLACE.SEARCH.SEARCH") },
    ]);
  },
  computed: {
    title() {
      return this.$t("APPROVE_PLACE.SEARCH.SEARCH");
    },
  },
  watch: {
    "datatable.options": {
      handler() {
        this.getDataFromApi().then((data) => {
          this.datatable.total = data.total;
          this.datatable.items = data.items;
        });
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss" scoped></style>
