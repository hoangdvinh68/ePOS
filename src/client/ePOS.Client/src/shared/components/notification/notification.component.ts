import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastModule } from 'primeng/toast';
import { NotificationService } from '@eservices';
import { MessageService } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [ToastModule],
  templateUrl: './notification.component.html',
  styles: ``,
  providers: [MessageService],
})
export class NotificationComponent implements OnInit, OnDestroy {
  private _destroy = new Subject<void>();
  constructor(
    private _notificationService: NotificationService,
    private _messageService: MessageService,
  ) {}

  ngOnInit() {
    this._notificationService
      .getObservable()
      .pipe(takeUntil(this._destroy))
      .subscribe((data) => {
        this._messageService.add({
          severity: data.status,
          detail: data.message,
        });
      });
  }

  ngOnDestroy() {
    this._destroy.next();
    this._destroy.complete();
  }
}
