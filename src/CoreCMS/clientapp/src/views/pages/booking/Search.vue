<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <KTCodePortlet v-bind:title="title">
          <template v-slot:body>
            <v-form ref="form" @submit.prevent="submitForm">
              <div class="row">
                <div class="col-4">
                  <v-text-field
                    v-model="form.searchKeyword"
                    :disabled="datatable.loading"
                    :label="$t('SHARED.KEYWORD')"
                    :hint="$t('BOOKING.SEARCH.KEYWORD_HINT')"
                    prepend-icon="mdi-file-document-box-search-outline"
                  ></v-text-field>
                </div>
                <div class="col-4">
                  <v-autocomplete
                    v-model="form.placeId"
                    :disabled="datatable.loading"
                    :items="form.placeItems"
                    :loading="form.placeLoading"
                    :search-input.sync="form.placeSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('BOOKING.SEARCH.PLACE_NAME_TH')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    @change="onPlaceChange()"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-4">
                  <v-autocomplete
                    v-model="form.roomId"
                    :disabled="datatable.loading"
                    :items="form.roomItems"
                    :loading="form.roomLoading"
                    :search-input.sync="form.roomSearch"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('BOOKING.SEARCH.ROOM_NAME_TH')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-4">
                  <v-dialog
                    ref="dialogStart"
                    v-model="form.startDateModel"
                    :disabled="form.loading"
                    :return-value.sync="form.startDate"
                    persistent
                    width="290px"
                  >
                    <template v-slot:activator="{ on }">
                      <v-text-field
                        v-model="computedStartDateFormatted"
                        :disabled="form.loading"
                        :label="$t('BOOKING.SEARCH.BOOKING_START_DATE')"
                        hint="DD/MM/YYYY format"
                        persistent-hint
                        prepend-icon="event"
                        readonly
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="form.startDate"
                      locale="th"
                      :disabled="form.loading"
                      scrollable
                    >
                      <v-spacer></v-spacer>
                      <v-btn
                        text
                        color="default"
                        @click="form.startDateModel = false"
                        >{{ $t("SHARED.CANCEL_BUTTON") }}</v-btn
                      >
                      <v-btn
                        text
                        color="primary"
                        @click="$refs.dialogStart.save(form.startDate)"
                        >{{ $t("SHARED.CHOOSE_BUTTON") }}</v-btn
                      >
                    </v-date-picker>
                  </v-dialog>
                </div>
                <div class="col-4">
                  <v-dialog
                    ref="dialogEnd"
                    v-model="form.endDateModel"
                    :disabled="form.loading"
                    :return-value.sync="form.endDate"
                    persistent
                    width="290px"
                  >
                    <template v-slot:activator="{ on }">
                      <v-text-field
                        v-model="computedEndDateFormatted"
                        :disabled="form.loading"
                        :label="$t('BOOKING.SEARCH.BOOKING_END_DATE')"
                        hint="DD/MM/YYYY format"
                        persistent-hint
                        prepend-icon="event"
                        readonly
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="form.endDate"
                      locale="th"
                      :disabled="form.loading"
                      scrollable
                    >
                      <v-spacer></v-spacer>
                      <v-btn
                        text
                        color="default"
                        @click="form.endDateModel = false"
                        >{{ $t("SHARED.CANCEL_BUTTON") }}</v-btn
                      >
                      <v-btn
                        text
                        color="primary"
                        @click="$refs.dialogEnd.save(form.endDate)"
                        >{{ $t("SHARED.CHOOSE_BUTTON") }}</v-btn
                      >
                    </v-date-picker>
                  </v-dialog>
                </div>
                <div class="col-4">
                  <v-autocomplete
                    v-model="form.inActiveStatus"
                    :disabled="datatable.loading"
                    :items="form.inActiveStatusItems"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('BOOKING.SEARCH.IN_ACTIVE_STATUS')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                  ></v-autocomplete>
                </div>
                <div class="col-4">
                  <v-autocomplete
                    v-model="form.isDeleted"
                    :disabled="datatable.loading"
                    :items="form.isDeletedItems"
                    hide-no-data
                    hide-selected
                    item-text="text"
                    item-value="value"
                    :label="$t('BOOKING.SEARCH.IS_DELETED')"
                    :placeholder="$t('SHARED.START_TYPING_TO_SEARCH')"
                    prepend-icon="mdi-database-search"
                    return-object
                    clearable
                    @change="onIsDeletedChange"
                  ></v-autocomplete>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <v-btn
                    :disabled="datatable.loading"
                    color="success"
                    class="mr-4"
                    tile
                    type="submit"
                  >
                    <v-icon v-if="!datatable.loading" left
                      >mdi-database-search</v-icon
                    >
                    <v-icon v-if="datatable.loading" left
                      >fa fa-spinner fa-spin</v-icon
                    >
                    {{ $t("SHARED.SEARCH_BUTTON") }}
                  </v-btn>
                  <v-btn
                    :disabled="datatable.loading"
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
              </div>
            </v-form>

            <v-divider class="my-4"></v-divider>

            <v-subheader
              ><v-icon left>mdi-table-search</v-icon>
              {{ $t("SHARED.SEARCH_RESULT") }}
              <v-spacer></v-spacer>
            </v-subheader>

            <v-data-table
              :headers="datatable.headers"
              :items="datatable.items"
              :loading="datatable.loading"
              :options.sync="datatable.options"
              :server-items-length="datatable.total"
              :footer-props="{
                'items-per-page-options': [30, 50, 100],
              }"
              multi-sort
            >
              <template v-slot:item.bookingId="{ item }">
                <v-icon
                  medium
                  class="mr-2"
                  @click="detailItem(item.bookingId)"
                  :title="$t('BOOKING.SEARCH.DETAIL_TITLE')"
                >
                  la la-file-text
                </v-icon>

                <v-icon
                  medium
                  class="mr-2"
                  v-if="
                    item.bookingStatusCode == 'BOOKING_STATUS_RESERVE' &&
                      item.paymentMethodCode == 'PAYMENT_METHOD_COD'
                  "
                  @click="paymentItem(item.bookingId)"
                  :title="$t('BOOKING.SEARCH.PAYMENT_TITLE')"
                >
                  fas fa-money-check-alt
                </v-icon>

                <v-icon
                  medium
                  class="mr-2"
                  v-if="item.isCheckIn"
                  @click="checkInItem(item.bookingId)"
                  :title="$t('BOOKING.SEARCH.CHECK_IN_TITLE')"
                >
                  check_circle
                </v-icon>

                <v-icon
                  medium
                  class="mr-2"
                  v-if="item.isCheckOut"
                  @click="checkOutItem(item.bookingId)"
                  :title="$t('BOOKING.SEARCH.CHECK_OUT_TITLE')"
                >
                  cancel
                </v-icon>

                <v-icon
                  medium
                  class="mr-2"
                  v-if="item.isCancel"
                  @click="cancelItem(item.bookingId)"
                  :title="$t('BOOKING.SEARCH.CANCEL_TITLE')"
                >
                  fas fa-trash-alt
                </v-icon>

                <v-icon
                  medium
                  class="mr-2"
                  v-if="item.isRefund"
                  @click="refundItem(item.bookingId)"
                  :title="$t('BOOKING.SEARCH.REFUND_TITLE')"
                >
                  fas fa-user-check
                </v-icon>
              </template>
            </v-data-table>

            <v-dialog
              v-model="datatable.loading"
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

            <v-dialog
              v-model="datatable.checkInDialog"
              persistent
              max-width="300"
            >
              <v-card>
                <v-card-title class="headline">
                  {{ $t("BOOKING.SEARCH.CHECK_IN_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.checkInDialogText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="datatable.checkInDialog = false"
                    >{{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmCheckInItem()"
                    >{{ $t("SHARED.CONFIRM_BUTTON") }}</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog
              v-model="datatable.checkOutDialog"
              persistent
              max-width="300"
            >
              <v-card>
                <v-card-title class="headline">
                  {{ $t("BOOKING.SEARCH.CHECK_OUT_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.checkOutDialogText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="datatable.checkOutDialog = false"
                    >{{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmCheckOutItem()"
                    >{{ $t("SHARED.CONFIRM_BUTTON") }}</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog
              v-model="datatable.cancelDialog"
              persistent
              max-width="300"
            >
              <v-card>
                <v-card-title class="headline">
                  {{ $t("BOOKING.SEARCH.CANCEL_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.cancelDialogText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="datatable.cancelDialog = false"
                    >{{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmCancelItem()"
                    >{{ $t("SHARED.CONFIRM_BUTTON") }}</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog
              v-model="datatable.refundDialog"
              persistent
              max-width="300"
            >
              <v-card>
                <v-card-title class="headline">
                  {{ $t("BOOKING.SEARCH.REFUND_DIALOG_HEADER") }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.refundDialogText }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="grey darken-1"
                    text
                    @click="datatable.refundDialog = false"
                    >{{ $t("SHARED.CANCEL_BUTTON") }}
                  </v-btn>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="confirmRefundItem()"
                    >{{ $t("SHARED.CONFIRM_BUTTON") }}</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>

            <v-dialog v-model="datatable.dialog" persistent max-width="300">
              <v-card>
                <v-card-title class="headline">
                  {{ datatable.dialogHeader }}
                </v-card-title>
                <v-card-text>
                  {{ datatable.dialogText }}
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
        searchKeyword: "",
        placeId: null,
        placeItems: [],
        placeSearch: "",
        placeLoading: false,
        roomId: null,
        roomItems: [],
        roomSearch: "",
        roomLoading: false,
        startDate: new Date().toISOString().substr(0, 10),
        startDateModel: false,
        startDateFormatted: this.formatDate(
          new Date().toISOString().substr(0, 10)
        ),
        endDate: this.getEndDate()
          .toISOString()
          .substr(0, 10),
        endDateModel: false,
        endDateFormatted: this.formatDate(
          this.getEndDate()
            .toISOString()
            .substr(0, 10)
        ),
        inActiveStatus: false,
        inActiveStatusItems: [
          {
            text: this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_YES"),
            value: false,
          },
          {
            text: this.$t("SYS_VARIABLE.IN_ACTIVE_STATUS_NO"),
            value: true,
          },
        ],
        isDeleted: false,
        isDeletedItems: [
          {
            text: this.$t("SYS_VARIABLE.IS_DELETED_NO"),
            value: false,
          },
          {
            text: this.$t("SYS_VARIABLE.IS_DELETED_YES"),
            value: true,
          },
        ],
      },
      datatable: {
        headers: [
          {
            text: this.$t("BOOKING.SEARCH.BOOKING_NUMBER"),
            value: "bookingNumber",
            align: "center",
          },
          {
            text: this.$t("BOOKING.SEARCH.ROOM_NAME_TH"),
            value: "roomNameTH",
          },
          {
            text: this.$t("BOOKING.SEARCH.PLACE_NAME_TH"),
            value: "placeNameTH",
          },
          {
            text: this.$t("BOOKING.SEARCH.BOOKING_START_DATE"),
            value: "bookingStartDateString",
            align: "center",
          },
          {
            text: this.$t("BOOKING.SEARCH.BOOKING_END_DATE"),
            value: "bookingEndDateString",
            align: "center",
          },
          {
            text: this.$t("BOOKING.SEARCH.PAYMENT_METHOD_NAME"),
            value: "paymentMethodName",
            align: "center",
          },
          {
            text: this.$t("BOOKING.SEARCH.BOOKING_STATUS_NAME"),
            value: "bookingStatusName",
            align: "center",
          },
          {
            text: this.$t("BOOKING.SEARCH.CUSTOMER_NAME"),
            value: "customerName",
            align: "center",
          },
          {
            text: this.$t("BOOKING.SEARCH.ACTION"),
            value: "bookingId",
            align: "center",
            sortable: false,
            width: "10%",
          },
        ],
        items: [],
        total: 0,
        loading: true,
        options: {
          sortBy: ["bookingNumber"],
          sortDesc: [true],
          itemsPerPage: 30,
        },
        dialog: false,
        dialogText: null,
        dialogHeader: null,
        checkInDialog: false,
        checkInDialogText: null,
        checkInDialogId: null,
        checkOutDialog: false,
        checkOutDialogText: null,
        checkOutDialogId: null,
        cancelDialog: false,
        cancelDialogText: null,
        cancelDialogId: null,
        refundDialog: false,
        refundDialogText: null,
        refundDialogId: null,
      },
    };
  },
  methods: {
    submitForm() {
      this.getDataFromApi().then((data) => {
        this.datatable.total = data.total;
        this.datatable.items = data.items;
      });
    },
    resetForm() {
      //this.$refs.form.reset();
      this.form.searchKeyword = "";
      this.form.placeId = null;
      this.form.roomId = null;
      this.form.startDate = new Date().toISOString().substr(0, 10);
      this.form.startDateFormatted = this.formatDate(
        new Date().toISOString().substr(0, 10)
      );
      this.form.endDate = this.getEndDate()
        .toISOString()
        .substr(0, 10);
      this.form.endDateFormatted = this.formatDate(
        this.getEndDate()
          .toISOString()
          .substr(0, 10)
      );
      this.form.inActiveStatus = false;
      this.form.isDeleted = false;
      this.submitForm();
    },
    getDataFromApi() {
      this.datatable.loading = true;
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Booking/Search", {
          GroupBy: this.datatable.options.groupBy,
          GroupDesc: this.datatable.options.groupDesc,
          ItemsPerPage: this.datatable.options.itemsPerPage,
          Page: this.datatable.options.page,
          SortBy: this.datatable.options.sortBy,
          SortDesc: this.datatable.options.sortDesc,
          SearchKeyword: this.form.searchKeyword,
          PlaceId: this.form.placeId ? this.form.placeId.value : null,
          RoomId: this.form.roomId ? this.form.roomId.value : null,
          InActiveStatus: this.form.inActiveStatus
            ? this.form.inActiveStatus.value
            : false,
          StartDate: this.form.startDateFormatted,
          EndDate: this.form.endDateFormatted,
          IsDeleted: this.form.isDeleted ? this.form.isDeleted.value : false,
        })
          .then(({ data }) => {
            if (!data.total && !data.items) {
              //OK CommandResult
              if (!data.status) {
                this.datatable.dialogText = data.message
                  ? data.message
                  : "Unknow error occurs";
                this.datatable.dialogHeader = "Result";
                this.datatable.dialog = true;
              }
            }
            resolve({
              items: data.items,
              total: data.total,
            });
          })
          .finally(() => {
            this.datatable.loading = false;
          });
      });
    },
    getPlaceFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.query("/Api/Place/Select2Place", {
          Search: this.form.placeSearch,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.placeLoading = false;
          });
      });
    },
    getRoomFromApi() {
      return new Promise((resolve) => {
        ApiService.setHeader();
        ApiService.post("/Api/Room/Select2Room", {
          Search: this.form.roomSearch,
          PlaceId: this.form.placeId ? this.form.placeId.value : null,
        })
          .then(({ data }) => {
            resolve(data);
          })
          .finally(() => {
            this.form.roomLoading = false;
          });
      });
    },
    closeDialog() {
      this.datatable.dialog = false;
      this.datatable.dialogText = null;
      this.datatable.dialogHeader = null;
      this.submitForm();
    },
    detailItem(id) {
      this.$router.push({ name: "detailBooking", params: { id } });
    },
    paymentItem(id) {
      this.$router.push({ name: "paymentBooking", params: { id } });
    },
    checkInItem(id) {
      this.datatable.checkInDialog = true;
      this.datatable.checkInDialogId = id;
      this.datatable.checkInDialogText = this.$t(
        "BOOKING.SEARCH.CHECK_IN_DIALOG_TEXT"
      );
    },
    confirmCheckInItem() {
      this.datatable.dialogHeader = this.$t(
        "BOOKING.SEARCH.CHECK_IN_RESULT_DIALOG_HEADER"
      );
      ApiService.setHeader();
      ApiService.update(
        "/Api/Booking",
        this.datatable.checkInDialogId + "/CheckIn"
      )
        .then(({ data }) => {
          //console.log(data);
          this.datatable.dialog = true;
          if (data.message) {
            this.datatable.dialogText = data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        })
        .catch(({ response }) => {
          //console.log(response);
          this.datatable.dialog = true;
          if (response.data) {
            this.datatable.dialogText = response.data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        });

      this.datatable.checkInDialogText = null;
      this.datatable.checkInDialogId = null;
      this.datatable.checkInDialog = false;
      //this.submitForm();
    },
    checkOutItem(id) {
      this.datatable.checkOutDialog = true;
      this.datatable.checkOutDialogId = id;
      this.datatable.checkOutDialogText = this.$t(
        "BOOKING.SEARCH.CHECK_OUT_DIALOG_TEXT"
      );
    },
    confirmCheckOutItem() {
      this.datatable.dialogHeader = this.$t(
        "BOOKING.SEARCH.CHECK_OUT_RESULT_DIALOG_HEADER"
      );
      ApiService.setHeader();
      ApiService.update(
        "/Api/Booking",
        this.datatable.checkOutDialogId + "/CheckOut"
      )
        .then(({ data }) => {
          //console.log(data);
          this.datatable.dialog = true;
          if (data.message) {
            this.datatable.dialogText = data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        })
        .catch(({ response }) => {
          //console.log(response);
          this.datatable.dialog = true;
          if (response.data) {
            this.datatable.dialogText = response.data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        });

      this.datatable.checkOutDialogText = null;
      this.datatable.checkOutDialogId = null;
      this.datatable.checkOutDialog = false;
      //this.submitForm();
    },
    cancelItem(id) {
      this.datatable.cancelDialog = true;
      this.datatable.cancelDialogId = id;
      this.datatable.cancelDialogText = this.$t(
        "BOOKING.SEARCH.CANCEL_DIALOG_TEXT"
      );
    },
    confirmCancelItem() {
      this.datatable.dialogHeader = this.$t(
        "BOOKING.SEARCH.CANCEL_RESULT_DIALOG_HEADER"
      );
      ApiService.setHeader();
      ApiService.update(
        "/Api/Booking",
        this.datatable.cancelDialogId + "/Cancel"
      )
        .then(({ data }) => {
          //console.log(data);
          this.datatable.dialog = true;
          if (data.message) {
            this.datatable.dialogText = data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        })
        .catch(({ response }) => {
          //console.log(response);
          this.datatable.dialog = true;
          if (response.data) {
            this.datatable.dialogText = response.data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        });

      this.datatable.cancelDialogText = null;
      this.datatable.cancelDialogId = null;
      this.datatable.cancelDialog = false;
      //this.submitForm();
    },
    refundItem(id) {
      this.datatable.refundDialog = true;
      this.datatable.refundDialogId = id;
      this.datatable.refundDialogText = this.$t(
        "BOOKING.SEARCH.REFUND_DIALOG_TEXT"
      );
    },
    confirmRefundItem() {
      this.datatable.dialogHeader = this.$t(
        "BOOKING.SEARCH.REFUND_RESULT_DIALOG_HEADER"
      );
      ApiService.setHeader();
      ApiService.update(
        "/Api/Booking",
        this.datatable.refundDialogId + "/Refund"
      )
        .then(({ data }) => {
          //console.log(data);
          this.datatable.dialog = true;
          if (data.message) {
            this.datatable.dialogText = data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        })
        .catch(({ response }) => {
          //console.log(response);
          this.datatable.dialog = true;
          if (response.data) {
            this.datatable.dialogText = response.data.message;
          } else {
            this.datatable.dialogText = "Unknow error occurs";
          }
        });

      this.datatable.refundDialogText = null;
      this.datatable.refundDialogId = null;
      this.datatable.refundDialog = false;
      //this.submitForm();
    },
    formatDate(date) {
      if (!date) return null;

      const [year, month, day] = date.split("-");
      return `${day}/${month}/${year}`;
    },
    getEndDate() {
      var endDate = new Date();
      endDate.setDate(endDate.getDate() + 7);
      return endDate;
    },
    onPlaceChange() {
      this.form.roomId = null;
      this.getRoomFromApi().then((data) => {
        this.form.roomItems = data;
      });
    },
    onIsDeletedChange() {
      this.form.inActiveStatus = this.form.isDeleted;
    },
  },
  mounted() {
    this.$store.dispatch(SET_BREADCRUMB, [
      { title: this.$t("MENU.BOOKING.SECTION"), route: "/booking" },
      { title: this.$t("MENU.BOOKING.SEARCH") },
    ]);
  },
  computed: {
    title() {
      return this.$t("MENU.BOOKING.SEARCH");
    },
    computedStartDateFormatted() {
      return this.formatDate(this.form.startDate);
    },
    computedEndDateFormatted() {
      return this.formatDate(this.form.endDate);
    },
  },
  watch: {
    "datatable.options": {
      handler() {
        this.getDataFromApi().then((data) => {
          this.datatable.total = data.total;
          this.datatable.items = data.items;
        });
      },
      deep: true,
    },
    "form.placeSearch": {
      handler() {
        this.getPlaceFromApi().then((data) => {
          this.form.placeItems = data;
        });
      },
    },
    "form.roomSearch": {
      handler() {
        this.getRoomFromApi().then((data) => {
          this.form.roomItems = data;
        });
      },
    },
    "form.startDate": {
      handler() {
        this.form.startDateFormatted = this.formatDate(this.form.startDate);
      },
    },
    "form.endDate": {
      handler() {
        this.form.endDateFormatted = this.formatDate(this.form.endDate);
      },
    },
  },
};
</script>

<style lang="scss" scoped></style>
