import React from 'react';
import { Card, CardMedia, CardContent, Typography, Button, CardActions, useMediaQuery, IconButton } from '@mui/material';
import { Product } from '../../global/models/product';
import theme from '../../global/layout/theme';
import { AddShoppingCart, VisibilityOutlined } from '@mui/icons-material';


interface Props {
  product: Product
}

const ProductCard: React.FC<Props> = ({product}: Props) => {
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));
  return (
    <Card style={{padding: '15px'}}>
      <CardMedia component="img" height="200" image={product.image} alt={product.name} />
      <CardContent style={{paddingBottom: '0px'}}>
        <Typography variant="h6" gutterBottom>
          {product.name}
        </Typography>
        <Typography variant="body2" color="textSecondary">
          {product.description}
        </Typography>
        <div style={{margin: '10px 0px 0px 0px', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
          <Typography textAlign={'center'} style={{width: '70%', borderRadius: '30px', color: '#FFFFFF', backgroundColor: '#C32B28', padding: '6px 0px 6px 0px'}}>
            {product.price} {product.currency}/{product.unit}
          </Typography>
        </div>
      </CardContent>
      <CardActions style={{justifyContent: 'center'}}>
        {isMobile ?         
        <IconButton color="secondary" style={{backgroundColor: '#C32B28', width: '50%', borderRadius: '20px'}}>
          <AddShoppingCart fontSize='large'/>
        </IconButton> :
        <IconButton color="secondary" style={{backgroundColor: '#C32B28'}}>
          <AddShoppingCart />
        </IconButton>
        }
        {isMobile ?         
        <IconButton color="primary" style={{width: '50%', borderRadius: '20px'}}>
          <VisibilityOutlined fontSize='large'/>
        </IconButton> :
        <IconButton color="primary">
          <VisibilityOutlined />
        </IconButton>
        }
      </CardActions>
    </Card>
  );
};

export default ProductCard;