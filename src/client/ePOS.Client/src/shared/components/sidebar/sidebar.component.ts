import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { CommonModule, NgTemplateOutlet } from '@angular/common';
import { filter } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, NgTemplateOutlet, CommonModule],
  templateUrl: './sidebar.component.html',
  styles: ``,
})
export class SidebarComponent implements OnInit {
  @Output() currentTitle = new EventEmitter<string>();

  items = [
    {
      faIcon: 'fa-solid fa-layer-group',
      title: 'Quản lý',
      url: '/management',
      selected: false,
    },
    {
      faIcon: 'fa-regular fa-cup-togo',
      title: 'Sản phẩm',
      url: '/library',
      selected: false,
    },
    {
      faIcon: '',
      title: 'Hồ sơ cá nhân',
      url: '/profile',
      selected: false,
      hidden: true,
    },
  ];

  constructor(private _router: Router) {}

  ngOnInit() {
    this.items.forEach((item) => {
      item.selected = this._router.url.includes(item.url);
      if (item.selected) this.currentTitle.emit(item.title);
    });
    this._router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        this.items.forEach((item) => {
          item.selected = item.url.split('/')[1] === event.url.split('/')[1];
          if (item.selected) this.currentTitle.emit(item.title);
        });
      });
  }
}
