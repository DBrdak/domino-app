import { DateTimeRange, DeliveryInfo, Location, Money, PersonalInfo } from "./common";
import { Product } from "./product";

export interface ShoppingCartItem {
  quantity: number
  unit: string
  kgPerPcs: number | null
  productId: string
  productName: string
  productImage: string
  price: Money
  totalValue: number
}

export class ShoppingCartItem implements ShoppingCartItem {
  constructor(product: Product, quantity: {unit: string, quantity: number}) {
    this.quantity = quantity.quantity
    this.unit = quantity.unit
    this.kgPerPcs = product.quantityModifier.kgPerPcs
    this.productId = product.id
    this.productName = product.name
    this.productImage = product.image
    this.price = product.price
    this.totalValue = 0
  }
}

export interface ShoppingCart {
  shoppingCartId: string
  items: ShoppingCartItem[]
  totalPrice: number
  currency: string
}

export class ShoppingCart implements ShoppingCart {  
  constructor(init?:ShoppingCart){
    Object.assign(this, init)
  }

  addShoppingCartItem = (shoppingCartItem:ShoppingCartItem) => {
    if(this.items.some(i => i.productId === shoppingCartItem.productId)) {
      this.removeShoppingCartItem(shoppingCartItem)
    } 
    this.items.push(shoppingCartItem)
  }

  removeShoppingCartItem = (item: ShoppingCartItem) => {
    this.items = this.items.filter(i => i.productId !== item.productId)
  }
}

export interface ShoppingCartCheckout {
  shoppingCartId: string;
  totalPrice: number;
  currency: string
  items: ShoppingCartItem[];
  phoneNumber: string;
  firstName: string;
  lastName: string;
  deliveryLocation: Location;
  deliveryDate: DateTimeRange;
}

export class ShoppingCartCheckout implements ShoppingCartCheckout {
  constructor(init: ShoppingCart, personalInfo: PersonalInfo, deliveryInfo: DeliveryInfo) {
    this.shoppingCartId = init?.shoppingCartId
    this.totalPrice = init.totalPrice
    this.items = init.items
    this.currency = init.currency
    this.phoneNumber = personalInfo.phoneNumber
    this.firstName = personalInfo.firstName
    this.lastName = personalInfo.lastName
    this.deliveryLocation = deliveryInfo.deliveryLocation
    this.deliveryDate = deliveryInfo.deliveryDate
  }
}