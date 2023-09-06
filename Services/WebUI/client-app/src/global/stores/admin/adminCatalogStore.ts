import { makeAutoObservable,} from "mobx";
import agent from "../../api/agent";
import { FilterOptions } from "../../models/filterOptions";
import { Pagination, PagingParams } from "../../models/pagination";
import { Product } from "../../models/product";

export default class AdminCatalogStore {
  productsRegistry = new Map<string, Product>()
  searchPhrase: string = ''
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

  private setProduct = (product: Product) => {
    this.productsRegistry.set(product.id, product)
  }

  loadProducts = async () => {
    this.setLoading(true)
    this.productsRegistry = new Map<string, Product>()
    try {
      const result = await agent.catalog.productsAdmin(this.axiosParams)
      result.forEach(i => this.setProduct(i))
      this.setLoading(false)
    } catch(e) {
      console.log(e)
      this.setLoading(false)
    }
  }

  setSearchPhrase(searchPhrase: string) {
    this.searchPhrase = searchPhrase
  }
}