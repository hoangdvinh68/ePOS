import { UnitTypeEnum } from '../enums/unit.enums';

export interface IUnit {
  id: string;
  subId: number;
  name: string;
  type: UnitTypeEnum;
}

export interface ICreateUnitRequest {
  name: string;
}

export interface IUpdateUnitRequest {
  id: string;
  name: string;
}
