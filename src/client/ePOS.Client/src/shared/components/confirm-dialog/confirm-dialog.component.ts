import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ConfirmDialogService, IConfirmationDialog } from '@eservices';
import { ConfirmationService } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [DialogModule],
  templateUrl: './confirm-dialog.component.html',
  styles: ``,
  providers: [ConfirmationService],
})
export class ConfirmDialogComponent implements OnInit, OnDestroy {
  private _destroy$!: Subject<void>;

  constructor(
    private _confirmDialogService: ConfirmDialogService,
    private _confirmationService: ConfirmationService,
  ) {}

  ngOnInit() {
    this._confirmDialogService
      .getObservable()
      .pipe(takeUntil(this._destroy$))
      .subscribe((model: IConfirmationDialog) => {
        console.log(333);
        this._confirmationService.confirm({
          header: model.title,
          message: model.message,
          acceptIcon: 'pi pi-check mr-2',
          rejectIcon: 'pi pi-times mr-2',
          acceptLabel: model.textAccept,
          rejectLabel: model.textReject,
          acceptButtonStyleClass: 'p-button-danger p-button-text',
          rejectButtonStyleClass: 'p-button-text p-button-text',
          accept: model.accept,
          reject: model.reject,
        });
      });
  }

  ngOnDestroy() {
    this._destroy$.next();
    this._destroy$.complete();
  }
}
