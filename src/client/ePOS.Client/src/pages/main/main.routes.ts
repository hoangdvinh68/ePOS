import { Routes } from '@angular/router';
import { MainComponent } from '@epos-pages/main/main.component';

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
        path: 'management',
        loadComponent: () =>
          import('./management/management.component').then(
            (c) => c.ManagementComponent,
          ),
      },
      {
        path: 'library',
        loadComponent: () =>
          import('./library/library.component').then((c) => c.LibraryComponent),
      },
    ],
  },
];
