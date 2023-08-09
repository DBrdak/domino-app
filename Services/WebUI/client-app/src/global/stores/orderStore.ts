import { makeAutoObservable } from "mobx";

export default class OrderStore {
  loading: boolean = false

  constructor(){
    makeAutoObservable(this)
  }
}