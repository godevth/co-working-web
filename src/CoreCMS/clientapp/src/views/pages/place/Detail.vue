<template>
    <div>
        <b-alert :show="form.errors && form.errors.length > 0"
                 variant="light"
                 class="alert red lighten-4"
                 ref="alert">
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
                        <v-form>
                            <v-row>
                                <v-col cols="12"
                                       md="4">
                                    <v-text-field v-model="form.nameTH"
                                                  :disabled="form.loading"
                                                  :counter="50"
                                                  :label="$t('PLACE.ADD_EDIT.NAME_TH')"
                                                  :rules="form.nameTHRules"
                                                  readonly></v-text-field>
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-text-field v-model="form.nameEN"
                                                  :disabled="form.loading"
                                                  :counter="50"
                                                  :label="$t('PLACE.ADD_EDIT.NAME_EN')"
                                                  :rules="form.nameENRules"
                                                  readonly></v-text-field>
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-autocomplete v-model="form.placeType"
                                                    :disabled="form.loading"
                                                    :items="form.placeTypeItems"
                                                    :loading="form.placeTypeLoading"
                                                    :search-input.sync="form.placeTypeSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('PLACE.SEARCH.PLACETYPE')"
                                                    return-object
                                                    clearable
                                                    :readonly="true"></v-autocomplete>
                                </v-col>
                                <v-col cols="12"
                                       md="12">
                                    <v-text-field v-model="form.address"
                                                  :disabled="form.loading"
                                                  :counter="500"
                                                  :label="$t('PLACE.ADD_EDIT.ADDRESS')"
                                                  :rules="form.addressRules"
                                                  readonly></v-text-field>
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-autocomplete v-model="form.province"
                                                    :disabled="form.loading"
                                                    :items="form.provinceItems"
                                                    :loading="form.provinceLoading"
                                                    :search-input.sync="form.provinceSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('PLACE.ADD_EDIT.PROVINCE')"
                                                    return-object
                                                    clearable
                                                    readonly></v-autocomplete>
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-autocomplete v-model="form.ampher"
                                                    :disabled="form.loading"
                                                    :items="form.ampherItems"
                                                    :loading="form.ampherLoading"
                                                    :search-input.sync="form.ampherSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('PLACE.ADD_EDIT.AMPHER')"
                                                    return-object
                                                    clearable
                                                    readonly></v-autocomplete>
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-autocomplete v-model="form.tambol"
                                                    :disabled="form.loading"
                                                    :items="form.tambolItems"
                                                    :loading="form.tambolLoading"
                                                    :search-input.sync="form.tambolSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('PLACE.ADD_EDIT.TAMBOl')"
                                                    return-object
                                                    clearable
                                                    readonly></v-autocomplete>
                                    <!-- <v-autocomplete v-model="form.tambol"
                                                    :disabled="form.loading"
                                                    :items="form.tambolItems"
                                                    :loading="form.tambolLoading"
                                                    :search-input.sync="form.tambolSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('PLACE.ADD_EDIT.TAMBOl')"
                                                    return-object
                                                    clearable
                                                    required></v-autocomplete> -->
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-text-field v-model="form.zipCode"
                                                  :disabled="form.loading"
                                                  :counter="5"
                                                  :label="$t('PLACE.ADD_EDIT.ZIPCODE')"
                                                  :rules="form.zipCodeRules"
                                                  readonly></v-text-field>
                                </v-col>
                                <div class="col-xl-12">
                                    <div class="form-group">
                                        <label>ตำแหน่งที่ตั้ง:</label>
                                        <div v-if="form.mLat===null">
                                            <button type="button"
                                                    class="btn m-btn--pill btn-info btn-block col-xl-2"
                                                    @click="getMap">
                                                ระบุข้อมูลตำแหน่งที่ตั้ง
                                            </button>
                                            <!-- ไม่มีข้อมูลตำแหน่งที่ตั้ง -->
                                        </div>
                                        <div v-else>
                                            <GmapMap :center="{lat:form.mLat, lng: form.mLng}"
                                                     :zoom="15"
                                                     :clickable="false"
                                                     @click="updateCoordinates"
                                                     style="width: 500px; height: 300px">
                                                <gmap-marker :key="index"
                                                             v-for="(m, index) in form.mMarkers"
                                                             :position="m.position"
                                                             :draggable="false"
                                                             @drag="updateCoordinates"
                                                             :clickable="false"
                                                             @click="updateCoordinates"></gmap-marker>
                                            </GmapMap>
                                            <br>
                                            <div>
                                                <button type="button"
                                                        class="btn btn-sm btn-secondary"
                                                        @click="openMap()">
                                                    <i class="far fa-map"></i> Open GoogleMaps
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="Facilities" class="col-xl-12 table-responsive">
                                    <label>สิ่งอำนวยความสะดวก:</label>
                                    <table class="table table-sm table-striped table-bordered table-hover table-checkable"
                                           style="min-width:2000px;">
                                        <thead>
                                            <tr>
                                                <th class="text-center" style="width: 2%;">ลำดับ</th>
                                                <th class="text-center" style="width: 8%;">สิ่งอำนวยความสะดวก</th>
                                                <th class="text-center" style="width: 10%;">จำนวน</th>
                                                <th class="text-center" style="width: 10%;">ราคาเช่าใช้ (หน่วยบาท)</th>
                                                <th class="text-center" style="width: 5%;">การจัดการ</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-if="fsItems == null || fsItems.length == 0">
                                                <td colspan="5" class="text-muted text-center">รายการใหม่</td>
                                            </tr>
                                            <template v-for="(item, i) in fsItems">
                                                <fs-item-row ref="facItems"
                                                             :key="item"
                                                             :rowIndex="i"
                                                             :isEdit="false"
                                                             :isEditable="false"
                                                             :item="item"></fs-item-row>
                                            </template>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                                <div id="imDate" class="col-xl-12 table-responsive">
                                    <label>เวลา เปิด-ปิด:</label>
                                    <table class="table table-sm table-striped table-bordered table-hover table-checkable"
                                           style="min-width:2000px;">
                                        <thead>
                                            <tr>
                                                <th class="text-center" style="width: 2%;">ลำดับ</th>
                                                <th class="text-center" style="width: 8%;">วันที่เริ่ม</th>

                                                <th class="text-center" style="width: 10%;">เวลาที่เริ่ม</th>
                                                <th class="text-center" style="width: 10%;">เวลาที่สิ้นสุด</th>
                                                <th class="text-center" style="width: 5%;">การจัดการ</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-if="imItems == null || imItems.length == 0">
                                                <td colspan="5" class="text-muted text-center">รายการวัน เปิด-ปิด สถานที่ ใหม่</td>
                                            </tr>
                                            <template v-for="(item, i) in imItems">
                                                <im-item-row ref="imdItems"
                                                             :key="item"
                                                             :rowIndex="i"
                                                             :item="item"
                                                             :isEdit="false"
                                                             :isEditable="false"></im-item-row>
                                            </template>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                                <v-col cols="12"
                                       md="12">
                                    <v-checkbox v-model="form.inActiveStatus"
                                                :disabled="form.loading"
                                                :label="$t('PLACE.ADD_EDIT.IN_ACTIVE_STATUS')"
                                                readonly></v-checkbox>
                                </v-col>
                            </v-row>

                            <v-dialog v-model="form.dialog" persistent max-width="300">
                                <v-card>
                                    <v-card-title class="headline">
                                        {{
                      $t("PLACE.ADD_EDIT.SUCCESS_DIALOG_HEADER")
                                        }}
                                    </v-card-title>
                                    <v-card-text>
                                        {{ $t("PLACE.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
                                    </v-card-text>
                                    <v-card-actions>
                                        <v-spacer></v-spacer>
                                        <v-btn color="green darken-1"
                                               text
                                               @click="closeDialog">{{ $t("SHARED.CLOSE_BUTTON") }}</v-btn>
                                    </v-card-actions>
                                </v-card>
                            </v-dialog>

                            <v-dialog v-model="form.loading"
                                      persistent
                                      hide-overlay
                                      width="300">
                                <v-card>
                                    <v-card-title class="headline">
                                        {{ $t("SHARED.PLEASE_WAIT") }}
                                    </v-card-title>
                                    <v-card-text>
                                        <p>
                                            {{ $t("SHARED.PROCESSING") }}
                                        </p>
                                        <v-progress-linear indeterminate
                                                           color="amber lighten-3"
                                                           class="mb-3"></v-progress-linear>
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
            libraries: "places"

            //// If you want to set the version, you can do so:
            // v: '3.26',
        }
    });

    export default {
        components: {
            KTCodePortlet,
            fsItemRow,
            imItemRow
        },
        data() {
            return {
                fsItems: [],
                imItems: [],
                isChange: true,
                i: 0,
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

                    mLat: null,
                    mLng: null,
                    mMarkers: null,

                    inActiveStatus: true,
                    id: null
                }
            };
        },
        computed: {
            title() {
                return this.$t("PLACE._PLACE.DETAIL");
            },
        },
        methods: {
            submitForm() {
                // var chk = this.$refs.form.validate();
                this.form.errors = [];
                this.postDataToApi();

                // if (chk) {
                //     this.postDataToApi();
                // }
            },
            resetForm() {
                this.$refs.form.reset();
            },
            getMap: function () {
                //get current lat,lon by IP
                navigator.geolocation.getCurrentPosition(position => {
                    this.form.mLat = position.coords.latitude;
                    this.form.mLng = position.coords.longitude;
                    this.form.mMarkers = [
                        {
                            position: { lat: this.form.mLat, lng: this.form.mLng },
                            // animation: google.maps.Animation.DROP
                        }
                    ];
                });
            },
            openMap() {
                var self = this;
                window.open("https://www.google.com/maps/search/?api=1&query=" + self.eShLat + "," + self.eShLng, "_blank");
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
                    selectedFacilityCodeData: null
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
                    selectedEndDayCodeData: null
                });
            },
            onRemoveImItemClick(index) {
                var self = this;
                self.imItems.splice(index, 1);
            },
            updateCoordinates(location) {
                this.form.mLat = location.latLng.lat();
                this.form.mLng = location.latLng.lng();
                this.form.mMarkers = [{ position: { lat: this.form.mLat, lng: this.form.mLng } }];
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
                        ProvinceId: this.form.province
                            ? this.form.province.value
                            : null
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
                        AmpherId: this.form.ampher
                            ? this.form.ampher.value
                            : null
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
                    })
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {
                            this.form.placeTypeLoading = false;
                        });
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
                            this.form.zipCode = data.data.zipcode;
                            this.form.mLat = parseFloat(data.data.latitude);
                            this.form.mLng = parseFloat(data.data.longitude);
                            this.form.inActiveStatus = !data.data.inActiveStatus;
                            this.form.mMarkers = [
                                { position: { lat: this.form.mLat, lng: this.form.mLng } }
                            ];
                            if (data.data.serviceItems.length > 0) {
                                for (let i = 0; i < data.data.serviceItems.length; i++) {
                                    var fitem = data.data.serviceItems[i];
                                    var fac = {
                                        value: fitem.facilityIds
                                    }
                                    this.fsItems.push({
                                        fsItemsId: fitem.facilityServicesId,
                                        facility: fac,
                                        qty: fitem.qty,
                                        price: fitem.price
                                    });
                                    setTimeout(function () {
                                        self.$refs.facItems[i].facilityItem = {
                                            value: data.data.serviceItems[i].facilityIds
                                        };
                                    }, 300);
                                }
                            }
                            if (data.data.dateItems.length > 0) {
                                for (let i = 0; i < data.data.dateItems.length; i++) {
                                    var item = data.data.dateItems[i];
                                    var im = {
                                        value: item.startDay
                                    }
                                    this.imItems.push({
                                        imItemsId: item.implementationDateId,
                                        startDay: im,
                                        startTime: item.startTime,
                                        endTime: item.endTime
                                    });
                                    setTimeout(function () {
                                        self.$refs.imdItems[i].startDayPlace = {
                                            value: data.data.dateItems[i].startDay
                                        }
                                    }, 100);
                                }
                            }
                        })
                        .finally(() => {
                            this.form.loading = false;
                        });
                });
            }
        },
        async mounted() {
            this.$store.dispatch(SET_BREADCRUMB, [
                { title: this.$t("PLACE._PLACE.SECTION"), route: "/Place" },
                { title: this.$t("PLACE._PLACE.DETAIL") }
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
            }

        }
    };
</script>

<style lang="scss" scoped></style>
