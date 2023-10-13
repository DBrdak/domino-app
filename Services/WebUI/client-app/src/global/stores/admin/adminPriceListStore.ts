import { makeAutoObservable,} from "mobx";
import {BusinessPriceListCreateValues, LineItemCreateValues, PriceList} from "../../models/priceList";
import {toast} from "react-toastify";
import loading = toast.loading;
import agent from "../../api/agent";
import {common} from "@mui/material/colors";
import {cleanup} from "@testing-library/react";

export default class AdminPriceListStore {
    priceListsRegistry: Map<string, PriceList> = new Map<string, PriceList>()
    selectedPriceList: PriceList | null = null
    private nonAggregatedProductNames: string[] = []
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    get priceLists() {
        return Array.from(this.priceListsRegistry.values())
    }

    private refreshSelectedPriceList() {
        this.setSelectedPriceList(this.selectedPriceList)
    }

    setSelectedPriceList(priceList: PriceList | null) {
        if(priceList === null) {
            this.selectedPriceList = null
            return
        }

        this.selectedPriceList = this.priceLists.find(pl => pl.id === priceList.id)!
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setPriceList(priceList: PriceList) {
        this.priceListsRegistry.set(priceList.id, priceList)
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
        this.priceListsRegistry.clear()
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
            this.refreshSelectedPriceList()
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
            this.refreshSelectedPriceList()
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
            this.refreshSelectedPriceList()
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