export interface FilterOptions {
  searchPhrase: string | null
  subcategory: string | null
  minPrice: number | null;
  maxPrice: number | null;
  isAvailable: boolean;
  isDiscounted: boolean;
  sortProperty: string | null;
  sortDirection: 'asc' | 'desc';
}

export class FilterOptions implements FilterOptions {
  searchPhrase: string | null
  subcategory: string | null;
  minPrice: number | null;
  maxPrice: number | null;
  isAvailable: boolean;
  isDiscounted: boolean;
  sortProperty: string | null;
  sortDirection: 'asc' | 'desc';

  constructor(options: FilterOptions) {
    this.searchPhrase = options.searchPhrase
    this.subcategory = options.subcategory;
    this.minPrice = options.minPrice;
    this.maxPrice = options.maxPrice;
    this.isAvailable = options.isAvailable;
    this.isDiscounted = options.isDiscounted;
    this.sortProperty = options.sortProperty;
    this.sortDirection = options.sortDirection;
  }
}