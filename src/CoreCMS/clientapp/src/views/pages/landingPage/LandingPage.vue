<template>
  <div class="kt-grid kt-grid--ver kt-grid--root">
    <div
      class="kt-grid__item kt-grid__item--fluid kt-grid kt-landing-v1"
      :style="{
        backgroundImage: `url(${backgroundImage})`
      }"
    >
      <div class="kt-landing-v1__container">
        <div class="row">
          <div class="col-md-2 mt-5 text-center">
            <img src="assets/media/landing-page/COSPACE_icon.png" style="max-width: 80%; height: auto;">
          </div>
          <div class="col-md-5 mt-5">
            <div class="col-12 mb-5 text-center text-md-left pl-xl-5">
              <h1 class="kt-h1-v1" style="margin-top: 0px;">Built to suit!</h1>
              <h3 class="kt-h3-v1">Enhance your experience at COSPACE</h3>
            </div>
            <div class="col-12 mb-5 pb-4 text-center text-md-left pl-xl-5">
              <img src="assets/media/landing-page/download_button.png" style="max-width: 30%; height: auto;">
            </div>
            <div class="row">
              <div class="col-6 col-xl-5 text-center" style="border-right: 2px solid #555;">
                <a href="https://cow-dev.sbpds.co.th/">
                  <img src="assets/media/landing-page/appstore_icon.png" style="max-width: 40%; height: auto;">
                </a>
                <p class="kt-p-v1">Download on<br />Appstore</p>
              </div>
              <div class="col-6 col-xl-5 text-center">
                <a href="https://cow-dev.sbpds.co.th/">
                  <img src="assets/media/landing-page/playstore_icon.png" style="max-width: 40%; height: auto;">
                </a>
                <p class="kt-p-v1">Download on<br />Play Store</p>
              </div>
            </div>
          </div>
          <div class="col-md-5 mt-5 text-center">
            <img src="assets/media/landing-page/phones.png" style="max-width: 75%; height: auto;">
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss">
@import "@/assets/sass/pages/landing-page/landing-page.scss";
</style>

<script>
export default {
  name: "landingPage",
  mounted() {},
  computed: {
    backgroundImage() {
      return process.env.BASE_URL + "assets/media/landing-page/bg1.png";
    }
  }
};
</script>
