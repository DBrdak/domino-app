import { Money, Photo, Quantity } from "./common";

export interface Product {
  id: string
  name: string;
  description: string;
  category: Category;
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
  image: string | null;
  price: Money | null;
  category: Category | null
  isWeightSwitchAllowed: boolean;
  singleWeight: number | null;
}

export class ProductCreateValues implements ProductCreateValues {
  constructor(init: ProductCreateValues | null) {
    this.name = init ? init.name : ''
    this.description = init ? init.description : ''
    this.image = null
    this.price = null
    this.category = null
    this.isWeightSwitchAllowed = init ? init.isWeightSwitchAllowed : false
    this.singleWeight = init ? init.singleWeight : 0
  }
}

export interface ProductUpdateValues {
  id: string;
  name: string;
  description: string;
  imageUrl: string;
  isWeightSwitchAllowed: boolean;
  singleWeight: number | null;
  isAvailable: boolean;
}

export class ProductUpdateValues implements ProductUpdateValues {
  constructor(product: Product, isAvailable: boolean | null) {
    this.id = product.id
    this.name = product.name
    this.description = product.description
    this.imageUrl = product.image.url
    this.isWeightSwitchAllowed = product.details.isWeightSwitchAllowed
    this.singleWeight = product.details.isWeightSwitchAllowed ? (product.details.singleWeight && product.details.singleWeight.value) : null
    this.isAvailable = isAvailable !== null ? isAvailable : product.details.isAvailable
  }
}