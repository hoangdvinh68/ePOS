import { Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { importProvidersFrom } from '@angular/core';
import { NgxsModule } from '@ngxs/store';
import { TenantState } from '@estores/tenant';
import { UnitState } from '@estores/unit';
import { CurrencyState } from '@estores/currency';

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
        providers: [
          importProvidersFrom(
            NgxsModule.forFeature([TenantState, UnitState, CurrencyState]),
          ),
        ],
      },
      {
        path: 'library',
        loadChildren: () =>
          import('@efeatures/library/library.routes').then((r) => r.routes),
      },
    ],
  },
];
