import { Component } from '@angular/core';
import { ChipsModule } from 'primeng/chips';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-setting-tenant',
  standalone: true,
  imports: [ChipsModule, DropdownModule],
  templateUrl: './setting-tenant.component.html',
  styles: ``,
})
export class SettingTenantComponent {}
