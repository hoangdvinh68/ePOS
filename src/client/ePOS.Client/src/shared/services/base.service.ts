import { Injectable } from '@angular/core';
import { environment } from '@eenvironments/environment';

@Injectable()
export class BaseService {
  protected host = environment.host;
  protected controller!: string;
  constructor() {}

  getApiUrl(action?: string) {
    return `${this.host}/api/v1/${this.controller}/${action}`;
  }
}
