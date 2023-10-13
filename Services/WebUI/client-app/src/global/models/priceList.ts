import { Money } from "./common"

export interface PriceList {
  id:string
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
  newPrice: Money
}

export interface BusinessPriceListCreateValues {
  name: string
  contractorName: string
}