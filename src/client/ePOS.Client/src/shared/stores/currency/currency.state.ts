import { ICurrency } from '@einterfaces/currency.interfaces';
import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  ListCurrency,
  ListCurrencyError,
  ListCurrencySuccess,
} from '@estores/currency/currency.actions';
import { CurrencyService } from '@eservices';
import { catchError, map, throwError } from 'rxjs';

export interface ICurrencyState {
  currencies: ICurrency[];
}

@Injectable()
@State<ICurrencyState>({
  name: 'currency',
  defaults: {
    currencies: [],
  },
})
export class CurrencyState {
  @Selector()
  static currencies(state: ICurrencyState): ICurrency[] {
    return state.currencies;
  }
  constructor(private _currencyService: CurrencyService) {}

  @Action(ListCurrency)
  listCurrency(ctx: StateContext<ICurrencyState>) {
    return this._currencyService.list().pipe(
      map((data) => ctx.dispatch(new ListCurrencySuccess(data))),
      catchError((error) => ctx.dispatch(new ListCurrencyError(error))),
    );
  }

  @Action(ListCurrencySuccess)
  listCurrencySuccess(
    ctx: StateContext<ICurrencyState>,
    { data }: ListCurrencySuccess,
  ) {
    ctx.patchState({
      currencies: data,
    });
  }

  @Action(ListCurrencyError)
  listCurrencyError(
    ctx: StateContext<ICurrencyState>,
    { error }: ListCurrencyError,
  ) {
    return throwError(error);
  }
}
