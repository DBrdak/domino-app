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
        if(localStorage.getItem('scid')) {
          const result = await agent.shoppingCart.get(localStorage.getItem('scid')!)
          this.setShoppingCart(result)
        } else {
          const result =  await agent.shoppingCart.get(this.shoppingCart.shoppingCartId)
          this.setShoppingCart(result)
        }
      } else {
        if(localStorage.getItem('scid')) {
          const result = await agent.shoppingCart.get(localStorage.getItem('scid')!)
          this.setShoppingCart(result)
        } else {
          const result = await agent.shoppingCart.get(uuid())
          this.setShoppingCart(result)
        }
      }
      if(!localStorage.getItem('scid')){
        localStorage.setItem('scid', this.shoppingCart!.shoppingCartId)
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
    this.shoppingCart = new ShoppingCart(shoppingCart)
  }

  setProduct(product: Product) {
    this.newProduct = product
  }

  setQuantity(quantity: number, unit: string) {
    this.newProductQuantity = {quantity,unit}
  }

  private createShoppingCartItem = () => {
    if(this.newProduct !== null && this.newProductQuantity !== null){
      this.newShoppingCartItem = new ShoppingCartItem(this.newProduct, this.newProductQuantity)
      this.newProduct = null
      this.newProductQuantity = null
    }
  }

  addShoppingItem = async () => {
    this.createShoppingCartItem()
    this.setLoading(true)
    try {
      if(!this.shoppingCart) {
        await this.loadShoppingCart()
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