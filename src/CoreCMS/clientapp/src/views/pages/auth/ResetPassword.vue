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
        <h3 class="kt-login__title">{{ $t("AUTH.FORGOT_PASSWORD.TITLE") }}</h3>
      </div>
      <v-form
        class="kt-form"
        ref="form"
        v-model="form.valid"
        lazy-validation
        @submit.prevent="onSubmit"
      >
        <div class="row">
          <div class="col-12">
            <v-text-field
              prepend-icon="mdi-lock"
              v-model="form.password"
              :label="PasswordPlaceholder"
              :clearable="true"
              :rules="form.passwordRules"
              required
              type="password"
            ></v-text-field>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <v-text-field
              prepend-icon="mdi-lock"
              v-model="form.confirmPassword"
              :label="ConfirmPasswordPlaceholder"
              :clearable="true"
              :rules="form.confirmPasswordRules"
              required
              type="password"
            ></v-text-field>
          </div>
        </div>
        <div class="row kt-login__actions">
          <div class="col-6">
            <button id="kt_submit" class="btn btn-primary btn-block">
              <i class="la la-check"></i> {{ $t("AUTH.FORGOT_PASSWORD.CONFIRM_BUTTON") }}
            </button>
          </div>
          <div class="col-6">
            <button class="btn btn-secondary btn-block" type="button" @click="goToLogin">
              <i class="la la-close"></i> {{ $t("AUTH.FORGOT_PASSWORD.CANCEL_BUTTON") }}
            </button>
          </div>
        </div>

        <v-dialog v-model="form.dialog" persistent max-width="300">
          <v-card>
            <v-card-title class="headline">
              {{
                $t("AUTH.FORGOT_PASSWORD.SUCCESS_DIALOG_HEADER")
              }}</v-card-title
            >
            <v-card-text>
                {{ $t("AUTH.FORGOT_PASSWORD.SUCCESS_DIALOG_TEXT") }}
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="green darken-1"
                text
                @click="goToLogin"
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
        valid: true,
        dialog: false,
        loading: false,
        errors: [],
        password: "",
        passwordRules: [
          (v) => !!v || this.$t("AUTH.FORGOT_PASSWORD.PASSWORD_REQUIRED"),
          (v) => (v && v.length <= 20) ||  this.$t("AUTH.FORGOT_PASSWORD.PASSWORD_COUNTER"),
          (v) => (v && v.length >= 6) ||  this.$t("AUTH.FORGOT_PASSWORD.PASSWORD_COUNTER_LT"),
        ],
        confirmPassword: "",
        confirmPasswordRules: [
          (v)  => !!v || this.$t("AUTH.FORGOT_PASSWORD.CONFIRM_PASSWORD_REQUIRED"),
          (v)  => (v && v.length <= 20) ||  this.$t("AUTH.FORGOT_PASSWORD.COMFIRM_PASSWORD_COUNTER"),
          (v)  => (v && v == this.form.password) ||  this.$t("AUTH.FORGOT_PASSWORD.COMFIRM_PASSWORD_EQ"),
          (v) => (v && v.length >= 6) ||  this.$t("AUTH.FORGOT_PASSWORD.COMFIRM_PASSWORD_COUNTER_LT"),
        ],
        userId: null,
        token: null,
      },
    };
  },
  methods: {
    onSubmit() {
      if (this.$refs.form.validate()) {
        this.form.errors = [];
        if(this.form.userId == null || this.form.token == null){
          this.form.errors.push("กรุณากด link ลืมรัหสผ่านจากอีเมลที่ระบบส่งไปให้เท่านั้น");
        }

        if(this.form.errors.length > 0)
        {
          this.$vuetify.goTo(this.$refs.alert,{duration : 1000, easing: 'easeInOutCubic', offset: -20});
        }
        else 
        {
          // set spinner to submit button
          const submitButton = document.getElementById("kt_submit");
          submitButton.classList.add(
            "kt-spinner",
            "kt-spinner--light",
            "kt-spinner--right"
          );

          // send login request
          ApiService.post("/Api/User/ResetPassword", {
            UserId: this.form.userId,
            Token: this.form.token,
            Password: this.form.password,
            ConfirmPassword: this.form.confirmPassword,
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
          })
          .finally(() => {
            submitButton.classList.remove(
              "kt-spinner",
              "kt-spinner--light",
              "kt-spinner--right"
            );
          });
        }
      }
    },
    goToLogin() {
      this.$router.push({ name: "landingPage" });
    },
  },
  computed: {
    PasswordPlaceholder() {
      return this.$t("AUTH.FORGOT_PASSWORD.PASSWORD");
    },
    ConfirmPasswordPlaceholder() {
      return this.$t("AUTH.FORGOT_PASSWORD.CONFIRM_PASSWORD");
    },
  },
  created() {
    this.form.userId = this.$route.query.userId;
    this.form.token = this.$route.query.token;
  }
};
</script>
