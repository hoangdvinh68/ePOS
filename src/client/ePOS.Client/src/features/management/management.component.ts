import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { IMenuItem, MenuComponent } from '@ecomponents';

@Component({
  selector: 'app-management',
  standalone: true,
  imports: [RouterOutlet, MenuComponent],
  templateUrl: './management.component.html',
  styles: ``,
})
export class ManagementComponent {
  state!: string;

  menuItems: IMenuItem[] = [
    {
      title: 'Cài đặt',
      url: '/management',
    },
    {
      title: 'Chi nhánh',
      url: '/management/shop',
    },
  ];
}
