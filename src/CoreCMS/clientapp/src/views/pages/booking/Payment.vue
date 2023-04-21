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
              autocomplete="off"
            >
              <v-row>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="form.bookingNumber"
                    :disabled="true"
                    :label="$t('BOOKING.PAYMENT.BOOKING_NUMBER')"
                  ></v-text-field>
                </v-col>

                <v-col cols="12" md="6">
                  <v-autocomplete
                    v-model="form.counterPaymentCode"
                    :disabled="form.loading"
                    :items="form.counterPaymentCodeItems"
                    :loading="form.counterPaymentCodeLoading"
                    :search-input.sync="form.counterPaymentCodeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('BOOKING.PAYMENT.COUNTER_PAYMENT')"
                    return-object
                    clearable
                    :rules="form.counterPaymentCodeRules"
                    required
                  ></v-autocomplete>
                </v-col>

                <v-col cols="12" md="6">
                  <!-- <v-text-field
                    v-model="form.total"
                    :disabled="true"
                    :label="$t('BOOKING.PAYMENT.TOTAL')"
                  ></v-text-field> -->
                  <vuetify-money
                    v-model="form.total"
                    v-bind:label="$t('BOOKING.PAYMENT.TOTAL')"
                    v-bind:disabled="true"
                    v-bind:valueWhenIsEmpty="form.valueWhenIsEmptyPrice"
                    v-bind:options="form.optionsPrice"
                    v-bind:properties="form.propertiesPrice"
                    :valueOptions="form.valueOptionsPrice"
                    v-on:CustomMinEvent="form.total = $event"
                    v-on:CustomMaxEvent="form.total = $event"
                  />
                  <span class="form-text"></span>
                </v-col>

                <v-col cols="12" md="6">
                  <!-- <v-text-field
                    v-model="form.paid"
                    :disabled="form.loading"
                    :label="$t('BOOKING.PAYMENT.PAID')"
                    :rules="form.paidRules"
                    type="number"
                    min="1"
                    required
                    @keyup="onPaidChange"
                    @change="onPaidChange"
                  ></v-text-field> -->
                  <vuetify-money
                    v-model="form.paid"
                    v-bind:label="$t('BOOKING.PAYMENT.PAID')"
                    v-bind:disabled="form.loading"
                    v-bind:valueWhenIsEmpty="form.valueWhenIsEmptyPrice"
                    v-bind:options="form.optionsPrice"
                    v-bind:properties="form.propertiesPrice"
                    :valueOptions="form.valueOptionsPrice"
                    v-on:CustomMinEvent="form.paid = $event"
                    v-on:CustomMaxEvent="form.paid = $event"
                    :rules="form.paidRules"
                    required
                  />
                  <span class="form-text"></span>
                </v-col>

                <v-col cols="12" md="6">
                  <!-- <v-text-field
                    v-model="form.receive"
                    :disabled="form.loading"
                    :label="$t('BOOKING.PAYMENT.RECEIVE')"
                    :rules="form.receiveRules"
                    type="number"
                    min="1"
                    required
                    @keyup="onReceiveChange"
                    @change="onReceiveChange"
                  ></v-text-field> -->
                  <vuetify-money
                    v-model="form.receive"
                    v-bind:label="$t('BOOKING.PAYMENT.RECEIVE')"
                    v-bind:disabled="form.loading"
                    v-bind:valueWhenIsEmpty="form.valueWhenIsEmptyPrice"
                    v-bind:options="form.optionsPrice"
                    v-bind:properties="form.propertiesPrice"
                    :valueOptions="form.valueOptionsPrice"
                    v-on:CustomMinEvent="form.receive = $event"
                    v-on:CustomMaxEvent="form.receive = $event"
                    :rules="form.receiveRules"
                    required
                  />
                  <span class="form-text"></span>
                </v-col>

                <v-col cols="12" md="6">
                  <!-- <v-text-field
                    v-model="form.change"
                    :disabled="true"
                    :label="$t('BOOKING.PAYMENT.CHANGE')"
                    filled
                  ></v-text-field> -->
                  <vuetify-money
                    v-model="form.change"
                    v-bind:label="$t('BOOKING.PAYMENT.CHANGE')"
                    v-bind:disabled="true"
                    v-bind:valueWhenIsEmpty="form.valueWhenIsEmptyPrice"
                    v-bind:options="form.optionsPrice"
                    v-bind:properties="form.propertiesPrice"
                    :valueOptions="form.valueOptionsPrice"
                    v-on:CustomMinEvent="form.change = $event"
                    v-on:CustomMaxEvent="form.change = $event"
                  />
                  <span class="form-text"></span>
                </v-col>

                <v-col cols="12" md="6">
                  <!-- <v-text-field
                    v-model="form.remaining"
                    :disabled="true"
                    :label="$t('BOOKING.PAYMENT.REMAINING')"
                    filled
                  ></v-text-field> -->
                  <vuetify-money
                    v-model="form.remaining"
                    v-bind:label="$t('BOOKING.PAYMENT.REMAINING')"
                    v-bind:disabled="true"
                    v-bind:valueWhenIsEmpty="form.valueWhenIsEmptyPrice"
                    v-bind:options="form.optionsPrice"
                    v-bind:properties="form.propertiesPrice"
                    :valueOptions="form.valueOptionsPrice"
                    v-on:CustomMinEvent="form.remaining = $event"
                    v-on:CustomMaxEvent="form.remaining = $event"
                  />
                  <span class="form-text"></span>
                </v-col>

                <v-col cols="12" md="3">
                  <v-menu
                    ref="paymentDateMenu"
                    v-model="form.paymentDateMenu"
                    :close-on-content-click="false"
                    :nudge-right="5"
                    transition="scale-transition"
                    offset-y
                    min-width="290px"
                  >
                    <template v-slot:activator="{ on }">
                      <v-text-field
                        v-model="computedPaymentDateFormatted"
                        :disabled="form.loading"
                        :label="$t('BOOKING.PAYMENT.PAYMENT_DATE')"
                        hint="DD/MM/YYYY format"
                        :rules="form.paymentDateRules"
                        required
                        persistent-hint
                        readonly
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="form.paymentDate"
                      scrollable
                      :disabled="form.loading"
                    >
                      <v-spacer></v-spacer>
                      <v-btn
                        text
                        color="primary"
                        @click="form.paymentDateMenu = false"
                        >Cancel</v-btn
                      >
                      <v-btn
                        text
                        color="primary"
                        @click="$refs.paymentDateMenu.save(form.paymentDate)"
                        >OK</v-btn
                      >
                    </v-date-picker>
                  </v-menu>
                </v-col>

                <v-col cols="12" md="3">
                  <v-menu
                    ref="paymentTimeMenu"
                    v-model="form.paymentTimeMenu"
                    :disabled="form.loading"
                    :close-on-content-click="false"
                    :nudge-right="5"
                    transition="scale-transition"
                    offset-y
                    min-width="290px"
                  >
                    <template v-slot:activator="{ on }">
                      <v-text-field
                        v-model="form.paymentTime"
                        :disabled="form.loading"
                        :label="$t('BOOKING.PAYMENT.PAYMENT_TIME')"
                        :rules="form.paymentTimeRules"
                        hint="HH:MM format"
                        persistent-hint
                        readonly
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-time-picker
                      v-model="form.paymentTime"
                      format="24hr"
                      scrollable
                      :disabled="form.loading"
                    >
                      <v-spacer></v-spacer>
                      <v-btn
                        text
                        color="primary"
                        @click="form.paymentTimeMenu = false"
                        >Cancel</v-btn
                      >
                      <v-btn
                        text
                        color="primary"
                        @click="$refs.paymentTimeMenu.save(form.paymentTime)"
                        >OK</v-btn
                      >
                    </v-time-picker>
                  </v-menu>
                </v-col>

                <v-col
                  cols="12"
                  md="6"
                  v-show="
                    form.counterPaymentCode
                      ? form.counterPaymentCode.value ===
                        'COUNTER_PAYMENT_TRANSFER'
                      : false
                  "
                >
                  <v-autocomplete
                    v-model="form.bankCode"
                    :disabled="form.loading"
                    :items="form.bankCodeItems"
                    :loading="form.bankCodeLoading"
                    :search-input.sync="form.bankCodeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('BOOKING.PAYMENT.BANK')"
                    return-object
                    clearable
                    :rules="form.bankCodeRules"
                    required
                  ></v-autocomplete>
                </v-col>

                <v-col
                  cols="12"
                  md="6"
                  v-show="
                    form.counterPaymentCode
                      ? form.counterPaymentCode.value ===
                        'COUNTER_PAYMENT_CREDIT_CARD'
                      : false
                  "
                >
                  <v-autocomplete
                    v-model="form.creditCardTypeCode"
                    :disabled="form.loading"
                    :items="form.creditCardTypeCodeItems"
                    :loading="form.creditCardTypeCodeLoading"
                    :search-input.sync="form.creditCardTypeCodeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('BOOKING.PAYMENT.CREDIT_CARD_TYPE')"
                    return-object
                    clearable
                    :rules="form.creditCardTypeCodeRules"
                    required
                  ></v-autocomplete>
                </v-col>

                <v-col
                  cols="12"
                  md="6"
                  v-show="
                    form.counterPaymentCode
                      ? form.counterPaymentCode.value ===
                        'COUNTER_PAYMENT_CREDIT_CARD'
                      : false
                  "
                >
                  <v-text-field
                    v-model="form.merchantId"
                    :disabled="form.loading"
                    :label="$t('BOOKING.PAYMENT.MERCHANT_ID')"
                  ></v-text-field>
                </v-col>

                <v-col
                  cols="12"
                  md="6"
                  v-show="
                    form.counterPaymentCode
                      ? form.counterPaymentCode.value ===
                        'COUNTER_PAYMENT_CREDIT_CARD'
                      : false
                  "
                >
                  <v-text-field
                    v-model="form.refCode"
                    :disabled="form.loading"
                    :label="$t('BOOKING.PAYMENT.REF_CODE')"
                  ></v-text-field>
                </v-col>

                <v-col
                  cols="12"
                  md="6"
                  v-show="
                    form.counterPaymentCode
                      ? form.counterPaymentCode.value ===
                        'COUNTER_PAYMENT_CREDIT_CARD'
                      : false
                  "
                >
                  <v-text-field
                    v-model="form.transactionId"
                    :disabled="form.loading"
                    :label="$t('BOOKING.PAYMENT.TRANSACTION_ID')"
                  ></v-text-field>
                </v-col>

                <v-col cols="12" md="12">
                  <v-file-input
                    accept="image/*"
                    show-size
                    v-model="form.picture"
                    :disabled="form.loading"
                    :label="$t('BOOKING.PAYMENT.PICTURE')"
                    :rules="form.pictureRules"
                    :hint="$t('BOOKING.PAYMENT.PICTURE')"
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
                    {{
                      $t("BOOKING.PAYMENT.SUCCESS_DIALOG_HEADER")
                    }}</v-card-title
                  >
                  <v-card-text>
                    {{ $t("BOOKING.PAYMENT.ADD_SUCCESS_DIALOG_TEXT") }}
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
                    {{ $t("SHARED.PLEASE_WAIT") }}</v-card-title
                  >
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
import KTCodePortlet from "@/views/partials/content/Portlet.vue";
import { SET_BREADCRUMB } from "@/store/breadcrumbs.module";
import ApiService from "@/common/api.service";
import VuetifyMoney from "../VuetifyMoney.vue";

