
export interface Pagination {
  curretPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PaginatedResult<T>{
  result: T | undefined;
  pagination: Pagination | undefined;

}