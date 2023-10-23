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
import LoginPage from "../../admin/home/LoginPage";
import AdminMain from "../../admin/home/AdminMain";
import OnlineShopPanel from "../../admin/onlineShop/OnlineShopPanel";
import SalesPanel from "../../admin/sales/SalesPanel";
import PricelistsPanel from "../../admin/pricelists/PricelistsPanel";
import ShopsPanel from "../../admin/shops/ShopsPanel";
import FuelPanel from "../../admin/fuel/FuelPanel";
import FleetPanel from "../../admin/fleet/FleetPanel";
import ContractorsPanel from "../../admin/contractors/ContractorsPanel";
import ButcheryPanel from "../../admin/butchery/ButcheryPanel";
import StatsPanel from "../../admin/stats/StatsPanel";
import CalculatorsPanel from "../../admin/calculators/CalculatorsPanel";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      // Customer
      {path: '', element: <HomePage />},
      {path: 'produkty', element: <CatalogMain category={null} />},
      {path: 'produkty/mięso', element: <CatalogMain category={'meat'} />},
      {path: 'produkty/wędliny', element: <CatalogMain category={'sausage'} />},
      {path: 'koszyk', element: <ShoppingCartPage />, },
      {path: 'koszyk/dane-osobowe', element: <PersonalInfo />},
      {path: 'koszyk/dane-wysyłki', element: <DeliveryInfo />},
      {path: 'koszyk/zamówienie', element: <OrderCompletion />},
      {path: `zamówienie/:id`, element: <OrderPage/>},
      {path: `zamówienie/undefined`, element: <NotFoundPage text="Nie mamy tego zamówienia 😔"/>},
      {path: 'o-nas', element: <AboutMain />},
      {path: 'dla-firm', element: <ContractorsMain />},
      {path: 'kontakt', element: <ContactMain />},
      // Admin
      {path: 'admin', element: <LoginPage />},
      {path: 'admin/główna', element: <AdminMain />},
      {path: 'admin/sklep-online', element: <OnlineShopPanel />},
      {path: 'admin/sprzedaz', element: <SalesPanel />},
      {path: 'admin/cenniki', element: <PricelistsPanel />},
      {path: 'admin/sklepy', element: <ShopsPanel />},
      {path: 'admin/paliwo', element: <FuelPanel />},
      {path: 'admin/flota', element: <FleetPanel />},
      {path: 'admin/kontrahenci', element: <ContractorsPanel />},
      {path: 'admin/masarnia', element: <ButcheryPanel />},
      {path: 'admin/statystyki', element: <StatsPanel />},
      {path: 'admin/kalkulatory', element: <CalculatorsPanel />},
      {path: '*', element: <NotFoundPage text={'Nie znaleźliśmy szukanej zawarości 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);