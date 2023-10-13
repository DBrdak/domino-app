import { makeAutoObservable } from "mobx"

interface Content {
  body: JSX.Element | null;
}

export default class AdminLayoutStore {
  content: Content = {
    body: null
  }

  constructor() {
    makeAutoObservable(this);
  }

  setSection = (content: JSX.Element) => {
    this.content.body = content;
  }

  clearSection = () => {
    this.content.body = null;
  }
}