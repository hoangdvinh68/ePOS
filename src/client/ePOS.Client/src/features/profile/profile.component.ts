import { Component } from '@angular/core';
import { IMenuItem, MenuComponent } from '@ecomponents';
import { InputTextModule } from 'primeng/inputtext';
import { NgTemplateOutlet } from '@angular/common';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [MenuComponent, InputTextModule, NgTemplateOutlet, ButtonModule],
  templateUrl: './profile.component.html',
  styles: ``,
})
export class ProfileComponent {
  items: IMenuItem[] = [
    {
      title: 'Thông tin',
      url: '/profile',
    },
  ];
}
