import { ContentChild, Directive, Input } from '@angular/core';
import { HeaderDirective } from './header.directive';
import { CellDirective } from './cell.directive';

@Directive({
  selector: 'app-column',
  standalone: true,
})
export class ColumnDirective {
  @ContentChild(HeaderDirective, { static: true }) headerTpl?: HeaderDirective;
  @ContentChild(CellDirective, { static: true }) cellTpl?: CellDirective;
  @Input() header = '';
  @Input() key = '';
  @Input() width?: string;
  constructor() {}
}
