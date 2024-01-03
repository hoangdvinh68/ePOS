import { Component } from '@angular/core';
import { AvatarModule } from 'primeng/avatar';
import { OverlayModule } from 'primeng/overlay';
import { NgTemplateOutlet } from '@angular/common';

@Component({
  selector: 'app-user-avatar',
  standalone: true,
  imports: [AvatarModule, OverlayModule, NgTemplateOutlet],
  templateUrl: './user-avatar.component.html',
  styles: ``,
})
export class UserAvatarComponent {
  visible = false;
}
