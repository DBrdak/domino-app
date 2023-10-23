import { useMediaQuery } from "@mui/material";
import theme from "../layout/theme";

export interface Pagination {
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export class Pagination implements Pagination {
  constructor(init: Pagination) {
    Object.assign(this, init)
  }
}

export class PaginatedResult<T> {
  items: T;
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;

  constructor(data: T, page: number, pageSize: number, totalCount: number, totalPages: number, hasNextPage: boolean, hasPreviousPage: boolean) {
    this.items = data;
    this.page = page;
    this.pageSize = pageSize;
    this.totalCount = totalCount;
    this.totalPages = totalPages;
    this.hasNextPage = hasNextPage;
    this.hasPreviousPage = hasPreviousPage;
  }
}

export class PagingParams {
  pageNumber
  pageSize
  
  constructor(pageNumber = 1, pageSize = 8) {
    this.pageNumber = pageNumber
    this.pageSize = pageSize
  }
}