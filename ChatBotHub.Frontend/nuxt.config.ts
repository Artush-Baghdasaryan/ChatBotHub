// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  ssr: false,

  vite: {
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: `
            @use "@/assets/scss" as *;
            @use "@/assets/scss/mixins" as *;
            `,
        },
      },
    },
  },

  // css: ['~/assets/css/main.css'],

  modules: [
    '@nuxtjs/google-fonts',
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