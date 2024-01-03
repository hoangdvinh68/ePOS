import { Routes } from '@angular/router';
import { MainComponent } from './main.component';

export const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        redirectTo: 'management',
        pathMatch: 'full',
      },
      {
        path: 'profile',
        loadChildren: () =>
          import('@efeatures/profile/profile.routes').then((r) => r.routes),
      },
      {
        path: 'management',
        loadChildren: () =>
          import('@efeatures/management/management.routes').then(
            (r) => r.routes,
          ),
      },
      {
        path: 'library',
        loadChildren: () =>
          import('@efeatures/library/library.routes').then((r) => r.routes),
      },
    ],
  },
];
