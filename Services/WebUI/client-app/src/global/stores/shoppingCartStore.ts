import { makeAutoObservable } from "mobx";
import { Product } from "../models/product";
import { ShoppingCart, ShoppingCartItem } from "../models/shoppingCart";
import agent from "../api/agent";
import {v4 as uuid} from 'uuid'

export default class ShoppingCartStore {
  shoppingCart: ShoppingCart | null = null
  newProduct: Product | null = null
  newProductQuantity: {quantity: number, unit: string} | null = null  
  newShoppingCartItem: ShoppingCartItem | null = null
  loading: boolean = false

  constructor() {
    makeAutoObservable(this);
  }

  loadShoppingCart = async () => {
    this.setLoading(true)
    try {
      if(this.shoppingCart) {
        const result =  await agent.shoppingCart.get(this.shoppingCart.shoppingCartId)
        this.shoppingCart = result
      } else {
        const result = await agent.shoppingCart.get(uuid())
        this.shoppingCart = result
      }
      this.setLoading(false)
    } catch(e) {
      console.log(e)
      this.setLoading(false)
    }
  }

  setLoading = (state: boolean) => {
    this.loading = state
  }

  private setShoppingCart(shoppingCart: ShoppingCart) {
    this.shoppingCart = shoppingCart
  }

  setProduct(product: Product) {
    this.newProduct = product
  }

  setQuantity(quantity: number, unit: string) {
    this.newProductQuantity = {quantity,unit}
  }

  createShoppingCartItem() {
    if(this.newProduct !== null && this.newProductQuantity !== null){
      this.newShoppingCartItem = new ShoppingCartItem(this.newProduct, this.newProductQuantity)
      this.newProduct = null
      this. newProductQuantity = null
    }
  }

  addShoppingItem = async () => {
    this.createShoppingCartItem()
    this.setLoading(true)
    try {
      if(!this.shoppingCart) {
        this.loadShoppingCart()
      } 
      this.newShoppingCartItem && this.shoppingCart?.addShoppingCartItem(this.newShoppingCartItem)
      const updatedShoppingCart = await agent.shoppingCart.update(this.shoppingCart!)
      this.setShoppingCart(updatedShoppingCart)
      this.setLoading(false)
    } catch(e) {
      console.log(e)
      this.setLoading(false)
    }
  }
}