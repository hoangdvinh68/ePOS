import {
  AfterViewInit,
  Component,
  ContentChildren,
  Input,
  QueryList,
  ViewChild,
} from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { ColumnDirective } from './directives/column.directive';
import { JsonPipe, NgForOf, NgIf, NgTemplateOutlet } from '@angular/common';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [TableModule, NgForOf, JsonPipe, NgIf, NgTemplateOutlet],
  templateUrl: './table.component.html',
  styles: ``,
})
export class TableComponent {
  @ViewChild('dynamicTable', { static: true }) dynamicTable!: Table;
  @ContentChildren(ColumnDirective) columns!: QueryList<ColumnDirective>;
  @Input() data!: any[];
}
