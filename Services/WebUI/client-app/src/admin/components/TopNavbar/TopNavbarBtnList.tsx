import { Button, Stack } from '@mui/material'
import React, { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom'
import ProductList from '../../onlineShop/productList/ProductList'
import TopNavbarBtn from '../topNavbar/TopNavbarBtn'
import PriceListsSection from "../../pricelists/priceListList/PriceListsSection";
import ProductListSection from "../../onlineShop/ProductListSection";
import ShopsListSection from "../../shops/shopsList/ShopsListSection";
import {SalePointMap} from "../../shops/salePointMap/SalePointMap";
import SalePointMapSection from "../../shops/salePointMap/SalePointMapSection";
import OrdersListSection from "../../onlineShop/OrdersListSection";

function TopNavbarBtnList() {
  const location = useLocation()
  const [content, setContent] = useState<{content: JSX.Element, text: string }[]>([])

  useEffect(() => {
    switch(location.pathname){
      case '/admin/sklep-online':
        setContent([
          {content: <ProductListSection />, text: 'lista produktów' },
          {content: <OrdersListSection />, text: 'zamówienia' },
        ])
        break
      case '/admin/sprzedaz':
        setContent([
          {content: <PriceListsSection />, text: 'raporty' },
        ])
        break
      case '/admin/cenniki':
        setContent([
          {content: <PriceListsSection />, text: 'lista cenników' },
        ])
        break
      case '/admin/sklepy':
        setContent([
          {content: <ShopsListSection />, text: 'lista sklepów' },
          {content: <SalePointMapSection />, text: 'punkty sprzedaży' },
        ])
        break
      case '/admin/paliwo':
        setContent([
          {content: <PriceListsSection />, text: 'raporty' },
          {content: <PriceListsSection />, text: 'dostawy' },
        ])
        break
      case '/admin/flota':
        setContent([
          {content: <PriceListsSection />, text: 'pojazdy' },
        ])
        break
      case '/admin/kontrahenci':
        setContent([
          {content: <PriceListsSection />, text: 'wkrótce' },
        ])
        break
      case '/admin/masarnia':
        setContent([
          {content: <PriceListsSection />, text: 'wkrótce' },
        ])
        break 
      case '/admin/statystyki':
        setContent([
          {content: <PriceListsSection />, text: 'wkrótce' },
        ])
        break 
      case '/admin/kalkulatory':
        setContent([
          {content: <PriceListsSection />, text: 'wkrótce' },
        ])
        break 
    }
  }, [location.pathname])
  

  return (
    <Stack direction={'row'} 
    display={'flex'} 
    justifyContent={'center'} 
    gap={10} 
    alignItems={'center'} 
    width={'100%'}>
      <>
        {content.map(c => (
          <TopNavbarBtn key={c.text} content={c.content} text={c.text} />
        ))}
      </>
    </Stack>
  )
}

export default TopNavbarBtnList