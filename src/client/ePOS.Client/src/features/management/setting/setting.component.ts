import { Component } from '@angular/core';
import { ISubMenuItem, SubMenuComponent } from '@ecomponents';
import { NgSwitch, NgSwitchCase } from '@angular/common';
import { SettingTenantComponent } from '@efeatures/management/setting/setting-tenant/setting-tenant.component';
import { SettingUnitComponent } from '@efeatures/management/setting/setting-unit/setting-unit.component';

@Component({
  selector: 'app-setting',
  standalone: true,
  imports: [
    SubMenuComponent,
    NgSwitchCase,
    NgSwitch,
    SettingTenantComponent,
    SettingUnitComponent,
  ],
  templateUrl: './setting.component.html',
  styles: ``,
})
export class SettingComponent {
  state!: string;
  items: ISubMenuItem[] = [
    {
      id: 'tenant',
      title: 'Cấu hình chung',
    },
    {
      id: 'unit',
      title: 'Thiết lập đơn vị',
    },
  ];
}
