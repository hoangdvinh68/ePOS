import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '@ecomponents';
import { UserAvatarComponent } from '@ecomponents';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, UserAvatarComponent],
  templateUrl: './main.component.html',
})
export class MainComponent {
  title = '';
  onTitleChange(title: string) {
    this.title = title;
  }
}
