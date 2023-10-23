import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import ShoppingCartStore from "./customer/shoppingCartStore";
import ProductStore from "./customer/productStore";
import OrderStore from "./customer/orderStore";
import AdminLayoutStore from "./admin/adminLayoutStore";
import AdminProductStore from "./admin/adminProductStore";
import AdminPriceListStore from "./admin/adminPriceListStore";
import AdminShopStore from "./admin/adminShopStore";
import AdminOrderStore from "./admin/adminOrderStore";
import ShopStore from "./customer/shopStore";

interface Store {
  modalStore: ModalStore
  shoppingCartStore: ShoppingCartStore,
  productStore: ProductStore
  orderStore: OrderStore
  shopStore: ShopStore
  adminLayoutStore: AdminLayoutStore
  adminProductStore: AdminProductStore
  adminPriceListStore: AdminPriceListStore
  adminShopStore: AdminShopStore
  adminOrderStore: AdminOrderStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  shoppingCartStore: new ShoppingCartStore(),
  productStore: new ProductStore(),
  orderStore: new OrderStore(),
  shopStore: new ShopStore(),
  adminLayoutStore: new AdminLayoutStore(),
  adminProductStore: new AdminProductStore(),
  adminPriceListStore: new AdminPriceListStore(),
  adminShopStore: new AdminShopStore(),
  adminOrderStore: new AdminOrderStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}