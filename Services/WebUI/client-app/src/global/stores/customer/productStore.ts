import { makeAutoObservable,} from "mobx";
import agent from "../../api/agent";
import { FilterOptions } from "../../models/filterOptions";
import { Pagination, PagingParams } from "../../models/pagination";
import { Product } from "../../models/product";

export default class ProductStore {
  productsRegistry = new Map<string, Product>()
  quantityMode: string | null = null
  pagination: Pagination | null = null
  pagingParams = new PagingParams()
  filterParams: FilterOptions = new FilterOptions()
  loadingNext: boolean = false
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

  setLoadingNext(state: boolean) {
    this.loadingNext = state
  }

  setPagination = (page: number, pageSize: number, totalCount: number, totalPages: number, hasNextPage: boolean, hasPreviousPage: boolean) => {
    this.pagination = new Pagination({page, pageSize, totalCount, totalPages, hasNextPage, hasPreviousPage})
  }

  setPagingParams = (pagingParams: PagingParams) => {
    this.pagingParams = pagingParams
  }

  private setProduct = (product: Product) => {
    this.productsRegistry.set(product.id, product)
  }

  loadProducts = async (category: string) => {
    !this.loadingNext && this.setLoading(true)
    !this.pagination && this.productsRegistry.clear()
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
    this.setPagingParams(new PagingParams())
    this.pagination = null
    this.filterParams = filter
  }

  setQuantityModeFor(productId: string) {
    this.quantityMode = productId
  }

  resetQuantityMode() {
    this.quantityMode = null
  }
}