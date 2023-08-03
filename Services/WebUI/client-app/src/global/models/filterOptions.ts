interface FilterOptions {
  subcategory: string
  minPrice: number | null;
  maxPrice: number | null;
  isAvailable: boolean;
  isDiscounted: boolean;
  sortProperty: string;
  sortDirection: 'asc' | 'desc';
  pcsMode: boolean;
}