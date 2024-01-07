import {
  ICreateUnitRequest,
  IUnit,
  IUpdateUnitRequest,
} from '@einterfaces/unit.interfaces';
import { HttpErrorResponse } from '@angular/common/http';

export class ListUnit {
  static readonly type = '[Unit] List';
}

export class ListUnitSuccess {
  static readonly type = '[Unit] List Success';

  constructor(public data: IUnit[]) {}
}

export class ListUnitError {
  static readonly type = '[Unit] List Unit Error';

  constructor(public error: HttpErrorResponse) {}
}

export class CreateUnit {
  static readonly type = '[Unit] Create';

  constructor(public payload: ICreateUnitRequest) {}
}

export class CreateUnitSuccess {
  static readonly type = '[Unit] Create Success';
}

export class CreateUnitError {
  static readonly type = '[Unit] Create Error';

  constructor(public error: HttpErrorResponse) {}
}

export class UpdateUnit {
  static readonly type = '[Unit] Update';

  constructor(public payload: IUpdateUnitRequest) {}
}

export class UpdateUnitSuccess {
  static readonly type = '[Unit] Update Success';
}

export class UpdateUnitError {
  static readonly type = '[Unit] Update Error';
  constructor(public error: HttpErrorResponse) {}
}

export class DeleteUnit {
  static readonly type = '[Unit] Delete';
  constructor(public id: string) {}
}

export class DeleteUnitSuccess {
  static readonly type = '[Unit] Delete Success';
}

export class DeleteUnitError {
  static readonly type = '[Unit] Delete Error';
  constructor(public error: HttpErrorResponse) {}
}
