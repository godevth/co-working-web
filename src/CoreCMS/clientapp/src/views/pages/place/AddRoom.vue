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
                        <v-form ref="form"
                                @submit.prevent="submitForm"
                                v-model="form.valid"
                                lazy-validation>
                            <v-row>
                                <v-col cols="12"
                                       md="4">
                                    <v-text-field v-model="form.nameTH"
                                                  :disabled="form.loading"
                                                  :counter="50"
                                                  :label="$t('ROOM.ADD_EDIT.NAME_TH')"
                                                  :rules="form.nameRulesTH"
                                                  required></v-text-field>
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-text-field v-model="form.nameEN"
                                                  :disabled="form.loading"
                                                  :counter="50"
                                                  :label="$t('ROOM.ADD_EDIT.NAME_EN')"
                                                  :rules="form.nameRulesEN"
                                                  required></v-text-field>
                                </v-col>
                                <v-col cols="12"
                                       md="4">
                                    <v-autocomplete v-model="form.roomType"
                                                    :disabled="form.loading"
                                                    :items="form.roomTypeItems"
                                                    :loading="form.roomTypeLoading"
                                                    :search-input.sync="form.roomTypeSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('PLACE.SEARCH.ROOMTYPE')"
                                                    :rules="form.roomTypeRules"
                                                    return-object
                                                    clearable></v-autocomplete>
                                </v-col>

                                <v-col cols="12"
                                       md="12">
                                    <v-textarea v-model="form.detailsTH"
                                                :disabled="form.loading"
                                                :counter="500"
                                                :label="$t('ROOM.ADD_EDIT.DEAILS_TH')"
                                                :rules="form.nameTHRules"
                                                required></v-textarea>
                                </v-col>

                                <v-col cols="12"
                                       md="12">
                                    <v-textarea v-model="form.detailsEN"
                                                :disabled="form.loading"
                                                :counter="500"
                                                :label="$t('ROOM.ADD_EDIT.DEAILS_EN')"
                                                :rules="form.nameTHRules"
                                                required></v-textarea>
                                </v-col>
                                <!-- <v-col cols="12"
                                       md="4">
                                    <v-text-field v-model="form.qty"
                                                  :disabled="loadingItem"
                                                  :label="$t('ROOM.ADD_EDIT.QTY')"
                                                  type="number"
                                                  min="0"></v-text-field>
                                </v-col> -->
                                <v-col cols="12"
                                       md="4">
                                    <vuetify-money v-model="form.capacity"
                                                   v-bind:label="$t('ROOM.ADD_EDIT.CAPACITY')"
                                                   v-bind:readonly="false"
                                                   v-bind:disabled="false"
                                                   v-bind:outlined="true"
                                                   v-bind:clearable="true"
                                                   v-bind:valueWhenIsEmpty="valueWhenIsEmptyCapacity"
                                                   v-bind:options="optionsCapacity"
                                                   v-bind:properties="propertiesCapacity"
                                                   :valueOptions="valueOptionsCapacity"
                                                   v-on:CustomMinEvent="form.capacity = $event"
                                                   v-on:CustomMaxEvent="form.capacity = $event" />
                                    <!-- <v-text-field v-model="form.capacity"
                                                  :disabled="loadingItem"
                                                  :label="$t('ROOM.ADD_EDIT.CAPACITY')"
                                                  :rules="form.qtyRules"
                                                  type="number"
                                                  min="0"></v-text-field> -->
                                </v-col>
                                <!-- <v-col cols="12"
                                       md="4">
                                    <v-text-field v-model="form.price"
                                                  :disabled="loadingItem"
                                                  :label="$t('ROOM.ADD_EDIT.PRICE')"
                                                  type="number"
                                                  min="0"></v-text-field>
                                </v-col> -->
                                <div id="Price" class="col-xl-12 table-responsive">
                                    <table class="table table-sm table-striped table-bordered table-hover table-checkable"
                                           style="min-width:2000px;">
                                        <thead>
                                            <tr>
                                                <th class="text-center" style="width: 2%;">ลำดับ</th>
                                                <th class="text-center" style="width: 8%;">ประเภทเวลา</th>
                                                <th class="text-center" style="width: 10%;">จำนวน</th>
                                                <th class="text-center" style="width: 10%;">ราคา</th>
                                                <th class="text-center" style="width: 5%;">การจัดการ</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-if="priceItems == null || priceItems.length == 0">
                                                <td colspan="5" class="text-muted text-center">คลิก + เพื่อเพิ่มรายการใหม่</td>
                                            </tr>
                                            <template v-for="(item, i) in priceItems">
                                                <p-item-row ref="priItems"
                                                            :key="item"
                                                            :rowIndex="i"
                                                            :item="item"
                                                            :isEdit="true"
                                                            @removeItem="onRemovePriceItemClick"></p-item-row>
                                            </template>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="11">
                                                    <button type="button"
                                                            class="btn btn-sm btn-brand"
                                                            @click.prevent="onAddPriceItemClick">
                                                        <i class="fa fa-plus"></i> เพิ่มรายการใหม่
                                                    </button>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                                <div id="Facilities" class="col-xl-12 table-responsive">
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
                                                <td colspan="5" class="text-muted text-center">คลิก + เพื่อเพิ่มรายการใหม่</td>
                                            </tr>
                                            <template v-for="(item, i) in fsItems">
                                                <fs-item-row ref="facItems"
                                                             :key="item"
                                                             :rowIndex="i"
                                                             :item="item"
                                                             :isEdit="true"
                                                             @removeItem="onRemoveItemClick"></fs-item-row>
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
                                <v-col cols="12" md="12">
                                    <v-file-input accept="image/*"
                                                  show-size
                                                  v-model="form.picture"
                                                  :disabled="form.loading"
                                                  :label="$t('ROOM.ADD_EDIT.IMAGE')"
                                                  :rules="form.pictureRules"
                                                  :hint="$t('ROOM.ADD_EDIT.PICTURE_HINT')"
                                                  persistent-hint
                                                  multiple
                                                  @change="addFile()"
                                                  truncate-length="50"></v-file-input>
                                </v-col>
                                <v-col cols="12"
                                       md="12">
                                    <v-checkbox v-model="form.inActiveStatus"
                                                :disabled="form.loading"
                                                :label="$t('ROOM.ADD_EDIT.IN_ACTIVE_STATUS')"
                                                required></v-checkbox>
                                </v-col>
                            </v-row>
                            <v-row>
                                <div class="col-12">
                                    <v-btn :disabled="!form.valid || form.loading"
                                           color="success"
                                           class="mr-4"
                                           tile
                                           type="submit">
                                        <v-icon left>la-save</v-icon>
                                        {{ $t("SHARED.SAVE_BUTTON") }}
                                    </v-btn>
                                    <v-btn :disabled="form.loading"
                                           color="default"
                                           class="mr-4"
                                           type="reset"
                                           tile
                                           @click.prevent="resetForm">
                                        <v-icon left>mdi-eraser</v-icon>
                                        {{ $t("SHARED.RESET_BUTTON") }}
                                    </v-btn>
                                </div>
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
    import fsItemRow from "./FacilityServiceRow";
    import pItemRow from "./PriceRow";
    import VuetifyMoney from "../VuetifyMoney.vue";

    export default {
        components: {
            KTCodePortlet,
            fsItemRow,
            pItemRow,
            "vuetify-money": VuetifyMoney
        },
        data() {
            return {
                fsItems: [],
                priceItems: [],
                isChange: true,
                pictureFile: [],
                i: 0,
                valueWhenIsEmptyCapacity: "0", // "0" or "" or null
                valueOptionsCapacity: {
                    min: 0,
                    minEvent: "CustomMinEvent",
                    max: 100000000,
                    maxEvent: "CustomMaxEvent"
                },
                optionsCapacity: {
                    locale: "en-US",
                    prefix: "",
                    suffix: "",
                    length: 11,
                    precision: 0
                },
                propertiesCapacity: {
                    hint: ""
                },
                form: {
                    valid: true,
                    dialog: false,
                    loading: false,
                    errors: [],

                    nameTH: "",
                    nameEN: "",
                    detailsTH: null,
                    detailsEN: null,
                    qty: "",
                    qtyRules: [
                        v => !!v || this.$t("ROOM.ADD_EDIT.QTY_VALIDATION"),
                        v => (v && v >= 0) || this.$t("ROOM.ADD_EDIT.QTY_COUNTER"),
                    ],
                    nameRulesTH: [
                        (v) => !!v || this.$t("ROOM.ADD_EDIT.NAME_TH_VALIDATION"),
                        (v) => (v && v.length <= 50) || this.$t("ROOM.ADD_EDIT.NAME_COUNTER"),
                    ],
                    nameRulesEN: [
                        (v) => !!v || this.$t("ROOM.ADD_EDIT.NAME_EN_VALIDATION"),
                        (v) => (v && v.length <= 50) || this.$t("ROOM.ADD_EDIT.NAME_COUNTER"),
                    ],
                    roomTypeRules: [
                        (v) => !!v || this.$t("ROOM.ADD_EDIT.ROOM_TYPE_VALIDATION"),
                    ],
                    picture: null,
                    pictureType: ["image/jpeg", "image/png"],
                    roomType: null,
                    roomTypeSearch: "",
                    roomTypeLoading: false,
                    roomTypeItems: [],
                    capacity: null,
                    price: null,
                    inActiveStatus: true,
                    GP:null
                }
            }
        },
        computed: {
            title() {
                return this.$t("PLACE._PLACE.ADD");
            },
        },
        methods: {
            submitForm() {
                var self = this;
                var chk = this.$refs.form.validate();
                if (chk) {
                    if (this.form.picture != null && this.form.picture.length > 0) {
                        for (let i = 0; i < this.form.picture.length; i++) {
                            // self.pictureFile.push({
                            //        Base64s:null,
                            //        ContentTypes:null
                            //     });
                            if (
                                this.form.picture[i] &&
                                !this.form.pictureType.includes(this.form.picture[i].type)
                            ) {
                                this.form.errors.push(
                                    "กรุณาเลือกรูป Format .jpg, .jpeg, .png เท่านั้น"
                                );
                            }

                            this.getFileBase64(this.form.picture[i]).then(file => {
                                console.log(file);
                                let mb = 2 * 1024 * 1024;
                                if (file.size > mb)
                                    this.form.errors.push("กรุณาเลือกรูปที่มีขนาดน้อยกว่าหรือเท่ากับ 2 MB");

                                // if (this.form.errors.length > 0) {
                                //     this.$vuetify.goTo(this.$refs.alert, { duration: 1000, easing: 'easeInOutCubic', offset: -20 });
                                //     chk = false;
                                // }
                                self.pictureFile[i].Base64s = file.base64;
                                self.pictureFile[i].ContentTypes = file.contentType;
                                // this.pictureFile.push({
                                //    fItem:file.base64,
                                //    tItem:file.contentType
                                // });
                            });
                        }
                    }
                    this.postDataToApi();
                }
                //this.postDataToApi();
            },
            addFile() {
                this.form.picture.forEach(element => {
                    // console.log(element);
                    this.getFileBase64(element).then(file => {
                        // console.log(file);
                        // let mb = 2 * 1024 * 1024;
                        // if (file.size > mb)
                        //     this.form.errors.push("กรุณาเลือกรูปที่มีขนาดน้อยกว่าหรือเท่ากับ 2 MB");
                        this.pictureFile.push({
                            Base64s: file.base64,
                            ContentTypes: file.contentType
                        });
                    });
                })
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
                this.fsItems.push({
                    facility: null,
                    qty: null,
                    price: null,
                    selectedFacilityCodeData: null
                });
            },
            onAddPriceItemClick() {
                // if (
                //   !$(this.$refs.form)
                //     .validate()
                //     .form()
                // ) {
                //   return false;
                // }

                this.priceItems.push({
                    timeType: null,
                    qty: null,
                    price: null,
                    gp: this.form.GP
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
            onRemoveItemClick(index) {
                var self = this;
                self.fsItems.splice(index, 1);
            },
            onRemovePriceItemClick(index) {
                var self = this;
                self.priceItems.splice(index, 1);
            },
            updateCoordinates(location) {
                this.form.mLat = location.latLng.lat();
                this.form.mLng = location.latLng.lng();
                this.form.mMarkers = [{ position: { lat: this.form.mLat, lng: this.form.mLng } }];
            },
            getRoomTypeFromApi() {
                return new Promise((resolve) => {
                    ApiService.setHeader();
                    ApiService.post("/Api/RoomType/Select2RoomType", {
                        search: this.form.roomTypeSearch,
                        isall: true
                    })
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {
                            this.form.roleLoading = false;
                        });
                });
            },
            getGP(id) {
                return new Promise((resolve) => {
                    ApiService.setHeader();
                    ApiService.get("/Api/Room/GP",id)
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {

                        });
                });
            },
            postDataToApi() {
                var self = this;
                this.form.loading = true;
                this.form.errors = [];

                // var pftems = [];
                // console.log(self.pictureFile);
                // console.log(self.pictureFile.length);
                // if(self.pictureFile.length>0){
                //     // console.log(self.pictureFile.length);
                //     for(let i=0;i<self.pictureFile.length;i++){
                //         // console.log(self.pictureFile[i]);
                //         pftems.push({
                //         Base64s:self.pictureFile[i].Base64s,
                //         ContentTypes:self.pictureFile[i].ContentTypes
                //     });
                //     }
                // }
                var pftems = [];
                self.pictureFile.forEach(element => {
                    console.log(element);
                    pftems.push({
                        Base64s: element.Base64s,
                        ContentTypes: element.ContentTypes
                    });
                });
                // console.log(pftems);

                var fItems = [];
                self.fsItems.forEach(element => {
                    fItems.push({
                        FacilityId: element.facility.value,
                        Qty: element.qty,
                        Price: element.price
                    });
                });
                var pItems = [];
                self.priceItems.forEach(element => {
                    pItems.push({
                        TimeType: element.timeType.value,
                        Qty: element.qty,
                        Price: element.price
                    });
                });

                ApiService.setHeader();
                ApiService.post("/Api/Room/Add",
                    {
                        Name_TH: self.form.nameTH,
                        Name_EN: self.form.nameEN,
                        RoomTypeId: self.form.roomType ? self.form.roomType.value : "",
                        PlaceId: this.$route.params.id,
                        Detail_TH: self.form.detailsTH,
                        Detail_EN: self.form.detailsEN,
                        // Qty: self.form.qty,
                        Capacity: self.form.capacity,
                        Price: pItems,
                        Pictures: pftems,
                        InActiveStatus: !self.form.inActiveStatus,
                        ServiceItems: fItems,
                    })
                    .then(({ data }) => {
                        if (data.status) {
                            // close dialog
                            this.form.dialog = true;
                        } else {
                            this.form.dialog = false;
                            this.form.loading = false;
                            this.form.errors.push(data.message);
                            this.$vuetify.goTo(this.$refs.alert, { duration: 1000, easing: 'easeInOutCubic', offset: -20 });
                        }
                    })
                    .catch(({ response }) => {
                        if (response.data) {
                            this.form.errors.push(response.data.message);
                        } else {
                            this.form.errors.push("Unknow error occurs");
                        }
                        this.$vuetify.goTo(this.$refs.alert, { duration: 1000, easing: 'easeInOutCubic', offset: -20 });
                        this.form.dialog = false;
                        this.form.loading = false;
                    });
            },
            closeDialog() {
                var paths = "searchRoom/" + this.$route.params.id
                console.log(paths)
                // not close but redirect to search page
                this.$router.push({ name: "searchRoom" });
            },
        },
        async mounted() {
            this.$store.dispatch(SET_BREADCRUMB, [
                { title: this.$t("ROOM._ROOM.SECTION"), route: "/Place" },
                { title: this.$t("ROOM._ROOM.ADD") },
            ]);
            this.getGP(this.$route.params.id).then((data) => {
                this.form.GP = data.data;
            });

            
        },
        watch: {
            "form.roomTypeSearch": {
                handler() {
                    this.getRoomTypeFromApi().then((data) => {
                        this.form.roomTypeItems = data;
                    });
                },
            },
        }
    };
</script>

<style lang="scss" scoped></style>
