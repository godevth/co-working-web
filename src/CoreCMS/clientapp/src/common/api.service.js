import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import JwtService from "@/common/jwt.service";
import { API_URL } from "@/common/config";

/**
 * Service to call HTTP request via Axios
 */
const ApiService = {
  init() {
    Vue.use(VueAxios, axios);
    Vue.axios.defaults.baseURL = API_URL;
  },

  refreshToken(store) {
    if (store.state.auth.isRefreshing) {
      return store.state.auth.refreshingCall;
    }

    return store.dispatch("loginByToken");
  },

  setAuthRefreshing(store) {
    Vue.axios.interceptors.response.use(
      (response) => response,
      (error) => {
        const status = error.response ? error.response.status : null;

        if (status === 401) {
          return this.refreshToken(store).then(() => {
            error.config.headers["Authorization"] =
              "Bearer " + JwtService.getToken();
            return Vue.axios.request(error.config);
          });
        }

        return Promise.reject(error);
      }
    );
  },

  /**
   * Set the default HTTP request headers
   */
  setHeader() {
    Vue.axios.defaults.headers.common[
      "Authorization"
    ] = `Bearer ${JwtService.getToken()}`;
  },

  query(resource, params) {
    return Vue.axios.get(resource, params).catch((error) => {
      // console.log(error);
      throw new Error(`[KT] ApiService ${error}`);
    });
  },

  /**
   * Send the GET HTTP request
   * @param resource
   * @param slug
   * @returns {*}
   */
  get(resource, slug = "") {
    return Vue.axios.get(`${resource}/${slug}`).catch((error) => {
      // console.log(error);
      throw new Error(`[KT] ApiService ${error}`);
    });
  },

  /**
   * Set the POST HTTP request
   * @param resource
   * @param params
   * @returns {*}
   */
  post(resource, params, options) {
    return Vue.axios.post(`${resource}`, params, options);
  },

  /**
   * Send the UPDATE HTTP request
   * @param resource
   * @param slug
   * @param params
   * @returns {IDBRequest<IDBValidKey> | Promise<void>}
   */
  update(resource, slug, params) {
    return Vue.axios.put(`${resource}/${slug}`, params);
  },

  /**
   * Send the PUT HTTP request
   * @param resource
   * @param params
   * @returns {IDBRequest<IDBValidKey> | Promise<void>}
   */
  put(resource, params) {
    return Vue.axios.put(`${resource}`, params);
  },

  /**
   * Send the DELETE HTTP request
   * @param resource
   * @returns {*}
   */
  delete(resource) {
    return Vue.axios.delete(resource).catch((error) => {
      // console.log(error);
      throw new Error(`[RWV] ApiService ${error}`);
    });
  },

  /**
   * Send the GET HTTP request
   * @param resource
   * @returns {blob}
   */
  download(resource) {
    return Vue.axios.get(resource, { responseType: "blob" }).catch((error) => {
      // console.log(error);
      throw new Error(`[KT] ApiService ${error}`);
    });
  },
};

export default ApiService;
