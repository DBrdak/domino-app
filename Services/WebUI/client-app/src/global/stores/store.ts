import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import ShoppingCartStore from "./shoppingCartStore";
import CatalogStore from "./catalogStore";
import OrderStore from "./orderStore";

interface Store {
  modalStore: ModalStore
  shoppingCartStore: ShoppingCartStore,
  catalogStore: CatalogStore
  orderStore: OrderStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  shoppingCartStore: new ShoppingCartStore(),
  catalogStore: new CatalogStore(),
  orderStore: new OrderStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}