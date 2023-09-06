import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import ShoppingCartStore from "./customer/shoppingCartStore";
import CatalogStore from "./customer/catalogStore";
import OrderStore from "./customer/orderStore";
import AdminLayoutStore from "./admin/adminLayoutStore";
import AdminCatalogStore from "./admin/adminCatalogStore";

interface Store {
  modalStore: ModalStore
  shoppingCartStore: ShoppingCartStore,
  catalogStore: CatalogStore
  orderStore: OrderStore
  adminLayoutStore: AdminLayoutStore
  adminCatalogStore: AdminCatalogStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  shoppingCartStore: new ShoppingCartStore(),
  catalogStore: new CatalogStore(),
  orderStore: new OrderStore(),
  adminLayoutStore: new AdminLayoutStore(),
  adminCatalogStore: new AdminCatalogStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}