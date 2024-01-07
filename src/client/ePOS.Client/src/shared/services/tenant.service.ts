import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { ITenant } from '@einterfaces/tenant.interfaces';
import { IAPIResponse } from '@einterfaces/system.interfaces';

@Injectable({
  providedIn: 'root',
})
export class TenantService extends BaseApiService {
  constructor(private _httpClient: HttpClient) {
    super();
    this.controller = 'tenant';
  }

  getTenant(tenantId: string): Observable<ITenant> {
    const api = this.getApiUrl('get');
    return this._httpClient
      .get<IAPIResponse<ITenant>>(api, {
        params: {
          tenantId,
        },
      })
      .pipe(map((response) => response.data));
  }
}
