// composables/useValidation.js
import { ref, reactive } from 'vue'

export const useValidation = () => {
  const formErrors = reactive({})
  
  const getWordEnding = (count) => {
    const lastTwo = count % 100
    const lastOne = count % 10
    
    if (lastTwo >= 11 && lastTwo <= 14) return 'ов'
    
    switch (lastOne) {
      case 1: return ''
      case 2:
      case 3:
      case 4: return 'а'
      default: return 'ов'
    }
  }

  const validateField = (value, rules) => {
    const validateTypes = {
      required: {
        pattern: /^required$/,
        test: () => !value.trim(),
        message: () => 'Обязательное поле!',
      },
      min: {
        pattern: /^min:(\d+)$/,
        test: (match) => value.length < parseInt(match[1]),
        message: (match) => `Минимум ${match[1]} символ${getWordEnding(parseInt(match[1]))}`,
      },
      max: {
        pattern: /^max:(\d+)$/,
        test: (match) => value.length > parseInt(match[1]),
        message: (match) => `Максимум ${match[1]} символ${getWordEnding(parseInt(match[1]))}`,
      },
    }

    for (const rule of rules) {
      for (const validatorName in validateTypes) {
        const validator = validateTypes[validatorName]
        const match = rule.match(validator.pattern)
        if (match) {
          if (validator.test(match)) {
            return validator.message(match)
          }
          break
        }
      }
    }
    return null
  }

  const validateForm = (formData, validationSchema) => {
    let isValid = true
    
    for (const field in validationSchema) {
      const rules = validationSchema[field].validate
      formErrors[field] = validateField(formData[field], rules)
      
      if (formErrors[field]) {
        isValid = false
      }
    }
    
    return isValid
  }

  return {
    formErrors,
    validateField,
    validateForm
  }
}