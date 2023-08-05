import { makeAutoObservable, reaction } from "mobx";
import { Product } from "../models/product";
import { Pagination, PagingParams } from "../models/pagination";
import { FilterOptions } from "../models/filterOptions";
import agent from "../api/agent";

export default class CatalogStore {
  productsRegistry = new Map<string, Product>()
  quantityMode: string | null = null
  pagination: Pagination | null = null
  pagingParams = new PagingParams()
  filterParams: FilterOptions | null = null
  predicate = new Map().set('all', true)
  loading: boolean = false

  constructor() {
    makeAutoObservable(this)
  }

  get axiosParams() {
    const params = new URLSearchParams()
    params.append('page', this.pagingParams.pageNumber.toString())
    params.append('pageSize', this.pagingParams.pageSize.toString())
    this.filterParams?.sortDirection && params.append('sortOrder', this.filterParams.sortDirection.toString())
    this.filterParams?.sortProperty && params.append('sortBy', this.filterParams.sortDirection.toString())
    this.filterParams?.searchPhrase && params.append('searchPhrase', this.filterParams.searchPhrase.toString())
    this.filterParams?.subcategory && params.append('subcategory', this.filterParams.subcategory.toString())
    this.filterParams?.minPrice && params.append('minPrice', this.filterParams.minPrice.toString())
    this.filterParams?.maxPrice && params.append('maxPrice', this.filterParams.maxPrice.toString())
    this.filterParams?.isAvailable && params.append('isAvailable', this.filterParams.isAvailable.toString())
    this.filterParams?.isDiscounted && params.append('isDiscounted', this.filterParams.isDiscounted.toString())
    return params
  }

  get products() {
    return Array.from(this.productsRegistry.values())
  }

  setLoading(state: boolean){
    this.loading = state
  }

  setPagination = (page: number, pageSize: number, totalCount: number, totalPages: number, hasNextPage: boolean, hasPreviousPage: boolean) => {
    this.pagination = new Pagination({page, pageSize, totalCount, totalPages, hasNextPage, hasPreviousPage})
  }

  private setProduct = (product: Product) => {
    this.productsRegistry.set(product.id, product)
  }

  loadProducts = async (category: string) => {
    this.setLoading(true)
    this.productsRegistry = new Map<string, Product>()
    try {
      const result = await agent.catalog.products(category, this.axiosParams)
      result.items.forEach(i => this.setProduct(i))
      this.setPagination(result.page, result.pageSize, result.totalCount, result.totalPages, result.hasNextPage, result.hasPreviousPage)
      this.setLoading(false)
    } catch(e) {
      console.log(e)
      this.setLoading(false)
    }
  }

  setQuantityModeFor(productId: string) {
    this.quantityMode = productId
  }

  resetQuantityMode() {
    this.quantityMode = null
  }
}