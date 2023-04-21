<template>
    <tr>
        <td class="text-center">
            <span class="form-control form-control-plaintext">{{ rowIndex + 1 }}</span>
        </td>
        <td class="form-group">
            <v-autocomplete v-model="adminItem"
                            :disabled="loadingItem"
                            :items="adminItems"
                            :loading="adminLoading"
                            :search-input.sync="adminSearchItem"
                            :id="'admin' + _uid"
                            :name="'admin' + _uid"
                            hide-no-data
                            hide-selected
                            item-text="text"
                            item-value="value"
                            ref="admin"
                            :label="$t('COMPANY.ADD_EDIT.ADMIN')"
                            :rules="adminRules"
                            @change="onChangeAdmin()"
                            return-object
                            clearable
                            :readonly="!isEdit"></v-autocomplete>
                            
            <span class="form-text"></span>
        </td>
        <td class="form-group">
            <v-autocomplete v-model="placeItem"
                            :disabled="loadingItem"
                            :items="placeItems"
                            :loading="placeLoading"
                            :search-input.sync="placeSearchItem"
                            :id="'place' + _uid"
                            :name="'place' + _uid"
                            hide-no-data
                            hide-selected
                            item-text="text"
                            item-value="value"
                            ref="place"
                            :label="$t('COMPANY.ADD_EDIT.PLACE')"
                            :rules="placeRules"
                            @change="onChangePlace()"
                            return-object
                            clearable
                            :readonly="!isEdit"></v-autocomplete>
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
    import ApiService from "@/common/api.service";

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

            return {
                companyId:null,
                validItem: true,
                dialogItem: false,
                loadingItem: false,
                adminItem: null,
                adminSearchItem: "",
                adminLoading: false,
                adminItems: [],
                placeItem: null,
                placeSearchItem: "",
                placeLoading: false,
                placeItems: [],
                adminRules:[
          (v) => !!v || this.$t("COMPANY.ADD_EDIT.ADMIN_VALIDATION"),
        ],
        placeRules:[
          (v) => !!v || this.$t("COMPANY.ADD_EDIT.PLACE_VALIDATION"),
        ],
            };
        },

        methods: {
            onRemoveItemClick() {
                var self = this;
                self.$emit("removeItem", self.rowIndex, self.item);
            },
            getAdminFromApi() {
                return new Promise((resolve) => {
                    ApiService.setHeader();
                    ApiService.post("/Api/User/Select2UserByType", {
                        search: this.adminSearchItem,
                        UserType:2,
                    })
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {
                            this.adminLoading = false;
                        });
                });
            },
            getPlaceFromApi() {
                var self = this;
                this.companyId = self.$parent.$root._route.params.id;
                console.log(this.companyId);
                return new Promise((resolve) => {
                    ApiService.setHeader();
                    ApiService.post("/Api/Place/Select2PlaceByCompany", {
                        search: this.placeSearchItem,
                        companyId:this.companyId
                    })
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {
                            this.placeLoading = false;
                        });
                });
            },
            onChangeAdmin() {
                var self = this;
                self.item.admin = self.adminItem;
            },
            onChangePlace() {
                var self = this;
                self.item.place = self.placeItem;
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
            "adminSearchItem": {
                handler() {
                    this.getAdminFromApi().then((data) => {
                        this.adminItems = data;
                    });
                }
            },
            "placeSearchItem": {
                handler() {
                    this.getPlaceFromApi().then((data) => {
                        this.placeItems = data;
                    });
                }
            }
        }
    };
</script>

<style lang="scss" scoped></style>
