import { RouteObject, Navigate } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import App from "../../App"
import CatalogMain from "../../customer/catalog/CatalogMain";

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