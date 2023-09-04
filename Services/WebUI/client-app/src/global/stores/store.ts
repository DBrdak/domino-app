import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import ShoppingCartStore from "./shoppingCartStore";
import CatalogStore from "./catalogStore";
import OrderStore from "./orderStore";
import AdminLayoutStore from "./adminLayoutStore";

interface Store {
  modalStore: ModalStore
  shoppingCartStore: ShoppingCartStore,
  catalogStore: CatalogStore
  orderStore: OrderStore
  adminLayoutStore: AdminLayoutStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  shoppingCartStore: new ShoppingCartStore(),
  catalogStore: new CatalogStore(),
  orderStore: new OrderStore(),
  adminLayoutStore: new AdminLayoutStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}