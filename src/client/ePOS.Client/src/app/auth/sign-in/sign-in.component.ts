import { Component, OnInit } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import {
  ReactiveFormsModule,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { JsonPipe } from '@angular/common';
import { PasswordModule } from 'primeng/password';
import { emailRegex } from '@eutilities';
import { NotificationService, UserService } from '@eservices';
import { ISignInRequest } from '@einterfaces/user.interfaces';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [
    InputTextModule,
    ButtonModule,
    ReactiveFormsModule,
    JsonPipe,
    PasswordModule,
  ],
  templateUrl: './sign-in.component.html',
  styles: ``,
})
export class SignInComponent implements OnInit {
  form!: UntypedFormGroup;
  constructor(
    private _formBuilder: UntypedFormBuilder,
    private _userService: UserService,
    private _notificationService: NotificationService,
    private _router: Router,
  ) {}

  ngOnInit() {
    this.form = this._formBuilder.group({
      email: [null, [Validators.required, Validators.pattern(emailRegex)]],
      password: [null, Validators.required],
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }
    this._userService.signIn(this.form.value as ISignInRequest).subscribe({
      next: (response) => {
        this._userService.setDataToken(response);
        this._notificationService.success('Đăng nhập thành công');
        this._router.navigate(['/']).then();
      },
    });
  }
}
