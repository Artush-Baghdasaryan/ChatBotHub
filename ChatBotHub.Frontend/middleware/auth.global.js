export default defineNuxtRouteMiddleware((to, from) => {
  // Исключаем публичные маршруты
  const publicRoutes = ['/auth', '/']
  if (publicRoutes.includes(to.path)) return
  
  // Проверяем наличие токена в localStorage
  // const authToken = process.client ? localStorage.getItem('authToken') : null
  
  const authToken = useCookie('authToken').value

  // console.log(authToken);
  if (!authToken) {
    // Перенаправляем на страницу входа
    return navigateTo('/auth')
  }
})
