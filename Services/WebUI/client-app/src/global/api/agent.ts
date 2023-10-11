import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/Routes";
import { PaginatedResult } from "../models/pagination";
import {Product, ProductCreateValues, ProductUpdateValues} from "../models/product";
import { ShoppingCart, ShoppingCartCheckout } from "../models/shoppingCart";
import {OnlineOrderRead, OrderUpdateValues} from "../models/order";
import {BusinessPriceListCreateValues, LineItemCreateValues, PriceList} from "../models/priceList";
import {DeliveryPoint, Shop, ShopCreateValues, ShopUpdateValues} from "../models/shop";

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay)
  })
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

axios.interceptors.response.use(async response => {
  if(process.env.NODE_ENV === "development") await sleep(1000);
  //const pagination = response.headers['pagination']
  return response;
}, (error:AxiosError) => {
  const {data, status, config, headers} = error.response! as AxiosResponse;
  switch(status) {
    case 400:
      if(config.method === 'get' && data.errors.hasOwnProperty('id')){
        router.navigate('/not-found');
      }
      if(data.errors) {
        const modalStateErrors = [];
        for (const key in data.errors){
          if(data.name) {
            modalStateErrors.push(data.name);
          }
        }
        throw modalStateErrors.flat();
      }else {
        toast.error(data.name);
      }
      toast.error(data.name);

      break;
    case 401:
      if(headers['www-authenticate']?.startsWith('Bearer error="invalid_token"')){
        toast.error("Session expired - please login again")
      }
      else toast.error('unauthorized')
      break;
    case 403:
      toast.error('forbidden')
      break;
    case 404:
      break;
    case 500:
      router.navigate('/server-error');
      break;
  }
  return Promise.reject(error);
})

const requests = {
  get: <T> (url: string) => axios.get<T>(url).then(responseBody),
  post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
  put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  delete: <T> (url: string) => axios.delete<T>(url).then(responseBody)
}

const catalog = {
  products: (category: string, params: URLSearchParams) => axios.get<PaginatedResult<Product[]>>(`/onlineshop/product/${category}`, {params}).then(responseBody),
  productsAdmin: (params: URLSearchParams) => axios.get<Product[]>(`/onlineshop/product`, {params}).then(responseBody),
  updateProduct: (newValues: ProductUpdateValues, photo: Blob | null) => {
    const formData = new FormData()
    photo && formData.append('Photo', photo)
    formData.append('Id', newValues.id)
    formData.append('Name', newValues.name)
    formData.append('Description', newValues.description)
    formData.append('Category', newValues.category)
    formData.append('Subcategory', newValues.subcategory)
    formData.append('IsAvailable', `${newValues.isAvailable}`)
    formData.append('IsWeightSwitchAllowed', `${newValues.isWeightSwitchAllowed}`)
    formData.append('SingleWeight', `${newValues.singleWeight}`)

    return axios.put<Product>(`/onlineshop/product`, formData).then(responseBody)
  },
  createProduct: (values: ProductCreateValues, photo: Blob) => {
    const formData = new FormData()
    photo && formData.append('Photo', photo)
    formData.append('Name', values.name)
    formData.append('Description', values.description)
    formData.append('Category', values.category)
    formData.append('Subcategory', values.subcategory)
    //formData.append('Price', null)
    formData.append('IsWeightSwitchAllowed', `${values.isWeightSwitchAllowed}`)
    formData.append('SingleWeight', `${values.singleWeight}`)

    return axios.post<Product>(`/onlineshop/product`, formData).then(responseBody)
  },
  deleteProduct: (productId: string) => axios.delete(`/onlineshop/product/${productId}`),
  getPriceLists: () => axios.get<PriceList[]>('/onlineshop/pricelist').then(responseBody),
  createRetailPriceList: () => axios.post('/onlineshop/pricelist/retail'),
  createBusinessPriceList: (command: BusinessPriceListCreateValues) => axios.post(`/onlineshop/pricelist/${command.contractorName}`),
  removePriceList: (priceListId: string) => axios.delete(`/onlineshop/pricelist/${priceListId}`),
  priceListAddLineItem: (request: LineItemCreateValues, priceListId: string) => axios.put(`/onlineshop/pricelist/${priceListId}/add`, request),
  priceListUpdateLineItem: (request: LineItemCreateValues, priceListId: string) => axios.put(`/onlineshop/pricelist/${priceListId}/update`, request),
  priceListRemoveLineItem: (priceListId: string, lineItemName: string) => axios.put(`/onlineshop/pricelist/${priceListId}/remove/${lineItemName}`),
}

const shoppingCart ={
  get: (shoppingCartId: string) => axios.get<ShoppingCart>(`/onlineshop/shoppingcart/${shoppingCartId}`).then(responseBody),
  update: (shoppingCart: ShoppingCart) => axios.post<ShoppingCart>('/onlineshop/shoppingcart', shoppingCart).then(responseBody),
  delete: (shoppingCartId: string) => axios.delete(`/onlineshop/shoppingcart/${shoppingCartId}`),
  checkout: (shoppingCart: ShoppingCartCheckout) => axios.post<string>('/onlineshop/shoppingcart/checkout', shoppingCart).then(responseBody)
}

const order = {
  get: (params: URLSearchParams) => axios.get<OnlineOrderRead>('/onlineshop/order', {params}).then(responseBody),
  cancel: (orderId: string) => axios.put('/onlineshop/order/cancel', {orderId}),
  getAll: () => axios.get<OnlineOrderRead[]>('/onlineshop/order/all'),
  updateOrder: (command: OrderUpdateValues) => axios.put('/onlineshop/order', command)
}

const shops = {
  getDeliveryPoints: () => axios.get<DeliveryPoint[]>('/shops/delivery-points').then(responseBody),
  getShops: () => axios.get<Shop[]>('/shops').then(responseBody),
  addShop: (command: ShopCreateValues) => axios.post<Shop>('/shops', command).then(responseBody),
  updateShop: (command: ShopUpdateValues) => axios.put<Shop>('/shops', command).then(responseBody),
  removeShop: (shopId: string) => axios.delete(`/shops/${shopId}`),
}

const agent = {
  catalog,
  shoppingCart,
  order
}

export default agent;