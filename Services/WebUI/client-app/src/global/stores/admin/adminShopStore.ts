import { makeAutoObservable,} from "mobx";
import {Shop} from "../../models/shop";

export default class AdminShopStore {
    shopsRegistry: Map<string, Shop> = new Map<string, Shop>()
    loading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    get shops() {
        return Array.from(this.shopsRegistry.values())
    }
}