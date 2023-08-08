import React, { useEffect, useState } from 'react'
import NavBar from '../components/NavBar'
import { observer } from 'mobx-react-lite'
import { Box, Button, Container, Grid, Paper, Stack, Typography, useMediaQuery } from '@mui/material'
import FilterPanel from './FilterPanel'
import ProductCatalog from './CatalogPanel'
import { Product } from '../../global/models/product'
import '../../global/styles/index.css'
import theme from '../../global/layout/theme'
import ShoppingCartBadge from './ShoppingCartBadge'
import { useStore } from '../../global/stores/store'
import { FilterOptions } from '../../global/models/filterOptions'
import { Link } from 'react-router-dom'

interface Props {
  category: string | null
}

function CatalogMain({category}: Props) {
  const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
  const {shoppingCartStore, catalogStore} = useStore()
  
  useEffect(() => {
    shoppingCartStore.loadShoppingCart()
  })
  
  useEffect(() => {
    category && catalogStore.loadProducts(category)
  }, [category])

  function handleApplySearch(name: string | null): void {
    catalogStore.filterParams!.searchPhrase = name
    catalogStore.loadProducts(category!)
  }

  function handleApplyFilter(filterOptions: FilterOptions): void {
    catalogStore.setFilter(filterOptions)
    catalogStore.loadProducts(category!)
  }

  //TODO Dodać animację dodania do koszyka + po dodaniu wyświetlać pencil zamiast koszyka
  return (
    !category ?
    <div style={{backgroundColor: '#E4E4E4', minHeight:'100vh'}}>
      <NavBar />
      <div style={{height: '75vh', width:'100%', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
        <Paper style={{padding: '40px'}} >
          <Stack direction={'column'} width={'100%'}>
            <Typography variant='h4' marginBottom={2}>Wybierz kategorię</Typography>
            <Stack direction={'row'} spacing={4} style={{display: 'flex', justifyContent: 'center', width: '100%'}}>
              <Link to={'mięso'}><Button variant='contained'>Mięso</Button></Link>
              <Link to={'wędliny'}><Button variant='contained'>Wędliny</Button></Link>
            </Stack>
          </Stack>
        </Paper>
      </div>
    </div>
    :
    <div style={{backgroundColor: '#E4E4E4', width: '100vw'}}>
      <NavBar />
      <div style={{marginTop: '20px', width: '100%', display: 'flex', justifyContent: 'center'}}>  
        <Grid container spacing={2} style={{width:'100%'}}>
            <Grid item xs={12} md={12} lg={2}> 
              <FilterPanel onApplyFilter={(filterOptions) => handleApplyFilter(filterOptions)} 
              onApplySearch={(name) => handleApplySearch(name)}/>
            </Grid>
            <Grid item xs={12} md={12} lg={10} style={{textAlign: 'center'}}>
              <ProductCatalog products={catalogStore.products} isLoading={catalogStore.loading} />
            </Grid>
            <ShoppingCartBadge />
        </Grid>
      </div>
    </div>
  )
}

export default observer(CatalogMain)