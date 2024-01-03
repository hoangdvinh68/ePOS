import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { PrimeNGConfig } from 'primeng/api';
import { NotificationComponent } from '@ecomponents';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NotificationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private _primengConfig: PrimeNGConfig) {}

  ngOnInit() {
    this._primengConfig.ripple = true;
    this._primengConfig.zIndex = {
      modal: 1100, // dialog, sidebar
      overlay: 1000, // dropdown, overlay panel
      menu: 1000, // overlay menus
      tooltip: 1100, // tooltip
    };
  }
}
