import { makeAutoObservable } from "mobx";
import { Product } from "../../models/product";
import { ShoppingCart, ShoppingCartCheckout, ShoppingCartItem } from "../../models/shoppingCart";
import agent from "../../api/agent";
import {v4 as uuid} from 'uuid'
import { DeliveryInfo, PersonalInfo, Quantity } from "../../models/common";
import OrderStore from "./orderStore";
import { store } from "../store";
import {DeliveryPoint} from "../../models/shop";

export default class ShoppingCartStore {
  shoppingCart: ShoppingCart | null = null
  newProduct: Product | null = null
  newProductQuantity: Quantity | null = null  
  newShoppingCartItem: ShoppingCartItem | null = null
  personalInfo: PersonalInfo | null = null
  deliveryPoint: DeliveryPoint | null = null
  shoppingCartCheckout: ShoppingCartCheckout | null = null
  loading: boolean = false
  subLoading: boolean = false

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

  private setLoading = (state: boolean) => {
    this.loading = state
  }

  private setSubLoading = (state: boolean) => {
    this.subLoading = state
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

  setQuantity(quantity: Quantity) {
    this.newProductQuantity = quantity
  }

  setPersonalInfo(personalInfo: PersonalInfo) {
    this.personalInfo = personalInfo
  }

  setDeliveryPoint(deliveryInfo: DeliveryPoint) {
    this.deliveryPoint = deliveryInfo
  }

  private createShoppingCartCheckout() {
    if(this.shoppingCart && this.personalInfo && this.deliveryPoint){
      this.shoppingCartCheckout = new ShoppingCartCheckout(this.shoppingCart, this.personalInfo, this.deliveryPoint)
    }
  }

  private createShoppingCartItem = () => {
    if(this.newProduct !== null && this.newProductQuantity !== null){
      this.newShoppingCartItem = new ShoppingCartItem(this.newProduct, this.newProductQuantity)
      this.newProduct = null
      this.newProductQuantity = null
    }
  }

  editShoppingItem = async (newQuantity: Quantity, productId: string) => {
    this.setSubLoading(true)
    try {
      if(!this.shoppingCart) {
        await this.loadShoppingCart()
      }
      this.shoppingCart?.items.map(i => i.productId === productId ? i.quantity = newQuantity : i)
      const updatedShoppingCart = await agent.shoppingCart.update(this.shoppingCart!)
      this.setShoppingCart(updatedShoppingCart)
    } catch(e) {
      console.log(e)
    } finally {
      this.setSubLoading(false)
    }
  }

  deleteShoppingItem = async (item: ShoppingCartItem) => {
    this.setSubLoading(true)
    try {
      if(!this.shoppingCart) {
        await this.loadShoppingCart()
      }
      this.shoppingCart?.removeShoppingCartItem(item)
      const updatedShoppingCart = await agent.shoppingCart.update(this.shoppingCart!)
      this.setShoppingCart(updatedShoppingCart)
    } catch(e) {
      console.log(e)
    } finally {
      this.setSubLoading(false)
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
      if(!this.shoppingCart) await this.loadShoppingCart()
      this.createShoppingCartCheckout()
      const result = this.shoppingCartCheckout && await agent.shoppingCart.checkout(this.shoppingCartCheckout)
      this.personalInfo?.phoneNumber && store.orderStore.setPhoneNumber(this.personalInfo.phoneNumber)
      result && store.orderStore.setId(result)
      localStorage.removeItem('scid')
    } catch (e) {
      console.log(e)
    } finally {
      this.setLoading(false)
    }
  }
}