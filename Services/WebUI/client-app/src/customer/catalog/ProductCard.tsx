import React, { useState } from 'react';
import { Card, CardMedia, CardContent, Typography, Button, CardActions, useMediaQuery, IconButton, Stack, Chip } from '@mui/material';
import { Product } from '../../global/models/product';
import theme from '../../global/layout/theme';
import { Add, AddCircle, AddCircleOutlined, AddShoppingCart, CancelOutlined, Clear, Draw, Edit, VisibilityOutlined } from '@mui/icons-material';
import { useStore } from '../../global/stores/store';
import { observer } from 'mobx-react-lite';
import QuantityInput from './QuantityInput';
import CompletionMark from '../../components/CompletionMark';


interface Props {
  product: Product
}

const ProductCard: React.FC<Props> = ({product}: Props) => {
  const [showCompletionMark, setShowCompletionMark] = useState(false);
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));
  const {shoppingCartStore, catalogStore} = useStore()

  function handleQuantityModeEnter(): void {
    catalogStore.setQuantityModeFor(product.id)
    shoppingCartStore.setProduct(product)
  }

  function handleAddToShoppingCart(): void {
    shoppingCartStore.addShoppingItem();
    catalogStore.resetQuantityMode();

    setShowCompletionMark(true);

    setTimeout(() => {
        setShowCompletionMark(false);
    }, 700);
}

  return (
    <Card style={{padding: '15px', position: 'relative', height: '400px'}}>
        {product.isDiscounted && (
        <Chip 
        label="Promocja" 
        color="primary" 
        style={{ position: 'absolute', top: '5px', left: '5px', zIndex: 1}}
        />
        )}
      <CardMedia component="img" height="200" image={product.image} alt={product.name} />
      {showCompletionMark ?
        <CardContent style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
          <CompletionMark />
        </CardContent>
      :
        <>
          <CardContent style={{paddingBottom: '0px'}}>
            <Typography variant="h6">
              {product.name}
            </Typography>
            {catalogStore.quantityMode !== product.id && 
            <>
              <Typography variant="body2" color="textSecondary">
                {product.description}
              </Typography>
              <div style={{margin: '10px 0px 0px 0px', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                {product.isDiscounted ?
                <Typography textAlign={'center'} 
                style={{width: '70%', borderRadius: '30px', color: '#FFFFFF', backgroundColor: '#C32B28', padding: '6px 0px 6px 0px'}}>
                  {product.price.amount} {product.price.currency}/{product.price.unit}
                </Typography>
                :
                <Typography textAlign={'center'} 
                style={{width: '70%', borderRadius: '30px', color: '#C32B28', border: '1px', borderColor: '#C32B28', padding: '6px 0px 6px 0px'}}>
                  {product.price.amount} {product.price.currency}/{product.price.unit}
                </Typography>
                }
              </div>
            </>}
          </CardContent>
          <CardActions style={{justifyContent: 'center'}}>
            {catalogStore.quantityMode !== product.id ?
            <>
              {isMobile ?
              <>
                {product.isAvailable ? 
                <>
                  { shoppingCartStore.shoppingCart?.items.some(i => i.productId === product.id) ?
                  <IconButton color="secondary" style={{backgroundColor: '#06992d', width: '50%', borderRadius: '20px'}}
                  onClick={() => handleQuantityModeEnter()} disabled={!product.isAvailable}>
                    <Edit />
                  </IconButton>
                  :
                  <IconButton color="secondary" style={{backgroundColor: '#C32B28', width: '50%', borderRadius: '20px'}}
                  onClick={() => handleQuantityModeEnter()} disabled={!product.isAvailable}>
                    <AddShoppingCart />
                  </IconButton>}
                </>
                :
                <Typography textAlign={'center'} 
                style={{width: '100%', color: '#C32B28'}}>
                  Produkt chwilowo niedostępny
                </Typography>
                }
              </> :
              <>
                {product.isAvailable ? 
                <>
                  { shoppingCartStore.shoppingCart?.items.some(i => i.productId === product.id) ?
                  <IconButton color="secondary" style={{backgroundColor: '#06992d'}} 
                  onClick={() => handleQuantityModeEnter()} disabled={!product.isAvailable}>
                    <Edit />
                  </IconButton>
                  :
                  <IconButton color="secondary" style={{backgroundColor: '#C32B28'}} 
                  onClick={() => handleQuantityModeEnter()} disabled={!product.isAvailable}>
                    <AddShoppingCart />
                  </IconButton>}
                </>
                :
                <Typography textAlign={'center'} 
                style={{width: '100%', color: '#C32B28'}}>
                  Produkt chwilowo niedostępny
                </Typography>
                }
              </>

              }
            </>
            :
            <Stack direction={'row'} style={{width: '100%', display: 'flex', justifyContent: 'center'}}>
              <QuantityInput isPcsAllowed={product.quantityModifier.isPcsAllowed} onInputSubmit={() => handleAddToShoppingCart()}/>
            </Stack>
            }
          </CardActions>
        </>
      }
    </Card>
  );
};

export default observer(ProductCard);