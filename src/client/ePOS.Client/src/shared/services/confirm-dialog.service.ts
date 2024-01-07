import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

export interface IConfirmationDialog {
  title?: string;
  message: string;
  textReject: string;
  textAccept: string;
  accept?: () => void;
  reject?: () => void;
}

@Injectable({
  providedIn: 'root',
})
export class ConfirmDialogService {
  private _subject = new Subject<IConfirmationDialog>();
  constructor() {}

  getObservable(): Observable<IConfirmationDialog> {
    return this._subject;
  }

  show(model: IConfirmationDialog) {
    this._subject.next(model);
    console.log(2222);
  }
}
