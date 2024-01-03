export interface IAPIResponse<T = any> {
  success: boolean;
  statusCode: number;
  message?: string;
  data: T;
}

export interface IUserClaimsValue {
  exp: number;
  id: string;
  tenantId: string;
  fullName: string;
  email: string;
}

export interface IQueryDetail {
  pageIndex: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
}
