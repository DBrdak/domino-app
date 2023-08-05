import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import ShoppingCartStore from "./shoppingCartStore";
import CatalogStore from "./catalogStore";

interface Store {
  modalStore: ModalStore
  shoppingCartStore: ShoppingCartStore,
  catalogStore: CatalogStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  shoppingCartStore: new ShoppingCartStore(),
  catalogStore: new CatalogStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}