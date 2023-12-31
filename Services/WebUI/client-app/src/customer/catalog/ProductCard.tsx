import React, { useState } from 'react';
import { Card, CardMedia, CardContent, Typography, CardActions, useMediaQuery, IconButton, Stack, Chip } from '@mui/material';
import { Product } from '../../global/models/product';
import theme from '../../global/layout/theme';
import { AddShoppingCart, Edit } from '@mui/icons-material';
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
  const {shoppingCartStore, productStore} = useStore()

  function handleQuantityModeEnter(): void {
    productStore.setQuantityModeFor(product.id)
    shoppingCartStore.setProduct(product)
  }

  function handleAddToShoppingCart(): void {
    shoppingCartStore.addShoppingItem();
    productStore.resetQuantityMode();

    setShowCompletionMark(true);

    setTimeout(() => {
        setShowCompletionMark(false);
    }, 700);
}

  return (
    <Card style={{padding: '15px', position: 'relative', height: '400px'}}>
        {product.details.isDiscounted && (
        <Chip 
        label="Promocja" 
        color="primary" 
        style={{ position: 'absolute', top: '5px', left: '5px', zIndex: 1}}
        />
        )}
      <CardMedia component="img" height="200" image={product.image.url} alt={product.name} />
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
            {productStore.quantityMode !== product.id &&
            <>
              <Typography variant="body2" color="textSecondary">
                {product.description}
              </Typography>
              <div style={{margin: '10px 0px 0px 0px', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                {product.details.isDiscounted ?
                <Typography textAlign={'center'} 
                style={{width: '70%', borderRadius: '30px', color: '#FFFFFF', backgroundColor: '#C32B28', padding: '6px 0px 6px 0px'}}>
                  {product.price.amount} {product.price.currency.code}/{product.price.unit?.code}
                </Typography>
                :
                <Typography textAlign={'center'} 
                style={{width: '70%', borderRadius: '30px', color: '#C32B28', border: '1px', borderColor: '#C32B28', padding: '6px 0px 6px 0px'}}>
                  {product.price.amount} {product.price.currency.code}/{product.price.unit?.code}
                </Typography>
                }
              </div>
            </>}
          </CardContent>
          <CardActions style={{justifyContent: 'center'}}>
            {productStore.quantityMode !== product.id ?
            <>
              {isMobile ?
              <>
                {product.details.isAvailable ? 
                <>
                  { shoppingCartStore.shoppingCart?.items.some(i => i.productId === product.id) ?
                  <IconButton color="secondary" style={{backgroundColor: '#06992d', width: '50%', borderRadius: '20px'}}
                  onClick={() => handleQuantityModeEnter()} disabled={!product.details.isAvailable}>
                    <Edit />
                  </IconButton>
                  :
                  <IconButton color="secondary" style={{backgroundColor: '#C32B28', width: '50%', borderRadius: '20px'}}
                  onClick={() => handleQuantityModeEnter()} disabled={!product.details.isAvailable}>
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
                {product.details.isAvailable ? 
                <>
                  { shoppingCartStore.shoppingCart?.items.some(i => i.productId === product.id) ?
                  <IconButton color="secondary" style={{backgroundColor: '#06992d'}} 
                  onClick={() => handleQuantityModeEnter()} disabled={!product.details.isAvailable}>
                    <Edit />
                  </IconButton>
                  :
                  <IconButton color="secondary" style={{backgroundColor: '#C32B28'}} 
                  onClick={() => handleQuantityModeEnter()} disabled={!product.details.isAvailable}>
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
              <QuantityInput isPcsAllowed={product.details.isWeightSwitchAllowed} onInputSubmit={() => handleAddToShoppingCart()}/>
            </Stack>
            }
          </CardActions>
        </>
      }
    </Card>
  );
};

export default observer(ProductCard);