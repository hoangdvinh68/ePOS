import { HttpErrorResponse } from '@angular/common/http';
import { ICurrency } from '@einterfaces/currency.interfaces';

export class ListCurrency {
  static readonly type = '[Currency] List';
}

export class ListCurrencySuccess {
  static readonly type = '[Currency] List Success';
  constructor(public data: ICurrency[]) {}
}

export class ListCurrencyError {
  static readonly type = '[Currency] List Error';
  constructor(public error: HttpErrorResponse) {}
}
