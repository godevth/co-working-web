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
                                lazy-validation
                                autocomplete="off">
                            <v-row>
                                <v-col cols="12"
                                       md="6">
                                    <v-text-field v-model="form.email"
                                                  :disabled="form.loading"
                                                  :counter="256"
                                                  :label="$t('USER.ADD_EDIT.EMAIL')"
                                                  :rules="form.emailRules"
                                                  type="email"
                                                  required></v-text-field>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-autocomplete v-model="form.userType"
                                                    :disabled="form.loading"
                                                    :items="form.userTypeItems"
                                                    :loading="form.userTypeLoading"
                                                    :search-input.sync="form.userTypeSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('USER.ADD_EDIT.USER_TYPE')"
                                                    @change="onChangeUserType()"
                                                    return-object
                                                    clearable></v-autocomplete>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-autocomplete v-model="form.role"
                                                    :disabled="form.loading"
                                                    :items="form.roleItems"
                                                    :loading="form.roleLoading"
                                                    :search-input.sync="form.roleSearch"
                                                    hide-no-data
                                                    hide-selected
                                                    item-text="text"
                                                    item-value="value"
                                                    :label="$t('USER.ADD_EDIT.ROLE')"
                                                    return-object
                                                    clearable></v-autocomplete>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-text-field v-model="form.firstName"
                                                  :disabled="form.loading"
                                                  :counter="450"
                                                  :label="$t('USER.ADD_EDIT.FIRST_NAME')"
                                                  :rules="form.firstNameRules"
                                                  required></v-text-field>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-text-field v-model="form.lastName"
                                                  :disabled="form.loading"
                                                  :counter="450"
                                                  :label="$t('USER.ADD_EDIT.LAST_NAME')"
                                                  :rules="form.lastNameRules"
                                                  required></v-text-field>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-menu ref="birthDateMenu"
                                            v-model="form.birthDateMenu"
                                            :disabled="form.promotionCode == 'BirthDate'"
                                            :close-on-content-click="false"
                                            :nudge-right="5"
                                            transition="scale-transition"
                                            offset-y
                                            min-width="290px">
                                        <template v-slot:activator="{ on }">
                                            <v-text-field v-model="computedBirthDateFormatted"
                                                          :disabled="form.loading"
                                                          :label="$t('USER.ADD_EDIT.BIRTH_DATE')"
                                                          hint="DD/MM/YYYY format"
                                                          :rules="form.birthDateRules"
                                                          required
                                                          persistent-hint
                                                          readonly
                                                          v-on="on"></v-text-field>
                                        </template>
                                        <v-date-picker v-model="form.birthDate"
                                                       scrollable
                                                       :disabled="form.loading">
                                            <v-spacer></v-spacer>
                                            <v-btn text
                                                   color="primary"
                                                   @click="form.birthDateMenu = false">Cancel</v-btn>
                                            <v-btn text
                                                   color="primary"
                                                   @click="$refs.birthDateMenu.save(form.birthDate)">OK</v-btn>
                                        </v-date-picker>
                                    </v-menu>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-text-field v-model="form.phoneNumber"
                                                  :disabled="form.loading"
                                                  :counter="50"
                                                  :label="$t('USER.ADD_EDIT.PHONE_NUMBER')"
                                                  :rules="form.phoneNumberRules"></v-text-field>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-text-field v-model="form.position"
                                                  :disabled="form.loading"
                                                  :counter="100"
                                                  :label="$t('USER.ADD_EDIT.POSITION')"
                                                  :rules="form.positionRules"></v-text-field>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-text-field v-model="form.password"
                                                  :disabled="form.loading"
                                                  :counter="20"
                                                  :append-icon="form.showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                                                  :rules="form.passwordRules"
                                                  :type="form.showPassword ? 'text' : 'password'"
                                                  :label="$t('USER.ADD_EDIT.PASSWORD')"
                                                  @click:append="form.showPassword = !form.showPassword"
                                                  autocomplete="new-password"></v-text-field>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-text-field v-model="form.confirmPassword"
                                                  :disabled="form.loading"
                                                  :counter="20"
                                                  :append-icon="form.showConfirmPassword ? 'mdi-eye' : 'mdi-eye-off'"
                                                  :rules="form.confirmPasswordRules"
                                                  :type="form.showConfirmPassword ? 'text' : 'password'"
                                                  :label="$t('USER.ADD_EDIT.COMFIRM_PASSWORD')"
                                                  @click:append="form.showConfirmPassword = !form.showConfirmPassword"></v-text-field>
                                </v-col>

                                <v-col cols="12"
                                       md="6">
                                    <v-checkbox v-model="form.inActiveStatus"
                                                :disabled="form.loading"
                                                :label="$t('USER.ADD_EDIT.IN_ACTIVE_STATUS')"
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
                      $t("USER.ADD_EDIT.SUCCESS_DIALOG_HEADER")
                                        }}
                                    </v-card-title>
                                    <v-card-text>
                                        {{ $t("USER.ADD_EDIT.ADD_SUCCESS_DIALOG_TEXT") }}
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
    import KTCodePortlet from "@/views/partials/content/Portlet.vue";
    import { SET_BREADCRUMB } from "@/store/breadcrumbs.module";
    import ApiService from "@/common/api.service";

    export default {
        components: {
            KTCodePortlet,
        },
        data() {
            return {
                form: {
                    valid: true,
                    dialog: false,
                    loading: false,
                    errors: [],
                    email: "",
                    role: "",
                    userType: "",
                    firstName: "",
                    lastName: "",
                    birthDateMenu: false,
                    birthDate: null,
                    birthDateFormatted: null,
                    phoneNumber: "",
                    position: "",
                    password: "",
                    confirmPassword: "",
                    inActiveStatus: true,
                    emailRules: [
                        (v) => !!v || this.$t("USER.ADD_EDIT.EMAIL_VALIDATION"),
                        (v) => (v && v.length <= 256) || this.$t("USER.ADD_EDIT.EMAIL_COUNTER"),
                        (v) => (v && /.+@.+\..+/.test(v)) || this.$t("USER.ADD_EDIT.EMAIL_FORMAT"),
                    ],
                    firstNameRules: [
                        (v) => !!v || this.$t("USER.ADD_EDIT.FIRST_NAME_VALIDATION"),
                        (v) => (v && v.length <= 450) || this.$t("USER.ADD_EDIT.FIRST_NAME_COUNTER"),
                    ],
                    lastNameRules: [
                        (v) => !!v || this.$t("USER.ADD_EDIT.LAST_NAME_VALIDATION"),
                        (v) => (v && v.length <= 450) || this.$t("USER.ADD_EDIT.LAST_NAME_COUNTER"),
                    ],
                    passwordRules: [
                        (v) => !!v || this.$t("USER.ADD_EDIT.PASSWORD_VALIDATION"),
                        (v) => (v && v.length <= 20) || this.$t("USER.ADD_EDIT.PASSWORD_COUNTER"),
                        (v) => (v && v.length >= 6) || this.$t("USER.ADD_EDIT.PASSWORD_COUNTER_LT"),
                    ],
                    confirmPasswordRules: [
                        (v) => !!v || this.$t("USER.ADD_EDIT.COMFIRM_PASSWORD_VALIDATION"),
                        (v) => (v && v.length <= 20) || this.$t("USER.ADD_EDIT.COMFIRM_PASSWORD_COUNTER"),
                        (v) => (v && v == this.form.password) || this.$t("USER.ADD_EDIT.COMFIRM_PASSWORD_EQ"),
                        (v) => (v && v.length >= 6) || this.$t("USER.ADD_EDIT.COMFIRM_PASSWORD_COUNTER_LT"),
                    ],
                    phoneNumberRules: [
                        (v) => (!v || (v && v.length <= 50)) || this.$t("USER.ADD_EDIT.PHONE_NUMBER_COUNTER"),
                    ],
                    positionRules: [
                        (v) => (!v || (v && v.length <= 100)) || this.$t("USER.ADD_EDIT.POSITION_COUNTER"),
                    ],
                    birthDateRules: [
                        (v) => !!v || this.$t("USER.ADD_EDIT.BIRTH_DATE_VALIDATION"),
                    ],
                    roleSearch: "",
                    roleLoading: false,
                    roleItems: [],
                    userTypeSearch: "",
                    userTypeLoading: false,
                    userTypeItems: [],
                    showPassword: false,
                    showConfirmPassword: false,
                },
            }
        },
        methods: {
            formatDate(date) {
                if (!date) return null;

                const [year, month, day] = date.split("-");
                return `${day}/${month}/${year}`;
            },
            parseDate(date) {
                if (!date) return null;

                const [day, month, year] = date.split("/");
                return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
            },
            submitForm() {
                if (this.$refs.form.validate()) {
                    this.postDataToApi();
                }
            },
            resetForm() {
                this.$refs.form.reset();
            },
            postDataToApi() {
                this.form.loading = true;
                this.form.errors = [];

                ApiService.setHeader();
                ApiService.post("/Api/User/Add", {
                    Email: this.form.email,
                    Password: this.form.password,
                    ConfirmPassword: this.form.confirmPassword,
                    FirstName: this.form.firstName,
                    LastName: this.form.lastName,
                    BirthDateString: this.form.birthDateFormatted,
                    PhoneNumber: this.form.phoneNumber,
                    Position: this.form.position,
                    Role: this.form.role
                        ? this.form.role.value
                        : null,
                    UserTypeId: this.form.userType
                        ? this.form.userType.value
                        : null,
                    InActiveStatus: !this.form.inActiveStatus,
                })
                    .then(({ data }) => {
                        if (data.status) {
                            // close dialog
                            this.form.dialog = true;
                        } else {
                            this.form.dialog = false;
                            this.form.loading = false;
                            this.form.errors.push(!!data.message || "Unknow error occurs");
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
                // not close but redirect to search page
                this.$router.push({ name: "search" });
            },
            getRoleFromApi() {
                return new Promise((resolve) => {
                    ApiService.setHeader();
                    ApiService.post("/Api/Role/RoleOptions", {
                        query: this.form.roleSearch,
                        userTypeId: this.form.userType
                            ? this.form.userType.value
                            : null
                    })
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {
                            this.form.roleLoading = false;
                        });
                });
            },
            getUserTypeFromApi() {
                return new Promise((resolve) => {
                    ApiService.setHeader();
                    ApiService.post("/Api/UserType/UserTypeOptions", {
                        query: this.form.userTypeSearch,
                    })
                        .then(({ data }) => {
                            resolve(data);
                        })
                        .finally(() => {
                            this.form.userTypeLoading = false;
                        });
                });
            },
            onChangeUserType() {
                this.form.role = null;
                this.getRoleFromApi().then((data) => {
                    this.form.roleItems = data;
                });
            },
        },
        mounted() {
            this.$store.dispatch(SET_BREADCRUMB, [
                { title: this.$t("MENU.USER.SECTION"), route: "/User" },
                { title: this.$t("MENU.USER.ADD") },
            ]);
        },
        computed: {
            title() {
                return this.$t("MENU.USER.ADD");
            },
            computedBirthDateFormatted() {
                return this.formatDate(this.form.birthDate);
            },
        },
        watch: {
            "form.roleSearch": {
                handler() {
                    this.getRoleFromApi().then((data) => {
                        this.form.roleItems = data;
                    });
                },
            },
            "form.userTypeSearch": {
                handler() {
                    this.getUserTypeFromApi().then((data) => {
                        this.form.userTypeItems = data;
                    });
                },
            },
            "form.birthDate": {
                handler() {
                    this.form.birthDateFormatted = this.formatDate(this.form.birthDate);
                },
            },
        },
    };
</script>

<style lang="scss" scoped></style>
