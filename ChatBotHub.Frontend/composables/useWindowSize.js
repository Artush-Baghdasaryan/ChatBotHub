import { ref, onMounted, onBeforeUnmount, computed } from 'vue'

export default function useWindowSize() {
  const windowWidth = ref(window.innerWidth)

  const breakpoints = {
    phone: 576,     // < 576px
    tablet: 768,    // ≥ 576px && < 768px
    laptop: 992,    // ≥ 768px && < 992px
    desktop: 1200,  // ≥ 992px && < 1200px
    wide: 1400      // ≥ 1200px
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
    window.addEventListener('resize', updateWidth)
  })

  onBeforeUnmount(() => {
    window.removeEventListener('resize', updateWidth)
  })

  return { width }
}