import { IUnit } from '@einterfaces/unit.interfaces';
import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { NotificationService, UnitService } from '@eservices';
import {
  CreateUnit,
  CreateUnitError,
  CreateUnitSuccess,
  DeleteUnit,
  DeleteUnitError,
  DeleteUnitSuccess,
  ListUnit,
  ListUnitError,
  ListUnitSuccess,
  UpdateUnit,
  UpdateUnitError,
  UpdateUnitSuccess,
} from '@estores/unit/unit.actions';
import { catchError, map, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

export interface IUnitState {
  units: IUnit[];
  loadingList: boolean;
  loadingCreate: boolean;
  loadingUpdate: boolean;
  loadingDelete: boolean;
}

@Injectable()
@State<IUnitState>({
  name: 'unit',
  defaults: {
    units: [],
    loadingList: false,
    loadingCreate: false,
    loadingUpdate: false,
    loadingDelete: false,
  },
})
export class UnitState {
  @Selector()
  static units(state: IUnitState): IUnit[] {
    return state.units;
  }

  @Selector()
  static loadingList(state: IUnitState): boolean {
    return state.loadingList;
  }

  @Selector()
  static loadingCreate(state: IUnitState): boolean {
    return state.loadingCreate;
  }

  @Selector()
  static loadingUpdate(state: IUnitState): boolean {
    return state.loadingUpdate;
  }

  @Selector()
  static loadingDelete(state: IUnitState): boolean {
    return state.loadingDelete;
  }

  constructor(
    private _unitService: UnitService,
    private _notificationService: NotificationService,
  ) {}

  @Action(ListUnit)
  listUnit(ctx: StateContext<IUnitState>) {
    ctx.patchState({
      loadingList: true,
    });
    return this._unitService.list().pipe(
      map((data) => ctx.dispatch(new ListUnitSuccess(data))),
      catchError((error) => ctx.dispatch(new ListUnitError(error))),
    );
  }

  @Action(ListUnitSuccess)
  listUnitSuccess(ctx: StateContext<IUnitState>, { data }: ListUnitSuccess) {
    ctx.patchState({
      units: data,
      loadingList: false,
    });
  }

  @Action(ListUnitError)
  listUnitError(ctx: StateContext<IUnitState>, { error }: ListUnitError) {
    ctx.patchState({
      loadingList: false,
    });
    return throwError(error);
  }

  @Action(CreateUnit)
  creatUnit(ctx: StateContext<IUnitState>, { payload }: CreateUnit) {
    ctx.patchState({
      loadingCreate: true,
    });
    return this._unitService.create(payload).pipe(
      map(() => ctx.dispatch(new CreateUnitSuccess())),
      catchError((error: HttpErrorResponse) =>
        ctx.dispatch(new CreateUnitError(error)),
      ),
    );
  }

  @Action(CreateUnitSuccess)
  creatUnitSuccess(ctx: StateContext<IUnitState>) {
    ctx.patchState({
      loadingCreate: false,
    });
    this._notificationService.success('Thêm đơn vị thành công');
  }

  @Action(CreateUnitError)
  creatUnitError(ctx: StateContext<IUnitState>, { error }: CreateUnitError) {
    ctx.patchState({
      loadingCreate: false,
    });
    return throwError(error);
  }

  @Action(UpdateUnit)
  updateUnit(ctx: StateContext<IUnitState>, { payload }: UpdateUnit) {
    ctx.patchState({
      loadingUpdate: true,
    });
    return this._unitService.update(payload).pipe(
      map(() => ctx.dispatch(new UpdateUnitSuccess())),
      catchError((error) => ctx.dispatch(new UpdateUnitError(error))),
    );
  }

  @Action(UpdateUnitSuccess)
  updateUnitSuccess(ctx: StateContext<IUnitState>) {
    ctx.patchState({
      loadingUpdate: false,
    });
    this._notificationService.success('Cập nhật đơn vị thành công');
  }

  @Action(UpdateUnitError)
  updateUnitError(ctx: StateContext<IUnitState>, { error }: UpdateUnitError) {
    ctx.patchState({
      loadingUpdate: false,
    });
    return throwError(error);
  }

  @Action(DeleteUnit)
  deleteUnit(ctx: StateContext<IUnitState>, { id }: DeleteUnit) {
    ctx.patchState({
      loadingDelete: true,
    });
    return this._unitService.delete(id).pipe(
      map(() => ctx.dispatch(new DeleteUnitSuccess())),
      catchError((error) => ctx.dispatch(new DeleteUnitError(error()))),
    );
  }

  @Action(DeleteUnitSuccess)
  deleteUnitSuccess(ctx: StateContext<IUnitState>) {
    ctx.patchState({
      loadingDelete: false,
    });
  }

  @Action(DeleteUnitError)
  deleteUnitError(ctx: StateContext<IUnitState>, { error }: DeleteUnitError) {
    ctx.patchState({
      loadingDelete: false,
    });
    return throwError(error);
  }
}
