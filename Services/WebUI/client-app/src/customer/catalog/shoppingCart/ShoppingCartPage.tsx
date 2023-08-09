import React, { useEffect } from 'react'
import { Button, Container, Card, CardMedia, CardContent, Typography, CardActions, AppBar, Toolbar, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, CircularProgress } from '@mui/material'
import ShoppingCartStore from '../../../global/stores/shoppingCartStore';
import { useStore } from '../../../global/stores/store';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react-lite';

const ShoppingCartPage: React.FC = () => {
  const {shoppingCartStore, catalogStore} = useStore()
  const {shoppingCart} = shoppingCartStore

  useEffect(() => {
    shoppingCartStore.loadShoppingCart()
  }, [])

  return (
    <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
      <Container style={{ maxWidth: '750px', backgroundColor: '#f5f5f5', padding: '20px', borderRadius: '10px' }}>
        <AppBar position="static" style={{ marginBottom: '20px' }}>
          <Toolbar>
            <Typography textAlign={'center'} width={'100%'} variant="h5">
              Koszyk
            </Typography>
          </Toolbar>
        </AppBar>
          <TableContainer component={Paper} style={{width:'100%', padding: '40px'}}>
            {shoppingCart ? (
            <Table sx={{ width: '100%' }} aria-label="spanning table" >
              <TableHead>
                <TableRow>
                  <TableCell style={{fontWeight: 900}} align="center" width={'100%'} colSpan={4}>
                    Produkty
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell style={{fontWeight: 900}}>Nazwa produktu</TableCell>
                  <TableCell style={{fontWeight: 900}} align="right">Ilość</TableCell>
                  <TableCell style={{fontWeight: 900}} align="right">Cena</TableCell>
                  <TableCell style={{fontWeight: 900}} align="right">Wartość</TableCell>
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
                  <TableCell style={{fontWeight: 900}} colSpan={3}>Suma</TableCell>
                  {shoppingCart && <TableCell style={{fontWeight: 900}} align="right">{shoppingCart.totalPrice.toFixed(1)} {shoppingCart.currency}</TableCell>}
                </TableRow>
              </TableBody>
            </Table>
            ) : (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '200px' }}>
              <CircularProgress />
            </div>
            )}
          </TableContainer>
          <Typography variant="h6" style={{ marginTop: '10px' }}>
            Przewidywany koszt zamówienia: {}
            {shoppingCart ? 
              `${shoppingCart.totalPrice.toFixed(1)} ${shoppingCart.currency}`
              :       
              <CircularProgress size={'1.25rem'} />
            }
          </Typography>
        <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'space-between' }}>
          <Link to={`/produkty`}>
            <Button variant="outlined" color="primary">
              Wróć do katalogu
            </Button>
          </Link>
          <Link to={'dane-osobowe'}>
            <Button variant="contained" color="primary">
              Złóż zamówienie
            </Button>
          </Link>
        </div>
      </Container>
    </div>
  );
};

export default observer(ShoppingCartPage);
