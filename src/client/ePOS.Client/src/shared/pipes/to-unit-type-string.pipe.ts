import { Pipe, PipeTransform } from '@angular/core';
import { UnitTypeEnum } from '../enums/unit.enums';

@Pipe({
  name: 'toUnitTypeString',
  standalone: true,
})
export class ToUnitTypeStringPipe implements PipeTransform {
  transform(value: UnitTypeEnum): string {
    switch (value) {
      case UnitTypeEnum.Default:
        return 'Mặc định';
      case UnitTypeEnum.Manual:
        return 'Tạo thủ công';
      default:
        return '';
    }
  }
}
