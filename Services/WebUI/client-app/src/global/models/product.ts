import { Money, QuantityModifier } from "./common";

export interface Product {
  id: string;
  name: string;
  description: string;
  category: string;
  subcategory: string;
  image: string;
  price: Money
  isAvailable: boolean;
  isDiscounted: boolean;
  quantityModifier: QuantityModifier;
}

export class Product implements Product {
  constructor(init?: Product) {
    Object.assign(this, init)
  }
}