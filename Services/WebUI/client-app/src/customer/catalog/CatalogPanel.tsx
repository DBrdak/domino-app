import React from 'react';
import { Grid } from '@mui/material';
import ProductCard from './ProductCard';
import { Product } from '../../global/models/product';
import { observer } from 'mobx-react-lite';

interface Props {
  products: Product[]
}

const ProductCatalog: React.FC<Props> = ({products}: Props) => {
  return (
    <Grid container spacing={4} >
      {products.map((product) => (
        <Grid item xs={12} sm={6} md={4} lg={4} xl={3 } key={product.id}>
          <ProductCard product={product} />
        </Grid>
      ))}
    </Grid>
  );
};

export default observer(ProductCatalog);