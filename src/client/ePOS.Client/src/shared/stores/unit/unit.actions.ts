import { IUnit } from '@einterfaces/unit.interfaces';
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
