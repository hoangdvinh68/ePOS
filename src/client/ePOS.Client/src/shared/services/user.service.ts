import { Injectable } from '@angular/core';
import { BaseService } from '@epos-shared/services/base.service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService {
  constructor() {
    super();
    this.controller = 'user';
  }
}
