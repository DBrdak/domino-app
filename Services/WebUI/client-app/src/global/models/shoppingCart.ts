import { DateTimeRange, Location } from "./common";
import { Product } from "./product";

export interface ShoppingCartItem {
  quantity: number
  unit: string
  kgPerPcs: number | null
  price: number
  productId: string
  productName: string
  productImage: string
}

export class ShoppingCartItem implements ShoppingCartItem {
  constructor(product: Product, quantity: {unit: string, quantity: number}) {
    this.quantity = quantity.quantity
    this.unit = quantity.unit
    this.kgPerPcs = product.quantityModifier.kgPerPcs
    this.price = product.price.amount
    this.productId = product.id
    this.productName = product.name
    this.productImage = product.image
  }
}

export interface ShoppingCart {
  shoppingCartId: string
  items: ShoppingCartItem[]
  totalPrice: number
}

export class ShoppingCart implements ShoppingCart {  
  constructor(init?:ShoppingCart){
    Object.assign(this, init)
  }

  addShoppingCartItem = (shoppingCartItem:ShoppingCartItem) => {
    this.items.push(shoppingCartItem)
  }
}

export interface ShoppingCartCheckout {
  shoppingCartId: string;
  totalPrice: number;
  items: ShoppingCartItem[];
  phoneNumber: string;
  firstName: string;
  lastName: string;
  deliveryLocation: Location;
  deliveryDate: DateTimeRange;
}

export class ShoppingCartCheckout implements ShoppingCartCheckout {
  constructor(init?: ShoppingCartCheckout) {
    Object.assign(this, init)
  }
}