import { makeAutoObservable,} from "mobx";
import {OnlineOrder, OnlineOrderRead, OrderUpdateValues} from "../../models/order";
import agent from "../../api/agent";

export default class AdminOrderStore {
    ordersRegistry: Map<string,OnlineOrderRead> = new Map<string, OnlineOrderRead>()
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    get orders() {
        return Array.from(this.ordersRegistry.values())
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setOrder(order: OnlineOrderRead) {
        this.ordersRegistry.set(order.id, order)
    }

    async loadOrders() {
        this.setLoading(true)
        this.ordersRegistry.clear()
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