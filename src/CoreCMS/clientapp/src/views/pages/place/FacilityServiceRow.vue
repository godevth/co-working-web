<template>
    <tr>
        <td class="text-center">
            <span class="form-control form-control-plaintext">{{ rowIndex + 1 }}</span>
        </td>
        <td class="form-group">
            <v-autocomplete v-model="facilityItem"
                            :disabled="loadingItem"
                            :items="facilityItems"
                            :loading="facilityLoading"
                            :search-input.sync="facilitySearchItem"
                            :id="'facility' + _uid"
                            :name="'facility' + _uid"
                            :rules="facilityRules"
                            hide-no-data
                            hide-selected
                            item-text="text"
                            item-value="value"
                            ref="facility"
                            :label="$t('PLACE.SEARCH.FACILITY')"
                            @change="onChangeFacility()"
                            return-object
                            clearable
                            :readonly="!isEdit"></v-autocomplete>

            <span class="form-text"></span>
        </td>
        <!-- :rules="qtyRules" -->
        <td class="form-group">
            <vuetify-money v-model="item.qty"
                           v-bind:label="$t('PLACE.ADD_EDIT.QTY')"
                           v-bind:readonly="!isEdit"
                           v-bind:disabled="!isEdit"
                           v-bind:outlined="true"
                           v-bind:clearable="true"
                           v-bind:valueWhenIsEmpty="valueWhenIsEmptyQty"
                           v-bind:options="optionsQty"
                           v-bind:properties="propertiesQty"
                           :valueOptions="valueOptionsQty"
                           v-on:CustomMinEvent="item.qty = $event"
                           v-on:CustomMaxEvent="item.qty = $event" />
            <!-- <v-text-field v-model="item.qty"
                          :disabled="loadingItem"
                          :label="$t('PLACE.ADD_EDIT.QTY')"
                          :rules="qtyRules"
                          :id="'qty'+_uid"
                          :name="'qty' + _uid"
                          type="number"
                          min="0"
                          ref="qty"
                          :readonly="!isEdit"></v-text-field> -->
            <span class="form-text"></span>
        </td>
        <td class="form-group">
            <vuetify-money v-model="item.price"
                           v-bind:label="$t('PLACE.ADD_EDIT.PRICE')"
                           v-bind:readonly="!isEdit"
                           v-bind:disabled="!isEdit"
                           v-bind:outlined="true"
                           v-bind:clearable="true"
                           v-bind:valueWhenIsEmpty="valueWhenIsEmptyPrice"
                           v-bind:options="optionsPrice"
                           v-bind:properties="propertiesPrice"
                           :valueOptions="valueOptionsPrice"
                           v-on:CustomMinEvent="item.price = $event"
                           v-on:CustomMaxEvent="item.price = $event" />
            <!-- <v-text-field v-model="item.price"
                          :disabled="loadingItem"
                          :label="$t('PLACE.ADD_EDIT.PRICE')"
                          :rules="priceRules"
                          :id="'price'+_uid"
                          :name="'price' + _uid"
                          type="number"
                          ref="price"
                          :readonly="!isEdit"></v-text-field>
            <span class="form-text"></span> -->
        </td>
        <td class="form-group">
            <center>
                <button type="button"
                        class="btn btn-outline-hover-danger btn-elevate btn-circle btn-icon"
                        @click.prevent="onRemoveItemClick"
                        v-if="isEditable">
                    <i class="text-danger la la-trash"></i>
                </button>
            </center>
        </td>
    </tr>
</template>

<script>
    import ApiService from "@/common/api.service";
    import VuetifyMoney from "../VuetifyMoney.vue";

    export default {
        components: {
            "vuetify-money": VuetifyMoney
        },
        props: {
            isEditable: {
                type: Boolean,
                default: true
            },
            isEdit: {
                type: Boolean,
                default: false
            },
            rowIndex: Number,
            item: {
                type: Object,
                required: true
            }
        },
        data() {

            return {
                valueWhenIsEmptyPrice: "0", // "0" or "" or null
                valueOptionsPrice: {
                    min: 0,
                    minEvent: "CustomMinEvent",
                    max: 100000000,
                    maxEvent: "CustomMaxEvent"
                },
                optionsPrice: {
                    locale: "en-US",
                    prefix: "",
                    suffix: "",
                    length: 11,
                    precision: 2
                },
                propertiesPrice: {
                    hint: ""
                },
                valueWhenIsEmptyQty: "0", // "0" or "" or null
                valueOptionsQty: {
                    min: 0,
                    minEvent: "CustomMinEvent",
                    max: 100000000,
                    maxEvent: "CustomMaxEvent"
                },
                optionsQty: {
                    locale: "en-US",
                    prefix: "",
                    suffix: "",
                    length: 11,
                    precision: 0
                },
                propertiesQty: {
                    hint: ""
                },
                validItem: true,
                dialogItem: false,
                loadingItem: false,
                facilityItem: null,
                facilitySearchItem: "",
                facilityLoading: false,
                facilityItems: [],
                qtyItem: null,
                priceItem: null,
                facilityRules: [
                    (v) => !!v || this.$t("FACILITY.ADD_EDIT.FACILITYSELECT_VALIDATION"),
                ],
                qtyRules: [
                    v => !!v || this.$t("FACILITY.ADD_EDIT.PRICE_VALIDATION"),
                    v => (v && v >= 0) || this.$t("FACILITY.ADD_EDIT.PRICE_COUNTER"),
                ],
                priceRules: [
                    v => !!v || this.$t("FACILITY.ADD_EDIT.QTY_VALIDATION"),
                    v => (v && v >= 0) || this.$t("FACILITY.ADD_EDIT.QTY_COUNTER"),
                ],
            };
        },

        methods: {
            onRemoveItemClick() {
                var self = this;
                self.$emit("removeItem", self.rowIndex, self.item);
            },
            getFacilityFromApi() {
                var self = this;
                return new Promise((resolve) => {
                    console.log(self);
                    ApiService.setHeader();
                    ApiService.post("/Api/Facility/Select2Facility", {
                        search: this.facilitySearchItem,
                        isall: true,
                    })
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {
                            this.facilityLoadingItem = false;
                        });
                });
            },
            onChangeFacility() {
                var self = this;
                self.item.facility = self.facilityItem;
                var data = self.facilityItem;
                self.item.selectedFacilityCodeData = {
                    value: data.value,
                    text: data.text
                }
            }
        },
        mounted() {
            // let self = this;

            // var arrows;
            // if (KTUtil.isRTL()) {
            //   arrows = {
            //     leftArrow: '<i class="la la-angle-right"></i>',
            //     rightArrow: '<i class="la la-angle-left"></i>'
            //   };
            // } else {
            //   arrows = {
            //     leftArrow: '<i class="la la-angle-left"></i>',
            //     rightArrow: '<i class="la la-angle-right"></i>'
            //   };
            // }
        },
        watch: {
            "facilitySearchItem": {
                handler() {
                    this.getFacilityFromApi().then((data) => {
                        this.facilityItems = data;
                    });
                }
            }
        }
    };
</script>

<style lang="scss" scoped></style>
