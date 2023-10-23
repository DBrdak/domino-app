import {DateTimeRange, TimeRange, WeekDay, Location} from "./common";
import {bool} from "yup";

export interface DeliveryPoint {
    location: Location
    workingDays: ShopWorkingDay[]
    possiblePickupDate: DateTimeRange[]
}

export interface Seller {
    firstName: string
    lastName: string
    phoneNumber: string
}

export interface ShopWorkingDay {
    weekDay: WeekDay
    isClosed: boolean
    openHours: TimeRange | null
    cachedOpenHours: TimeRange | null
}

export interface SalePoint {
    location: Location
    weekDay: WeekDay
    isClosed: boolean
    openHours: TimeRange | null
    cachedOpenHours: TimeRange | null
}

export interface MobileShopDto {
    vehiclePlateNumber: string
}

export interface StationaryShopDto {
    location: Location
}

export interface StationaryShopUpdateValues {
    initWeekSchedule: ShopWorkingDay[] | null
    weekDayToUpdate: WeekDay | null
    newWorkingHoursInWeekDay: TimeRange | null
    weekDayAsHoliday: WeekDay | null
    weekDayAsWorkingDay: WeekDay | null
}

export interface MobileShopUpdateValues {
    newVehicleNumberPlate: string | null
    newSalePoint: SalePoint | null
    salePointToRemove: SalePoint | null
    salePointToDisable: SalePoint | null
    salePointToEnable: SalePoint | null
    updatedSalePoint: SalePoint | null
}

export interface ShopCreateValues {
    shopName: string
    mobileShopData: MobileShopDto | null
    stationaryShopData: StationaryShopDto | null
}

export interface  ShopUpdateValues {
    shopToUpdateId: string
    newSeller: Seller | null
    sellerToDelete: Seller | null
    mobileShopUpdateValues: MobileShopUpdateValues | null
    stationaryShopUpdateValues: StationaryShopUpdateValues | null
}


export interface Shop {
    id: string
    shopName: string
    sellers: Seller[]
}

export interface MobileShop extends Shop {
    vehiclePlateNumber: string
    salePoints: SalePoint[]
}

export class MobileShop implements MobileShop {
    constructor(init:ShopCreateValues) {
        this.shopName = init.shopName
        this.vehiclePlateNumber = init.mobileShopData!.vehiclePlateNumber
    }
}

export interface StationaryShop extends Shop {
    location: Location
    weekSchedule: ShopWorkingDay[]
}

export class StationaryShop implements StationaryShop {
    constructor(init:ShopCreateValues) {
        this.shopName = init.shopName
        this.location = init.stationaryShopData!.location
    }
}