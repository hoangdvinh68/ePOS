import { Component, OnInit } from '@angular/core';
import { AvatarModule } from 'primeng/avatar';
import { OverlayModule } from 'primeng/overlay';
import { NgTemplateOutlet } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { RippleModule } from 'primeng/ripple';
import { UserService } from '@eservices';

@Component({
  selector: 'app-user-avatar',
  standalone: true,
  imports: [
    AvatarModule,
    OverlayModule,
    NgTemplateOutlet,
    RouterLink,
    RippleModule,
  ],
  templateUrl: './user-avatar.component.html',
  styles: ``,
})
export class UserAvatarComponent implements OnInit {
  visible = false;
  fullName: string | undefined;
  email: string | undefined;
  label: string | undefined;

  constructor(
    private _userService: UserService,
    private _router: Router,
  ) {}

  ngOnInit() {
    this.fullName = this._userService.getUserClaimsValue()?.fullName;
    this.email = this._userService.getUserClaimsValue()?.email;
    const fullNameArray = this.fullName?.split(' ') ?? [];
    this.label = fullNameArray[fullNameArray.length - 1][0];
  }

  onSignOut() {
    this._userService.clearDataToken();
    this._router.navigate(['/auth/sign-in']).then();
  }
}
