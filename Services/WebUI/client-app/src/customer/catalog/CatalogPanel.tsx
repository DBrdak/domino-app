import React from 'react';
import { Grid } from '@mui/material';
import ProductCard from './ProductCard';
import { Product } from '../../global/models/product';
import { observer } from 'mobx-react-lite';
import LoadingCard from '../components/LoadingCard';

interface Props {
  products: Product[]
  isLoading: boolean
}

const ProductCatalog: React.FC<Props> = ({products, isLoading}: Props) => {  
  return (
    <Grid container spacing={4}>
      {isLoading ?
        Array(12).fill(0).map((_, idx) => (
          <Grid item xs={12} sm={6} md={4} lg={4} xl={3} key={idx}>
            <LoadingCard />
          </Grid>
        ))
        :
        products.map((product) => (
          <Grid item xs={12} sm={6} md={4} lg={4} xl={3} key={product.id}>
            <ProductCard product={product} />
          </Grid>
        ))
      }
    </Grid>
  );
}

export default observer(ProductCatalog);