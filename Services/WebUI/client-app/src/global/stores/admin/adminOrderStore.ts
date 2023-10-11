import { makeAutoObservable,} from "mobx";
import {OnlineOrder, OnlineOrderRead, OrderUpdateValues} from "../../models/order";
import agent from "../../api/agent";

export default class AdminOrderStore {
    orders: OnlineOrderRead[] = []
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setOrder(order: OnlineOrderRead) {
        this.orders.push(order)
    }

    async loadOrders() {
        this.setLoading(true)
        try {
            const result = await agent.order.getAll()
            result.forEach(o => this.setOrder(o))
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async updateOrder(values: OrderUpdateValues) {
        this.setLoading(true)
        try {
            await agent.order.updateOrder(values)
            await this.loadOrders()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

}