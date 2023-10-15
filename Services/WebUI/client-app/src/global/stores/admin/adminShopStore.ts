import { makeAutoObservable,} from "mobx";
import {SalePoint, Seller, Shop, ShopCreateValues, ShopUpdateValues, ShopWorkingDay} from "../../models/shop";
import agent from "../../api/agent";
import {TimeRange, WeekDay, Location} from "../../models/common";

export default class AdminShopStore {
    shopsRegistry: Map<string, Shop> = new Map<string, Shop>()
    mobileShopCreateValues: ShopCreateValues | null = null
    stationaryShopCreateValues: ShopCreateValues | null = null
    shopToUpdateId: string | null = null
    mobileShopUpdateValues: ShopUpdateValues | null = null
    stationaryShopUpdateValues: ShopUpdateValues | null = null
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    get shops() {
        return Array.from(this.shopsRegistry.values())
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setShop(shop: Shop) {
        this.shopsRegistry.set(shop.id, shop)
    }

    private resetCreateValues() {
        this.stationaryShopCreateValues = null
        this.mobileShopCreateValues = null
    }

    private resetUpdateValues() {
        this.shopToUpdateId = null
        this.mobileShopUpdateValues = null
        this.stationaryShopUpdateValues = null
    }

    setMobileShopCreateValues(shopName: string, vehiclePlateNumber: string) {
        this.mobileShopCreateValues = {
            shopName: shopName,
            mobileShopData: {vehiclePlateNumber: vehiclePlateNumber},
            stationaryShopData: null
        }
    }

    setStationaryShopCreateValues(shopName: string, location: Location) {
        this.stationaryShopCreateValues = {
            shopName: shopName,
            mobileShopData: null,
            stationaryShopData: {location: location}
        }
    }

    setShopToUpdateId(id: string) {
        this.shopToUpdateId = id
    }

    setMobileShopUpdateValues(
        newSeller: Seller | null,
        sellerToRemove: Seller | null,
        newSalePoint: SalePoint | null,
        salePointToRemove: SalePoint | null,
        salePointToDisable: SalePoint | null,
        salePointToEnable: SalePoint | null) {
        this.mobileShopUpdateValues = this.shopToUpdateId ? {
            shopToUpdateId: this.shopToUpdateId,
            newSeller,
            sellerToRemove,
            mobileShopUpdateValues: {
                newSalePoint,
                salePointToRemove,
                salePointToDisable,
                salePointToEnable
            },
            stationaryShopUpdateValues: null
        } : null
    }

    setStationaryShopUpdateValues(
        newSeller: Seller | null,
        sellerToRemove: Seller | null,
        initWeekSchedule: ShopWorkingDay[],
        weekDayToUpdate: WeekDay,
        newWorkingHoursInWeekDay: TimeRange,
        weekDayAsHoliday: WeekDay,
        weekDayAsWorkingDay: WeekDay) {
        this.stationaryShopUpdateValues = this.shopToUpdateId ? {
            shopToUpdateId: this.shopToUpdateId,
            newSeller,
            sellerToRemove,
            mobileShopUpdateValues: null,
            stationaryShopUpdateValues: {
                initWeekSchedule,
                weekDayToUpdate,
                newWorkingHoursInWeekDay,
                weekDayAsHoliday,
                weekDayAsWorkingDay
            }
        }   : null
    }

    async loadShops() {
        this.setLoading(true)
        this.shopsRegistry.clear()
        try {
            const result = await agent.shops.getShops()
            result.forEach(s => this.setShop(s))
            console.log(result)
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async createShop() {
        this.setLoading(true)
        try {
            this.stationaryShopCreateValues ?
                await agent.shops.addShop(this.stationaryShopCreateValues) :
                this.mobileShopCreateValues && await agent.shops.addShop(this.mobileShopCreateValues)
            await this.loadShops()
        } catch (e) {
            console.log(e)
        } finally {
            this.resetCreateValues()
            this.setLoading(false)
        }
    }

    async updateShop() {
        this.setLoading(true)
        try {
            this.stationaryShopUpdateValues ?
                await agent.shops.updateShop(this.stationaryShopUpdateValues) :
                this.mobileShopUpdateValues && await agent.shops.updateShop(this.mobileShopUpdateValues)
            await this.loadShops()
        } catch (e) {
            console.log(e)
        } finally {
            this.resetUpdateValues()
            this.setLoading(false)
        }
    }

    async deleteShop(shopId: string) {
        this.setLoading(true)
        try {
            await agent.shops.removeShop(shopId)
            await this.loadShops()
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }
}