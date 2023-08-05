import { makeAutoObservable } from "mobx";
import { Product } from "../models/product";
import { ShoppingCartItem } from "../models/shoppingCart";

export default class ShoppingCartStore {
  shoppingCartRegistry = new Map<string, ShoppingCartItem>()
  newProduct: Product | null = null
  newProductQuantity: {quantity: number, unit: string} | null = null  
  newShoppingCartItem: ShoppingCartItem | null = null

  constructor() {
    makeAutoObservable(this);
  }

  get shoppingCart() {
    return Array.from(this.shoppingCartRegistry.values())
  }

  setProduct(product: Product) {
    this.newProduct = product
  }

  setQuantity(quantity: number, unit: string) {
    this.newProductQuantity = {quantity,unit}
  }

  createShoppingItem() {
    if(this.newProduct !== null && this.newProductQuantity !== null){
      this.newShoppingCartItem = new ShoppingCartItem(this.newProduct, this.newProductQuantity)
      this.newProduct = null
      this. newProductQuantity = null
    }
  }

  addShoppingItem() {
    // To be continued
  }
}