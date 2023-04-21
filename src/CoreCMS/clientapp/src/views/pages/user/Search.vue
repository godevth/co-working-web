<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <KTCodePortlet v-bind:title="title">
          <template v-slot:body>
            <v-form ref="form" @submit.prevent="submitForm">
              <div class="row">
                <div class="col-6">
                  <v-text-field
                    v-model="form.searchKeyword"
                    :disabled="datatable.loading"
                    :label="$t('SHARED.KEYWORD')"
                    :hint="$t('USER.SEARCH.KEYWORD_HINT')"
                    prepend-icon="mdi-file-document-box-search-outline"
                  ></v-text-field>
                </div>
                <div class="col-6">
                  <v-autocomplete
                    v-model="form.role"
                    :disabled="datatable.loading"
                    :items="form.roleItems"
                    :loading="form.roleLoading"
                    :search-input.sync="form.roleSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('USER.SEARCH.ROLE')"
                    :placeholder="
                      $t('SHARED.START_TYPING_TO_SEARCH')
                    "
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-6">
                  <v-autocomplete
                    v-model="form.openId"
                    :disabled="datatable.loading"
                    :items="form.openIdItems"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('USER.SEARCH.IS_OPEN_ID')"
                    :placeholder="
                      $t('SHARED.START_TYPING_TO_SEARCH')
                    "
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-6">
                  <v-autocomplete
                    v-model="form.userType"
                    :disabled="datatable.loading"
                    :items="form.userTypeItems"
                    :loading="form.userTypeLoading"
                    :search-input.sync="form.userTypeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('USER.SEARCH.USER_TYPE')"
                    :placeholder="
                      $t('SHARED.START_TYPING_TO_SEARCH')
                    "
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

            <v-subheader
              ><v-icon left>mdi-table-search</v-icon>
              {{ $t("SHARED.SEARCH_RESULT") }}
              <v-spacer></v-spacer>
              <v-btn
                color="info"
                class="mr-4"
                type="button"
                tile
                to="/User/Add"
                target="_self"
              >
                <v-icon left>add_circle_outline</v-icon>
                {{ $t("MENU.USER.ADD") }}
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
              <template v-slot:item.openID="{ item }">
                <v-icon 
                  :class="getOpenIdClass(item.openID)">
                  {{ getOpenIdIcon(item.openID) }}
                </v-icon>
              </template>

              <template v-slot:item.inActiveStatus="{ item }">
                <v-btn
                  :color="getInActiveStatusColor(item.inActiveStatus)"
                    class="mr-4"
                    tile
                    @click="toggleItem(item.userId, item.inActiveStatus)"
                >
                  <v-icon left>{{ getInActiveStatusIcon(item.inActiveStatus) }}</v-icon>
                  {{ getInActiveStatusText(item.inActiveStatus) }}
                </v-btn>
              </template>

              <template v-slot:item.userId="{ item }">
                <v-icon
                  medium
                  class="mr-2"
                  @click="editItem(item.userId)"
                >
                  mdi-pencil
                </v-icon>
                <v-icon
                  medium
                  @click="deleteItem(item.userId)"
                >
                  mdi-delete
                </v-icon>
              </template>
            </v-data-table>

            <v-dialog
              v-model="datatable.loading"
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
                    >{{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmToggleItem()"
                    >{{ $t("SHARED.CONFIRM_BUTTON") }}</v-btn
                  >
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
                    >{{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmDeleteItem()"
                    >{{ $t("SHARED.CONFIRM_BUTTON") }}</v-btn
                  >
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
                  <v-btn
                    color="green darken-1"
                    text
                    @click="closeDialog"
                    >{{ $t("SHARED.CLOSE_BUTTON") }}</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>
            
          </template>
        </KTCodePortlet>
      </div>
    </div>
  </div>
</template>

<script>
import KTCodePortlet from "@/views/partials/content/Portlet.vue";
import { SET_BREADCRUMB } from "@/store/breadcrumbs.module";
import ApiService from "@/common/api.service";

export default {
  components: {
    KTCodePortlet,
  },
  data() {
    return {
      form: {
        searchKeyword: "",
        role: null,
        roleSearch: "",
        roleLoading: false,
        roleItems: [],
        userType: null,
        userTypeSearch: "",
        userTypeLoading: false,
        userTypeItems: [],
        openId: null,
        openIdItems: [
          {
            text: this.$t("SYS_VARIABLE.OPEN_ID_YES"),
            value: true,
          },
          {
            text: this.$t("SYS_VARIABLE.OPEN_ID_NO"),
            value: false,
          },
        ],
      },
      datatable: {
        headers: [
          {
            text: this.$t("USER.SEARCH.USERNAME"),
            value: "userName",
          },
          {
            text: this.$t("USER.SEARCH.EMAIL"),
            value: "email",
          },
          {
            text: this.$t("USER.SEARCH.ROLE"),
            value: "roles",
          },
          {
            text: this.$t("USER.SEARCH.DISPLAY_NAME"),
            value: "displayName",
          },
          {
            text: this.$t("USER.SEARCH.USER_TYPE"),
            value: "userType",
            align: "center",
          },
          {
            text: this.$t("USER.SEARCH.PHONE_NUMBER"),
            value: "phoneNumber",
            align: "center",
          },
          {
            text: this.$t("USER.SEARCH.POSITION"),
            value: "position",
          },
          {
            text: this.$t("USER.SEARCH.IS_OPEN_ID"),
            value: "openID",
            align: "center",
          },
          {
            text: this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS"),
            value: "inActiveStatus",
            align: "center",
          },
          {
            text: this.$t("USER.SEARCH.ACTION"),
            value: "userId",
            align: "center",
            sortable: false,
            width: "10%"
          },
        ],
        items: [],
        total: 0,
        loading: true,
        options: {
          sortBy: ["userName"],
          sortDesc: [false],
          itemsPerPage: 30,
        },
        toggleDialog: false,
        toggleDialogText : null,
        toggleDialogId : null,
        deleteDialog: false,
        deleteDialogId : null,
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
    getDataFromApi() {
      this.datatable.loading = true;
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/User/Search", {
          GroupBy: this.datatable.options.groupBy,
          GroupDesc: this.datatable.options.groupDesc,
          ItemsPerPage: this.datatable.options.itemsPerPage,
          Page: this.datatable.options.page,
          SortBy: this.datatable.options.sortBy,
          SortDesc: this.datatable.options.sortDesc,
          SearchKeyword: this.form.searchKeyword,
          Role: this.form.role
            ? this.form.role.value
            : null,
          OpenId: this.form.openId
            ? this.form.openId.value
            : null,    
          UserTypeId: this.form.userType
            ? this.form.userType.value
            : null,
        })
          .then(({ data }) => {
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
    getRoleFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Role/RoleOptions", {
          query: this.form.roleSearch,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roleLoading = false;
          });
      });
    },
    getOpenIdClass(openId){
      if (openId) 
        return 'text-success';
      else 
        return 'text-danger';
    },
    getOpenIdIcon(openId){
      if (openId) 
        return 'check_circle';
      else 
        return 'cancel';
    },
    getInActiveStatusColor(inActiveStatus) {
      if (inActiveStatus) 
        return 'error';
      else 
        return 'success';
    },
    getInActiveStatusText(inActiveStatus) {
      if (inActiveStatus) 
        return this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_NO");
      else 
        return this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_YES");
    },
    getInActiveStatusIcon(inActiveStatus) {
      if (inActiveStatus) 
        return 'pause';
      else 
        return 'play_arrow';
    },
    toggleItem(id,inActiveStatus){
      this.datatable.toggleDialog = true;
      this.datatable.toggleDialogId = id;
      if (inActiveStatus) 
        this.datatable.toggleDialogText = this.$t("SHARED.TOGGLE_OPEN_DIALOG_TEXT");
      else 
        this.datatable.toggleDialogText =  this.$t("SHARED.TOGGLE_CLOSE_DIALOG_TEXT");
    },
    confirmToggleItem(){
      this.datatable.dialogHeader = this.$t("SHARED.TOGGLE_RESULT_DIALOG_HEADER");
      ApiService.setHeader();
      ApiService.update("/Api/User", this.datatable.toggleDialogId +"/Toggle")
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
    deleteItem(id){
      this.datatable.deleteDialog = true;
      this.datatable.deleteDialogId = id;
    },
    confirmDeleteItem(){
      this.datatable.dialogHeader = this.$t("SHARED.DELETE_RESULT_DIALOG_HEADER");
      ApiService.setHeader();
      ApiService.update("/Api/User", this.datatable.deleteDialogId +"/Delete")
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
    editItem(id) {

      this.$router.push({ name: "editUser", params: { id } });
    },
    getUserTypeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/UserType/UserTypeOptions", {
          query: this.form.userTypeSearch,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.userTypeLoading = false;
          });
      });
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("MENU.USER.SECTION"), route: "/user" },
      { title: this.$t("MENU.USER.SEARCH") },
    ]);
  },
  computed: {
    title() {
      return this.$t("MENU.USER.SEARCH");
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
    "form.roleSearch": {
      handler() {
        this.getRoleFromApi().then((data) => {
          this.form.roleItems = data;
        });
      },
    },
    "form.userTypeSearch": {
      handler() {
        this.getUserTypeFromApi().then((data) => {
          this.form.userTypeItems = data;
        });
      },
    },
  },
};
</script>

<style lang="scss" scoped></style>
