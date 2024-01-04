import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { map, Observable } from 'rxjs';
import { IUnit } from '@einterfaces/unit.interfaces';
import { HttpClient } from '@angular/common/http';
import { IAPIResponse } from '@einterfaces/system.interfaces';

@Injectable({
  providedIn: 'root',
})
export class UnitService extends BaseService {
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
}
