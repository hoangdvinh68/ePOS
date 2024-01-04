export interface ITenant {
  id: string;
  subId: string;
  name: string;
  code: string;
  currencyId: string;
  companyName?: string;
  companyAddress?: string;
  isTaxAppliedAllItem: boolean;
  taxRate?: number;
  isItemPriceIncludeTax?: boolean;
  createdAt: any;
}
