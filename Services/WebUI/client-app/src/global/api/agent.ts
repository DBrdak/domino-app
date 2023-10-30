import axios, { AxiosResponse } from "axios"
import { toast } from "react-toastify"
import { router } from "../router/Routes"
import { PaginatedResult } from "../models/pagination"
import {Product, ProductCreateValues, ProductUpdateValues} from "../models/product"
import { ShoppingCart, ShoppingCartCheckout } from "../models/shoppingCart"
import {OnlineOrderRead, OrderUpdateValues} from "../models/order"
import {BusinessPriceListCreateValues, LineItemCreateValues, PriceList} from "../models/priceList"
import {DeliveryPoint, Shop, ShopCreateValues, ShopUpdateValues} from "../models/shop"

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay)
  })
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

axios.interceptors.response.use(async(response) => {
  if(process.env.NODE_ENV === "development") {
    await sleep(1000)
  }
    return response
}, (error) => {
  console.log(error.data)
      if (error.response) {
        if (error.response.data && error.response.data.isSuccess === false) {
          const errorMessage = error.response.data.error.message;
          toast.error(errorMessage);
          return Promise.reject();
        } else {
          switch(error.response.status) {
            case 400:
              if(error.response.config.method === 'get' && error.response.data.errors.hasOwnProperty('id')){
                router.navigate('/not-found');
              }
              break;
            case 401:
              if( error.response.headers['www-authenticate']?.startsWith('Bearer error="invalid_token"')){
                toast.error("Session expired - please login again")
              }
              else toast.error('unauthorized')
              break;
            case 403:
              toast.error('forbidden')
              break;
            case 404:
              router.navigate('/not-found');
              break;
            case 500:
              router.navigate('/server-error');
              break;
          }
        }
      }

      // Pass the error to the next handler
      return Promise.reject(error);
    }
);

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
    formData.append('Id', newValues.id)
    formData.append('Description', newValues.description)
    formData.append('IsAvailable', `${newValues.isAvailable}`)
    formData.append('ImageUrl', `${newValues.imageUrl}`)
    formData.append('IsWeightSwitchAllowed', `${newValues.isWeightSwitchAllowed}`)
    newValues.singleWeight && formData.append('SingleWeight', `${newValues.singleWeight}` )
    formData.append('Photo', photo ? photo : 'null')
    console.log({key: Array.from(formData.keys()), value: Array.from(formData.values())})
    return axios.put<Product>(`/onlineshop/product`, formData).then(responseBody)
  },
  createProduct: (values: ProductCreateValues, photo: Blob) => {
    const formData = new FormData()
    photo && formData.append('Photo', photo, values.name)
    formData.append('Name', values.name)
    formData.append('Description', values.description)
    formData.append('IsWeightSwitchAllowed', `${values.isWeightSwitchAllowed}`)
    formData.append('SingleWeight', `${values.singleWeight}`)

    return axios.post<Product>(`/onlineshop/product`, formData).then(responseBody)
  },
  deleteProduct: (productId: string) => axios.delete(`/onlineshop/product/${productId}`),
  getPriceLists: () => axios.get<PriceList[]>('/onlineshop/pricelist').then(responseBody),
  createBusinessPriceList: (command: BusinessPriceListCreateValues) => axios.post(`/onlineshop/pricelist/${command.contractorName}`, command),
  removePriceList: (priceListId: string) => axios.delete(`/onlineshop/pricelist/${priceListId}`),
  priceListAddLineItem: (request: LineItemCreateValues, priceListId: string) => axios.put(`/onlineshop/pricelist/${priceListId}/add`, request),
  priceListUpdateLineItem: (request: LineItemCreateValues, priceListId: string) => axios.put(`/onlineshop/pricelist/${priceListId}/update`, request),
  priceListRemoveLineItem: (priceListId: string, lineItemName: string) => axios.put(`/onlineshop/pricelist/${priceListId}/remove/${lineItemName}`),
  uploadPriceList: (file: File, priceListId: string) => {
    if (!file) {
      return Promise.reject('File is missing');
    }

    const formData = new FormData();
    formData.append('priceListFile', file);

    return axios.post(`/onlineshop/pricelist/${priceListId}/xlsx`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    }).then(responseBody)
  },
  downloadPriceList: (priceListId: string, priceListName: string) => {
    axios
        .get(`/onlineshop/pricelist/${priceListId}/xlsx`, {
          responseType: 'arraybuffer',
        })
        .then((response) => {
          const blob = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

          const link = document.createElement('a');
          link.href = window.URL.createObjectURL(blob);
          link.download = `${priceListName}.xlsx`;
          link.click();
        })
        .catch((error) => {
          console.error('Error downloading file:', error);
        });
  }
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
  getAll: () => axios.get<OnlineOrderRead[]>('/onlineshop/order/all').then(responseBody),
  updateOrder: (command: OrderUpdateValues) => axios.put('/onlineshop/order', command),
  downloadOrders: (params: URLSearchParams) => {
    axios.get('/onlineshop/order/pdf', { params, responseType: 'blob' })
        .then(response => {
          const blob = new Blob([response.data], { type: 'application/pdf' });

          const url = window.URL.createObjectURL(blob);

          const a = document.createElement('a');
          a.href = url;
          a.download = `Zamówienia-${new Date().toLocaleDateString('pl')}.pdf`;
          a.style.display = 'none';
          document.body.appendChild(a);
          a.click();

          window.URL.revokeObjectURL(url);
        })
        .catch((error) => {
          console.error('Error downloading PDF:', error);
        });
  }
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
  order,
  shops
}

export default agent;