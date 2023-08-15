import React, { useEffect } from 'react'
import { Button, Container, Card, CardMedia, CardContent, Typography, CardActions, AppBar, Toolbar, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, CircularProgress, Box } from '@mui/material'
import ShoppingCartStore from '../../../global/stores/shoppingCartStore';
import { useStore } from '../../../global/stores/store';
import { Link, useNavigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { ShoppingCartTableItem } from './ShoppingCartTableItem';
import LoadingComponent from '../../../components/LoadingComponent';
import ShoppingCartListItem from './ShoppingCartListItem';
import { ShoppingCartItem } from '../../../global/models/shoppingCart';
import { usePreventNavigation } from '../../../global/router/routeProtection';
import { setPageTitle } from '../../../global/utils/pageTitle';

const ShoppingCartPage: React.FC = () => {
  const {shoppingCartStore, catalogStore} = useStore()
  const {shoppingCart} = shoppingCartStore
  const navigate = useNavigate()

  useEffect(() => {
    setPageTitle('Koszyk')
    shoppingCartStore.loadShoppingCart();
  }, [])
  usePreventNavigation([shoppingCartStore.shoppingCart?.items], '/produkty')  

 if(shoppingCartStore.loading) {
  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', verticalAlign: 'middle', minHeight:'100vh', padding: '20px 0px 20px 0px' }}>
      <LoadingComponent text='Wczytywanie koszyka...'/>
    </div>
  )
 }

  function handleQuantityChange(newQuantity: number, productId: string): void {
    shoppingCartStore.editShoppingItem(newQuantity, productId)
  }

  function handleRemove(item: ShoppingCartItem): void {
    shoppingCartStore.deleteShoppingItem(item)
  }

 return (
  <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', verticalAlign: 'middle', minHeight:'100vh', padding: '20px 0px 20px 0px' }}>
  <Container style={{ maxWidth: '750px', backgroundColor: '#f5f5f5', padding: '20px', borderRadius: '10px' }}>
    <AppBar position="static" style={{ marginBottom: '20px' }}>
      <Toolbar>
        <Typography textAlign={'center'} width={'100%'} variant="h5">
          Koszyk
        </Typography>
      </Toolbar>
    </AppBar>
    {shoppingCart?.items.map(i => 
      <ShoppingCartListItem item={i} key={i.productId} 
      onQuantityChange={(newQuantity) => handleQuantityChange(newQuantity, i.productId)} 
      onRemove={() => handleRemove(i)} />)
    }
    <Typography variant="h6" style={{ marginTop: '10px' }}>
      Przewidywany koszt zamówienia: {}
        {shoppingCart && shoppingCart.totalPrice && shoppingCart.currency && `${Number(shoppingCart.totalPrice.toFixed(1))} ${shoppingCart.currency}`}
    </Typography>
    <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'space-between' }}>
      <Link to={`/produkty`}>
        <Button variant="outlined" color="primary">
          Wróć do katalogu
        </Button>
      </Link>
        <Button variant="contained" color="primary" onClick={() => navigate('dane-osobowe')}
        disabled={!shoppingCart || shoppingCart.items.length < 1 || shoppingCartStore.subLoading}>
          {shoppingCartStore.subLoading ? <CircularProgress /> : "Złóż zamówienie"}
        </Button>
    </div>
  </Container>
</div>  
 )
};

export default observer(ShoppingCartPage);
