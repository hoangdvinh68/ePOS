import { Routes } from '@angular/router';
import { authGuard } from '@eguards';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./main/main.routes').then((r) => r.routes),
    canActivate: [authGuard],
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.routes').then((r) => r.routes),
  },
];
