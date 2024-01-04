import { Component, OnInit } from '@angular/core';
import { IMenuItem, MenuComponent } from '@ecomponents';
import { InputTextModule } from 'primeng/inputtext';
import { NgTemplateOutlet } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import {
  ReactiveFormsModule,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { UserService } from '@eservices';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    MenuComponent,
    InputTextModule,
    NgTemplateOutlet,
    ButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './profile.component.html',
  styles: ``,
})
export class ProfileComponent implements OnInit {
  form!: UntypedFormGroup;
  items: IMenuItem[] = [
    {
      title: 'Thông tin',
      url: '/profile',
    },
  ];

  constructor(
    private _formBuilder: UntypedFormBuilder,
    private _userService: UserService,
  ) {}

  ngOnInit() {
    this.form = this._formBuilder.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      email: [null, Validators.required],
    });
    const email = this._userService.getUserClaimsValue()?.email;
    this._userService.getProfile(email!).subscribe({
      next: (response) => {
        this.form.patchValue({
          firstName: response.firstName,
          lastName: response.lastName,
          email: response.email,
        });
      },
    });
  }
}
