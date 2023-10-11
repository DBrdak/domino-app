import { Money, Photo, Quantity } from "./common";

export interface Product {
  id: string
  name: string;
  description: string;
  category: Category;
  subcategory: string;
  image: Photo;
  price: Money;
  details: ProductDetails;
  discountedPrice: Money | null;
  alternativeUnitPrice: Money | null;
}
export interface Category {
  value: string
}

interface ProductDetails {
  isAvailable: boolean;
  isDiscounted: boolean;
  isWeightSwitchAllowed: boolean;
  singleWeight: Quantity | null; 
}

export class Product implements Product {
  constructor(init?: Product) {
    Object.assign(this, init)
  }
}

export interface ProductCreateValues {
  name: string;
  description: string;
  category: string;
  subcategory: string;
  image: string | null;
  price: Money | null;
  isWeightSwitchAllowed: boolean;
  singleWeight: number | null;
}

export class ProductCreateValues implements ProductCreateValues {
  constructor(init: ProductCreateValues | null) {
    this.name = init ? init.name : ''
    this.description = init ? init.description : ''
    this.category = init ? init.category : ''
    this.subcategory = init ? init.subcategory : ''
    this.image = null
    this.price = null
    this.isWeightSwitchAllowed = init ? init.isWeightSwitchAllowed : false
    this.singleWeight = init ? init.singleWeight : 0
  }
}

export interface ProductUpdateValues {
  id: string;
  name: string;
  description: string;
  category: string;
  subcategory: string;
  imageUrl: string | null;
  isWeightSwitchAllowed: boolean;
  singleWeight: number | null;
  isAvailable: boolean;
}

export class ProductUpdateValues implements ProductUpdateValues {
  constructor(init: ProductUpdateValues) {
    this.id = init.id
    this.name = init.name
    this.description = init.description
    this.category = init.category
    this.subcategory = init.subcategory
    this.imageUrl = null
    this.isWeightSwitchAllowed = init.isWeightSwitchAllowed
    this.singleWeight = init.isWeightSwitchAllowed ? init.singleWeight : null
    this.isAvailable = init.isAvailable
  }
}