import { ITenant } from '@einterfaces/tenant.interfaces';
import { HttpErrorResponse } from '@angular/common/http';

export class GetTenant {
  static readonly type = '[Tenant] Get';

  constructor() {}
}

export class GetTenantSuccess {
  static readonly type = '[Tenant] Get Success';

  constructor(public response: ITenant) {}
}

export class GetTenantError {
  static readonly type = '[Tenant] Get Error';

  constructor(public error: HttpErrorResponse) {}
}
