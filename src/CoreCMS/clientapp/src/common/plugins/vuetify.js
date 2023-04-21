import Vue from "vue";
import Vuetify from "vuetify/lib/framework";

Vue.use(Vuetify);

// Translation provided by Vuetify (typescript)
import th from "vuetify/es5/locale/th";
import en from "vuetify/es5/locale/en";

export default new Vuetify({
    lang: {
        locales: { th, en },
    },
    theme: {
        options: {
            customProperties: true,
        },
        themes: {
            light: {
                primary: "#5867dd",
                secondary: "#e8ecfa",
                accent: "#5d78ff",
                error: "#fd397a",
                info: "#5578eb",
                success: "#0abb87",
                warning: "#ffb822",
            },
        },
    },
});