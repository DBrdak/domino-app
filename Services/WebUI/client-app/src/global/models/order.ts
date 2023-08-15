import { object } from "yup";
import { DateTimeRange, Location, Money } from "./common";
import { ShoppingCart, ShoppingCartCheckout, ShoppingCartItem } from "./shoppingCart";

export interface OrderItem {
  id: string | null
  orderId: string | null
  price: Money
  quantity: number
  unit: string
  totalValue: number
  productName: string
  status: string | null
}

export interface OnlineOrder {
  // Shopping Cart Info
  totalPrice: number;
  currency: string
  items: OrderItem[];

  // Personal Info
  phoneNumber: string;
  firstName: string;
  lastName: string;

  // Delivery Info
  deliveryLocation: Location;
  deliveryDate: DateTimeRange;

  // Order Info
  orderId: string | null;
  createdDate: Date | null;
  isCanceled: boolean | null;
  status: string | null;
  isConfirmed: boolean | null;
  isRejected: boolean | null;
}

export class OnlineOrderRead implements OnlineOrder {
  constructor(init: OnlineOrder) {
    Object.assign(this, init)
  }
  totalPrice!: number;
  currency!: string;
  items!: OrderItem[];
  phoneNumber!: string;
  firstName!: string;
  lastName!: string;
  deliveryLocation!: Location;
  deliveryDate!: DateTimeRange;
  orderId!: string;
  createdDate!: Date;
  isCanceled!: boolean;
  status!: string;
  isConfirmed!: boolean;
  isRejected!: boolean;
}

export class OnlineOrderCreate implements OnlineOrder {
  totalPrice: number;
  currency!: string;
  items: OrderItem[];
  phoneNumber: string;
  firstName: string;
  lastName: string;
  deliveryLocation: Location;
  deliveryDate: DateTimeRange;
  orderId: null;
  createdDate: null;
  isCanceled: null;
  status: null;
  isConfirmed: null;
  isRejected: null

  constructor(init: ShoppingCartCheckout) {
    this.totalPrice = init.totalPrice;
    this.items = this.convertToOrderItems(init.items);
    this.phoneNumber = init.phoneNumber;
    this.firstName = init.firstName;
    this.lastName = init.lastName;
    this.deliveryLocation = init.deliveryLocation;
    this.deliveryDate = init.deliveryDate;
    this.orderId = null
    this.createdDate = null
    this.isCanceled = null
    this.status = null
    this.isConfirmed = null
    this.isRejected = null
  }

  private convertToOrderItems(shoppingCartItems: ShoppingCartItem[]): OrderItem[] {
    let orderItems: OrderItem[] = []

    shoppingCartItems.map(i => {
      orderItems.push(new OrderItem(i))
    })

    return orderItems
  }
}

export class OrderItem implements OrderItem {
  id: string | null;
  orderId: string | null;
  price!: Money;
  totalValue!: number;
  productName!: string;
  status: string | null;

  constructor(init: ShoppingCartItem){
    Object.assign(this, init)
    this.id = null
    this.orderId = null
    this.status = null
  }
}

export interface OrderCredentials {
  phoneNumber: string
  orderId: string
}