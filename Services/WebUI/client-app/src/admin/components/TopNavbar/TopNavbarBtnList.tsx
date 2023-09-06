import { Button, Stack } from '@mui/material'
import React, { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom'
import ProductList from '../../onlineShop/productList/ProductList'
import TopNavbarBtn from '../topNavbar/TopNavbarBtn'
import ProductListSection from '../../onlineShop/productList/ProductListSection'

function TopNavbarBtnList() {
  const location = useLocation()
  const [content, setContent] = useState<{content: JSX.Element, text: string }[]>([])

  useEffect(() => {
    switch(location.pathname){
      case '/admin/sklep-online':
        setContent([
          {content: <ProductListSection />, text: 'lista produktów' },
          {content: <ProductList />, text: 'zamówienia' },
        ])
        break
      case '/admin/sprzedaz':
        setContent([
          {content: <ProductList />, text: 'raporty' },
        ])
        break
      case '/admin/cenniki':
        setContent([
          {content: <ProductList />, text: 'lista cenników' },
        ])
        break
      case '/admin/sklepy':
        setContent([
          {content: <ProductList />, text: 'lista sklepów' },
          {content: <ProductList />, text: 'punkty sprzedaży' },
        ])
        break
      case '/admin/paliwo':
        setContent([
          {content: <ProductList />, text: 'raporty' },
          {content: <ProductList />, text: 'dostawy' },
        ])
        break
      case '/admin/flota':
        setContent([
          {content: <ProductList />, text: 'pojazdy' },
        ])
        break
      case '/admin/kontrahenci':
        setContent([
          {content: <ProductList />, text: 'wkrótce' },
        ])
        break
      case '/admin/masarnia':
        setContent([
          {content: <ProductList />, text: 'wkrótce' },
        ])
        break 
      case '/admin/statystyki':
        setContent([
          {content: <ProductList />, text: 'wkrótce' },
        ])
        break 
      case '/admin/kalkulatory':
        setContent([
          {content: <ProductList />, text: 'wkrótce' },
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