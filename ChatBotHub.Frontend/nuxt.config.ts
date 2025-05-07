// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },

  vite: {
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: '@use "@/assets/scss" as *;',
        },
      },
    },
  },

  // css: ['~/assets/css/main.css'],

  modules: [
    '@nuxtjs/google-fonts',
    '@nuxt/content',
    // '@nuxt/ui',
  ],

  googleFonts: {
    families: {
      Roboto: true,
      Inter: [200, 400, 700],
    },
    display: 'swap',
    prefetch: true,
    preconnect: true,
    preload: true,
  },

  // ui: {
  //   fonts: false,
  //   colorMode: false,
  // }
})