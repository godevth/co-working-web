import ApiService from "@/common/api.service";
import JwtService from "@/common/jwt.service";

// action types
export const VERIFY_AUTH = "verifyAuth";
export const LOGIN = "login";
export const LOGOUT = "logout";
export const REGISTER = "register";
export const UPDATE_USER = "updateUser";
export const LOGIN_BY_TOKEN = "loginByToken";

// mutation types
export const PURGE_AUTH = "logOut";
export const SET_AUTH = "setUser";
export const SET_ERROR = "setError";
export const SET_REFRESHING = "setRefreshing";
export const PURGE_REFRESHING = "purgeRefreshing";

const state = {
  errors: null,
  user: {},
  userInfo: null,
  isAuthenticated: !!JwtService.getToken(),
  isRefreshing: false,
  refreshingCall: null,
};

const getters = {
  currentUser(state) {
    return state.user;
  },
  isAuthenticated(state) {
    return state.isAuthenticated;
  },
};

const actions = {
  [LOGIN](context, credentials) {
    return new Promise((resolve, reject) => {
      var data = new FormData();
      data.set("client_id", "coworking.backoffice");
      data.set("grant_type", "password");
      data.set(
        "scope",
        "openid profile corecms corecms.backoffice offline_access"
      );
      data.set("username", credentials.email);
      data.set("password", credentials.password);

      ApiService.post("/connect/token", data)
        .then(({ data }) => {
          context.commit(SET_AUTH, data);

          ApiService.setHeader();
          ApiService.post("/connect/userinfo")
            .then(({ data }) => {
              let roles = ["super_admin", "system", "admin", "business_owner"];
              if (roles.includes(data.role)) {
                resolve(data);
              } else {
                context.commit(PURGE_AUTH);
                context.commit(SET_ERROR, ["Unauthorized"]);
                reject("Unauthorized");
              }
            })
            .catch(({ response }) => {
              var msg = response.data.error_description
                ? response.data.error_description
                : response.data.error;
              context.commit(PURGE_AUTH);
              context.commit(SET_ERROR, [msg]);
              reject(msg);
            });
        })
        .catch(({ response }) => {
          var msg = response.data.error_description
            ? response.data.error_description
            : response.data.error;
          context.commit(SET_ERROR, [msg]);
          reject(msg);
        });
    });
  },
  [LOGIN_BY_TOKEN](context) {
    return new Promise((resolve, reject) => {
      if (JwtService.getRefreshToken()) {
        var data = new FormData();
        data.set("client_id", "coworking.backoffice");
        data.set("grant_type", "refresh_token");
        data.set("refresh_token", JwtService.getRefreshToken());

        context.commit(
          SET_REFRESHING,
          ApiService.post("/connect/token", data)
            .then(({ data }) => {
              context.commit(SET_AUTH, data);
              context.commit(PURGE_REFRESHING);
              context.dispatch(VERIFY_AUTH);
              resolve(data);
            })
            .catch(() => {
              context.commit(PURGE_AUTH);
              context.commit(PURGE_REFRESHING);
              reject("Invalid refresh token");
            })
        );
      } else {
        context.commit(PURGE_AUTH);
        reject("No refresh token");
      }
    });
  },
  [LOGOUT](context) {
    context.commit(PURGE_AUTH);
  },
  [VERIFY_AUTH](context) {
    if (JwtService.getToken()) {
      ApiService.setHeader();
      ApiService.post("/connect/userinfo")
        .then(({ data }) => {
          let user = {};
          user.preferred_username = data.preferred_username;
          user.name = data.name;
          user.email = data.email;
          user.role = data.role;
          user.access_token = JwtService.getToken();
          user.refresh_token = JwtService.getRefreshToken();

          let roles = ["super_admin", "system", "admin", "business_owner"];
          if (roles.includes(data.role)) {
            let user = {};
            user.preferred_username = data.preferred_username;
            user.name = data.name;
            user.email = data.email;
            user.role = data.role;
            user.access_token = JwtService.getToken();
            user.refresh_token = JwtService.getRefreshToken();
            context.commit(SET_AUTH, user);
          } else {
            context.commit(PURGE_AUTH);
          }
        })
        .catch(({ response }) => {
          context.commit(SET_ERROR, [response.data.error]);
        });
    } else {
      context.commit(PURGE_AUTH);
    }
  },
};

const mutations = {
  [SET_ERROR](state, error) {
    state.errors = error;
  },
  [SET_AUTH](state, user) {
    state.isAuthenticated = true;
    state.user = user;
    state.errors = {};
    JwtService.saveToken(state.user.access_token);
    JwtService.saveRefreshToken(state.user.refresh_token);
  },
  [PURGE_AUTH](state) {
    state.isAuthenticated = false;
    state.user = {};
    state.errors = {};
    JwtService.destroyToken();
    JwtService.destroyRefreshToken();
  },
  [SET_REFRESHING](state, call) {
    state.isRefreshing = true;
    state.refreshingCall = call;
  },
  [PURGE_REFRESHING](state) {
    state.isRefreshing = false;
    state.refreshingCall = null;
  },
};

export default {
  state,
  actions,
  mutations,
  getters,
};
