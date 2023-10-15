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

  setSection = (content: JSX.Element | null) => {
    this.content.body = content;
  }

  clearSection = () => {
    this.setSection(null);
  }
}