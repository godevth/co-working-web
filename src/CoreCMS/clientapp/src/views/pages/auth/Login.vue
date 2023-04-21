<template>
  <div class="kt-login__container">
    <div class="kt-login__logo">
      <a href="#">
        <img src="assets/media/logos/logo-mini-lg-dark.png" />
      </a>
    </div>
    <div class="kt-login__signin">
      <div class="kt-login__head">
        <h3 class="kt-login__title">{{ $t("AUTH.LOGIN.TITLE") }}</h3>
      </div>
      <!--begin::Form-->
      <v-form
        class="kt-form"
        ref="form"
        v-model="form.valid"
        lazy-validation
        @submit.prevent="onSubmit"
      >
        <div class="row" v-if="errors && errors.length > 0">
          <div class="col-12">
            <v-alert type="error">
              {{ errors[0] }}
            </v-alert>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <v-text-field
              prepend-icon="mdi-account"
              v-model="form.email"
              ref="email"
              :label="UsernameOrEmailPlaceholder"
              :clearable="true"
              :rules="form.emailRules"
              required
            ></v-text-field>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <v-text-field
              prepend-icon="mdi-lock"
              v-model="form.password"
              ref="password"
              :label="PasswordPlaceholder"
              :clearable="true"
              :rules="form.passwordRules"
              required
              type="password"
            ></v-text-field>
          </div>
        </div>
        <div class="kt-login__actions">
          <button id="kt_submit" class="btn btn-primary btn-block">
            <i class="la la-sign-in"></i> {{ $t("AUTH.LOGIN.BUTTON") }}
          </button>
        </div>
      </v-form>
      <!--end::Form-->
    </div>
  </div>
</template>

<style lang="scss" scoped>
.kt-spinner.kt-spinner--right:before {
  right: 8px;
}
</style>

<script>
import { mapState } from "vuex";
import { LOGIN, LOGOUT } from "@/store/auth.module";

import { validationMixin } from "vuelidate";
import { minLength, required } from "vuelidate/lib/validators";

export default {
  mixins: [validationMixin],
  name: "login",
  data() {
    return {
      // Remove this dummy login info
      form: {
        valid: true,
        email: "",
        emailRules: [
          (v) => !!v || this.$t("AUTH.LOGIN.USERNAME_OR_EMAIL_REQUIRED"),
        ],
        password: "",
        passwordRules: [(v) => !!v || this.$t("AUTH.LOGIN.PASSWORD_REQUIRED")],
      },
    };
  },
  validations: {
    form: {
      email: {
        required,
      },
      password: {
        required,
        minLength: minLength(3),
      },
    },
  },
  methods: {
    validateState(name) {
      const { $dirty, $error } = this.$v.form[name];
      return $dirty ? !$error : null;
    },
    resetForm() {
      this.form = {
        email: null,
        password: null,
      };

      this.$nextTick(() => {
        this.$v.$reset();
      });
    },
    onSubmit() {
      if (this.$refs.form.validate()) {
        const email = this.$v.form.email.$model;
        const password = this.$v.form.password.$model;

        // clear existing errors
        this.$store.dispatch(LOGOUT);

        // set spinner to submit button
        const submitButton = document.getElementById("kt_submit");
        submitButton.classList.add(
          "kt-spinner",
          "kt-spinner--light",
          "kt-spinner--right"
        );

        // send login request
        this.$store
          .dispatch(LOGIN, { email, password })
          // go to which page after successfully login
          .then(() => {
            this.$router.push({ name: "dashboard" });
          })
          .finally(() => {
            submitButton.classList.remove(
              "kt-spinner",
              "kt-spinner--light",
              "kt-spinner--right"
            );
          });
      }
      else 
      {
        //focus
        if(!this.form.email)
          this.$refs.email.focus();
        else if(!this.form.password)
          this.$refs.password.focus();
        else
          this.$refs.email.focus();
      }
    },
  },
  computed: {
    ...mapState({
      errors: (state) => state.auth.errors,
    }),
    backgroundImage() {
      return process.env.BASE_URL + "assets/media/bg/bg-4.jpg";
    },
    UsernameOrEmailPlaceholder() {
      return this.$t("AUTH.LOGIN.USERNAME_OR_EMAIL");
    },
    PasswordPlaceholder() {
      return this.$t("AUTH.LOGIN.PASSWORD");
    },
  },
};
</script>
