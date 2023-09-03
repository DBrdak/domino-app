import { Money, Photo, Quantity } from "./common";

export interface Product {
  id: string;
  name: string;
  description: string;
  category: Category;
  subcategory: string;
  image: Photo;
  price: Money
  details: ProductDetails;
  discountedPrice: Money | null
  alternativeUnitPrice: Money | null
}

export interface Category {
  value: string
}

export interface ProductDetails {
  isAvailable: boolean
  isDiscounted: boolean
  isWeightSwitchAllowed: boolean
  singleWeight: Quantity | null
}

export class Product implements Product {
  constructor(init?: Product) {
    Object.assign(this, init)
  }
}