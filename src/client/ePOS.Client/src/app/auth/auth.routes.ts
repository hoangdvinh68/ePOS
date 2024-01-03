import { Routes } from '@angular/router';
import { AuthComponent } from './auth.component';
import { SignInComponent } from './sign-in/sign-in.component';

export const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./sign-in/sign-in.component').then((c) => c.SignInComponent),
      },
      {
        path: 'sign-up',
        loadComponent: () =>
          import('./sign-up/sign-up.component').then((c) => c.SignUpComponent),
      },
    ],
  },
];
