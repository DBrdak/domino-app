import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/Routes";
import { store } from "../stores/store";
import { request } from "http";
import { PaginatedResult } from "../models/pagination";
import { Product } from "../models/product";
import { ShoppingCart, ShoppingCartCheckout } from "../models/shoppingCart";
import { OnlineOrder, OnlineOrderRead, OrderCredentials } from "../models/order";

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
      router.navigate('/not-found')
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
  products: (category: string, params: URLSearchParams) => axios.get<PaginatedResult<Product[]>>(`/onlineshop/catalog/${category}`, {params}).then(responseBody)
}

const shoppingCart ={
  get: (shoppingCartId: string) => axios.get<ShoppingCart>(`/onlineshop/shoppingcart/${shoppingCartId}`).then(responseBody),
  update: (shoppingCart: ShoppingCart) => axios.post<ShoppingCart>('/onlineshop/shoppingcart', shoppingCart).then(responseBody),
  delete: (shoppingCartId: string) => axios.delete(`/onlineshop/shoppingcart/${shoppingCartId}`),
  checkout: (shoppingCart: ShoppingCartCheckout) => axios.post<string>('/onlineshop/shoppingcart/checkout', shoppingCart).then(responseBody)
}

const order = {
  get: (params: URLSearchParams) => axios.get<OnlineOrderRead>('/onlineshop/order', {params}).then(responseBody),
  cancel: (orderId: string) => axios.put('/onlineshop/order/cancel', {orderId})
}

const agent = {
  catalog,
  shoppingCart,
  order
}

export default agent;