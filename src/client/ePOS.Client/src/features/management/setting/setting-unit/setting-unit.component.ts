import { Component, OnInit } from '@angular/core';
import {
  CellDirective,
  ColumnDirective,
  HeaderDirective,
  TableComponent,
} from '@ecomponents';
import { Select, Store } from '@ngxs/store';
import {
  CreateUnit,
  DeleteUnit,
  ListUnit,
  UnitState,
  UpdateUnit,
} from '@estores/unit';
import { Observable } from 'rxjs';
import { IUnit } from '@einterfaces/unit.interfaces';
import { AsyncPipe, NgIf } from '@angular/common';
import { ToUnitTypeStringPipe } from '@epipes/_index';
import {
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { UnitTypeEnum } from '@eenums/unit.enums';
import { scrollToTop } from '@eutilities';
import { ConfirmDialogService } from '@eservices';

@Component({
  selector: 'app-setting-unit',
  standalone: true,
  imports: [
    TableComponent,
    AsyncPipe,
    ColumnDirective,
    CellDirective,
    ToUnitTypeStringPipe,
    HeaderDirective,
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    ButtonModule,
    NgIf,
  ],
  templateUrl: './setting-unit.component.html',
  styles: ``,
})
export class SettingUnitComponent implements OnInit {
  @Select(UnitState.units) units$!: Observable<IUnit[]>;
  @Select(UnitState.loadingCreate) loadingCreate$!: Observable<boolean>;
  @Select(UnitState.loadingUpdate) loadingUpdate$!: Observable<boolean>;
  @Select(UnitState.loadingDelete) loadingDelete$!: Observable<boolean>;
  form!: UntypedFormGroup;
  formType: 'create' | 'update' | undefined;
  protected readonly UnitTypeEnum = UnitTypeEnum;

  constructor(
    private _store: Store,
    private _formBuilder: FormBuilder,
    private _confirmDialogService: ConfirmDialogService,
  ) {}
  ngOnInit() {
    this._store.dispatch(new ListUnit());
    this.buildForm();
  }

  onCreate() {
    scrollToTop();
    this.formType = 'create';
  }

  onUpdate(unit: IUnit) {
    scrollToTop();
    this.formType = 'update';
    this.form.patchValue({
      id: unit.id,
      name: unit.name,
    });
  }

  onCancel() {
    this.form.reset();
    this.formType = undefined;
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    switch (this.formType) {
      case 'create':
        this._store.dispatch(new CreateUnit(this.form.value)).subscribe({
          next: () => {
            this._store.dispatch(new ListUnit());
            this.onCancel();
          },
        });
        break;
      case 'update':
        this._store.dispatch(new UpdateUnit(this.form.value)).subscribe({
          next: () => {
            this._store.dispatch(new ListUnit());
            this.onCancel();
          },
        });
        break;
    }
  }

  onDelete(id: string) {
    this._confirmDialogService.show({
      title: 'Xóa đơn vị',
      message: 'Bạn có muốn xóa đơn vị không?',
      textReject: 'Hoàn tác',
      textAccept: 'Đồng ý',
      accept: () => {
        this._store.dispatch(new DeleteUnit(id)).subscribe({
          next: () => {
            this._store.dispatch(new ListUnit());
          },
        });
      },
    });
    console.log(111);
  }

  private buildForm() {
    this.form = this._formBuilder.group({
      id: [null],
      name: [null, [Validators.required]],
    });
  }
}
