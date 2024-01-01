import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('@epos-pages/main/main.routes').then((r) => r.routes),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('@epos-pages/auth/auth.routes').then((r) => r.routes),
  },
];
