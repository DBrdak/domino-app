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