export default {
  components: {
    KTCodePortlet,
    "vuetify-money": VuetifyMoney,
  },
  data() {
    return {
      form: {
        valid: true,
        dialog: false,
        loading: false,
        errors: [],
        bookingNumber: "",
        counterPaymentCode: null,
        counterPaymentCodeSearch: "",
        counterPaymentCodeLoading: false,
        counterPaymentCodeItems: [],
        total: 0,
        paid: 0,
        receive: 0,
        change: 0,
        remaining: 0,
        paymentDateMenu: false,
        paymentDate: this.getDateTimeNow()
          .toISOString()
          .substr(0, 10),
        paymentDateFormatted: this.formatDate(
          this.getDateTimeNow()
            .toISOString()
            .substr(0, 10)
        ),
        paymentTime: this.getDateTimeNow()
          .toTimeString()
          .substr(0, 5),
        paymentTimeMenu: false,
        bankCode: null,
        bankCodeSearch: "",
        bankCodeLoading: false,
        bankCodeItems: [],
        creditCardTypeCode: null,
        creditCardTypeCodeSearch: "",
        creditCardTypeCodeLoading: false,
        creditCardTypeCodeItems: [],
        merchantId: null,
        refCode: null,
        transactionId: null,
        picture: null,
        pictureType: ["image/jpeg", "image/png"],
        counterPaymentCodeRules: [
          (v) => !!v || this.$t("BOOKING.PAYMENT.COUNTER_PAYMENT_VALIDATION"),
        ],
        paidRules: [
          (v) => !!v || this.$t("BOOKING.PAYMENT.PAID_VALIDATION"),
          (v) => {
            let vr = v.replace(",", "");
            //console.log("vr : ", vr);
            return Number(vr) > 0 || this.$t("BOOKING.PAYMENT.PAID_GT_0");
          },
        ],
        receiveRules: [
          (v) => !!v || this.$t("BOOKING.PAYMENT.RECEIVE_VALIDATION"),
          (v) => {
            let vr = v.replace(",", "");
            return Number(vr) > 0 || this.$t("BOOKING.PAYMENT.RECEIVE_GT_0");
          },
          (v) => {
            //console.log("receiveRules ", Number(this.form.paid), Number(v));
            let vr = v.replace(",", "");
            return (
              Number(vr) >= Number(this.form.paid) ||
              this.$t("BOOKING.PAYMENT.RECEIVE_GT_PAID")
            );
          },
        ],
        paymentDateRules: [
          (v) => !!v || this.$t("BOOKING.PAYMENT.PAYMENT_DATE_VALIDATION"),
        ],
        paymentTimeRules: [
          (v) => !!v || this.$t("BOOKING.PAYMENT.PAYMENT_TIME_VALIDATION"),
        ],
        bankCodeRules: [
          (v) =>
            this.form.counterPaymentCode == null ||
            this.form.counterPaymentCode.value != "COUNTER_PAYMENT_TRANSFER" ||
            (this.form.counterPaymentCode.value == "COUNTER_PAYMENT_TRANSFER" &&
              !!v) ||
            this.$t("BOOKING.PAYMENT.BANK_VALIDATION"),
        ],
        creditCardTypeCodeRules: [
          (v) =>
            this.form.counterPaymentCode == null ||
            this.form.counterPaymentCode.value !=
              "COUNTER_PAYMENT_CREDIT_CARD" ||
            (this.form.counterPaymentCode.value ==
              "COUNTER_PAYMENT_CREDIT_CARD" &&
              !!v) ||
            this.$t("BOOKING.PAYMENT.CREDIT_CARD_TYPE_VALIDATION"),
        ],
        valueWhenIsEmptyPrice: "0", // "0" or "" or null
        valueOptionsPrice: {
          min: 0,
          minEvent: "CustomMinEvent",
          max: 100000000,
          maxEvent: "CustomMaxEvent",
        },
        optionsPrice: {
          locale: "en-US",
          prefix: "",
          suffix: "",
          length: 11,
          precision: 2,
        },
        propertiesPrice: {
          hint: "",
        },
      },
    };
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
      if (this.$refs.form.validate()) {
        if (this.form.picture) {
          if (!this.form.pictureType.includes(this.form.picture.type)) {
            this.form.errors.push(
              "กรุณาเลือกรูป Format .jpg, .jpeg, .png เท่านั้น"
            );
          }

          this.getFileBase64(this.form.picture).then((file) => {
            let mb = 2 * 1024 * 1024;
            if (file.size > mb)
              this.form.errors.push(
                "กรุณาเลือกรูปที่มีขนาดน้อยกว่าหรือเท่ากับ 2 MB"
              );

            let chk = true;
            if (this.form.errors.length > 0) {
              this.$vuetify.goTo(this.$refs.alert, {
                duration: 1000,
                easing: "easeInOutCubic",
                offset: -20,
              });
              chk = false;
            }
            if (chk) {
              this.postDataToApi(file);
            }
          });
        } else {
          let chk = true;
          if (this.form.errors.length > 0) {
            this.$vuetify.goTo(this.$refs.alert, {
              duration: 1000,
              easing: "easeInOutCubic",
              offset: -20,
            });
            chk = false;
          }

          if (chk) this.postDataToApi();
        }
      }
    },
    resetForm() {
      this.$refs.form.reset();
    },
    postDataToApi(file) {
      this.form.loading = true;
      this.form.errors = [];

      ApiService.setHeader();
      ApiService.post("/Api/Booking/Payment", {
        BookingId: this.$route.params.id,
        PaymentMethodCode: "PAYMENT_METHOD_COD",
        CounterPaymentCode: this.form.counterPaymentCode
          ? this.form.counterPaymentCode.value
          : null,
        Total: this.form.total,
        Paid: this.form.paid,
        Receive: this.form.receive,
        Change: this.form.change,
        Remaining: this.form.remaining,
        PaymentDate: new Date(
          this.form.paymentDate + " " + this.form.paymentTime
        ).getTime(),
        BankCode: this.form.bankCode ? this.form.bankCode.value : null,
        CreditCardTypeCode: this.form.creditCardTypeCode
          ? this.form.creditCardTypeCode.value
          : null,
        MerchantId: this.form.merchantId,
        RefCode: this.form.refCode,
        TransactionId: this.form.transactionId,
        Picture: file != null ? file.base64 : null,
        ContentType: file != null ? file.contentType : null,
      })
        .then(({ data }) => {
          if (data.status) {
            // close dialog
            this.form.dialog = true;
          } else {
            this.form.dialog = false;
            this.form.loading = false;
            this.form.errors.push(!!data.message || "Unknow error occurs");
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
      this.$router.push({ name: "searchBooking" });
    },
    getCounterPaymentCodeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Variables/Options", {
          Search: "COUNTER_PAYMENT",
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.counterPaymentCodeLoading = false;
          });
      });
    },
    getBankCodeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Variables/Options", {
          Search: "BANK",
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.counterPaymentCodeLoading = false;
          });
      });
    },
    getCreditCardTypeCodeFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Variables/Options", {
          Search: "CREDIT_CARD_TYPE",
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.counterPaymentCodeLoading = false;
          });
      });
    },
    getDataFromApi(id) {
      this.form.loading = true;
      return new Promise(() => {
        ApiService.setHeader();
        ApiService.get("/Api/Booking", id)
          .then(({ data }) => {
            if (!data.bookingNumber) {
              //OK CommandResult
              if (!data.status) {
                this.form.dialogText = data.message
                  ? data.message
                  : "Unknow error occurs";
                this.form.dialogHeader = "Result";
                this.form.dialog = true;
              }
            } else {
              this.form.bookingNumber = data.bookingNumber;
              this.form.total = data.remaining;
              this.form.paid = data.remaining;
            }
            //console.log(this.form);
          })
          .finally(() => {
            this.form.loading = false;
          });
      });
    },
    onPaidChange() {
      //console.log("total : ", this.form.total);
      //console.log("paid : ", this.form.paid);
      this.form.remaining = this.form.total - this.form.paid;
      //console.log("remaining : ", this.form.remaining);
      //console.log("===========");
      this.onReceiveChange();
      this.$refs.form.validate();
    },
    onReceiveChange() {
      //console.log("receive : ", this.form.receive);
      //console.log("paid : ", this.form.paid);
      this.form.change = this.form.receive - this.form.paid;
      //console.log("change : ", this.form.change);
      //console.log("===========");
      this.$refs.form.validate();
    },
    getDateTimeNow() {
      return new Date();
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("MENU.BOOKING.SECTION"), route: "/booking" },
      { title: this.$t("MENU.BOOKING.PAYMENT") },
    ]);
    this.getDataFromApi(this.$route.params.id);
  },
  computed: {
    title() {
      return this.$t("MENU.BOOKING.PAYMENT");
    },
    computedPaymentDateFormatted() {
      return this.formatDate(this.form.paymentDate);
    },
  },
  watch: {
    "form.counterPaymentCodeSearch": {
      handler() {
        this.getCounterPaymentCodeFromApi().then((data) => {
          this.form.counterPaymentCodeItems = data;
        });
      },
    },
    "form.bankCodeSearch": {
      handler() {
        this.getBankCodeFromApi().then((data) => {
          this.form.bankCodeItems = data;
        });
      },
    },
    "form.creditCardTypeCodeSearch": {
      handler() {
        this.getCreditCardTypeCodeFromApi().then((data) => {
          this.form.creditCardTypeCodeItems = data;
        });
      },
    },
    "form.paymentDate": {
      handler() {
        this.form.paymentDateFormatted = this.formatDate(this.form.paymentDate);
      },
    },
    "form.paid": {
      handler() {
        this.onPaidChange();
      },
    },
    "form.receive": {
      handler() {
        this.onReceiveChange();
      },
    },
  },
};
</script>

<style lang="scss" scoped></style>
