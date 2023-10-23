export interface FilterOptions {
  searchPhrase: string | null
  subcategory: string | null
  minPrice: number | null;
  maxPrice: number | null;
  isAvailable: boolean;
  isDiscounted: boolean;
  sortProperty: string
  sortDirection: 'asc' | 'desc';
}

export class FilterOptions implements FilterOptions {
  constructor() {
    this.searchPhrase = null
    this.subcategory = null
    this.minPrice = null
    this.maxPrice = null
    this.isAvailable = false
    this.isDiscounted = false
    this.sortProperty = 'name'
    this.sortDirection = 'asc'
  }
}