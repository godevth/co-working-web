<template>
  <!-- begin:: Header Topbar -->
  <div class="kt-header__topbar">
    <!--begin: Search -->
    <div
      v-if="searchDisplay"
      class="kt-header__topbar-item kt-header__topbar-item--search"
      id="kt_quick_search_toggle"
    >
      <div class="kt-header__topbar-wrapper" data-toggle="dropdown">
        <span class="kt-header__topbar-icon">
          <img
            svg-inline
            class="kt-svg-icon"
            src="@/assets/media/icons/svg/General/Search.svg"
            alt=""
          />
        </span>
      </div>
      <div
        class="dropdown-menu dropdown-menu-fit dropdown-menu-lg dropdown-menu-right"
        v-on:click.stop
      >
        <KTSearchDefault></KTSearchDefault>
      </div>
    </div>
    <!--end: Search -->

    <!--begin: Notifications -->
    <div
      v-if="notificationsDisplay"
      class="kt-header__topbar-item"
      id="kt_notification_toggle"
    >
      <div class="kt-header__topbar-wrapper" data-toggle="dropdown">
        <span class="kt-header__topbar-icon kt-pulse kt-pulse--brand">
          <img
            svg-inline
            class="kt-svg-icon"
            src="@/assets/media/icons/svg/Code/Compiling.svg"
            alt=""
          />
          <span class="kt-pulse__ring"></span>
        </span>
      </div>
      <div
        class="dropdown-menu dropdown-menu-fit dropdown-menu-xl dropdown-menu-right"
        v-on:click.stop
      >
        <form>
          <KTDropdownNotification></KTDropdownNotification>
        </form>
      </div>
    </div>
    <!--end: Notifications -->

    <!--begin: Quick Actions -->
    <div v-if="quickActionsDisplay" class="kt-header__topbar-item">
      <div
        class="kt-header__topbar-wrapper"
        id="kt_quick_action_toggle"
        data-toggle="dropdown"
      >
        <span class="kt-header__topbar-icon">
          <img
            svg-inline
            class="kt-svg-icon"
            src="@/assets/media/icons/svg/Media/Equalizer.svg"
            alt=""
          />
        </span>
      </div>
      <div
        class="dropdown-menu dropdown-menu-fit dropdown-menu-xl dropdown-menu-right"
        v-on:click.stop
      >
        <KTDropdownQuickAction></KTDropdownQuickAction>
      </div>
    </div>
    <!--end: Quick Actions -->

    <!--begin: My Cart -->
    <div v-if="myCartDisplay" class="kt-header__topbar-item">
      <div
        class="kt-header__topbar-wrapper"
        id="kt_my_cart_toggle"
        data-toggle="dropdown"
      >
        <span class="kt-header__topbar-icon">
          <img
            svg-inline
            class="kt-svg-icon"
            src="@/assets/media/icons/svg/Shopping/Cart3.svg"
            alt=""
          />
        </span>
      </div>
      <div
        class="dropdown-menu dropdown-menu-fit dropdown-menu-xl dropdown-menu-right"
        v-on:click.stop
      >
        <KTDropdownMyCart></KTDropdownMyCart>
      </div>
    </div>
    <!--end: My Cart -->

    <!--begin: Quick panel toggler -->
    <div
      v-if="quickPanelDisplay"
      class="kt-header__topbar-item kt-header__topbar-item--quick-panel"
      v-b-tooltip.hover.bottom="'Quick panel'"
    >
      <span class="kt-header__topbar-icon" id="kt_quick_panel_toggler_btn">
        <img
          svg-inline
          class="kt-svg-icon"
          src="@/assets/media/icons/svg/Layout/Layout-4-blocks.svg"
          alt=""
        />
      </span>
    </div>
    <!--end: Quick panel toggler -->

    <!--begin: Language bar -->
    <div
      v-if="languagesDisplay"
      class="kt-header__topbar-item kt-header__topbar-item--langs"
    >
      <div
        class="kt-header__topbar-wrapper"
        id="kt_language_toggle"
        data-toggle="dropdown"
      >
        <span class="kt-header__topbar-icon">
          <img :src="languageFlag || getLanguageFlag" alt="" />
        </span>
      </div>
      <div
        class="dropdown-menu dropdown-menu-fit dropdown-menu-right"
        v-on:click.stop
      >
        <KTDropdownLanguage
          v-on:language-changed="onLanguageChanged"
        ></KTDropdownLanguage>
      </div>
    </div>
    <!--end: Language bar -->

    <!--begin: User Bar -->
    <div
      v-if="userDisplay"
      class="kt-header__topbar-item kt-header__topbar-item--user"
    >
      <div
        class="kt-header__topbar-wrapper"
        id="kt_user_toggle"
        data-toggle="dropdown"
      >
        <div class="kt-header__topbar-user">
          <span class="kt-header__topbar-welcome kt-hidden-mobile"
            >{{ $t("TOPBAR.HI") }},</span
          >
          <span class="kt-header__topbar-username kt-hidden-mobile">{{
            user.name
          }}</span>
          <img
            class="kt-hidden"
            alt="Pic"
            src="@/assets/media/users/300_25.jpg"
          />
          <!--use below badge element instead the user avatar to display username's first letter(remove kt-hidden class to display it) -->
          <span
            class="kt-badge kt-badge--username kt-badge--unified-success kt-badge--lg kt-badge--rounded kt-badge--bold"
            >{{ firstCharOfName }}</span
          >
        </div>
      </div>
      <div
        class="dropdown-menu dropdown-menu-fit dropdown-menu-xl dropdown-menu-right"
        v-on:click.stop
      >
        <KTDropdownUser></KTDropdownUser>
      </div>
    </div>
    <!--end: User Bar -->
  </div>
  <!-- end:: Header Topbar -->
