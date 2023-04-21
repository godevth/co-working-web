<template>
    <tr>
        <td class="text-center">
            <span class="form-control form-control-plaintext">{{ rowIndex + 1 }}</span>
        </td>
        <td class="form-group">
            <v-autocomplete v-model="timeTypeItem"
                            :disabled="loadingItem"
                            :items="timeTypeItems"
                            :loading="timeTypeLoading"
                            :search-input.sync="timeTypeSearchItem"
                            :id="'timeType' + _uid"
                            :name="'timeType' + _uid"
                            hide-no-data
                            hide-selected
                            item-text="text"
                            item-value="value"
                            ref="timeType"
                            :label="$t('PLACE.SEARCH.TIMETYPE')"
                            @change="onChangeTimeType()"
                            :rules="roomPriceeRules"
                            return-object
                            clearable
                            :readonly="!isEdit"></v-autocomplete>

            <span class="form-text"></span>
        </td>
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
            <span class="form-text"></span>
        </td>
        <td class="form-group">
            <vuetify-money v-model="item.price"
                           v-bind:label="$t('ROOM.ADD_EDIT.ROOMPRICE')"
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
             <p>จำนวนเงินที่ได้รับ : {{calGp(item.price,item.gp)}} </p>
            <span class="form-text"></span>
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
                timeTypeItem: null,
                timeTypeSearchItem: "",
                timeTypeLoading: false,
                timeTypeItems: [
                    {
                        text: "Month",
                        value: "Month",
                    },
                    {
                        text: "Day",
                        value: "Day",
                    },
                    {
                        text: "Hours",
                        value: "Hours",
                    }

                ],
                qtyItem: null,
                priceItem: null,
                roomPriceeRules: [
                    (v) => !!v || this.$t("ROOM.ADD_EDIT.ROOM_PRICE_VALIDATION"),
                ],
                qtyRules: [
                    v => !!v || this.$t("ROOM.ADD_EDIT.PRICE_VALIDATION"),
                    v => (v && v >= 0) || this.$t("ROOM.ADD_EDIT.PRICE_COUNTER"),
                ],
                priceRules: [
                    v => !!v || this.$t("ROOM.ADD_EDIT.QTY_VALIDATION"),
                    v => (v && v >= 0) || this.$t("ROOM.ADD_EDIT.QTY_COUNTER"),
                ],
            };
        },

        methods: {
            onRemoveItemClick() {
                var self = this;
                self.$emit("removeItem", self.rowIndex, self.item);
            },
            onChangeTimeType() {
                var self = this;
                self.item.timeType = self.timeTypeItem;
            },
            calGp(price,gp)
            {
                var vat = (gp/100)*price;
                var total = price - vat;
                if(price == null && gp == undefined)
                {
                    return "";
                }
                
                return total.toFixed(2);
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
            // "facilitySearchItem": {
            //     handler() {
            //         this.getFacilityFromApi().then((data) => {
            //             this.facilityItems = data;
            //         });
            //     }
            // }
        }
    };
</script>

<style lang="scss" scoped></style>
