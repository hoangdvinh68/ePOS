import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { ITenant } from '@einterfaces/tenant.interfaces';
import {
  GetTenant,
  GetTenantError,
  GetTenantSuccess,
} from '@estores/tenant/tenant.actions';
import { TenantService, UserService } from '@eservices';
import { catchError, map, throwError } from 'rxjs';

export interface ITenantState {
  tenant?: ITenant;
}

@Injectable()
@State<ITenantState>({
  name: 'tenant',
  defaults: {
    tenant: undefined,
  },
})
export class TenantState {
  @Selector()
  static tenant(state: ITenantState): ITenant | undefined {
    return state.tenant;
  }

  constructor(
    private _tenantService: TenantService,
    private _userService: UserService,
  ) {}

  @Action(GetTenant)
  getTenant(ctx: StateContext<ITenantState>) {
    return this._tenantService
      .getTenant(this._userService.getUserClaimsValue()?.tenantId!)
      .pipe(
        map((response) => ctx.dispatch(new GetTenantSuccess(response))),
        catchError((error) => ctx.dispatch(new GetTenantError(error))),
      );
  }

  @Action(GetTenantSuccess)
  getTenantSuccess(
    ctx: StateContext<ITenantState>,
    { response }: GetTenantSuccess,
  ) {
    ctx.patchState({
      tenant: response,
    });
  }

  @Action(GetTenantError)
  getTenantError(ctx: StateContext<ITenantState>, { error }: GetTenantError) {
    return throwError(error);
  }
}
