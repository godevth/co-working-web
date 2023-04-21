<template>
    <tr>
        <td class="text-center">
            <span class="form-control form-control-plaintext">{{ rowIndex + 1 }}</span>
        </td>
        <td class="form-group">
            <v-autocomplete ref="startDayPlace"
                            v-model="startDayPlace"
                            :disabled="loadingItem"
                            :items="DayItems"
                            hide-no-data
                            hide-selected
                            item-text="text"
                            item-value="value"
                            :label="$t('PLACE.ADD_EDIT.START_DAY')"
                            return-object
                            :rules="startDayRules"
                            @change="onChangeStartDay()"
                            clearable
                            :readonly="!isEdit"></v-autocomplete>
        </td>
        <!-- <td class="form-group">
            <v-autocomplete ref="endDayPlace"
                            v-model="endDayPlace"
                            :disabled="loadingItem"
                            :items="DayItems"
                            hide-no-data
                            hide-selected
                            item-text="text"
                            item-value="value"
                            :label="$t('PLACE.ADD_EDIT.END_DAY')"
                            return-object
                            @change="onChangeEndDay()"
                            clearable></v-autocomplete>
        </td> -->
        <td class="form-group">
            <v-menu ref="startTimePlace"
                    v-model="startTimePlace"
                    :disabled="loadingItem"
                    :close-on-content-click="false"
                    :nudge-right="5"
                    transition="scale-transition"
                    offset-y
                    min-width="290px">
                <template v-slot:activator="{ on }">
                    <v-text-field v-model="item.startTime"
                                  :disabled="loadingItem"
                                  :label="$t('PLACE.ADD_EDIT.START_TIME')"
                                  :rules="startTimeRules"
                                  hint="HH:MM format"
                                  persistent-hint
                                  readonly
                                  v-on="on"></v-text-field>
                </template>
                <v-time-picker v-model="item.startTime"
                               format="24hr"
                               scrollable
                               :readonly="!isEdit"
                               :disabled="loadingItem">
                    <v-spacer></v-spacer>
                    <v-btn text
                           color="primary"
                           @click="startTimePlace = false">Cancel</v-btn>
                    <v-btn text
                           color="primary"
                           @click="$refs.startTimePlace.save(item.startTime)">OK</v-btn>
                </v-time-picker>
            </v-menu>
        </td>
        <td class="form-group">
            <v-menu ref="endTimePlace"
                    v-model="endTimePlace"
                    :disabled="loadingItem"
                    :close-on-content-click="false"
                    :nudge-right="5"
                    transition="scale-transition"
                    offset-y
                    min-width="290px">
                <template v-slot:activator="{ on }">
                    <v-text-field v-model="item.endTime"
                                  :disabled="loadingItem"
                                  :label="$t('PLACE.ADD_EDIT.END_TIME')"
                                  :rules="endTimeRules"
                                  hint="HH:MM format"
                                  persistent-hint
                                  readonly
                                  v-on="on"></v-text-field>
                </template>
                <v-time-picker v-model="item.endTime"
                               format="24hr"
                               scrollable
                               :readonly="!isEdit"
                               :disabled="loadingItem">
                    <v-spacer></v-spacer>
                    <v-btn text
                           color="primary"
                           @click="endTimePlace = false">Cancel</v-btn>
                    <v-btn text
                           color="primary"
                           @click="$refs.endTimePlace.save(item.endTime)">OK</v-btn>
                </v-time-picker>
            </v-menu>
        </td>
        <td class="form-group">
            <center>
                <button type="button"
                        class="btn btn-outline-hover-danger btn-elevate btn-circle btn-icon"
                        @click.prevent="onRemoveImItemClick"
                        v-if="isEditable">
                    <i class="text-danger la la-trash"></i>
                </button>
            </center>
        </td>
    </tr>
</template>

<script>
    export default {
        components: {
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
            var self = this;
            return {
                validItem: true,
                dialogItem: false,
                loadingItem: false,
                startDayPlace: null,
                // endDayPlace:null,
                qtyItem: null,
                priceItem: null,
                qtyRules: null,
                priceRules: null,
                startTimePlace: false,
                endTimePlace: false,
                startDayRules: [
                    (v) => !!v || this.$t("PLACE.ADD_EDIT.START_DAY_VALIDATION"),
                ],
                startTimeRules: [
                    (v) => !!v || this.$t("PLACE.ADD_EDIT.START_TIME_VALIDATION"),
                ],
                endTimeRules: [
                    (v) => !!v || this.$t("PLACE.ADD_EDIT.END_TIME_VALIDATION"),
                    v => (v && v >= self.item.startTime) || this.$t("PLACE.ADD_EDIT.END_TIME_COUNTER"),
                ],
                DayItems: [
                    {
                        text: "Sunday",
                        value: "Sunday",
                    },
                    {
                        text: "Monday",
                        value: "Monday",
                    },
                    {
                        text: "Tuesday",
                        value: "Tuesday",
                    },
                    {
                        text: "Wednesday",
                        value: "Wednesday",
                    },
                    {
                        text: "Thursday",
                        value: "Thursday",
                    },
                    {
                        text: "Friday",
                        value: "Friday",
                    },
                    {
                        text: "Saturday",
                        value: "Saturday",
                    },
                ],
            };
        },

        methods: {
            onRemoveImItemClick() {
                var self = this;
                self.$emit("removeItem", self.rowIndex, self.item);
            },
            onChangeStartDay() {
                var self = this;
                self.item.startDay = self.startDayPlace;
                var data = self.startDayPlace;
                console.log(data);
                self.item.selectedStartDayCodeData = {
                    value: data.value,
                    text: data.text
                }
            },
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
        watch: {}
    };
</script>

<style lang="scss" scoped></style>
