import { makeAutoObservable,} from "mobx";
import agent from "../../api/agent";
import { FilterOptions } from "../../models/filterOptions";
import { Pagination, PagingParams } from "../../models/pagination";
import {Product, ProductCreateValues, ProductUpdateValues} from "../../models/product";
import {bool} from "yup";

export default class AdminProductStore {
  productsRegistry = new Map<string, Product>()
  searchPhrase: string = ''
  newProductValues: ProductCreateValues | null = null
  productUpdateValues: ProductUpdateValues | null = null
  photo: Blob | null = null
  loading: boolean = false

  constructor() {
    makeAutoObservable(this)
  }

  get axiosParams() {
    const params = new URLSearchParams()
    params.append('searchPhrase', this.searchPhrase)
    return params
  }

  get products() {
    return Array.from(this.productsRegistry.values())
  }

  setLoading(state: boolean){
    this.loading = state
  }

  setSearchPhrase(searchPhrase: string | null) {
    this.searchPhrase = searchPhrase ? searchPhrase : ''
  }

  setNewProductValues(newProductValues: ProductCreateValues) {
    this.newProductValues = new ProductCreateValues(newProductValues)
  }

  setPhoto(newPhoto: Blob) {
    this.photo = newPhoto
  }
  private setProduct = (product: Product) => {
    this.productsRegistry.set(product.id, product)
  }

  setProductUpdateValues(product: Product, isAvailable: boolean | null) {
    this.productUpdateValues = new ProductUpdateValues(product, isAvailable)
  }

  resetCreateValues() {
    this.newProductValues = null
    this.photo = null
  }

  resetUpdateValues() {
    this.productUpdateValues = null
    this.photo = null
  }

  loadProducts = async () => {
    this.setLoading(true)
    this.productsRegistry.clear()
    try {
      const result = await agent.catalog.productsAdmin(this.axiosParams)
      result.forEach(i => this.setProduct(i))
      this.setLoading(false)
    } catch(e) {
      console.log(e)
      this.setLoading(false)
    }
  }

  async addProduct() {
    this.setLoading(true)
    try {
      this.newProductValues && this.photo &&
          await agent.catalog.createProduct(this.newProductValues, this.photo)
      await this.loadProducts()
    } catch (e) {
      console.log(e)
    } finally {
      this.resetCreateValues()
      this.setLoading(false)
    }
  }

  async updateProduct() {
    this.setLoading(true)
    try {
      this.productUpdateValues && await agent.catalog.updateProduct(this.productUpdateValues, this.photo)
      await this.loadProducts()
    } catch (e) {
      console.log(e)
    } finally {
      this.resetUpdateValues()
      this.setLoading(false)
    }
  }

  async deleteProduct(id: string) {
    this.setLoading(true)
    try {
      await agent.catalog.deleteProduct(id)
      await this.loadProducts()
    } catch (e) {
      console.log(e)
    } finally {
      this.setLoading(false)
    }
  }
}