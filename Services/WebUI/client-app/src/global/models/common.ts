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
  currency: string
  unit: string
}

export interface QuantityModifier {
  isPcsAllowed: boolean
  kgPerPcs: number | null
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