import { makeAutoObservable,} from "mobx";

export default class ShopStore {

    constructor() {
        makeAutoObservable(this)
    }
}