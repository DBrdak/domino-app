import { PathOptions } from "leaflet";
import { DateTimeRange, DeliveryInfo, Location, Money, PersonalInfo, Photo, Quantity } from "./common";
import { Product } from "./product";
import {DeliveryPoint} from "./shop";

export interface ShoppingCartItem {
  quantity: Quantity
  price: Money
  totalValue: Money
  productId: string
  productName: string
  productImage: Photo
  singleWeight: Quantity | null
  alternativeUnitPrice: Money | null
}

export class ShoppingCartItem implements ShoppingCartItem {
  constructor(product: Product, quantity: Quantity) {
    this.quantity = quantity
    this.productId = product.id
    this.productName = product.name
    this.productImage = product.image
    this.price = product.price
    this.singleWeight = product.details.singleWeight
    this.alternativeUnitPrice = product.alternativeUnitPrice
  }
}

export interface ShoppingCart {
  shoppingCartId: string
  items: ShoppingCartItem[]
  totalPrice: Money
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
  shoppingCart: ShoppingCart;
  phoneNumber: string;
  firstName: string;
  lastName: string;
  deliveryLocation: Location;
  deliveryDate: DateTimeRange;
}

export class ShoppingCartCheckout implements ShoppingCartCheckout {
  constructor(init: ShoppingCart, personalInfo: PersonalInfo, deliveryPoint: DeliveryPoint) {
    this.shoppingCart = init
    this.phoneNumber = personalInfo.phoneNumber
    this.firstName = personalInfo.firstName
    this.lastName = personalInfo.lastName
    this.deliveryLocation = deliveryPoint.location
    this.deliveryDate = deliveryPoint.possiblePickupDate[0]
  }
}