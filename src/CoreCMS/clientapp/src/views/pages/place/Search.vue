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
                    v-model="form.placeType"
                    :disabled="datatable.loading"
                    :items="form.placeTypeItems"
                    :loading="form.placeTypeLoading"
                    :search-input.sync="form.placeTypeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.SEARCH.PLACETYPE')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-3">
                  <v-autocomplete
                    v-model="form.roomType"
                    :disabled="datatable.loading"
                    :items="form.roomTypeItems"
                    :loading="form.roomTypeLoading"
                    :search-input.sync="form.roomTypeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.SEARCH.ROOMTYPE')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-3">
                  <v-autocomplete
                    v-model="form.facility"
                    :disabled="datatable.loading"
                    :items="form.facilityItems"
                    :loading="form.facilityLoading"
                    :search-input.sync="form.facilitySearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.SEARCH.FACILITY')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-3">
                  <v-autocomplete
                    v-model="form.province"
                    :disabled="datatable.loading"
                    :items="form.provinceItems"
                    :loading="form.provinceLoading"
                    :search-input.sync="form.provinceSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.SEARCH.PROVINCE')"
                    @change="onChange()"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-3">
                  <v-autocomplete
                    v-model="form.ampher"
                    :disabled="datatable.loading"
                    :items="form.ampherItems"
                    :loading="form.ampherLoading"
                    :search-input.sync="form.ampherSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.SEARCH.AMPHER')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
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

            <v-subheader>
              <v-icon left>mdi-table-search</v-icon>
              {{ $t("SHARED.SEARCH_RESULT") }}
              <v-spacer></v-spacer>
              <v-btn
                color="info"
                class="mr-4"
                type="button"
                tile
                to="/place/Add"
                target="_self"
              >
                <v-icon left>add_circle_outline</v-icon>
                {{ $t("PLACE._PLACE.ADD") }}
              </v-btn>
            </v-subheader>

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
                <v-btn
                  :color="getInActiveStatusColor(item.inActiveStatus,item.isApproveCreate,item.isApproveDelete)"
                  class="mr-4"
                  tile
                  block
                >
                  {{ getInActiveStatusText(item.inActiveStatus,item.isApproveCreate,item.isApproveDelete) }}
                </v-btn>
              </template>

              <template v-slot:item.id="{ item }">
                <v-icon medium class="mr-2" @click="searchRoom(item.id)">
                  mdi-menu
                </v-icon>
                <!-- <v-icon
                                  medium
                                  class="mr-2"
                                  @click="detailItem(item.id)"
                                >
                                  mdi-content-paste
                                </v-icon> -->
                <v-icon medium class="mr-2" @click="genQRCode(item.id)">
                  mdi-qrcode
                </v-icon>
                <v-icon
                  medium
                  class="mr-2"
                  @click="searchPrivilege(item.id)"
                  v-show="
                    item.seeingCode == 'SEEING_TYPE_PRIVATE' ||
                      item.seeingCode == 'SEEING_TYPE_PRIVATE_ONLY'
                  "
                >
                  mdi-eye
                </v-icon>
                <v-icon
                  medium
                  class="mr-2"
                  @click="searchTheme(item.id)"
                  v-show="
                    item.seeingCode == 'SEEING_TYPE_PRIVATE' ||
                      item.seeingCode == 'SEEING_TYPE_PRIVATE_ONLY'
                  "
                >
                  fas fa-brush
                </v-icon>
                <v-icon
                  medium
                  class="mr-2"
                  @click="searchTerm(item.id)"
                  v-show="
                    item.seeingCode == 'SEEING_TYPE_PRIVATE' ||
                      item.seeingCode == 'SEEING_TYPE_PRIVATE_ONLY'
                  "
                >
                  mdi-language-html5
                </v-icon>
                <v-icon medium class="mr-2" @click="editItem(item.id)">
                  mdi-pencil
                </v-icon>
                <v-icon medium @click="deleteItem(item.id)">
                  mdi-delete
                </v-icon>
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
              v-model="datatable.toggleDialog"
              persistent
              max-width="300"
            >
              <v-card>
                <v-card-title class="headline">
                  {{ $t("SHARED.TOGGLE_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.toggleDialogText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="datatable.toggleDialog = false"
                  >
                    {{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmToggleItem()"
                  >
                    {{ $t("SHARED.CONFIRM_BUTTON") }}
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog
              v-model="datatable.deleteDialog"
              persistent
              max-width="300"
            >
              <v-card>
                <v-card-title class="headline">
                  {{ $t("SHARED.DELETE_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                  {{ $t("SHARED.CONFIRM_DELETE_DIALOG_TEXT") }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="datatable.deleteDialog = false"
                  >
                    {{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmDeleteItem()"
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

import KTPortlet from "@/views/partials/content/Portlet.vue";

export default {
  components: {
    KTPortlet,
  },
  data() {
    return {
      form: {
        searchKeyword: "",
        placeType: null,
        placeTypeSearch: "",
        placeTypeLoading: false,
        placeTypeItems: [],
        roomType: null,
        roomTypeSearch: "",
        roomTypeLoading: false,
        roomTypeItems: [],
        facility: null,
        facilitySearch: "",
        facilityLoading: false,
        facilityItems: [],
        ampher: null,
        ampherSearch: "",
        ampherLoading: false,
        ampherItems: [],
        province: null,
        provinceSearch: "",
        provinceLoading: false,
        provinceItems: [],
        inActiveStatus: null,
        inActiveStatusItems: [
          {
            text: this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_YES"),
            value: false,
          },
          {
            text: this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_NO"),
            value: true,
          },
        ],
      },
      datatable: {
        headers: [
          {
            text: this.$t("PLACE.SEARCH.NAME_TH"),
            value: "placeNameTH",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.NAME_EN"),
            value: "placeNameEN",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.ADDRESS"),
            value: "address",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.AMPHER"),
            value: "ampherNameTH",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.PROVINCE"),
            value: "provinceNameTH",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.PLACETYPE"),
            value: "placeTypeName",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.ROOMTYPE"),
            value: "roomType",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.FACILITY"),
            value: "facility",
            align: "center",
          },
          {
            text: this.$t("PLACE.SEARCH.IN_ACTIVE_STATUS"),
            value: "inActiveStatus",
            align: "center",
            
          },
          {
            text: this.$t("PLACE.SEARCH.ACTION"),
            value: "id",
            align: "center",
            sortable: false,
            width: "10%",
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
        toggleDialog: false,
        toggleDialogText: null,
        toggleDialogId: null,
        deleteDialog: false,
        deleteDialogId: null,
        dialog: false,
        dialogText: null,
        dialogHeader: null,
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
    setApmher() {},
    getDataFromApi() {
      this.datatable.loading = true;
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Place/Search", {
          GroupBy: this.datatable.options.groupBy,
          GroupDesc: this.datatable.options.groupDesc,
          ItemsPerPage: this.datatable.options.itemsPerPage,
          Page: this.datatable.options.page,
          SortBy: this.datatable.options.sortBy,
          SortDesc: this.datatable.options.sortDesc,
          SearchKeyword: this.form.searchKeyword,
          ProvinceId: this.form.province ? this.form.province.value : null,
          AmpherId: this.form.ampher ? this.form.ampher.value : null,
          PlaceTypeId: this.form.placeType ? this.form.placeType.value : null,
          RoomTypeId: this.form.roomType ? this.form.roomType.value : null,
          FacilityId: this.form.facility ? this.form.facility.value : null,
          SearchInActiveStatus: this.form.inActiveStatus
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
      // if (inActiveStatus) return "error";
      // else return "success";
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
    getPlaceTypeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/PlaceType/Select2PlaceType", {
          search: this.form.placeTypeSearch,
          isAll: false,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roleLoading = false;
          });
      });
    },
    getRoomTypeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/RoomType/Select2RoomType", {
          search: this.form.roomTypeSearch,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roleLoading = false;
          });
      });
    },
    getFacilityFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Facility/Select2Facility", {
          search: this.form.facilitySearch,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roleLoading = false;
          });
      });
    },
    getProvinceFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/MasterLocation/Select2Province", {
          search: this.form.provinceSearch,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roleLoading = false;
          });
      });
    },
    getAmpherFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/MasterLocation/Select2Ampher", {
          Search: this.form.ampherSearch,
          AmpherId: null,
          ProvinceId: this.form.province ? this.form.province.value : null,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roleLoading = false;
          });
      });
    },
    onChange() {
      this.getAmpherFromApi().then((data) => {
        this.form.ampherItems = data;
      });
    },
    editItem(id) {
      this.$router.push({ name: "editPlace", params: { id } });
    },
    // detailItem(id){
    //   this.$router.push({ name: "detailPlace", params: { id } });
    // },
    searchRoom(id) {
      this.$router.push({ name: "searchRoom", params: { id } });
    },
    toggleItem(id, inActiveStatus) {
      this.datatable.toggleDialog = true;
      this.datatable.toggleDialogId = id;
      if (inActiveStatus)
        this.datatable.toggleDialogText = this.$t(
          "SHARED.TOGGLE_OPEN_DIALOG_TEXT"
        );
      else
        this.datatable.toggleDialogText = this.$t(
          "SHARED.TOGGLE_CLOSE_DIALOG_TEXT"
        );
    },
    // confirmToggleItem(){
    //   this.datatable.dialogHeader = this.$t("SHARED.TOGGLE_RESULT_DIALOG_HEADER");
    //   ApiService.setHeader();
    //   ApiService.update("/Api/Menu", this.datatable.toggleDialogId +"/Toggle")
    //     .then(({ data }) => {
    //       //console.log(data);
    //       this.datatable.dialog = true;
    //       if (data.message) {
    //         this.datatable.dialogText = data.message;
    //       } else {
    //         this.datatable.dialogText = "Unknow error occurs";
    //       }
    //     })
    //     .catch(({ response }) => {
    //       //console.log(response);
    //       this.datatable.dialog = true;
    //       if (response.data) {
    //         this.datatable.dialogText = response.data.message;
    //       } else {
    //         this.datatable.dialogText = "Unknow error occurs";
    //       }
    //     });

    //   this.datatable.toggleDialogText = null;
    //   this.datatable.toggleDialogId = null;
    //   this.datatable.toggleDialog = false;
    // },
    closeDialog() {
      this.datatable.dialog = false;
      this.datatable.dialogText = null;
      this.datatable.dialogHeader = null;
      this.submitForm();
    },
    deleteItem(id) {
      this.datatable.deleteDialog = true;
      this.datatable.deleteDialogId = id;
    },
    confirmDeleteItem() {
      this.datatable.dialogHeader = this.$t(
        "SHARED.DELETE_RESULT_DIALOG_HEADER"
      );
      ApiService.setHeader();
      ApiService.update("/Api/Place", this.datatable.deleteDialogId + "/Delete")
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

      this.datatable.deleteDialogId = null;
      this.datatable.deleteDialog = false;
    },
    genQRCode(id) {
      this.datatable.loading = true;
      return new Promise(() => {
        ApiService.setHeader();
        ApiService.download(`/Api/Place/QRCode/${id}`)
          .then(({ data, headers }) => {
            var blob = new Blob([data], { type: headers["content-type"] });
            var url = window.URL.createObjectURL(blob);
            window.open(url);
          })
          .finally(() => {
            this.datatable.loading = false;
          });
      });
    },
    searchPrivilege(id) {
      this.$router.push({ name: "searchPrivilege", params: { id } });
    },
    searchTheme(id) {
      this.$router.push({ name: "searchTheme", params: { id } });
    },
    searchTerm(id) {
      this.$router.push({ name: "searchTermAndCondition", params: { id } });
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      {
        title: this.$t("LOCATION_TPYE._LOCATION_TPYE.SECTION"),
        route: "/place",
      },
      { title: this.$t("LOCATION_TPYE._LOCATION_TPYE.SEARCH") },
    ]);
  },
  computed: {
    title() {
      return this.$t("LOCATION_TPYE._LOCATION_TPYE.SEARCH");
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
    "form.placeTypeSearch": {
      handler() {
        this.getPlaceTypeFromApi().then((data) => {
          this.form.placeTypeItems = data;
        });
      },
    },
    "form.roomTypeSearch": {
      handler() {
        this.getRoomTypeFromApi().then((data) => {
          this.form.roomTypeItems = data;
        });
      },
    },
    "form.facilitySearch": {
      handler() {
        this.getFacilityFromApi().then((data) => {
          this.form.facilityItems = data;
        });
      },
    },
    "form.provinceSearch": {
      handler() {
        this.getProvinceFromApi().then((data) => {
          this.form.provinceItems = data;
        });
      },
    },
    "form.ampherSearch": {
      handler() {
        this.getAmpherFromApi().then((data) => {
          this.form.ampherItems = data;
        });
      },
    },
  },
};
</script>

<style lang="scss" scoped></style>
