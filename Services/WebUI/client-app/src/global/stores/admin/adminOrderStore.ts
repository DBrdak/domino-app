import {makeAutoObservable, runInAction,} from "mobx";
import {OnlineOrderRead, OrderUpdateValues} from "../../models/order";
import agent from "../../api/agent";
import {store} from "../store";

export default class AdminOrderStore {
    ordersRegistry: Map<string,OnlineOrderRead> = new Map<string, OnlineOrderRead>()
    loading: boolean = false
    loadingOrder: {loading: boolean, orderId: string} = {loading: false, orderId: ''}
    loadingPdf: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    get axiosParams() {
        const params = new URLSearchParams()
        this.ordersToPrint.length > 0 &&
            this.ordersToPrint.forEach(o => params.append("ordersId", o.id))
        return params
    }

    get ordersToPrint() {
        return Array.from(this.ordersRegistry.values()).filter(o =>
            !o.isPrinted && (o.status.statusMessage === "Potwierdzone" || o.status.statusMessage === "Potwierdzone ze zmianami"))
    }

    get orders() {
        return Array.from(this.ordersRegistry.values())
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setLoadingPdf(state: boolean) {
        this.loading = state
    }

    private setOrderLoading(state: boolean, orderId: string) {
        this.loadingOrder = {loading: state, orderId: orderId}
    }

    private setOrder(order: OnlineOrderRead) {
        this.ordersRegistry.set(order.id, order)
    }


    async loadOrders(loading: boolean = true) {
        loading && this.setLoading(true)
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
        } catch (e) {
            console.log(e)
        } finally {
            loading && this.setLoading(false)
        }
    }

    async updateOrder(values: OrderUpdateValues) {
        this.setOrderLoading(true, values.orderId)
        try {
            await agent.order.updateOrder(values)
            await this.loadOrders(false)
        } catch (e) {
            console.log(e)
        } finally {
            this.setOrderLoading(false, '')
        }
    }

    async downloadOrders() {
        this.setLoadingPdf(true)
        try {
            this.ordersToPrint.length > 0 && await agent.order.downloadOrders(this.axiosParams)
            await this.loadOrders(false)
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoadingPdf(false)
        }
    }
}