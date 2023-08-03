import { RouteObject, Navigate } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import App from "../layout/App"
import CatalogMain from "../../customer/catalog/CatalogMain";
import HomePage from "../../customer/home/HomePage";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: 'produkty', element: <CatalogMain />},
      {path: '*', element: <Navigate replace={true} to='/' />}
    ]
  }
]

export const router = createBrowserRouter(routes);