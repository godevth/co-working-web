<template>
  <!-- begin:: Footer -->
  <div
    class="kt-footer kt-grid__item kt-grid kt-grid--desktop kt-grid--ver-desktop"
    id="kt_footer"
  >
    <div
      class="kt-container"
      v-bind:class="{ 'kt-container--fluid': isFluid() }"
    >
      <div class="kt-footer__copyright">
        2020 &nbsp;&copy;&nbsp;
        <a href="https://www.sbpds.co.th" target="_blank" class="kt-link ml-1">
          SBP Digital Service co., ltd
        </a>
      </div>
      <div class="kt-footer__menu">
        <a
          href="mailto:support@sbpds.co.th"
          target="_blank"
          class="kt-footer__menu-link kt-link"
          >Contact</a
        >
      </div>
    </div>
  </div>
  <!-- end:: Footer -->
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "KTFooter",
  methods: {
    /**
     * Check if footer container is fluid
     * @returns {boolean}
     */
    isFluid() {
      return this.layoutConfig("footer.self.width") === "fluid";
    },
  },
  computed: {
    ...mapGetters(["layoutConfig"]),
  },
};
</script>
