import React, { useEffect } from 'react'
import { Button, Container, Card, CardMedia, CardContent, Typography, CardActions, AppBar, Toolbar, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material'
import ShoppingCartStore from '../../../global/stores/shoppingCartStore';
import { useStore } from '../../../global/stores/store';

const ShoppingCartPage: React.FC = () => {
  const {shoppingCartStore} = useStore()
  const {shoppingCart} = shoppingCartStore
//TODO Dopracować działanie koszyka po refreshu
  useEffect(() => {
    if(!shoppingCart) {
      shoppingCartStore.loadShoppingCart()
    }
  })

  return (
    <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
      <Container style={{ maxWidth: '750px', backgroundColor: '#f5f5f5', padding: '20px', borderRadius: '10px' }}>
        <AppBar position="static" style={{ marginBottom: '20px' }}>
          <Toolbar>
            <Typography textAlign={'center'} width={'100%'} variant="h6">
              Koszyk
            </Typography>
          </Toolbar>
        </AppBar>
          <TableContainer component={Paper} style={{width:'100%', padding: '40px'}}>
            <Table sx={{ width: '100%' }} aria-label="spanning table" >
              <TableHead >
                <TableRow>
                  <TableCell align="center" width={'100%'} colSpan={4}>
                    Produkty
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell>Nazwa produktu</TableCell>
                  <TableCell align="right">Ilość</TableCell>
                  <TableCell align="right">Cena</TableCell>
                  <TableCell align="right">Wartość</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {shoppingCart && shoppingCart.items.map((item) => (
                  <TableRow key={item.productId.concat(item.unit)}>
                    <TableCell>{item.productName}</TableCell>
                    <TableCell align="right">{item.quantity} {item.unit}</TableCell>
                    <TableCell align="right">{item.price.amount} {item.price.currency}/{item.price.unit}</TableCell>
                    <TableCell align="right">{item.totalValue.toFixed(1)} {item.price.currency}</TableCell>
                  </TableRow>
                ))}
                <TableRow>
                  <TableCell colSpan={3}>Suma</TableCell>
                  {shoppingCart && <TableCell align="right">{shoppingCart.totalPrice.toFixed(1)} {shoppingCart.currency}</TableCell>}
                </TableRow>
              </TableBody>
            </Table>
          </TableContainer>
        <Typography variant="h6" style={{ marginTop: '10px' }}>Przewidywany koszt zamówienia: {shoppingCart?.totalPrice.toFixed(1)} {shoppingCart?.currency}</Typography>
        <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'space-between' }}>
          <Button variant="contained" color="secondary">
            Wróć do katalogu
          </Button>
          <Button variant="contained" color="primary">
          Złóż zamówienie
          </Button>
        </div>
      </Container>
    </div>
  );
};

export default ShoppingCartPage;
