import { makeAutoObservable, reaction } from "mobx";
import { Product } from "../models/product";
import { Pagination, PagingParams } from "../models/pagination";
import { FilterOptions } from "../models/filterOptions";
import agent from "../api/agent";
import { useMediaQuery } from "@mui/material";
import theme from "../layout/theme";

export default class CatalogStore {
  productsRegistry = new Map<string, Product>()
  quantityMode: string | null = null
  pagination: Pagination | null = null
  pagingParams = new PagingParams()
  filterParams: FilterOptions = new FilterOptions()
  loading: boolean = false

  constructor() {
    makeAutoObservable(this)
  }

  get axiosParams() {
    const params = new URLSearchParams()
    params.append('page', this.pagingParams.pageNumber.toString())
    params.append('pageSize', this.pagingParams.pageSize.toString())
    params.append('sortOrder', this.filterParams.sortDirection.toString())
    params.append('sortBy', this.filterParams.sortProperty.toString())
    this.filterParams?.searchPhrase && params.append('searchPhrase', this.filterParams.searchPhrase.toString())
    //this.filterParams?.subcategory && params.append('subcategory', this.filterParams.subcategory.toString())
    this.filterParams?.minPrice && params.append('minPrice', this.filterParams.minPrice.toString())
    this.filterParams?.maxPrice && params.append('maxPrice', this.filterParams.maxPrice.toString())
    params.append('isAvailable', this.filterParams.isAvailable.toString())
    params.append('isDiscounted', this.filterParams.isDiscounted.toString())
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

  setFilter(filter: FilterOptions) {
    this.filterParams = filter
  }

  setQuantityModeFor(productId: string) {
    this.quantityMode = productId
  }

  resetQuantityMode() {
    this.quantityMode = null
  }
}