import { Injectable } from '@angular/core';
import { environment } from '@eenvironments/environment';

@Injectable()
export class BaseApiService {
  protected host = environment.host;
  protected controller!: string;
  constructor() {}

  protected getApiUrl(action?: string) {
    return `${this.host}/api/v1/${this.controller}/${action}`;
  }
}
