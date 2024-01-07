import { Component, OnInit } from '@angular/core';
import { DropdownModule } from 'primeng/dropdown';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { ITenant } from '@einterfaces/tenant.interfaces';
import {
  FormBuilder,
  ReactiveFormsModule,
  UntypedFormGroup,
} from '@angular/forms';
import { GetTenant, TenantState } from '@estores/tenant';
import { AsyncPipe } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { CurrencyState, ListCurrency } from '@estores/currency';
import { ICurrency } from '@einterfaces/currency.interfaces';
import moment from 'moment';

@Component({
  selector: 'app-setting-tenant',
  standalone: true,
  imports: [DropdownModule, ReactiveFormsModule, AsyncPipe, InputTextModule],
  templateUrl: './setting-tenant.component.html',
  styles: ``,
})
export class SettingTenantComponent implements OnInit {
  form!: UntypedFormGroup;
  @Select(TenantState.tenant) tenant$!: Observable<ITenant>;
  @Select(CurrencyState.currencies) currencies$!: Observable<ICurrency[]>;
  constructor(
    private _store: Store,
    private _formBuilder: FormBuilder,
  ) {}

  ngOnInit() {
    this.buildForm();
    this._store.dispatch(new GetTenant());
    this._store.dispatch(new ListCurrency());
    this.tenant$.subscribe({
      next: (data) => {
        this.form.patchValue({
          id: data?.id,
          name: data?.name,
          code: data?.code,
          currencyId: data?.currencyId,
          companyName: data?.companyName,
          companyAddress: data?.companyAddress,
          createdAt: moment(data?.createdAt).format('DD-MM-YYYY'),
        });
      },
    });
  }

  private buildForm() {
    this.form = this._formBuilder.group({
      id: [null],
      name: [null],
      code: [null],
      currencyId: [null],
      companyName: [null],
      companyAddress: [null],
      createdAt: [null],
    });
  }
}
