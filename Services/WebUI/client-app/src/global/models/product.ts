export interface Product {
  id: string;
  name: string;
  description: string;
  category: string;
  subcategory: string;
  image: string;
  price: number;
  currency: string;
  unit: string;
  isAvailable: boolean;
  isDiscounted: boolean;
  isPcsAllowed: boolean;
  kgPerPcs: number;
}

export class Product implements Product {
  constructor(init?: Product) {
    Object.assign(this, init)
  }
}