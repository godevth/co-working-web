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
                <KTPortlet v-bind:title="title">
                    <template v-slot:body>
                        <v-form ref="form"
                                @submit.prevent="submitForm"
                                v-model="form.valid"
                                lazy-validation>
                            <v-row>
                                <v-col cols="6"
                                       md="6">
                                    <v-text-field v-model="form.name"
                                                  :disabled="form.loading"
                                                  :counter="50"
                                                  :label="$t('ROOM_TPYE.ADD_EDIT.NAME_TH')"
                                                  :rules="form.nameRulesTH"
                                                  required></v-text-field>
                                </v-col>
                                <v-col cols="6"
                                       md="6">
                                    <v-text-field v-model="form.nameEN"
                                                  :disabled="form.loading"
                                                  :counter="50"
                                                  :label="$t('ROOM_TPYE.ADD_EDIT.NAME_EN')"
                                                  :rules="form.nameENRulesTH"
                                                  required></v-text-field>
                                </v-col>
                                <v-col cols="6" md="6">
                                    <v-file-input
                                        accept="image/*"
                                        show-size
                                        v-model="form.picture"
                                        :disabled="form.loading"
                                        :label="$t('FACILITY.ADD_EDIT.ICON')"
                                        :rules="form.pictureRules"
                                        :hint="$t('FACILITY.ADD_EDIT.PICTURE_HINT')"
                                        persistent-hint
                                        multiple
                                        @change="addFile()"
                                        truncate-length="50"
                                    ></v-file-input>
                                    </v-col>
                                    <v-col cols="6" md="6"> </v-col>
                                    <v-col
                                    cols="6"
                                    md="6"
                                    v-for="item in pictureFile"
                                    :key="item.id"
                                    >
                                    <v-img
                                        :v-model="pictureFile"
                                        v-if="item.id"
                                        :src="item.url"
                                        :disabled="form.loading"
                                        aspect-ratio="10"
                                        contain
                                    >
                                        
                                    </v-img>
                                </v-col>

                                <v-col cols="12"
                                       md="12">
                                    <v-checkbox v-model="form.inActiveStatus"
                                                :disabled="form.loading"
                                                :label="$t('ROOM_TPYE.ADD_EDIT.IN_ACTIVE_STATUS')"
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
                      $t("ROOM_TPYE.ADD_EDIT.SUCCESS_DIALOG_HEADER")
                                        }}
                                    </v-card-title>
                                    <v-card-text>
                                        {{ $t("ROOM_TPYE.ADD_EDIT.EDIT_SUCCESS_DIALOG_TEXT") }}
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
            KTPortlet
        },
        data() {
            return {
                pictureFile: [],
                form: {
                    valid: true,
                    dialog: false,
                    loading: false,
                    errors: [],
                    name: "",
                    nameEN: "",
                    inActiveStatus: true,
                    id: null,
                    picture: null,
                    pictureType: ["image/png"],
                    nameRulesTH: [
                        (v) => !!v || this.$t("ROOM.ADD_EDIT.NAME_TH_VALIDATION"),
                        (v) => (v && v.length <= 50) || this.$t("ROOM.ADD_EDIT.NAME_COUNTER"),
                    ],
                    nameRulesEN: [
                        (v) => !!v || this.$t("ROOM.ADD_EDIT.NAME_EN_VALIDATION"),
                        (v) => (v && v.length <= 50) || this.$t("ROOM.ADD_EDIT.NAME_COUNTER"),
                    ],
                }
            };
        },
        computed: {
            title() {
                return this.$t("ROOM_TPYE._ROOM_TPYE.EDIT");
            },
        },
        watch: {

        },
        methods: {
            submitForm() {
                var chk = this.$refs.form.validate();
                this.form.errors = [];


                if (chk) {
                    if (this.form.picture != null) {
                    for (let i = 0; i < this.form.picture.length; i++) {
                        if (
                        this.form.picture[0] &&
                        !this.form.pictureType.includes(this.form.picture[0].type)
                        ) {
                        this.form.errors.push("กรุณาเลือกรูป Format.png เท่านั้น");
                        } else {
                        this.getFileBase64(this.form.picture[i]).then((file) => {
                            console.log(file);
                            let mb = 2 * 43 * 43;
                            if (file.size > mb) {
                            this.form.errors.push(
                                "กรุณาเลือกรูปที่มีขนาดน้อยกว่าหรือเท่ากับ 3 kb"
                            );
                            } 
                            else {
                            this.pictureFile[i].Base64s = file.base64;
                            this.pictureFile[i].ContentTypes = file.contentType;
                            this.postDataToApi();
                            }
                        });
                        }
                    }
                    }
                    else
                    {
                    this.postDataToApi();
                    }
                }
            },
            addFile() {
                this.pictureFile = [];
                this.form.picture.forEach((element) => {
                    this.getFileBase64(element).then((file) => {
                    this.pictureFile.push({
                        Base64s: file.base64,
                        ContentTypes: file.contentType,
                        url: null,
                        base64Url: null,
                        id: null,
                    });
                    });
                });
            },
            getFileBase64(file) {
                return new Promise((resolve) => {
                    let reader = new FileReader();
                    reader.onload = () => {
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
            resetForm() {
                this.$refs.form.reset();
            },
            postDataToApi() {
                this.form.loading = true;
                this.form.errors = [];

                var pftems = [];
                this.pictureFile.forEach((element) => {
                    console.log(element);
                    pftems.push({
                    Id: element.id,
                    Base64s: element.Base64s,
                    ContentTypes: element.ContentTypes,
                    });
                });

                ApiService.setHeader();
                ApiService.put("/Api/RoomType/Edit", {
                    Id: this.form.id,
                    Name: this.form.name,
                    NameEN: this.form.nameEN,
                    InActiveStatus: !this.form.inActiveStatus,
                    Picture: pftems
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
                                offset: -20
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
                            offset: -20
                        });
                        this.form.dialog = false;
                        this.form.loading = false;
                    });
            },
            closeDialog() {
                // not close but redirect to search page
                this.$router.push({ name: "searchRoomType" });
            },
            getDataFromApi(id) {
                this.form.loading = true;
                return new Promise(() => {
                    ApiService.setHeader();
                    ApiService.get("/Api/RoomType", id)
                        .then(({ data }) => {
                            this.form.id = data.data.id;
                            this.form.name = data.data.name;
                            this.form.nameEN = data.data.nameEN
                            this.form.inActiveStatus = !data.data.inActiveStatus;
                            if (data.data.picture != null) {
                                this.pictureFile.push({
                                    Base64s: null,
                                    url: data.data.picture.downloadFileByteUrl,
                                    base64Url: data.data.picture.downloadFileBase64Url,
                                    id: data.data.picture.id,
                                    ContentTypes: null,
                                });
                                };
                            
                        })
                        .finally(() => {
                            this.form.loading = false;
                        });
                });
            }
        },
        mounted() {
            this.$store.dispatch(SET_BREADCRUMB, [
                { title: this.$t("ROOM_TPYE._ROOM_TPYE.SECTION"), route: "/RoomType" },
                { title: this.$t("ROOM_TPYE._ROOM_TPYE.EDIT") }
            ]);

            this.getDataFromApi(this.$route.params.id);
        }
    };
</script>

<style lang="scss" scoped></style>
