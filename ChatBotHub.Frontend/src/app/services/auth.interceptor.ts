import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { JwtTokenService } from './jwt-token.service';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
  const jwtService = inject(JwtTokenService);
  const token = jwtService.getToken();
  
  if (token) {
    const cloned = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
    console.log("set token", cloned);
    return next(cloned);
  }
  return next(req);
};
