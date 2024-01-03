import { Routes } from '@angular/router';
import { ManagementComponent } from '@efeatures/management/management.component';

export const routes: Routes = [
  {
    path: '',
    component: ManagementComponent,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./setting/setting.component').then((c) => c.SettingComponent),
      },
      {
        path: 'shop',
        loadComponent: () =>
          import('./shop/shop.component').then((c) => c.ShopComponent),
      },
    ],
  },
];
