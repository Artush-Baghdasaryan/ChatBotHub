import { CanActivateFn, Router } from '@angular/router';
import { JwtTokenService } from '../services/jwt-token.service';
import { inject } from '@angular/core';

export const authGuardGuard: CanActivateFn = (route, state) => {
  const jwtTokenService = inject(JwtTokenService);
  const router = inject(Router);

  const token = jwtTokenService.getToken();
  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  return true;
};
