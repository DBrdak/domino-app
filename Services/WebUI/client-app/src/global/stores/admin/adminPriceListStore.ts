import { makeAutoObservable,} from "mobx";
import {BusinessPriceListCreateValues, LineItemCreateValues, PriceList} from "../../models/priceList";
import {toast} from "react-toastify";
import loading = toast.loading;
import agent from "../../api/agent";
import {common} from "@mui/material/colors";

export default class AdminPriceListStore {
    priceLists: PriceList[] = []
    private nonAggregatedProductNames: string[] = []
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    setPriceList(priceList: PriceList) {
        this.priceLists.push(priceList)
    }

    private setNonAgregatedProductName(name: string) {
        this.nonAggregatedProductNames.push(name)
    }

    async getNonAggregatedProductNames() {
        await this.loadNonAggregatedProductNames()
        return this.nonAggregatedProductNames
    }

    async loadPriceLists() {
        this.setLoading(true)
        try {
            const result = await agent.catalog.getPriceLists()
            result.forEach(pl => this.setPriceList(pl))
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async createRetailPriceList() {
        this.setLoading(true)
        try {
            await agent.catalog.createRetailPriceList()
            await this.loadPriceLists()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async createBusinessPriceList(command: BusinessPriceListCreateValues) {
        this.setLoading(true)
        try {
            await agent.catalog.createBusinessPriceList(command)
            await this.loadPriceLists()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async removePriceList(priceListId: string) {
        this.setLoading(true)
        try {
            await agent.catalog.removePriceList(priceListId)
            await this.loadPriceLists()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async createLineItem(priceListId: string, newLineItem: LineItemCreateValues) {
        this.setLoading(true)
        try {
            await agent.catalog.priceListAddLineItem(newLineItem, priceListId)
            await this.loadPriceLists()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async updateLineItem(priceListId: string, values: LineItemCreateValues) {
        this.setLoading(true)
        try {
            await agent.catalog.priceListUpdateLineItem(values, priceListId)
            await this.loadPriceLists()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async removeLineItem(priceListId: string, lineItemName: string) {
        this.setLoading(true)
        try {
            await agent.catalog.priceListRemoveLineItem(priceListId, lineItemName)
            await this.loadPriceLists()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async loadNonAggregatedProductNames() {
        this.setLoading(true)
        this.nonAggregatedProductNames = []
        try {
            await this.loadPriceLists()
            const retailPriceList = this.priceLists.find(pl => pl.contractor.name === 'Retail')
            const nonAggregatedLineItems = retailPriceList &&
                retailPriceList.lineItems.filter(li => li.productId === null)
            const nonAggregatedProductNames = nonAggregatedLineItems &&
                nonAggregatedLineItems.map(li => li.name)
            nonAggregatedProductNames && nonAggregatedProductNames.forEach(n => this.setNonAgregatedProductName(n))
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }
}