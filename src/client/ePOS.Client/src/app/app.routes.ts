import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./main/main.routes').then((r) => r.routes),
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.routes').then((r) => r.routes),
  },
];
