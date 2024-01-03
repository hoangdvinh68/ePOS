import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForOf } from '@angular/common';

export interface ISubMenuItem {
  id?: string;
  title: string;
  active?: boolean;
}

@Component({
  selector: 'app-sub-menu',
  standalone: true,
  imports: [NgForOf],
  templateUrl: './sub-menu.component.html',
  styles: ``,
})
export class SubMenuComponent implements OnInit {
  @Input() items: ISubMenuItem[] = [];
  @Output() itemActive = new EventEmitter<string>();

  ngOnInit() {
    this.items[0].active = true;
    this.itemActive.emit(this.items[0].id);
  }

  onActive(item: ISubMenuItem) {
    this.items.forEach((item) => (item.active = false));
    item.active = true;
    this.itemActive.emit(item.id);
  }
}
