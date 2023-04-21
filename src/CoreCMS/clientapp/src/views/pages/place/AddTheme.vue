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
                    :label="$t('THEME.ADD_EDIT.PLACE_NAME')"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.name"
                    :disabled="form.loading"
                    :counter="100"
                    :label="$t('THEME.ADD_EDIT.NAME')"
                    :rules="form.nameRules"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="12">
                  <v-textarea
                    v-model="form.description"
                    :disabled="form.loading"
                    :counter="2000"
                    :label="$t('THEME.ADD_EDIT.DESCRIPTION')"
                    :rules="form.descriptionRules"
                    required
                  ></v-textarea>
                </v-col>
                <v-col cols="12" md="12">
                  <v-file-input
                    accept="image/*"
                    show-size
                    v-model="form.logoLight"
                    :disabled="form.loading"
                    :label="$t('THEME.ADD_EDIT.LOGO_LIGHT')"
                    :hint="$t('THEME.ADD_EDIT.LOGO_HINT')"
                    persistent-hint
                  ></v-file-input>
                </v-col>
                <v-col cols="12" md="12">
                  <v-file-input
                    accept="image/*"
                    show-size
                    v-model="form.logoDark"
                    :disabled="form.loading"
                    :label="$t('THEME.ADD_EDIT.LOGO_DARK')"
                    :hint="$t('THEME.ADD_EDIT.LOGO_HINT')"
                    persistent-hint
                  ></v-file-input>
                </v-col>
                <v-col cols="12" md="12">
                  <v-file-input
                    accept="image/*"
                    show-size
                    v-model="form.bgLight"
                    :disabled="form.loading"
                    :label="$t('THEME.ADD_EDIT.BG_LIGHT')"
                    :hint="$t('THEME.ADD_EDIT.BG_HINT')"
                    persistent-hint
                  ></v-file-input>
                </v-col>
                <v-col cols="12" md="12">
                  <v-file-input
                    accept="image/*"
                    show-size
                    v-model="form.bgDark"
                    :disabled="form.loading"
                    :label="$t('THEME.ADD_EDIT.BG_DARK')"
                    :hint="$t('THEME.ADD_EDIT.BG_HINT')"
                    persistent-hint
                  ></v-file-input>
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
                    {{ $t("THEME.ADD_EDIT.SUCCESS_DIALOG_HEADER") }}
                  </v-card-title>
                  <v-card-text>
                    {{ $t("THEME.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
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
        name: null,
        description: null,
        bgLight: null,
        bgDark: null,
        logoLight: null,
        logoDark: null,
        pictureType: ["image/jpeg", "image/png"],
        logoType: ["image/png"],
        nameRules: [
          (v) => !!v || this.$t("THEME.ADD_EDIT.NAME_VALIDATION"),
          (v) =>
            (v && v.length <= 100) || this.$t("THEME.ADD_EDIT.NAME_COUNTER"),
        ],
        descriptionRules: [
          (v) =>
            !v ||
            (v && v.length <= 2000) ||
            this.$t("THEME.ADD_EDIT.PHONE_NUMBER_COUNTER"),
        ],
      },
    };
  },
  computed: {
    title() {
      return this.$t("THEME._THEME.ADD");
    },
  },
  methods: {
    getFileBase64(file) {
      return new Promise((resolve) => {
        let reader = new FileReader();
        reader.onload = () => {
          //console.log(file);
          setTimeout(function() {
            resolve({
              url: reader.result,
              base64: reader.result.split(",")[1],
              contentType: file.name.split(".")[1],
              size: file.size,
            });
          }, 100);
        };
        reader.readAsDataURL(file);
      });
    },
    getImageMetadata(file) {
      return new Promise((reslove) => {
        let image = new Image();
        image.onload = () => {
          //console.log(image);
          reslove({
            w: image.width,
            h: image.height,
          });
        };
        image.src = file;
      });
    },
    submitForm() {
      var chk = this.$refs.form.validate();
      this.form.errors = [];
      var vPics = new Array();
      var obj;
      //#region จะต้องเลือกทั้งคู่ light, dark
      if (
        (this.form.logoLight && !this.form.logoDark) ||
        (!this.form.logoLight && this.form.logoDark)
      ) {
        this.form.errors.push(
          "กรุณาเลือก" +
            this.$t("THEME.ADD_EDIT.LOGO_LIGHT") +
            "และ" +
            this.$t("THEME.ADD_EDIT.LOGO_DARK")
        );
      }

      if (
        (this.form.bgLight && !this.form.bgDark) ||
        (!this.form.bgLight && this.form.bgDark)
      ) {
        this.form.errors.push(
          "กรุณาเลือก" +
            this.$t("THEME.ADD_EDIT.BG_LIGHT") +
            "และ" +
            this.$t("THEME.ADD_EDIT.BG_DARK")
        );
      }
      //#endregion

      //#region Check Format
      if (this.form.logoLight) {
        if (!this.form.logoType.includes(this.form.logoLight.type)) {
          this.form.errors.push(
            "[" +
              this.$t("THEME.ADD_EDIT.LOGO_LIGHT") +
              "] กรุณาเลือกรูป Format .jpg, .jpeg, .png เท่านั้น"
          );
        } else {
          obj = {
            type: "THEME_TYPE_LOGO_LIGHT",
            file: this.form.logoLight,
          };
          vPics.push(obj);
        }
      }

      if (this.form.logoDark) {
        if (!this.form.logoType.includes(this.form.logoDark.type)) {
          this.form.errors.push(
            "[" +
              this.$t("THEME.ADD_EDIT.LOGO_DARK") +
              "] กรุณาเลือกรูป Format .jpg, .jpeg, .png เท่านั้น"
          );
        } else {
          obj = {
            type: "THEME_TYPE_LOGO_DARK",
            file: this.form.logoDark,
          };
          vPics.push(obj);
        }
      }

      if (this.form.bgLight) {
        if (!this.form.pictureType.includes(this.form.bgLight.type)) {
          this.form.errors.push(
            "[" +
              this.$t("THEME.ADD_EDIT.BG_LIGHT") +
              "] กรุณาเลือกรูป Format .jpg, .jpeg, .png เท่านั้น"
          );
        } else {
          obj = {
            type: "THEME_TYPE_BG_LIGHT",
            file: this.form.bgLight,
          };
          vPics.push(obj);
        }
      }

      if (this.form.bgDark) {
        if (!this.form.pictureType.includes(this.form.bgDark.type)) {
          this.form.errors.push(
            "[" +
              this.$t("THEME.ADD_EDIT.BG_DARK") +
              "] กรุณาเลือกรูป Format .jpg, .jpeg, .png เท่านั้น"
          );
        } else {
          obj = {
            type: "THEME_TYPE_BG_DARK",
            file: this.form.bgDark,
          };
          vPics.push(obj);
        }
      }
      //#endregion

      if (vPics.length == 0) {
        this.form.errors.push("กรุณาเลือกรูปอย่างน้อย 1 Set (โลโก้, พื้นหลัง)");
      }

      var pics = new Array();
      var promise = new Array();
      var promise2 = new Array();

      vPics.forEach((element) => {
        var p = this.getFileBase64(element.file).then((file) => {
          obj = {
            type: element.type,
            file: file,
          };
          pics.push(obj);

          var p2 = this.getImageMetadata(file.url).then((image) => {
            let mb = 2 * 1024 * 1024;
            if (file.size > mb) {
              this.form.errors.push(
                "กรุณาเลือกรูปที่มีขนาดน้อยกว่าหรือเท่ากับ 2 MB"
              );
            }
            //#region Check รูป Logo
            if (
              element.type == "THEME_TYPE_LOGO_LIGHT" ||
              element.type == "THEME_TYPE_LOGO_DARK"
            ) {
              if (image.h > image.w) {
                this.form.errors.push(
                  "กรุณาเลือกรูปที่มีขนาดของความกว้างมากกว่าความสูง (รูปแนวนอน)"
                );
              }
              if (image.h > 120) {
                this.form.errors.push("กรุณาเลือกรูปที่มีความสูงไม่เกิน 120px");
              }
              if (image.w > 450) {
                this.form.errors.push(
                  "กรุณาเลือกรูปที่มีความกว้างไม่เกิน 450px"
                );
              }
            }
            //#endregion
            //#region Check รูป BG
            if (
              element.type == "THEME_TYPE_BG_LIGHT" ||
              element.type == "THEME_TYPE_BG_DARK"
            ) {
              if (image.h < image.w) {
                this.form.errors.push(
                  "กรุณาเลือกรูปที่มีขนาดของความกว้างน้อยกว่าความสูง (รูปแนวตั้ง)"
                );
              }
            }
            //#endregion
          });
          promise2.push(p2);
        });
        promise.push(p);
      });

      if (this.form.errors.length > 0) {
        this.$vuetify.goTo(this.$refs.alert, {
          duration: 1000,
          easing: "easeInOutCubic",
          offset: -20,
        });
        chk = false;
      }

      if (chk && this.form.errors.length == 0) {
        Promise.all(promise).then(() => {
          Promise.all(promise2).then(() => {
            // console.log("pics len", pics.length);
            // console.log("pics ", pics);
            this.postDataToApi(pics);
          });
        });
      }
    },
    resetForm() {
      this.form.name = null;
      this.form.description = null;
      this.form.logoLight = null;
      this.form.logoDark = null;
      this.form.bgLight = null;
      this.form.bgDark = null;
      //this.$refs.form.reset();
    },
    postDataToApi(pics) {
      this.form.loading = true;
      this.form.errors = [];

      //#region loop pics
      var pictures = [];
      var p;
      // console.log("post pics len", pics.length);
      // console.log("post pics ", pics);

      pics.forEach((element) => {
        //console.log("element ", element);
        p = {
          Type: element.type,
          ContentType: element.file.contentType,
          Base64: element.file.base64,
        };
        pictures.push(p);
      });
      //#endregion

      //console.log("post pictures ", pictures);

      ApiService.setHeader();
      ApiService.post("/Api/PlaceTheme/Add", {
        PlaceId: this.$route.params.id,
        Name: this.form.name,
        Description: this.form.description,
        Pictures: pictures,
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
        name: "searchTheme",
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
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      {
        title: this.$t("THEME._THEME.SECTION"),
        route: "/place/searchTheme/" + this.$route.params.id,
      },
      { title: this.$t("THEME._THEME.ADD") },
    ]);

    this.getPlaceApi(this.$route.params.id);
  },
  watch: {},
};
</script>

<style lang="scss" scoped></style>
