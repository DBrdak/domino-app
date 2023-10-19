import { object } from "yup";
import { DateTimeRange, Location, Money, Quantity } from "./common";
import { ShoppingCart, ShoppingCartCheckout, ShoppingCartItem } from "./shoppingCart";

export interface OrderItem {
  id: string | null
  orderId: string | null
  price: Money
  quantity: Quantity
  totalValue: Money 
  productName: string
}

export interface OnlineOrder {
  // Shopping Cart Info
  totalPrice: Money;
  items: OrderItem[];

  // Personal Info
  phoneNumber: string;
  firstName: string;
  lastName: string;

  // Delivery Info
  deliveryLocation: Location;
  deliveryDate: DateTimeRange;

  // Order Info
  id: string | null;
  createdDate: Date | null;
  completionDate: Date | null;
  expiryDate: Date | null;
  status: OrderStatus | null;
}

export class OnlineOrderRead implements OnlineOrder {
  constructor(init: OnlineOrder) {
    Object.assign(this, init)
  }
  totalPrice!: Money;
  items!: OrderItem[];
  phoneNumber!: string;
  firstName!: string;
  lastName!: string;
  deliveryLocation!: Location;
  deliveryDate!: DateTimeRange;
  id!: string;
  createdDate!: Date;
  completionDate!: Date | null;
  expiryDate!: Date | null;
  status!: OrderStatus;
}

export class OnlineOrderCreate implements OnlineOrder {
  totalPrice: Money;
  items: OrderItem[];
  phoneNumber: string;
  firstName: string;
  lastName: string;
  deliveryLocation: Location;
  deliveryDate: DateTimeRange;
  id!: null;
  createdDate!: null;
  completionDate!: null;
  expiryDate!: null;
  status!: null;

  constructor(init: ShoppingCartCheckout) {
    this.totalPrice = init.shoppingCart.totalPrice;
    this.items = this.convertToOrderItems(init.shoppingCart.items);
    this.phoneNumber = init.phoneNumber;
    this.firstName = init.firstName;
    this.lastName = init.lastName;
    this.deliveryLocation = init.deliveryLocation;
    this.deliveryDate = {start: new Date(init.deliveryDate.start), end: new Date(init.deliveryDate.end)};
    this.id = null
    this.createdDate = null
    this.completionDate = null
    this.expiryDate = null
    this.status = null
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
  totalValue!: Money;
  quantity!: Quantity
  productName!: string;

  constructor(init: ShoppingCartItem){
    Object.assign(this, init)
    this.id = null
    this.orderId = null
  }
}

export interface OrderCredentials {
  phoneNumber: string
  orderId: string
}

export interface OrderStatus {
  statusMessage: string
}

export interface OrderUpdateValues {
  orderId: string
  status: string
  smsMessage: string | null
  modifiedOrder: OnlineOrder | null
}