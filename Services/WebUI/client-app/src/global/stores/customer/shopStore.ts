import { makeAutoObservable,} from "mobx";
import {DeliveryPoint} from "../../models/shop";
import agent from "../../api/agent";

export default class ShopStore {
    deliveryPoints: DeliveryPoint[] = []
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setDeliveryPoint(deliveryPoint: DeliveryPoint) {
        this.deliveryPoints.push(deliveryPoint)
    }

    private clearDeliveryPoints() {
        this.deliveryPoints = []
    }

    async loadDeliveryPoints() {
        this.setLoading(true)
        this.clearDeliveryPoints()
        try {
            const result = await agent.shops.getDeliveryPoints()
            result.forEach(dp => this.setDeliveryPoint(dp))
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }
}