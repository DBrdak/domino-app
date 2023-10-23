import { makeAutoObservable } from "mobx";
import agent from "../../api/agent";
import { OnlineOrder } from "../../models/order";

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

  private setLoading(state: boolean) {
    this.loading = state
  }

  setOrder(order: OnlineOrder) {
    this.order = order
  }

  setId(id: string) {
    this.orderId = id
    localStorage.setItem('ord-id', id)
  }

  setPhoneNumber(phoneNumber: string) {
    this.phoneNumber = phoneNumber
    localStorage.setItem('ord-ph-num', phoneNumber)
  }

  async loadOrder() {
    this.setLoading(true)
    try {
      !this.phoneNumber && localStorage.getItem('ord-ph-num') && this.setPhoneNumber(localStorage.getItem('ord-ph-num')!)
      !this.orderId && localStorage.getItem('ord-id') && this.setId(localStorage.getItem('ord-id')!)
      const result = this.phoneNumber && this.orderId && await agent.order.get(this.axiosParams)
      result && this.setOrder(result)
    } catch(error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }

  async cancelOrder() {
    this.setLoading(true)
    try {
      if(!this.order){
        await this.loadOrder()
      }
      this.order?.id && await agent.order.cancel(this.order.id)
      await this.loadOrder()
    } catch(error) {
      console.log(error)
    } finally {
      this.setLoading(false)
    }
  }
}