import { Injectable } from '@angular/core';
import { environment } from '@epos-environments/environment';

@Injectable()
export class BaseService {
  protected host: string = environment.host;
  protected controller!: string;

  constructor() {}

  getApiUrl(action: string) {
    return `${this.host}/api/v1/${this.controller}/${action}`;
  }
}
