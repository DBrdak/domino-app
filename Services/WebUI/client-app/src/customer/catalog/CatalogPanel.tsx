import React from 'react';
import { Grid } from '@mui/material';
import ProductCard from './ProductCard';
import { Product } from '../../global/models/product';

interface Props {
  products: Product[]
}

const ProductCatalog: React.FC<Props> = ({products}: Props) => {
  return (
    <Grid container spacing={3}>
      {products.map((product) => (
        <Grid item xs={12} sm={6} md={4} key={product.id}>
          <ProductCard product={product} />
        </Grid>
      ))}
    </Grid>
  );
};

export default ProductCatalog;