</template>

<script>
import { mapState, mapGetters } from "vuex";

import KTSearchDefault from "@/views/theme/topbar/SearchDefault.vue";
import KTDropdownNotification from "@/views/theme/topbar/DropdownNotification.vue";
import KTDropdownQuickAction from "@/views/theme/topbar/DropdownQuickAction.vue";
import KTDropdownMyCart from "@/views/theme/topbar/DropdownMyCart.vue";
import KTDropdownLanguage from "@/views/theme/topbar/DropdownLanguage.vue";
import KTDropdownUser from "@/views/theme/topbar/DropdownUser.vue";
import i18nService from "@/common/i18n.service.js";

export default {
  name: "KTTopbar",
  data() {
    return {
      languageFlag: "",
      languages: i18nService.languages,
    };
  },
  components: {
    KTSearchDefault,
    KTDropdownNotification,
    KTDropdownQuickAction,
    KTDropdownMyCart,
    KTDropdownLanguage,
    KTDropdownUser,
  },
  methods: {
    onLanguageChanged() {
      this.languageFlag = this.languages.find((val) => {
        return val.lang === i18nService.getActiveLanguage();
      }).flag;
    },
  },
  computed: {
    ...mapState({
      user: (state) => state.auth.user,
    }),
    ...mapGetters(["layoutConfig"]),
    firstCharOfName() {
      return this.user.name ? this.user.name.substr(0, 1) : "";
    },
    getLanguageFlag() {
      return this.onLanguageChanged();
    },
    /**
     * @returns {boolean}
     */
    searchDisplay() {
      return !!this.layoutConfig("header.topbar.search.display");
    },
    /**
     * @returns {boolean}
     */
    notificationsDisplay() {
      return !!this.layoutConfig("header.topbar.notifications.display");
    },
    /**
     * @returns {boolean}
     */
    quickActionsDisplay() {
      return !!this.layoutConfig("header.topbar.quick-actions.display");
    },
    /**
     * @returns {boolean}
     */
    userDisplay() {
      return !!this.layoutConfig("header.topbar.user.display");
    },
    /**
     * @returns {boolean}
     */
    languagesDisplay() {
      return !!this.layoutConfig("header.topbar.languages.display");
    },
    /**
     * @returns {boolean}
     */
    myCartDisplay() {
      return !!this.layoutConfig("header.topbar.my-cart.display");
    },
    /**
     * @returns {boolean}
     */
    quickPanelDisplay() {
      return !!this.layoutConfig("header.topbar.quick-panel.display");
    },
  },
};
</script>
