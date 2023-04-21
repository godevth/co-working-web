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
            <v-row>
              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.BOOKING_NUMBER") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.bookingNumber
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.ROOM_NAME_TH") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.roomNameTH
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.ROOM_CAPACITY") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.roomCapacity
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.PLACE_NAME_TH") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.placeNameTH
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.PAYMENT_METHOD_NAME") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.paymentMethodName
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.BOOKING_STATUS_NAME") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.bookingStatusName
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.CUSTOMER_NAME") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.customerName
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.CUSTOMER_EMAIL") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.customerEmail
                }}</span>
              </v-col>

              <v-col cols="12" md="4">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.CUSTOMER_PHONE_NUMBER") }} :</label
                >
                <span class="form-control-plaintext">{{
                  form.customerPhoneNumber
                }}</span>
              </v-col>

              <v-col cols="12" md="12">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.REMARK") }} :</label
                >
                <span class="form-control-plaintext">{{ form.remark }}</span>
              </v-col>

              <v-col cols="12" md="12">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.QR_CODE") }} :</label
                >

                <v-dialog v-model="form.qrCodeDialog" width="500">
                  <template v-slot:activator="{ on, attrs }">
                    <v-flex>
                      <img
                        :src="form.qrCode"
                        width="20%"
                        v-on="on"
                        v-bind="attrs"
                      />
                    </v-flex>
                  </template>

                  <v-card>
                    <v-card-title class="headline grey lighten-2">
                      {{ $t("BOOKING.ADD_EDIT.QR_CODE") }}
                    </v-card-title>

                    <v-card-text>
                      <img :src="form.qrCode" width="100%" />
                    </v-card-text>

                    <v-card-text>
                      <v-row>
                        <v-col cols="12" md="5" offset="1">
                          <label class="label">
                            <b
                              >{{ $t("BOOKING.ADD_EDIT.BOOKING_STATUS_NAME") }}
                              :
                            </b>
                            {{ form.bookingStatusName }}
                          </label>
                        </v-col>
                        <v-col
                          cols="12"
                          md="5"
                          offset="1"
                          v-if="form.remaining > 0"
                        >
                          <label class="label text-danger">
                            <b>{{ $t("BOOKING.ADD_EDIT.REMAINING") }} : </b>
                            {{ form.remaining }}
                          </label>
                        </v-col>
                        <v-col
                          cols="12"
                          md="5"
                          offset="1"
                          v-if="form.lastPaymentPaid"
                        >
                          <label class="label">
                            <b
                              >{{ $t("BOOKING.ADD_EDIT.LAST_PAYMENT_PAID") }} :
                            </b>
                            {{ form.lastPaymentPaid }}
                          </label>
                        </v-col>
                        <v-col
                          cols="12"
                          md="5"
                          offset="1"
                          v-if="form.lastPaymentDate"
                        >
                          <label class="label">
                            <b
                              >{{ $t("BOOKING.ADD_EDIT.LAST_PAYMENT_DATE") }} :
                            </b>
                            {{ form.lastPaymentDate }}
                          </label>
                        </v-col>
                      </v-row>
                    </v-card-text>

                    <v-divider></v-divider>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="green darken-1"
                        text
                        @click="form.qrCodeDialog = false"
                        >{{ $t("SHARED.CLOSE_BUTTON") }}</v-btn
                      >
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-col>

              <v-col cols="12" md="12">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.BOOKING_DATES") }} :</label
                >
                <v-data-table
                  :headers="form.bookingDatesHeaders"
                  :items="form.bookingDates"
                  :items-per-page="5"
                  :sort-by="['startDate']"
                  :sort-desc="[false]"
                  class="elevation-1"
                ></v-data-table>
              </v-col>

              <v-col cols="12" md="12">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.BOOKING_FACILITYS") }} :</label
                >
                <v-data-table
                  :headers="form.bookingFacilitiesHeaders"
                  :items="form.bookingFacilities"
                  :items-per-page="5"
                  :sort-by="['facilityName']"
                  :sort-desc="[false]"
                  class="elevation-1"
                >
                  <template v-slot:item="{ item }">
                    <tr>
                      <td>{{ item.facilityName }}</td>
                      <td class="text-center">{{ item.priceFormat }}</td>
                      <td class="text-center">{{ item.qty }}</td>
                      <td class="text-center">{{ item.totalFormat }}</td>
                    </tr>
                  </template>
                  <template slot="body.append">
                    <tr>
                      <th></th>
                      <th class="text-center">
                        {{ $t("BOOKING.ADD_EDIT.SUM") }}
                      </th>
                      <th class="text-center">{{ sumFieldFacility("qty") }}</th>
                      <th class="text-center">
                        {{ form.facilityTotal }}
                      </th>
                    </tr>
                  </template>
                </v-data-table>
              </v-col>

              <v-col cols="12" md="12">
                <label class="label"
                  >{{ $t("BOOKING.ADD_EDIT.PRICE_DESC") }} :</label
                >
                <v-data-table
                  :headers="form.bookingPricesHeaders"
                  :items="form.bookingPrices"
                  class="elevation-1"
                  hide-default-footer
                >
                  <template v-slot:item="{ item }">
                    <tr>
                      <td class="text-center">{{ item.priceTimeType }}</td>
                      <td class="text-center">{{ item.priceQty }}</td>
                      <td class="text-center">{{ item.priceFormat }}</td>
                      <td class="text-center">{{ item.qty }}</td>
                      <td class="text-center">{{ item.totalFormat }}</td>
                    </tr>
                    <tr>
                      <th colspan="2"></th>
                      <th class="text-center">
                        {{ $t("BOOKING.ADD_EDIT.SUM") }}
                      </th>
                      <th class="text-center">{{ item.qty }}</th>
                      <th class="text-center">
                        {{ item.totalFormat }}
                      </th>
                    </tr>
                  </template>
                  <template slot="body.append">
                    <tr>
                      <th colspan="3"></th>
                      <th class="text-center">
                        {{ $t("BOOKING.ADD_EDIT.DISCOUNT") }}
                      </th>
                      <th class="text-center">{{ form.discount }}</th>
                    </tr>
                    <tr>
                      <th colspan="3"></th>
                      <th class="text-center">
                        {{ $t("BOOKING.ADD_EDIT.TOTAL") }}
                      </th>
                      <th class="text-center">{{ form.total }}</th>
                    </tr>
                    <tr>
                      <th colspan="3"></th>
                      <th class="text-center">
                        {{ $t("BOOKING.ADD_EDIT.TAX") }}
                      </th>
                      <th class="text-center">{{ form.tax }}</th>
                    </tr>
                    <tr>
                      <th colspan="3"></th>
                      <th class="text-center">
                        {{ $t("BOOKING.ADD_EDIT.GRAND_TOTAL") }}
                      </th>
                      <th class="text-center">{{ form.grandTotal }}</th>
                    </tr>
                  </template>
                </v-data-table>
              </v-col>
            </v-row>
            <v-row>
              <div class="col-12">
                <v-btn
                  :disabled="form.loading"
                  color="default"
                  class="mr-4"
                  tile
                  @click.prevent="goToPage"
                >
                  <v-icon left>la-close</v-icon>
                  {{ $t("SHARED.BACK_BUTTON") }}
                </v-btn>
              </div>
            </v-row>

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

            <v-dialog v-model="form.dialog" persistent max-width="300">
              <v-card>
                <v-card-title class="headline">
                  {{ form.dialogHeader }}
                </v-card-title>
                <v-card-text>
                  {{ form.dialogText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="green darken-1" text @click="closeDialog">{{
                    $t("SHARED.CLOSE_BUTTON")
                  }}</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
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
        dialogText: null,
        dialogHeader: null,
        errors: [],
        bookingNumber: null,
        roomNameTH: null,
        roomCapacity: null,
        placeNameTH: null,
        priceTimeType: null,
        priceQty: null,
        price: null,
        bookingStatusName: null,
        paymentMethodName: null,
        qty: null,
        isOwner: null,
        customerEmail: null,
        customerName: null,
        customerPhoneNumber: null,
        total: null,
        tax: null,
        discount: null,
        grandTotal: null,
        remaining: null,
        remark: null,
        bookingDates: [],
        bookingFacilities: [],
        bookingPrices: [],
        qrCode: null,
        facilityTotal: null,
        bookingDatesHeaders: [
          {
            text: this.$t("BOOKING.ADD_EDIT.BOOKING_START_DATE"),
            align: "center",
            value: "startDate",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.BOOKING_END_DATE"),
            align: "center",
            value: "endDate",
          },
        ],
        bookingFacilitiesHeaders: [
          {
            text: this.$t("BOOKING.ADD_EDIT.FACILITY_NAME"),
            align: "start",
            value: "facilityName",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.FACILITY_PRICE"),
            align: "center",
            value: "price",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.FACILITY_QTY"),
            align: "center",
            value: "qty",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.FACILITY_TOTAL"),
            align: "center",
            value: "total",
          },
        ],
        bookingPricesHeaders: [
          {
            text: this.$t("BOOKING.ADD_EDIT.PRICE_TIME_TYPE"),
            align: "center",
            sortable: false,
            value: "priceTimeType",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.PRICE_QTY"),
            align: "center",
            sortable: false,
            value: "priceQty",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.PRICE"),
            align: "center",
            sortable: false,
            value: "price",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.QTY"),
            align: "center",
            sortable: false,
            value: "qty",
          },
          {
            text: this.$t("BOOKING.ADD_EDIT.PRICE_TOTAL"),
            align: "center",
            sortable: false,
            value: "total",
          },
        ],
        qrCodeDialog: false,
      },
    };
  },
  methods: {
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
              this.form.roomNameTH = data.roomNameTH;
              this.form.roomCapacity = data.roomCapacity;
              this.form.placeNameTH = data.placeNameTH;
              this.form.priceTimeType = data.priceTimeType;
              this.form.priceQty = data.priceQty;
              this.form.price = data.price;
              this.form.bookingStatusName = data.bookingStatusName;
              this.form.paymentMethodName = data.paymentMethodName;
              this.form.qty = data.qty;
              this.form.isOwner = data.isOwner;
              this.form.customerEmail = data.customerEmail;
              this.form.customerName = data.customerName;
              this.form.customerPhoneNumber = data.customerPhoneNumber;
              this.form.total = data.totalFormat;
              this.form.tax = data.taxFormat;
              this.form.discount = data.discountFormat;
              this.form.grandTotal = data.grandTotalFormat;
              this.form.remark = data.remark;
              this.form.remaining = data.remaining;
              this.form.lastPaymentDate = data.lastPaymentDateString;
              this.form.lastPaymentPaid = data.lastPaymentPaid;
              this.form.qrCode = "data:image/jpeg;base64," + data.qrCode;
              this.form.facilityTotal = data.facilityTotalFormat;

              //bookingDates
              data.bookingDates.forEach((bd) => {
                this.form.bookingDates.push({
                  startDate: bd.startDateString,
                  endDate: bd.endDateString,
                  openDay: bd.openDay,
                  openTime: bd.openTime,
                  closeTime: bd.closeTime,
                });
              });

              //bookingFacilities
              data.bookingFacilities.forEach((bf) => {
                this.form.bookingFacilities.push({
                  facilityName: bf.facilityName,
                  price: bf.price,
                  priceFormat: bf.priceFormat,
                  qty: bf.qty,
                  total: bf.total,
                  totalFormat: bf.totalFormat,
                });
              });

              //bookingPrices
              this.form.bookingPrices.push({
                priceTimeType: data.priceTimeType,
                price: data.price,
                priceFormat: data.priceFormat,
                priceQty: data.priceQty,
                qty: data.qty,
                total: data.priceTotal,
                totalFormat: data.priceTotalFormat,
              });
            }
            //console.log(this.form);
          })
          .finally(() => {
            this.form.loading = false;
          });
      });
    },
    goToPage() {
      this.$router.push({ name: "booking" });
    },
    sumFieldFacility(key) {
      // sum data in give key (property)
      return this.form.bookingFacilities.reduce((a, b) => a + (b[key] || 0), 0);
    },
    closeDialog() {
      this.form.dialog = false;
      this.form.dialogText = null;
      this.form.dialogHeader = null;
      this.$router.push({ name: "booking" });
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("MENU.BOOKING.SECTION"), route: "/booking" },
      { title: this.$t("MENU.BOOKING.DETAIL") },
    ]);
    this.getDataFromApi(this.$route.params.id);
  },
  computed: {
    title() {
      return this.$t("MENU.BOOKING.DETAIL");
    },
  },
};
</script>

<style lang="scss" scoped></style>
