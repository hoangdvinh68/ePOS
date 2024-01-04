import { IUnit } from '@einterfaces/unit.interfaces';
import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { UnitService } from '@eservices';
import {
  ListUnit,
  ListUnitError,
  ListUnitSuccess,
} from '@estores/unit/unit.actions';
import { catchError, map, throwError } from 'rxjs';

export interface IUnitState {
  units: IUnit[];
}

@Injectable()
@State<IUnitState>({
  name: 'unit',
  defaults: {
    units: [],
  },
})
export class UnitState {
  @Selector()
  static units(state: IUnitState): IUnit[] {
    return state.units;
  }
  constructor(private _unitService: UnitService) {}

  @Action(ListUnit)
  listUnit(ctx: StateContext<IUnitState>) {
    return this._unitService.list().pipe(
      map((data) => ctx.dispatch(new ListUnitSuccess(data))),
      catchError((error) => ctx.dispatch(new ListUnitError(error))),
    );
  }

  @Action(ListUnitSuccess)
  listUnitSuccess(ctx: StateContext<IUnitState>, { data }: ListUnitSuccess) {
    ctx.patchState({
      units: data,
    });
  }

  @Action(ListUnitError)
  listUnitError(ctx: StateContext<IUnitState>, { error }: ListUnitError) {
    return throwError(error);
  }
}
