import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { NotificationService, UserService } from '@eservices';

export const authGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router);
  const notificationService = inject(NotificationService);

  const canActive = userService.isAccessTokenExpiredOrNull();

  if (!canActive) {
    userService.clearDataToken();
    router.navigate(['/auth/sign-in']).then();
    notificationService.info('Bạn cần phải đăng nhập');
    return false;
  }

  return true;
};
