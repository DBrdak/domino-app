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

function CatalogMain() {
  const [category, setCategory] = useState<string | null>(null)
  const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
  const {shoppingCartStore, catalogStore} = useStore()
  
  useEffect(() => {
    shoppingCartStore.loadShoppingCart()
  })

  function handleCategorySet(c: string): void {
    setCategory(c)
    catalogStore.loadProducts(c)
  }

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
    <>
      <NavBar />
      <div style={{height: '75vh ', width:'100%', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
        <Paper style={{padding: '40px'}}>
          <Stack direction={'column'} width={'100%'}>
            <Typography variant='h4' marginBottom={2}>Wybierz kategorię</Typography>
            <Stack direction={'row'} spacing={4} style={{display: 'flex', justifyContent: 'center', width: '100%'}}>
              <Button variant='contained' onClick={() => handleCategorySet('meat')}>Mięso</Button>
              <Button variant='contained' onClick={() => handleCategorySet('sausage')}>Wędliny</Button>
            </Stack>
          </Stack>
        </Paper>
      </div>
    </>
    :
    <>
      <NavBar />
      <Container style={{margin: '20px 0px 0px 0px'}}>  
        <Grid container spacing={2} width={'98vw'} >
            <Grid item xs={12} md={12} lg={2}> 
              <FilterPanel onApplyFilter={(filterOptions) => handleApplyFilter(filterOptions)} 
              onApplySearch={(name) => handleApplySearch(name)}/>
            </Grid>
            <Grid item xs={12} md={12} lg={10} style={{textAlign: 'center'}}>
              <ProductCatalog products={catalogStore.products} isLoading={catalogStore.loading} />
            </Grid>
        </Grid>
        <ShoppingCartBadge />
      </Container>
    </>
  )
}

export default observer(CatalogMain)