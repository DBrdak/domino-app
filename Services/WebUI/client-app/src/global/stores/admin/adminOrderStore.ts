import {makeAutoObservable, runInAction,} from "mobx";
import {OnlineOrderRead, OrderUpdateValues} from "../../models/order";
import agent from "../../api/agent";
import {store} from "../store";

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
            await runInAction(async() => {
                await store.adminShopStore.loadShops()
                result.forEach(o => {
                    o.shop = store.adminShopStore.shops.find(s => s.id === o.shopId) ?? null
                    this.setOrder(o)
                })
            })
            console.log(this.ordersRegistry)
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