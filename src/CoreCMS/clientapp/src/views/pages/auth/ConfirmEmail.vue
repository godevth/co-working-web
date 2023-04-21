<template>
  <div class="kt-login__container">
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
    </div>
    <div class="kt-login__logo">
      <a href="#">
        <img src="assets/media/logos/logo-mini-lg-dark.png" />
      </a>
    </div>
    <div class="kt-login__signin">
      <div class="kt-login__head">
        <h3 class="kt-login__title">{{ $t("AUTH.CONFIRM_EMAIL.TITLE") }}</h3>
      </div>
      <v-form
        class="kt-form"
        ref="form"
        lazy-validation
        @submit.prevent="onSubmit"
      >
        <div class="row kt-login__actions">
          <div class="col-12">
            <button class="btn btn-primary btn-block" type="button" @click="goToApp">
              <i class="la la-sign-in"></i> {{ $t("AUTH.CONFIRM_EMAIL.BACK_BUTTON") }}
            </button>
          </div>
        </div>

        <v-dialog v-model="form.dialog" persistent max-width="300">
          <v-card>
            <v-card-title class="headline">
              {{
                $t("AUTH.CONFIRM_EMAIL.SUCCESS_DIALOG_HEADER")
              }}</v-card-title
            >
            <v-card-text>
                {{ $t("AUTH.CONFIRM_EMAIL.SUCCESS_DIALOG_TEXT") }}
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="green darken-1"
                text
                @click="goToApp"
                >{{ $t("SHARED.CLOSE_BUTTON") }}</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-form>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.kt-spinner.kt-spinner--right:before {
  right: 8px;
}
</style>

<script>
import ApiService from "@/common/api.service";

export default {
  name: "login",
  data() {
    return {
      // Remove this dummy login info
      form: {
        dialog: false,
        loading: false,
        errors: [],
        userId: null,
        randomCode: null,
      },
    };
  },
  methods: {
    onSubmit() {
      // send login request
      ApiService.post("/Api/User/ConfirmEmail", {
        UserId: this.form.userId,
        RandomCode: this.form.randomCode
      })
      .then(({ data }) => {
        if (data.status) {
          // close dialog
          this.form.dialog = true;
        } else {
          this.form.dialog = false;
          this.form.loading = false;
          this.form.errors.push(!!data.message || "Unknow error occurs");
          this.$vuetify.goTo(this.$refs.alert,{duration : 1000, easing: 'easeInOutCubic', offset: -20});
        }
      })
      .catch(({ response }) => {
        if (response.data) {
          this.form.errors.push(response.data.message);
        } else {
          this.form.errors.push("Unknow error occurs");
        }
        this.$vuetify.goTo(this.$refs.alert,{duration : 1000, easing: 'easeInOutCubic', offset: -20});
        this.form.dialog = false;
        this.form.loading = false;
      });
    },
    goToApp() {
      // window.open("https://apps.apple.com/th/app/facebook/id284882215", '_blank');
      this.$router.push({ name: "landingPage" });
      this.form.dialog = false;
      this.form.loading = false;
    },
  },
  mounted() {
    this.onSubmit();
  },
  created() {
    this.form.userId = this.$route.query.userId;
    this.form.randomCode = this.$route.query.randomCode;
  }
};
</script>
