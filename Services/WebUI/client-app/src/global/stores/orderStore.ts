import { makeAutoObservable } from "mobx";
import { OnlineOrder, OrderCredentials } from "../models/order";
import { ShoppingCart, ShoppingCartCheckout } from "../models/shoppingCart";
import agent from "../api/agent";

export default class OrderStore {
  order: OnlineOrder | null = null
  orderId: string | null = null
  phoneNumber: string | null = null
  loading: boolean = false

  constructor(){
    makeAutoObservable(this)
  }

  get axiosParams() {
    const params = new URLSearchParams()
    this.orderId && params.append('orderId', this.orderId)
    this.phoneNumber && params.append('phoneNumber', this.phoneNumber)
    return params
  }

  setLoading(state: boolean) {
    this.loading = state
  }

  setOrder(order: OnlineOrder) {
    this.order = order
  }

  setId(id: string) {
    this.orderId = id
  }

  setPhoneNumber(phoneNumber: string) {
    this.phoneNumber = phoneNumber
  }

  async loadOrder() {
    this.setLoading(true)
    try {
      const result = await agent.order.get(this.axiosParams)
      console.log(result)
      this.setOrder(result)
    } catch(error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  async cancelOrder() {
    this.setLoading(true)
    try {
      const result = this.order && await agent.order.cancel(this.order)
      await this.loadOrder()
    } catch(error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }
}