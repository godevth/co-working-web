<template>
  <v-app>
    <div class="kt-grid kt-grid--ver kt-grid--root">
      <div
        class="kt-grid kt-grid--hor kt-grid--root  kt-login kt-login--v3 kt-login--signin"
        id="kt_login"
      >
        <div
          class="kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor"
          style="background-image: url(assets/media/bg/bg-3.jpg);"
        >
          <div class="kt-grid__item kt-grid__item--fluid kt-login__wrapper">
            <router-view></router-view>
          </div>
        </div>
      </div>
    </div>
  </v-app>
</template>

<!-- Load login custom page styles -->
<style lang="scss">
@import "@/assets/sass/pages/login/login-3.scss";
@import "@/assets/sass/pages/login/login-sbp.scss";
</style>

<script>
import { mapState } from "vuex";

export default {
  name: "auth",
  methods: {},
  computed: {
    ...mapState({
      errors: (state) => state.auth.errors,
    }),
    backgroundImage() {
      return process.env.BASE_URL + "assets/media/bg/bg-4.jpg";
    },
  },
};
</script>
