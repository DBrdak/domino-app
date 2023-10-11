export interface Location {
  name: string
  longitude: string
  latitude: string
}

export interface DateTimeRange{
  start: Date
  end: Date
}

export interface Money {
  amount: number
  currency: Currency
  unit?: Unit
}

export interface Quantity {
  value: number
  unit: Unit
}

export interface Currency {
  code: string
}

export interface Unit {
  code: string
}

export interface PersonalInfo {
  phoneNumber: string
  firstName: string
  lastName: string
}

export interface DeliveryInfo {
  deliveryDate: DateTimeRange
  deliveryLocation: Location
}

export interface Photo {
  url: string
}

export interface WeekDay {
  value: string
}

export interface TimeRange {
  start: string
  end: string
}
