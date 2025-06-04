import { ref, onMounted, onBeforeUnmount, computed } from 'vue'

export default function useWindowSize() {
  const windowWidth = ref(0) // Инициализируем нулем вместо window.innerWidth

  const breakpoints = {
    phone: 576,
    tablet: 768,
    laptop: 992,
    desktop: 1200,
    wide: 1400
  }

  const updateWidth = () => {
    windowWidth.value = window.innerWidth
  }

  const width = computed(() => ({
    current: windowWidth.value,
    isPhone: windowWidth.value < breakpoints.phone,
    isTablet: windowWidth.value < breakpoints.tablet,
    isLaptop: windowWidth.value < breakpoints.laptop,
    isDesktop: windowWidth.value < breakpoints.desktop,
    isWideScreen: windowWidth.value >= breakpoints.desktop
  }))

  onMounted(() => {
    // Устанавливаем начальное значение только на клиенте
    if (typeof window !== 'undefined') {
      windowWidth.value = window.innerWidth
      window.addEventListener('resize', updateWidth)
    }
  })

  onBeforeUnmount(() => {
    if (typeof window !== 'undefined') {
      window.removeEventListener('resize', updateWidth)
    }
  })

  return { width }
}