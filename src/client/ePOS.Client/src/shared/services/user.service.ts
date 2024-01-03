import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ISignInRequest, ISignInResponse } from '@einterfaces/user.interfaces';
import { map, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IAPIResponse, IUserClaimsValue } from '@einterfaces/system.interfaces';
import { jwtDecode } from 'jwt-decode';
import {
  ACCESS_TOKEN,
  REFRESH_TOKEN,
  USER_CLAIMS,
} from '@econstants/system.constants';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService {
  constructor(private _httpClient: HttpClient) {
    super();
    this.controller = 'user';
  }

  signIn(payload: ISignInRequest): Observable<ISignInResponse> {
    const api = this.getApiUrl('sign-in');
    return this._httpClient
      .post<IAPIResponse<ISignInResponse>>(api, payload)
      .pipe(map((response) => response.data));
  }

  setDataToken(data: ISignInResponse) {
    localStorage.setItem(ACCESS_TOKEN, data.accessToken);
    localStorage.setItem(REFRESH_TOKEN, data.refreshToken);
    localStorage.setItem(
      USER_CLAIMS,
      JSON.stringify(this.decodeToken(data.accessToken)),
    );
  }

  clearDataToken() {
    localStorage.removeItem(ACCESS_TOKEN);
    localStorage.removeItem(REFRESH_TOKEN);
    localStorage.removeItem(USER_CLAIMS);
  }

  getUserClaimsValue(): IUserClaimsValue | null {
    const data = localStorage.getItem(USER_CLAIMS);
    return data ? JSON.parse(data) : null;
  }

  isAccessTokenExpiredOrNull(): boolean {
    const claims = this.getUserClaimsValue();
    if (!claims) return false;
    return claims.exp * 1000 < Date.now();
  }

  decodeToken(token: string): IUserClaimsValue {
    return jwtDecode<IUserClaimsValue>(token);
  }
}
