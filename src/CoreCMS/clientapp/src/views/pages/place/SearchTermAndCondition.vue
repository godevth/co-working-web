<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <KTPortlet v-bind:title="title">
          <template v-slot:body>
            <v-col cols="12" md="12">
              <v-text-field
                v-model="form.placeName"
                :disabled="true"
                :label="$t('TERM_AND_CONDITION.SEARCH.PLACE_NAME')"
                required
              ></v-text-field>
            </v-col>
            <v-form ref="form" @submit.prevent="submitForm">
              <div class="row">
                <div class="col-8">
                  <v-text-field
                    v-model="form.searchKeyword"
                    :disabled="datatable.loading"
                    :label="$t('SHARED.KEYWORD')"
                    :hint="$t('TERM_AND_CONDITION.SEARCH.KEYWORD_HINT')"
                    prepend-icon="mdi-file-document-box-search-outline"
                  ></v-text-field>
                </div>
                <div class="col-4">
                  <v-autocomplete
                    v-model="form.inActiveStatus"
                    :disabled="datatable.loading"
                    :items="form.inActiveStatusItems"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('TERM_AND_CONDITION.SEARCH.IN_ACTIVE_STATUS')"
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
                :to="'/place/addTermAndCondition/' + form.placeId"
                target="_self"
              >
                <v-icon left>add_circle_outline</v-icon>
                {{ $t("TERM_AND_CONDITION._TERM_AND_CONDITION.ADD") }}
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
                  :color="getInActiveStatusColor(item.inActiveStatus)"
                  class="mr-4"
                  tile
                  @click="toggleItem(item.termId, item.inActiveStatus)"
                >
                  {{ getInActiveStatusText(item.inActiveStatus) }}
                </v-btn>
              </template>

              <template v-slot:item.termId="{ item }">
                <v-icon medium class="mr-2" @click="editItem(item.termId)">
                  mdi-pencil
                </v-icon>
                <v-icon medium @click="deleteItem(item.termId)">
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
        placeId: this.$route.params.id,
        placeName: "",
        searchKeyword: "",
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
            text: this.$t("TERM_AND_CONDITION.SEARCH.NAME"),
            value: "name",
            align: "center",
          },
          {
            text: this.$t("TERM_AND_CONDITION.SEARCH.CREATE_DATE"),
            value: "createDateString",
            align: "center",
          },
          {
            text: this.$t("TERM_AND_CONDITION.SEARCH.CREATE_USER"),
            value: "createUser",
            align: "center",
          },
          {
            text: this.$t("TERM_AND_CONDITION.SEARCH.IN_ACTIVE_STATUS"),
            value: "inActiveStatus",
            align: "center",
          },
          {
            text: this.$t("TERM_AND_CONDITION.SEARCH.ACTION"),
            value: "termId",
            align: "center",
            sortable: false,
            width: "10%",
          },
        ],
        items: [],
        total: 0,
        loading: true,
        options: {
          sortBy: ["createDateString"],
          sortDesc: [true],
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
      //this.$refs.form.reset();
      this.form.inActiveStatus = null;
      this.form.searchKeyword = "";
      this.submitForm();
    },
    getDataFromApi() {
      this.datatable.loading = true;
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/TermAndCondition/Search", {
          GroupBy: this.datatable.options.groupBy,
          GroupDesc: this.datatable.options.groupDesc,
          ItemsPerPage: this.datatable.options.itemsPerPage,
          Page: this.datatable.options.page,
          SortBy: this.datatable.options.sortBy,
          SortDesc: this.datatable.options.sortDesc,
          SearchKeyword: this.form.searchKeyword,
          PlaceId: this.$route.params.id,
          InActiveStatus: this.form.inActiveStatus
            ? this.form.inActiveStatus.value
            : null,
        })
          .then(({ data }) => {
            //console.log(data);
            if (data.total > 0) {
              this.form.placeName = data.items[0].placeName;
            } else {
              this.getPlaceApi(this.$route.params.id);
            }
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
    getInActiveStatusColor(inActiveStatus) {
      if (inActiveStatus) return "error";
      else return "success";
    },
    getInActiveStatusText(inActiveStatus) {
      if (inActiveStatus) return this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_NO");
      else return this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_YES");
    },
    toggleItem(id, inActiveStatus) {
      this.datatable.toggleDialog = true;
      this.datatable.toggleDialogId = id;
      if (inActiveStatus)
        this.datatable.toggleDialogText =
          this.$t("SHARED.TOGGLE_OPEN_DIALOG_TEXT") +
          " " +
          this.$t("TERM_AND_CONDITION.SEARCH.TOGGLE_OPEN_EXTRA");
      else
        this.datatable.toggleDialogText = this.$t(
          "SHARED.TOGGLE_CLOSE_DIALOG_TEXT"
        );
    },
    confirmToggleItem() {
      this.datatable.dialogHeader = this.$t(
        "SHARED.TOGGLE_RESULT_DIALOG_HEADER"
      );
      ApiService.setHeader();
      ApiService.update(
        "/Api/TermAndCondition",
        this.datatable.toggleDialogId + "/Toggle"
      )
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

      this.datatable.toggleDialogText = null;
      this.datatable.toggleDialogId = null;
      this.datatable.toggleDialog = false;
    },
    closeDialog() {
      this.datatable.dialog = false;
      this.datatable.dialogText = null;
      this.datatable.dialogHeader = null;
      this.submitForm();
    },
    editItem(id) {
      this.$router.push({ name: "editTermAndCondition", params: { id } });
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
      ApiService.update(
        "/Api/TermAndCondition",
        this.datatable.deleteDialogId + "/Delete"
      )
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
        title: this.$t("TERM_AND_CONDITION._TERM_AND_CONDITION.SECTION"),
        route: "/place/searchTermAndCondition/" + this.$route.params.id,
      },
      { title: this.$t("TERM_AND_CONDITION._TERM_AND_CONDITION.SEARCH") },
    ]);
  },
  computed: {
    title() {
      return this.$t("TERM_AND_CONDITION._TERM_AND_CONDITION.SEARCH");
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
