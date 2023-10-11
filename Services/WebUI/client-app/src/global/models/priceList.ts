import { Money } from "./common"

export interface PriceList {
  name: string
  lineItems: LineItem[]
  contractor: Contractor
}

export interface LineItem {
  name: string
  price: Money
  productId: string | null
}

export interface Contractor {
  name: string
}

export interface LineItemCreateValues  {
  lineItemName: string
  price: Money
}

export interface BusinessPriceListCreateValues {
  name: string
  contractorName: string
}