import React from 'react'
import NavBar from '../components/NavBar'
import { observer } from 'mobx-react-lite'
import { Box, Container, Grid } from '@mui/material'
import FilterPanel from './FilterPanel'
import ProductCatalog from './CatalogPanel'
import { Product } from '../../global/models/product'
import '../../global/styles/index.css'

function CatalogMain() {
const prods: Product[] = [
  {
    id: '1',
    name: 'Kiełbasa Pyszna',
    description: 'Przykładowy opis wędliny',
    category: 'Sausage',
    subcategory: 'Kiełbasy',
    image: 'assets/examples/kpyszna.jpg',
    price: 11.9,
    currency: 'PLN',
    unit: 'kg',
    isAvailable: true,
    isDiscounted: true,
    isPcsAllowed: false,
    kgPerPcs: 0,
  },  
  {
    id: '2',
    name: 'Karkówka b/k',
    description: 'Przykładowy opis mięsa',
    category: 'Meat',
    subcategory: '',
    image: 'assets/examples/kark.jpg',
    price: 25.9,
    unit: 'kg',
    currency: 'PLN',
    isAvailable: true,
    isDiscounted: false,
    isPcsAllowed: true,
    kgPerPcs: 2,
  },  
  {
    id: '3',
    name: 'Boczek Marysieńki',
    description: 'Przykładowy opis wędliny',
    category: 'Sausage',
    subcategory: 'Seria Marysieńki',
    image: 'assets/examples/bokmar.jpg',
    price: 38.9,
    currency: 'PLN',
    unit: 'kg',
    isAvailable: true,
    isDiscounted: false,
    isPcsAllowed: false,
    kgPerPcs: 0,
  },

]

  return (
    <>
      <NavBar />
      <Container style={{margin: '20px 0px 0px 0px'}}>
        <Grid container spacing={2}>
          <Grid item xs={12} md={3} >
            <FilterPanel onApplyFilter={(filterOptions) => console.log(filterOptions)} 
            onApplySearch={(name) => console.log(name)} subcategories={prods.map(p => p.subcategory)}/>
          </Grid>
          <Grid item xs={12} md={9} style={{textAlign: 'center'}}>
            <ProductCatalog products={prods} />
          </Grid>
        </Grid>
      </Container>
    </>
  )
}

export default observer(CatalogMain)