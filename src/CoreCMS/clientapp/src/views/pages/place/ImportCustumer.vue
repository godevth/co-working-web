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
                
                <v-col clos="12" md = "12">
                    <v-btn
                    color="warning"
                    class="mr-4"
                    type="button"
                    tile
                    target="_self"
                    @click.prevent="Download"
                    >
                        <v-icon left>mdi-download</v-icon>
                        {{ $t("PRIVILEGE._PRIVILEGE.DOWNLOAD_FORMAT") }}
                    </v-btn>
                </v-col>

                <v-col cols="12" md="12">
                  <v-radio-group
                    v-model="form.typeImport"
                    :disabled="form.loading"
                    :label="$t('PRIVILEGE._PRIVILEGE.TYPE_IMPORT')"
                    required
                  >
                    <v-radio label="Import User" value="User">
                      <template v-slot:label>
                        <div> {{ $t("PRIVILEGE._PRIVILEGE.IMPORT_USER_TEXT") }} <strong class="error--text">{{ $t("PRIVILEGE._PRIVILEGE.IMPORT_USER_TEXT_DES") }}</strong></div>
                      </template>
                    </v-radio> 
                    <v-radio label="Import Invite" value="Invite">
                      <template v-slot:label>
                        <div> {{ $t("PRIVILEGE._PRIVILEGE.IMPORT_INVITE_TEXT") }} <strong class="error--text">{{ $t("PRIVILEGE._PRIVILEGE.IMPORT_INVITE_TEXT_DES") }}</strong></div>
                      </template>
                    </v-radio>
                  </v-radio-group>
                </v-col>

                <v-col cols="12" md="12">
                  <v-file-input
                    show-size
                    type="file"           
                    v-model="form.file"
                    ref="excelUpload"
                    accept=".xlsx"
                    :disabled="form.loading"
                    :label="$t('PRIVILEGE._PRIVILEGE.UPLOAD_CUSTUMER')"
                    :rules="form.fileRules"
                    :hint="$t('PRIVILEGE._PRIVILEGE.UPLOAD_CUSTUMER_HINT')"
                    persistent-hint
                    required
                    @change="handleFileUpload"
                  ></v-file-input> 

                </v-col>

                <v-col 
                  cols="12" 
                  md="12" 
                > 
                <v-subheader v-if="form.previewExcel">
                <v-icon left>mdi-table-search</v-icon>
                   {{ $t('PRIVILEGE._PRIVILEGE.TABLE_TITLE') }}
                </v-subheader>
                <v-data-table 
                  v-if="form.previewExcel"
                  :headers="excelData.header"
                  :items="excelData.results"
                  :items-per-page="5"
                  class="elevation-1"
                ></v-data-table>
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
                    {{ $t("PRIVILEGE._PRIVILEGE.IMPORT_SUCCESS") }}
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
import XLSX from "xlsx";

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
        file: null,
        previewExcel: false,
        typeImport: "User",
        fileType: ["application/vnd.ms-excel"],
        fileRules: [
          (v) => !!v || this.$t("PRIVILEGE._PRIVILEGE.FILE_VALIDATION"),
        ],
      },
      excelData: {
        header: [],
        results: null,

      },
    };
  },
  computed: {
    title() {
      return this.$t("PRIVILEGE._PRIVILEGE.IMPORT_CUSTUMER");
    },
  },
  methods: {
    Download(){
      window.open("/Files/CustomerForm");
    },
    FileUpload(){
        this.file = this.$refs.file.files[0];
    },
    handleFileUpload(e) {
      if(e == undefined){
        this.form.previewExcel = false;
      }
      const rawFile = e;
      if (!rawFile) return; 
      this.upload(rawFile);
    },
    upload(rawFile) {
      this.$refs["excelUpload"].value = null;
      if (!this.beforeUpload) {
        this.readerData(rawFile);
        return;
      }
      const before = this.beforeUpload(rawFile);
      if (before) {
        this.readerData(rawFile);
      }
    },
    readerData(rawFile) {
      this.loading = true;
      return new Promise((resolve) => {
        const reader = new FileReader();
        reader.onload = (e) => {
          const data = e.target.result;
          const workbook = XLSX.read(data, { type: "array" });
          const firstSheetName = workbook.SheetNames[0];
          const worksheet = workbook.Sheets[firstSheetName];
          const header = this.getHeaderRow(worksheet);
          const results = XLSX.utils.sheet_to_json(worksheet);
          this.generateData({ header, results });
          this.loading = false;
          resolve();
        };
        reader.readAsArrayBuffer(rawFile);
      });
    },
    getHeaderRow(sheet) {
      const headers = [];
      const range = XLSX.utils.decode_range(sheet["!ref"]);
      let C;
      const R = range.s.r;
      for (C = range.s.c; C <= range.e.c; ++C) {
        const cell = sheet[XLSX.utils.encode_cell({ c: C, r: R })];
        let hdr = C;
        if (cell && cell.t) hdr = XLSX.utils.format_cell(cell);
        headers.push(hdr);
      }
      return headers;
    },
    generateData({ header, results }) {
      this.excelData.header = [];
      this.excelData.results = [];

      for (let i = 0; i < 1000; i++) {
        if(results[i] == null){
          break;
        }
        this.excelData.results.push(results[i])
      }
      
      for (let i = 0; i < header.length-1; i++) {
        this.excelData.header.push({
          text: header[i],
          value: header[i],
        });
      }
      this.form.previewExcel = true;
    },
    submitForm() {
      var chk = this.$refs.form.validate();

      if (this.form.file && !this.form.fileType.includes(this.form.file.type)) {
        this.form.errors.push("กรุณาเลือกไฟล์ Format .xlsx เท่านั่น ");
      } 
      if (chk) {
        
        if(this.form.typeImport == "User")
        {
          this.postDataToApiUser();
        }
        else if(this.form.typeImport == "Invite")
        {
          this.postDataToApiInvite();
        }
      }
    },
    resetForm() {
      this.form.users = [];
      this.form.file = null;
      this.excelData.header = [];
      this.excelData.results = [];
      this.form.previewExcel = false;
      //this.$refs.form.reset();
    },
    postDataToApiUser() {
      this.form.loading = true;
      this.form.errors = [];


      ApiService.setHeader();
      ApiService.post("/Api/Privilege/ImportUser", {
        PlaceId: this.$route.params.id,
        ListUser: this.excelData.results,
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

    postDataToApiInvite() {
      this.form.loading = true;
      this.form.errors = [];


      ApiService.setHeader();
      ApiService.post("/Api/Privilege/ImportInvite", {
        PlaceId: this.$route.params.id,
        ListUser: this.excelData.results,
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

  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      {
        title: this.$t("PRIVILEGE._PRIVILEGE.SECTION"),
        route: "/place/searchPrivilege/" + this.$route.params.id,
      },
      { title: this.$t("PRIVILEGE._PRIVILEGE.IMPORT_CUSTUMER") },
    ]);

    this.getPlaceApi(this.$route.params.id);
  },
  watch: {
    "form.usersSearch": {
      handler() {

      },
    },
  },
};
</script>

<style lang="scss" scoped></style>
