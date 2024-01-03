import { HttpInterceptorFn } from '@angular/common/http';
import { ACCESS_TOKEN } from '@econstants/system.constants';

export const accessTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const accessToken = localStorage.getItem(ACCESS_TOKEN);
  return !accessToken
    ? next(req)
    : next(
        req.clone({
          setHeaders: {
            Authorization: `Bearer ${accessToken}`,
          },
        }),
      );
};
