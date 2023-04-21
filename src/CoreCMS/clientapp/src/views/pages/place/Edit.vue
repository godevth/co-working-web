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
        <KTCodePortlet v-bind:title="title">
          <template v-slot:body>
            <v-form
              ref="form"
              @submit.prevent="submitForm"
              v-model="form.valid"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" md="4">
                  <v-text-field
                    v-model="form.nameTH"
                    :disabled="form.loading"
                    :counter="50"
                    :label="$t('PLACE.ADD_EDIT.NAME_TH')"
                    :rules="form.nameRulesTH"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="4">
                  <v-text-field
                    v-model="form.nameEN"
                    :disabled="form.loading"
                    :counter="50"
                    :label="$t('PLACE.ADD_EDIT.NAME_EN')"
                    :rules="form.nameRulesEN"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="4">
                  <v-autocomplete
                    v-model="form.placeType"
                    :disabled="form.loading"
                    :items="form.placeTypeItems"
                    :loading="form.placeTypeLoading"
                    :search-input.sync="form.placeTypeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.SEARCH.PLACETYPE')"
                    :rules="form.placeTypeRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col cols="12" md="4">
                  <v-autocomplete
                    v-model="form.company"
                    :disabled="form.loading"
                    :items="form.companyItems"
                    :loading="form.companyLoading"
                    :search-input.sync="form.companySearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.ADD_EDIT.COMPANY')"
                    return-object
                    :rules="form.companyRules"
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col cols="12" md="4">
                  <v-autocomplete
                    v-model="form.paymentMethods"
                    :items="form.paymentMethodsItems"
                    :loading="form.paymentMethodsLoading"
                    :search-input.sync="form.paymentMethodsSearch"
                    :rules="form.paymentMethodsRules"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.ADD_EDIT.PAYMENT_METHOD')"
                    return-object
                    multiple
                  >
                    <template v-slot:selection="data">
                      <v-chip
                        v-bind="data.attrs"
                        :input-value="data.selected"
                        close
                        @click="data.select"
                        @click:close="remove(data.item)"
                      >
                        {{ data.item.text }}
                      </v-chip>
                    </template>
                  </v-autocomplete>
                </v-col>
                <v-col cols="12" md="4">
                  <v-autocomplete
                    v-model="form.seeing"
                    :disabled="form.loading"
                    :items="form.seeingItems"
                    :loading="form.seeingLoading"
                    :search-input.sync="form.seeingSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.ADD_EDIT.SEEING')"
                    :rules="form.seeingRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col
                  cols="12"
                  md="12"
                  v-show="
                    form.seeing
                      ? form.seeing.value === 'SEEING_TYPE_PRIVATE' ||
                        form.seeing.value === 'SEEING_TYPE_PRIVATE_ONLY'
                      : false
                  "
                >
                  <v-combobox
                    v-model="form.domains"
                    :label="$t('PLACE.ADD_EDIT.DOMAIN')"
                    :rules="form.domainsRules"
                    :disabled="form.loading"
                    small-chips
                    return-object
                    clearable
                    required
                    multiple
                  >
                  </v-combobox>
                </v-col>
                <v-col cols="12" md="12">
                  <v-text-field
                    v-model="form.address"
                    :disabled="form.loading"
                    :counter="500"
                    :label="$t('PLACE.ADD_EDIT.ADDRESS')"
                    :rules="form.addressRules"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="4">
                  <v-autocomplete
                    v-model="form.province"
                    :disabled="form.loading"
                    :items="form.provinceItems"
                    :loading="form.provinceLoading"
                    :search-input.sync="form.provinceSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.ADD_EDIT.PROVINCE')"
                    @change="onChangeProvince()"
                    :rules="form.provinceRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col cols="12" md="4">
                  <v-autocomplete
                    v-model="form.ampher"
                    :disabled="form.loading"
                    :items="form.ampherItems"
                    :loading="form.ampherLoading"
                    :search-input.sync="form.ampherSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.ADD_EDIT.AMPHER')"
                    @change="onChangeAmpher()"
                    :rules="form.ampherRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col cols="12" md="4">
                  <v-autocomplete
                    v-model="form.tambol"
                    :disabled="form.loading"
                    :items="form.tambolItems"
                    :loading="form.tambolLoading"
                    :search-input.sync="form.tambolSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('PLACE.ADD_EDIT.TAMBOl')"
                    :rules="form.tambolRules"
                    return-object
                    clearable
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col cols="12" md="4">
                  <v-text-field
                    v-model="form.zipCode"
                    :disabled="form.loading"
                    :counter="5"
                    :label="$t('PLACE.ADD_EDIT.ZIPCODE')"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="12">
                  <v-text-field
                    v-model="form.nearBy"
                    :disabled="form.loading"
                    :counter="50"
                    :label="$t('PLACE.ADD_EDIT.NEAR_BY')"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="12">
                  <v-textarea
                    v-model="form.details"
                    :disabled="form.loading"
                    :counter="500"
                    :label="$t('PLACE.ADD_EDIT.DEAILS')"
                    required
                  ></v-textarea>
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field
                    v-model="form.subMerchantId"
                    disabled
                    :label="$t('PLACE.ADD_EDIT.SUBMERCHANT')"
                  ></v-text-field>
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field
                    v-model="form.GP"
                    disabled
                    :label="$t('PLACE.ADD_EDIT.GP')"
                  ></v-text-field>
                </v-col>

                <v-col cols="12" md="12">
                  <v-file-input
                    accept="image/*,.pdf,.xlsx,.doc,.docx,.xls"
                    show-size
                    v-model="form.file"
                    :disabled="form.loading"
                    :label="$t('PLACE.ADD_EDIT.UPLOAD_FILE')"
                    persistent-hint
                    multiple
                    @change="addFile()"
                  ></v-file-input>
                </v-col>

                <v-col clos="12" md="12">
                  <label v-if="downloadLink == null" style="color: red;"
                    >*ไม่พบไฟล์แนบ</label
                  >
                  <v-btn
                    v-if="downloadLink != null"
                    color="info"
                    class="mr-4"
                    type="button"
                    tile
                    target="_self"
                    @click.prevent="Download"
                  >
                    <v-icon left>mdi-download</v-icon>
                    {{ $t("PLACE.ADD_EDIT.DOWNLOAD_FILE") }}
                  </v-btn>
                </v-col>
                <div class="col-xl-12">
                  <div class="form-group">
                    <label>ตำแหน่งที่ตั้ง:</label>
                    <div v-if="form.mLat === null">
                      <button
                        type="button"
                        class="btn m-btn--pill btn-info btn-block col-xl-2"
                        @click="getMap"
                      >
                        ระบุข้อมูลตำแหน่งที่ตั้ง
                      </button>
                      <!-- ไม่มีข้อมูลตำแหน่งที่ตั้ง -->
                    </div>
                    <div v-else>
                      <GmapMap
                        :center="{ lat: form.mLat, lng: form.mLng }"
                        :zoom="15"
                        :clickable="true"
                        @click="updateCoordinates"
                        style="width: 500px; height: 300px"
                      >
                        <gmap-marker
                          :key="index"
                          v-for="(m, index) in form.mMarkers"
                          :position="m.position"
                          :draggable="true"
                          @drag="updateCoordinates"
                          :clickable="true"
                          @click="updateCoordinates"
                        ></gmap-marker>
                      </GmapMap>
                      <br />
                      <div>
                        <button
                          type="button"
                          class="btn btn-sm btn-secondary"
                          @click="openMap()"
                        >
                          <i class="far fa-map"></i> Open GoogleMaps
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
                <div id="Facilities" class="col-xl-12 table-responsive">
                  <table
                    class="table table-sm table-striped table-bordered table-hover table-checkable"
                    style="min-width:2000px;"
                  >
                    <thead>
                      <tr>
                        <th class="text-center" style="width: 2%;">ลำดับ</th>
                        <th class="text-center" style="width: 8%;">
                          สิ่งอำนวยความสะดวก
                        </th>
                        <th class="text-center" style="width: 10%;">จำนวน</th>
                        <th class="text-center" style="width: 10%;">
                          ราคาเช่าใช้ (หน่วยบาท)
                        </th>
                        <th class="text-center" style="width: 5%;">
                          การจัดการ
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-if="fsItems == null || fsItems.length == 0">
                        <td colspan="5" class="text-muted text-center">
                          คลิก + เพื่อเพิ่มรายการใหม่
                        </td>
                      </tr>
                      <template v-for="(item, i) in fsItems">
                        <fs-item-row
                          ref="facItems"
                          :key="item"
                          :rowIndex="i"
                          :item="item"
                          :isEdit="true"
                          @removeItem="onRemoveItemClick"
                        ></fs-item-row>
                      </template>
                    </tbody>
                    <tfoot>
                      <tr>
                        <td colspan="11">
                          <button
                            type="button"
                            class="btn btn-sm btn-brand"
                            @click.prevent="onAddItemClick"
                          >
                            <i class="fa fa-plus"></i> เพิ่มรายการใหม่
                          </button>
                        </td>
                      </tr>
                    </tfoot>
                  </table>
                </div>
                <div id="imDate" class="col-xl-12 table-responsive">
                  <table
                    class="table table-sm table-striped table-bordered table-hover table-checkable"
                    style="min-width:2000px;"
                  >
                    <thead>
                      <tr>
                        <th class="text-center" style="width: 2%;">ลำดับ</th>
                        <th class="text-center" style="width: 8%;">
                          วันที่เริ่ม
                        </th>

                        <th class="text-center" style="width: 10%;">
                          เวลาที่เริ่ม
                        </th>
                        <th class="text-center" style="width: 10%;">
                          เวลาที่สิ้นสุด
                        </th>
                        <th class="text-center" style="width: 5%;">
                          การจัดการ
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-if="imItems == null || imItems.length == 0">
                        <td colspan="5" class="text-muted text-center">
                          คลิก + เพื่อเพิ่มรายการวัน เปิด-ปิด สถานที่ ใหม่
                        </td>
                      </tr>
                      <template v-for="(item, i) in imItems">
                        <im-item-row
                          ref="imdItems"
                          :key="item"
                          :rowIndex="i"
                          :item="item"
                          :isEdit="true"
                          @removeItem="onRemoveImItemClick"
                        ></im-item-row>
                      </template>
                    </tbody>
                    <tfoot>
                      <tr>
                        <td colspan="6">
                          <button
                            type="button"
                            class="btn btn-sm btn-brand"
                            @click.prevent="onAddImItemClick"
                          >
                            <i class="fa fa-plus"></i> เพิ่มรายการใหม่
                          </button>
                        </td>
                      </tr>
                    </tfoot>
                  </table>
                </div>
                <v-col cols="12" md="12">
                  <v-checkbox
                    v-model="form.inActiveStatus"
                    :disabled="form.loading"
                    :label="$t('PLACE.ADD_EDIT.IN_ACTIVE_STATUS')"
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
                    {{ $t("PLACE.ADD_EDIT.SUCCESS_DIALOG_HEADER") }}
                  </v-card-title>
                  <v-card-text>
                    {{ $t("PLACE.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
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
        </KTCodePortlet>
      </div>
    </div>
  </div>
</template>

<script>
import ApiService from "@/common/api.service";
import { SET_BREADCRUMB } from "@/store/breadcrumbs.module";
import KTCodePortlet from "@/views/partials/content/Portlet.vue";
import * as VueGoogleMaps from "vue2-google-maps";
import Vue from "vue";
import fsItemRow from "./FacilityServiceRow";
import imItemRow from "./ImplementationDateRow";
Vue.use(VueGoogleMaps, {
  load: {
    key: "AIzaSyCeGao-PkONmiyVksl46W_OUHt854bcrqE",
    libraries: "places",

    //// If you want to set the version, you can do so:
    // v: '3.26',
  },
});

export default {
  components: {
    KTCodePortlet,
    fsItemRow,
    imItemRow,
  },
  data() {
    return {
      fsItems: [],
      imItems: [],
      isChange: true,
      i: 0,
      file: [],
      downloadLink: null,
      form: {
        valid: true,
        dialog: false,
        loading: false,
        errors: [],
        nameTH: "",
        nameEN: "",
        address: "",
        tambol: null,
        tambolSearch: "",
        tambolLoading: false,
        tambolItems: [],
        ampher: null,
        ampherSearch: "",
        ampherLoading: false,
        ampherItems: [],
        province: null,
        provinceSearch: "",
        provinceLoading: false,
        provinceItems: [],
        zipCode: "",
        placeType: null,
        placeTypeSearch: "",
        placeTypeLoading: false,
        placeTypeItems: [],
        company: null,
        companySearch: "",
        companyLoading: false,
        companyItems: [],
        nearBy: null,
        details: null,
        seeing: null,
        seeingSearch: "",
        seeingLoading: false,
        seeingItems: [],
        domains: null,
        paymentMethods: [],
        paymentMethodsSearch: "",
        paymentMethodsLoading: false,
        paymentMethodsItems: [],
        file: null,

        mLat: null,
        mLng: null,
        mMarkers: null,

        inActiveStatus: true,
        id: null,
        isAll: true,

        GP:null,
        subMerchantId :null,
        nameRulesTH: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.NAME_TH_VALIDATION"),
          (v) =>
            (v && v.length <= 50) || this.$t("PLACE.ADD_EDIT.NAME_COUNTER"),
        ],
        nameRulesEN: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.NAME_EN_VALIDATION"),
          (v) =>
            (v && v.length <= 50) || this.$t("PLACE.ADD_EDIT.NAME_COUNTER"),
        ],
        placeTypeRules: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.PLACE_TYPE_VALIDATION"),
        ],
        companyRules: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.COMPANY_VALIDATION"),
        ],
        provinceRules: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.PROVINCE_VALIDATION"),
        ],
        ampherRules: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.AMPHER_VALIDATION"),
        ],
        tambolRules: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.TAMBOL_VALIDATION"),
        ],
        addressRules: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.ADDRESS_VALIDATION"),
        ],
        seeingRules: [
          (v) => !!v || this.$t("PLACE.ADD_EDIT.SEEING_VALIDATION"),
        ],
        paymentMethodsRules: [
          (v) =>
            !!(v && v.length) ||
            this.$t("PLACE.ADD_EDIT.PAYMENT_METHOD_VALIDATION"),
        ],
        domainsRules: [
          (v) =>
            this.form.seeing == null ||
            (this.form.seeing.value != "SEEING_TYPE_PRIVATE" &&
              this.form.seeing.value != "SEEING_TYPE_PRIVATE_ONLY") ||
            (this.form.seeing.value == "SEEING_TYPE_PRIVATE" && !!v) ||
            (this.form.seeing.value == "SEEING_TYPE_PRIVATE_ONLY" && !!v) ||
            this.$t("PLACE.ADD_EDIT.DOMAIN_VALIDATION"),
          (v) =>
            !v || /@.+\..+/.test(v) || this.$t("PLACE.ADD_EDIT.DOMAIN_FORMAT"),
        ],
      },
    };
  },
  computed: {
    title() {
      return this.$t("PLACE._PLACE.EDIT");
    },
  },
  methods: {
    submitForm() {
      var chk = this.$refs.form.validate();
      if (chk) {
        this.postDataToApi();
      }
      // if (chk) {
      //     this.postDataToApi();
      // }
    },
    resetForm() {
      this.$refs.form.reset();
    },
    getMap: function() {
      //get current lat,lon by IP
      navigator.geolocation.getCurrentPosition((position) => {
        this.form.mLat = position.coords.latitude;
        this.form.mLng = position.coords.longitude;
        this.form.mMarkers = [
          {
            position: { lat: this.form.mLat, lng: this.form.mLng },
            // animation: google.maps.Animation.DROP
          },
        ];
      });
    },
    openMap() {
      var self = this;
      window.open(
        "https://www.google.com/maps/search/?api=1&query=" +
          self.eShLat +
          "," +
          self.eShLng,
        "_blank"
      );
    },
    onAddItemClick() {
      // if (
      //   !$(this.$refs.form)
      //     .validate()
      //     .form()
      // ) {
      //   return false;
      // }

      this.fsItems.push({
        fsItemsId: null,
        facility: null,
        qty: null,
        price: null,
        selectedFacilityCodeData: null,
      });
    },
    onRemoveItemClick(index) {
      var self = this;
      self.fsItems.splice(index, 1);
    },
    onAddImItemClick() {
      // if (
      //   !$(this.$refs.form)
      //     .validate()
      //     .form()
      // ) {
      //   return false;
      // }

      this.imItems.push({
        imItemsId: null,
        startDay: null,
        // endDay: null,
        startTime: "00:00",
        endTime: "00:00",
        selectedStartDayCodeData: null,
        selectedEndDayCodeData: null,
      });
    },
    onRemoveImItemClick(index) {
      var self = this;
      self.imItems.splice(index, 1);
    },
    updateCoordinates(location) {
      this.form.mLat = location.latLng.lat();
      this.form.mLng = location.latLng.lng();
      this.form.mMarkers = [
        { position: { lat: this.form.mLat, lng: this.form.mLng } },
      ];
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
            this.form.provinceLoading = false;
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
            this.form.ampherLoading = false;
          });
      });
    },
    getTambolFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/MasterLocation/Select2Tumbol", {
          TumbolId: null,
          AmpherId: this.form.ampher ? this.form.ampher.value : null,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.tambolLoading = false;
          });
      });
    },
    getPlaceTypeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/PlaceType/Select2PlaceType", {
          search: this.form.placeTypeSearch,
          isAll: this.form.isAll,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.placeTypeLoading = false;
          });
      });
    },
    getCompanyFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Company/Select2Company", {
          search: this.form.companySearch,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.companyLoading = false;
          });
      });
    },
    onChangeProvince() {
      this.form.ampher = null;
      this.form.tambol = null;
      this.getAmpherFromApi().then((data) => {
        this.form.ampherItems = data;
      });
    },
    onChangeAmpher() {
      this.form.tambol = null;
      this.getTambolFromApi().then((data) => {
        this.form.tambolItems = data;
      });
    },
    onChangeTambol() {
      this.form.zipCode = this.form.tambol.data;
    },
    postDataToApi() {
      var self = this;
      this.form.loading = true;
      this.form.errors = [];

      var fItems = [];
      self.fsItems.forEach((element) => {
        fItems.push({
          FacilityServicesId: element.fsItemsId,
          FacilityId: element.facility.value,
          Qty: element.qty,
          Price: element.price,
        });
      });
      var imItem = [];
      self.imItems.forEach((element) => {
        imItem.push({
          ImplementationDateId: element.imItemsId,
          StartDay: element.startDay.value,
          StartTime: element.startTime,
          EndTime: element.endTime,
        });
      });

      var ftems = [];
      self.file.forEach((element) => {
        console.log(element);
        ftems.push({
          Base64s: element.Base64s,
          ContentTypes: element.ContentTypes,
          FileName: element.FileName,
        });
      });
      //console.log(self.form.paymentMethods);
      var pItems = [];
      self.form.paymentMethods.forEach((element) => {
        //console.log(element);
        pItems.push(element.value);
      });
      ApiService.setHeader();
      ApiService.put("/Api/Place/Edit", {
        PlaceId: this.form.id,
        Name_TH: self.form.nameTH,
        Name_EN: self.form.nameEN,
        Address: self.form.address,
        TambolId: self.form.tambol ? self.form.tambol.value : "",
        AmpherId: self.form.ampher ? self.form.ampher.value : "",
        ProvinceId: self.form.province ? self.form.province.value : "",
        PlaceTypeId: self.form.placeType ? self.form.placeType.value : "",
        Zipcode: self.form.zipCode,
        Latitude: self.form.mLat,
        Longitude: self.form.mLng,
        InActiveStatus: !self.form.inActiveStatus,
        ServiceItems: fItems,
        DateItems: imItem,
        NearBy: self.form.nearBy,
        Details: self.form.details,
        CompanyId: self.form.company ? self.form.company.value : "",
        Seeing: self.form.seeing ? self.form.seeing.value : "",
        PaymentMethods: pItems,
        Domains: self.form.domains,
        Pictures: ftems,
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
      this.$router.push({ name: "searchPlace" });
    },
    getDataFromApi(id) {
      let self = this;
      this.form.loading = true;
      return new Promise(() => {
        ApiService.setHeader();
        ApiService.get("/Api/Place", id)
          .then(({ data }) => {
            this.form.id = data.data.placeId;
            this.form.nameTH = data.data.name_TH;
            this.form.nameEN = data.data.name_EN;
            this.form.address = data.data.address;
            this.form.tambol = { value: data.data.tambolId };
            this.form.ampher = { value: data.data.ampherId };
            this.form.province = { value: data.data.provinceId };
            this.form.placeType = { value: data.data.placeTypeId };
            this.form.company = { value: data.data.companyId };
            this.form.zipCode = data.data.zipcode;
            this.form.nearBy = data.data.nearBy;
            this.form.details = data.data.details;
            this.form.mLat = parseFloat(data.data.latitude);
            this.form.mLng = parseFloat(data.data.longitude);
            this.form.inActiveStatus = !data.data.inActiveStatus;
            this.form.seeing = { value: data.data.seeing };
            this.form.subMerchantId = data.data.subMerchantId,
            this.form.GP = data.data.gp
            if(data.data.pictures.length > 0)
            {
              this.downloadLink = data.data.pictures[0].downloadFileByteUrl;
            }

            this.form.mMarkers = [
              { position: { lat: this.form.mLat, lng: this.form.mLng } },
            ];
            if (data.data.serviceItems.length > 0) {
              for (let i = 0; i < data.data.serviceItems.length; i++) {
                var fitem = data.data.serviceItems[i];
                var fac = {
                  value: fitem.facilityIds,
                };
                this.fsItems.push({
                  fsItemsId: fitem.facilityServicesId,
                  facility: fac,
                  qty: fitem.qty,
                  price: fitem.price,
                });
                setTimeout(function() {
                  self.$refs.facItems[i].facilityItem = {
                    value: data.data.serviceItems[i].facilityIds,
                  };
                }, 300);
              }
            }
            if (data.data.dateItems.length > 0) {
              for (let i = 0; i < data.data.dateItems.length; i++) {
                var item = data.data.dateItems[i];
                var im = {
                  value: item.startDay,
                };
                this.imItems.push({
                  imItemsId: item.implementationDateId,
                  startDay: im,
                  startTime: item.startTime,
                  endTime: item.endTime,
                });
                setTimeout(function() {
                  self.$refs.imdItems[i].startDayPlace = {
                    value: data.data.dateItems[i].startDay,
                  };
                }, 100);
              }
            }
            if (data.data.domains.length > 0) {
              this.form.domains = data.data.domains;
            }
            this.getPaymentMethodsFromApi().then((item) => {
              this.form.paymentMethodsItems = item;
              data.data.paymentMethodItems.forEach((element) => {
                var code = item.filter((o) => o.value == element.value);
                this.form.paymentMethods.push(code[0]);
              });
            });
            //console.log(this.form.paymentMethods);
          })
          .finally(() => {
            this.form.loading = false;
          });
      });
    },
    getSeeingFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Variables/Options", {
          Search: "SEEING_TYPE",
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.seeingLoading = false;
          });
      });
    },
    getPaymentMethodsFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Variables/Options", {
          Search: "PAYMENT_METHOD",
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.paymentMethodsLoading = false;
          });
      });
    },
    remove(item) {
      const index = this.form.paymentMethods.indexOf(item);
      if (index >= 0) this.form.paymentMethods.splice(index, 1);
    },
    addFile() {
      this.form.file.forEach((element) => {
        this.getFileBase64(element).then((file) => {
          this.file.push({
            Base64s: file.base64,
            ContentTypes: file.contentType,
            FileName: element.name,
          });
          console.log(this.file);
        });
      });
    },
    getFileBase64(file) {
      return new Promise((resolve) => {
        let reader = new FileReader();
        reader.onload = () => {
          //console.log(file);
          resolve({
            url: reader.result,
            base64: reader.result.split(",")[1],
            contentType: file.name.split(".")[1],
            size: file.size,
          });
        };
        reader.readAsDataURL(file);
      });
    },
    Download() {
      //var uri = this.downloadLink;
      window.open(this.downloadLink);
    },
  },
  async mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("PLACE._PLACE.SECTION"), route: "/Place" },
      { title: this.$t("PLACE._PLACE.EDIT") },
    ]);

    this.getDataFromApi(this.$route.params.id);
  },
  watch: {
    "form.placeTypeSearch": {
      handler() {
        this.getPlaceTypeFromApi().then((data) => {
          this.form.placeTypeItems = data;
        });
      },
    },
    "form.companySearch": {
      handler() {
        this.getCompanyFromApi().then((data) => {
          this.form.companyItems = data;
        });
      },
    },
    "form.tambolSearch": {
      handler() {
        this.getTambolFromApi().then((data) => {
          this.form.tambolItems = data;
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
    "form.provinceSearch": {
      handler() {
        this.getProvinceFromApi().then((data) => {
          this.form.provinceItems = data;
        });
      },
    },
    "form.seeingSearch": {
      handler() {
        this.getSeeingFromApi().then((data) => {
          this.form.seeingItems = data;
        });
      },
    },
    "form.paymentMethodsSearch": {
      handler() {
        this.getPaymentMethodsFromApi().then((data) => {
          this.form.paymentMethodsItems = data;
        });
      },
    },
  },
};
</script>

<style lang="scss" scoped></style>
