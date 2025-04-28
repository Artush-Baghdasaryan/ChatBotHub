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

  modules: ['@nuxtjs/google-fonts', {
    families: {
      Roboto: true,
      Inter: [400, 700],
    },
    display: 'swap', // Оптимизация загрузки (font-display: swap)
    prefetch: true, // Предзагрузка шрифтов
    preconnect: true, // Предварительное подключение к fonts.googleapis.com
    preload: true, 
  }],
})