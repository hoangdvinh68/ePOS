import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { ICurrency } from '@einterfaces/currency.interfaces';
import { IAPIResponse } from '@einterfaces/system.interfaces';

@Injectable({
  providedIn: 'root',
})
export class CurrencyService extends BaseService {
  constructor(private _httpClient: HttpClient) {
    super();
    this.controller = 'currency';
  }

  list(): Observable<ICurrency[]> {
    const api = this.getApiUrl('list');
    return this._httpClient
      .get<IAPIResponse<ICurrency[]>>(api)
      .pipe(map((response) => response.data));
  }
}
