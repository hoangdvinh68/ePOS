import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { map, Observable } from 'rxjs';
import {
  ICreateUnitRequest,
  IUnit,
  IUpdateUnitRequest,
} from '@einterfaces/unit.interfaces';
import { HttpClient } from '@angular/common/http';
import { IAPIResponse } from '@einterfaces/system.interfaces';

@Injectable({
  providedIn: 'root',
})
export class UnitService extends BaseApiService {
  constructor(private _httpClient: HttpClient) {
    super();
    this.controller = 'unit';
  }

  list(): Observable<IUnit[]> {
    const api = this.getApiUrl('list');
    return this._httpClient
      .get<IAPIResponse<IUnit[]>>(api)
      .pipe(map((response) => response.data));
  }

  create(payload: ICreateUnitRequest): Observable<any> {
    const api = this.getApiUrl('create');
    return this._httpClient.post<IAPIResponse<any>>(api, payload);
  }

  update(payload: IUpdateUnitRequest): Observable<any> {
    const api = this.getApiUrl('update');
    return this._httpClient.put<IAPIResponse<any>>(api, payload);
  }

  delete(id: string): Observable<any> {
    const api = this.getApiUrl('delete');
    return this._httpClient.delete<IAPIResponse<any>>(api, {
      params: {
        id,
      },
    });
  }
}
