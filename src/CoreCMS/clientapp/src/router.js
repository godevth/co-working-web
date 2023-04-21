import Vue from "vue";
import Router from "vue-router";

Vue.use(Router);

export default new Router({
  mode: "history",
  routes: [
    {
      path: "/",
      redirect: "/dashboard",
      component: () => import("@/views/theme/Base"),
      children: [
        {
          path: "/dashboard",
          name: "dashboard",
          component: () => import("@/views/pages/Dashboard.vue"),
        },
        {
          path: "/user",
          name: "user",
          redirect: "/user/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin"],
          },
          children: [
            {
              path: "search",
              name: "search",
              component: () => import("@/views/pages/user/Search.vue"),
            },
            {
              path: "add",
              name: "add",
              component: () => import("@/views/pages/user/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editUser",
              component: () => import("@/views/pages/user/Edit.vue"),
            },
          ],
        },
        {
          path: "/role",
          name: "role",
          redirect: "/role/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin"],
          },
          children: [
            {
              path: "search",
              name: "searchRole",
              component: () => import("@/views/pages/role/Search.vue"),
            },
            {
              path: "add",
              name: "addRole",
              component: () => import("@/views/pages/role/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editRole",
              component: () => import("@/views/pages/role/Edit.vue"),
            },
          ],
        },
        {
          path: "/userType",
          name: "userType",
          redirect: "/userType/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin"],
          },
          children: [
            {
              path: "search",
              name: "searchUserType",
              component: () => import("@/views/pages/userType/Search.vue"),
            },
            {
              path: "add",
              name: "addUserType",
              component: () => import("@/views/pages/userType/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editUserType",
              component: () => import("@/views/pages/userType/Edit.vue"),
            },
          ],
        },
        {
          path: "/placeType",
          name: "placeType",
          redirect: "/placeType/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin", "business_owner", "system"],
          },
          children: [
            {
              path: "search",
              name: "searchPlaceType",
              component: () => import("@/views/pages/placeType/Search.vue"),
            },
            {
              path: "add",
              name: "addPlaceType",
              component: () => import("@/views/pages/placeType/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editPlaceType",
              component: () => import("@/views/pages/placeType/Edit.vue"),
            },
          ],
        },
        {
          path: "/roomType",
          name: "roomType",
          redirect: "/roomType/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin", "business_owner", "system"],
          },
          children: [
            {
              path: "search",
              name: "searchRoomType",
              component: () => import("@/views/pages/roomType/Search.vue"),
            },
            {
              path: "add",
              name: "addRoomType",
              component: () => import("@/views/pages/roomType/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editRoomType",
              component: () => import("@/views/pages/roomType/Edit.vue"),
            },
          ],
        },
        {
          path: "/facilityType",
          name: "facilityType",
          redirect: "/facilityType/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin", "business_owner", "system"],
          },
          children: [
            {
              path: "search",
              name: "searchFacilityType",
              component: () => import("@/views/pages/facilityType/Search.vue"),
            },
            {
              path: "add",
              name: "addFacilityType",
              component: () => import("@/views/pages/facilityType/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editFacilityType",
              component: () => import("@/views/pages/facilityType/Edit.vue"),
            },
          ],
        },
        {
          path: "/facility",
          name: "facility",
          redirect: "/facility/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin", "business_owner", "system"],
          },
          children: [
            {
              path: "search",
              name: "searchFacility",
              component: () => import("@/views/pages/facility/Search.vue"),
            },
            {
              path: "add",
              name: "addFacility",
              component: () => import("@/views/pages/facility/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editFacility",
              component: () => import("@/views/pages/facility/Edit.vue"),
            },
          ],
        },
        {
          path: "/company",
          name: "company",
          redirect: "/company/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin"],
          },
          children: [
            {
              path: "search",
              name: "searchCompany",
              component: () => import("@/views/pages/company/Search.vue"),
            },
            {
              path: "add",
              name: "addCompany",
              component: () => import("@/views/pages/company/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editCompany",
              component: () => import("@/views/pages/company/Edit.vue"),
            },
          ],
        },
        {
          path: "/place",
          name: "place",
          redirect: "/place/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin", "business_owner", "system"],
          },
          children: [
            {
              path: "search",
              name: "searchPlace",
              component: () => import("@/views/pages/place/Search.vue"),
            },
            {
              path: "add",
              name: "addPlace",
              component: () => import("@/views/pages/place/Add.vue"),
            },
            {
              path: "edit/:id",
              name: "editPlace",
              component: () => import("@/views/pages/place/Edit.vue"),
            },
            {
              path: "detail/:id",
              name: "detailPlace",
              component: () => import("@/views/pages/place/Detail.vue"),
            },
            {
              path: "searchRoom/:id",
              name: "searchRoom",
              component: () => import("@/views/pages/place/SearchRoom.vue"),
            },
            {
              path: "addRoom/:id",
              name: "addRoom",
              component: () => import("@/views/pages/place/AddRoom.vue"),
            },
            {
              path: "editRoom/:id",
              name: "editRoom",
              component: () => import("@/views/pages/place/EditRoom.vue"),
            },
            {
              path: "searchPrivilege/:id",
              name: "searchPrivilege",
              component: () =>
                import("@/views/pages/place/SearchPrivilege.vue"),
            },
            {
              path: "addPrivilege/:id",
              name: "addPrivilege",
              component: () => import("@/views/pages/place/AddPrivilege.vue"),
            },
            {
              path: "importCustumer/:id",
              name: "importCustumer",
              component: () => import("@/views/pages/place/ImportCustumer.vue"),
            },
            {
              path: "addCustumer/:id",
              name: "addCustumer",
              component: () => import("@/views/pages/place/AddCustumer.vue"),
            },
            {
              path: "searchTheme/:id",
              name: "searchTheme",
              component: () => import("@/views/pages/place/SearchTheme.vue"),
            },
            {
              path: "addTheme/:id",
              name: "addTheme",
              component: () => import("@/views/pages/place/AddTheme.vue"),
            },
            {
              path: "searchTermAndCondition/:id",
              name: "searchTermAndCondition",
              component: () =>
                import("@/views/pages/place/SearchTermAndCondition.vue"),
            },
            {
              path: "addTermAndCondition/:id",
              name: "addTermAndCondition",
              component: () =>
                import("@/views/pages/place/AddTermAndCondition.vue"),
            },
            {
              path: "editTermAndCondition/:id",
              name: "editTermAndCondition",
              component: () =>
                import("@/views/pages/place/EditTermAndCondition.vue"),
            },
          ],
        },
        {
          path: "/booking",
          name: "booking",
          redirect: "/booking/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin", "business_owner", "system"],
          },
          children: [
            {
              path: "search",
              name: "searchBooking",
              component: () => import("@/views/pages/booking/Search.vue"),
            },
            {
              path: "detail/:id",
              name: "detailBooking",
              component: () => import("@/views/pages/booking/Detail.vue"),
            },
            {
              path: "payment/:id",
              name: "paymentBooking",
              component: () => import("@/views/pages/booking/Payment.vue"),
            },
          ],
        },
        {
          path: "/approve",
          name: "approve",
          redirect: "/approve/search",
          component: () => import("@/views/pages/vuetify/Vuetify.vue"),
          meta: {
            permission: ["super_admin", "admin"],
          },
          children: [
            {
              path: "search",
              name: "searchApprove",
              component: () => import("@/views/pages/approvePlace/Search.vue"),
            },
          ],
        },
      ],
    },
    {
      path: "/error",
      name: "error",
      component: () => import("@/views/pages/error/Error.vue"),
      children: [
        {
          path: "error-1",
          name: "error-1",
          component: () => import("@/views/pages/error/Error-1.vue"),
        },
        {
          path: "error-2",
          name: "error-2",
          component: () => import("@/views/pages/error/Error-2.vue"),
        },
        {
          path: "error-3",
          name: "error-3",
          component: () => import("@/views/pages/error/Error-3.vue"),
        },
        {
          path: "error-4",
          name: "error-4",
          component: () => import("@/views/pages/error/Error-4.vue"),
        },
        {
          path: "error-5",
          name: "error-5",
          component: () => import("@/views/pages/error/Error-5.vue"),
        },
        {
          path: "error-6",
          name: "error-6",
          component: () => import("@/views/pages/error/Error-6.vue"),
        },
      ],
    },
    {
      path: "/",
      component: () => import("@/views/pages/auth/Auth"),
      children: [
        {
          name: "login",
          path: "/login",
          component: () => import("@/views/pages/auth/Login"),
        },
        {
          name: "register",
          path: "/register",
          component: () => import("@/views/pages/auth/Register"),
        },
        {
          name: "resetPassword",
          path: "/resetPassword",
          component: () => import("@/views/pages/auth/ResetPassword"),
        },
        {
          name: "confirmEmail",
          path: "/confirmEmail",
          component: () => import("@/views/pages/auth/ConfirmEmail"),
        },
        {
          name: "termAndCondition",
          path: "/termAndCondition",
          component: () => import("@/views/pages/auth/TermAndCondition"),
        },
      ],
    },
    {
      path: "/landingPage",
      name: "landingPage",
      component: () => import("@/views/pages/landingPage/LandingPage.vue"),
    },
    {
      path: "*",
      redirect: "/404",
    },
    {
      // the 404 route, when none of the above matches
      path: "/404",
      name: "404",
      component: () => import("@/views/pages/error/Error-1.vue"),
    },
  ],
});
