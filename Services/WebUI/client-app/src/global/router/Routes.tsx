import { RouteObject, Navigate } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import App from "../layout/App"
import CatalogMain from "../../customer/catalog/CatalogMain";
import HomePage from "../../customer/home/HomePage";
import AboutMain from "../../customer/about/AboutMain";
import ContractorsMain from "../../customer/contractors/ContractorsMain";
import ContactMain from "../../customer/contact/ContactMain";
import ShoppingCartPage from "../../customer/catalog/shoppingCart/ShoppingCartPage";
import path from "path";
import PersonalInfo from "../../customer/catalog/shoppingCart/PersonalInfoStep";
import OrderCompletion from "../../customer/catalog/shoppingCart/OrderCompletion";
import DeliveryInfo from "../../customer/catalog/shoppingCart/DeliveryInfoStep";
import OrderPage from "../../customer/catalog/order/OrderPage";
import NotFoundPage from "../../components/NotFoundPage";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: '', element: <HomePage />},
      {path: 'produkty', element: <CatalogMain category={null} />},
      {path: 'produkty/mięso', element: <CatalogMain category={'meat'} />},
      {path: 'produkty/wędliny', element: <CatalogMain category={'sausage'} />},
      {path: 'koszyk', element: <ShoppingCartPage />, },
      {path: 'koszyk/dane-osobowe', element: <PersonalInfo />},
      {path: 'koszyk/dane-wysyłki', element: <DeliveryInfo />},
      {path: 'koszyk/zamówienie', element: <OrderCompletion />},
      {path: `zamówienie/:id`, element: <OrderPage/>},
      {path: 'o-nas', element: <AboutMain />},
      {path: 'dla-firm', element: <ContractorsMain />},
      {path: 'kontakt', element: <ContactMain />},
      {path: '*', element: <NotFoundPage text={'Nie znaleziono szukanej zawartości 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);