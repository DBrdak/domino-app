import {DateTimeRange, TimeRange, WeekDay} from "./common";
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
    initWeekSchedule: ShopWorkingDay[]
    weekDayToUpdate: WeekDay
    newWorkingHoursInWeekDay: TimeRange
    weekDayAsHoliday: WeekDay
    weekDayAsWorkingDay: WeekDay
}

export interface MobileShopUpdateValues {
    newSalePoint: SalePoint | null
    salePointToRemove: SalePoint | null
    salePointToDisable: SalePoint | null
    salePointToEnable: SalePoint | null
}

export interface ShopCreateValues {
    shopName: string
    mobileShopData: MobileShopDto | null
    stationaryShopData: StationaryShopDto | null
}

export interface  ShopUpdateValues {
    shopToUpdateId: string
    newSeller: Seller | null
    sellerToRemove: Seller | null
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

export interface StationaryShop extends Shop {
    location: Location
    weekSchedule: ShopWorkingDay[]
}