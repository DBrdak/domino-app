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
// TODO Dodać że gdy zaznaczone isAvailable to pokazują siętylko dostępne, a gdy niezaznaczone to i te i te
// TODO To samo dla isDiscounted
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