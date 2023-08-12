import { makeAutoObservable } from "mobx";
import { Product } from "../models/product";
import { ShoppingCart, ShoppingCartCheckout, ShoppingCartItem } from "../models/shoppingCart";
import agent from "../api/agent";
import {v4 as uuid} from 'uuid'
import { DeliveryInfo, PersonalInfo } from "../models/common";
import OrderStore from "./orderStore";
import { store } from "./store";

export default class ShoppingCartStore {
  shoppingCart: ShoppingCart | null = null
  newProduct: Product | null = null
  newProductQuantity: {quantity: number, unit: string} | null = null  
  newShoppingCartItem: ShoppingCartItem | null = null
  personalInfo: PersonalInfo | null = null
  deliveryInfo: DeliveryInfo | null = null
  shoppingCartCheckout: ShoppingCartCheckout | null = null
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

  private clearShoppingCart() {
    this.shoppingCart = null
  }

  setProduct(product: Product) {
    this.newProduct = product
  }

  setQuantity(quantity: number, unit: string) {
    this.newProductQuantity = {quantity,unit}
  }

  setPersonalInfo(personalInfo: PersonalInfo) {
    this.personalInfo = personalInfo
  }

  setDeliveryInfo(deliveryInfo: DeliveryInfo) {
    this.deliveryInfo = deliveryInfo
  }

  private setShoppingCartCheckout() {
    if(this.shoppingCart && this.personalInfo && this.deliveryInfo){
      this.shoppingCartCheckout = new ShoppingCartCheckout(this.shoppingCart, this.personalInfo, this.deliveryInfo)
    }
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
      console.log(updatedShoppingCart)
      this.setShoppingCart(updatedShoppingCart)
      console.log(this.shoppingCart)
      this.setLoading(false)
    } catch(e) {
      console.log(e)
      this.setLoading(false)
    }
  }

  deleteShoppingCart = async () => {
    this.setLoading(true)
    try {
      if(!this.shoppingCart) await this.loadShoppingCart()
      await agent.shoppingCart.delete(this.shoppingCart!.shoppingCartId)
      this.clearShoppingCart()
      localStorage.removeItem('scid')
    } catch(e) {
      console.log(e)
    } finally {
      this.setLoading(false)
    }
  }

  checkoutShoppingCart= async () => {
    this.setLoading(true)
    try {
      if(!this.shoppingCart) await this.loadShoppingCart
      this.setShoppingCartCheckout()
      const result = this.shoppingCartCheckout && await agent.shoppingCart.checkout(this.shoppingCartCheckout)
      store.orderStore.setId(result!)
      while(!result){
        console.log(result)
      }
    } catch (e) {
      console.log(e)
    } finally {
      this.setLoading(false)
    }
  }
}