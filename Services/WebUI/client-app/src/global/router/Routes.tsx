import { RouteObject, Navigate } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import App from "../layout/App"
import CatalogMain from "../../customer/catalog/CatalogMain";
import HomePage from "../../customer/home/HomePage";
import AboutMain from "../../customer/about/AboutMain";
import ContractorsMain from "../../customer/contractors/ContractorsMain";
import ContactMain from "../../customer/contact/ContactMain";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: '', element: <HomePage />},
      {path: 'produkty', element: <CatalogMain />},
      {path: 'o-nas', element: <AboutMain />},
      {path: 'dla-firm', element: <ContractorsMain />},
      {path: 'kontakt', element: <ContactMain />}
      //{path: '*', element: <Navigate replace={true} to='/' />}
    ]
  }
]

export const router = createBrowserRouter(routes);