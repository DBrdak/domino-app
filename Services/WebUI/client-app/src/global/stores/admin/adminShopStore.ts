import { makeAutoObservable,} from "mobx";
import {
    MobileShop,
    SalePoint,
    Seller,
    Shop,
    ShopCreateValues,
    ShopUpdateValues,
    ShopWorkingDay, StationaryShop
} from "../../models/shop";
import agent from "../../api/agent";
import {TimeRange, WeekDay, Location} from "../../models/common";

export default class AdminShopStore {
    shopsRegistry: Map<string, Shop> = new Map<string, Shop>()
    mobileShopCreateValues: ShopCreateValues | null = null
    stationaryShopCreateValues: ShopCreateValues | null = null
    shopToUpdateId: string | null = null
    shopUpdateValues: ShopUpdateValues | null = null
    mobileShopUpdateValues: ShopUpdateValues | null = null
    stationaryShopUpdateValues: ShopUpdateValues | null = null
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    get shops() {
        return Array.from(this.shopsRegistry.values())
    }

    get mobileShops() {
        return Array.from(this.shopsRegistry.values())
            .filter(s => (s as any).salePoints)
            .map(ms => (ms as MobileShop))
    }

    get stationaryShops() {
        return Array.from(this.shopsRegistry.values())
            .filter(s => (s as any).location)
            .map(ss => (ss as StationaryShop))
    }

    get salePoints() {
        const salePoints = Array.from(this.shopsRegistry.values())
            .filter(s => (s as any).salePoints)
            .map(ms => (ms as MobileShop))
            .flatMap(ms => ms.salePoints)
        const uniqueSalePoints = new Map<string, SalePoint>()
        salePoints.forEach(sp => uniqueSalePoints.set(sp.location.name, sp))

        return Array.from(uniqueSalePoints.values())
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
        this.shopUpdateValues = null
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

    setShopUpdateValues(
        newSeller: Seller | null,
        sellerToRemove: Seller | null) {
        this.shopUpdateValues = this.shopToUpdateId ? {
            shopToUpdateId: this.shopToUpdateId,
            newSeller,
            sellerToDelete: sellerToRemove,
            mobileShopUpdateValues: null,
            stationaryShopUpdateValues: null
        } : null
    }

    setMobileShopUpdateValues(
        newVehicleNumberPlate: string | null,
        newSalePoint: SalePoint | null,
        salePointToRemove: SalePoint | null,
        salePointToDisable: SalePoint | null,
        salePointToEnable: SalePoint | null,
        updatedSalePoint: SalePoint | null) {

        if(!this.shopUpdateValues) {
            this.shopToUpdateId && this.setShopUpdateValues(null, null)
        }

        this.mobileShopUpdateValues = this.shopUpdateValues ? {
            shopToUpdateId: this.shopUpdateValues.shopToUpdateId,
            newSeller: this.shopUpdateValues.newSeller,
            sellerToDelete:  this.shopUpdateValues.sellerToDelete,
            mobileShopUpdateValues: {
                newVehicleNumberPlate,
                newSalePoint,
                salePointToRemove,
                salePointToDisable,
                salePointToEnable,
                updatedSalePoint
            },
            stationaryShopUpdateValues: null
        } : null
    }

    setStationaryShopUpdateValues(
        initWeekSchedule: ShopWorkingDay[] | null,
        weekDayToUpdate: WeekDay | null,
        newWorkingHoursInWeekDay: TimeRange | null,
        weekDayAsHoliday: WeekDay | null,
        weekDayAsWorkingDay: WeekDay | null) {

        if(!this.shopUpdateValues) {
            this.shopToUpdateId && this.setShopUpdateValues(null, null)
        }

        this.stationaryShopUpdateValues = this.shopUpdateValues ? {
            shopToUpdateId: this.shopUpdateValues.shopToUpdateId,
            newSeller: this.shopUpdateValues.newSeller,
            sellerToDelete:  this.shopUpdateValues.sellerToDelete,
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
            if(this.stationaryShopUpdateValues && !this.mobileShopUpdateValues) {
                await agent.shops.updateShop(this.stationaryShopUpdateValues)
            } else if(!this.stationaryShopUpdateValues && this.mobileShopUpdateValues) {
                await agent.shops.updateShop(this.mobileShopUpdateValues)
            } else if(!this.stationaryShopUpdateValues && !this.mobileShopUpdateValues && this.shopUpdateValues) {
                await agent.shops.updateShop(this.shopUpdateValues)
            }
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