import React, { useState, useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Paper, CircularProgress, Typography, AppBar, Toolbar, Container, Stack, Button } from "@mui/material";
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import { Link } from "react-router-dom";
import CompletionMark from "../../../components/CompletionMark";
import { useStore } from "../../../global/stores/store";
import { OnlineOrderRead } from "../../../global/models/order";
import { getPolishDayOfWeek } from "./temp";
import { usePreventNavigation } from "../../../global/router/routeProtection";
import LoadingComponent from "../../../components/LoadingComponent";
import DateTimeRangeDisplay from "../../../components/DateTimeRangeDisplay";
import { setPageTitle } from "../../../global/utils/pageTitle";

const OrderCompletion: React.FC = observer(() => {
  const [order, setOrder] = useState<OnlineOrderRead | null>(null)
  const {orderStore, shoppingCartStore} = useStore()

  usePreventNavigation([
    shoppingCartStore.shoppingCart, shoppingCartStore.personalInfo, shoppingCartStore.deliveryInfo
  ], '/koszyk')

  useEffect(() => {
    const processCheckoutAndLoadOrder = async () => {
        await shoppingCartStore.checkoutShoppingCart()
        if (orderStore.orderId) {
          await orderStore.loadOrder()
        }
        setOrder(orderStore.order as OnlineOrderRead)
    }
    processCheckoutAndLoadOrder()
    setPageTitle('Zamówienie')
  }, []);

  if (orderStore.loading || shoppingCartStore.loading) {
    return (
      <LoadingComponent text="Przetwarzanie zamówienia..." fullScreen />
    );
  }

  return (
    order &&
    <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
        <Paper style={{ padding: 50, margin: 'auto', maxWidth: 750, textAlign: 'center' }}>
          <AppBar position="static" style={{ marginBottom: '20px' }}>
            <Toolbar>
              <Typography textAlign={'center'} width={'100%'} variant="h5">
                Dane Zamówienia
              </Typography>
            </Toolbar>
          </AppBar>
          <Typography variant="subtitle1">Imię: {order?.firstName}</Typography>
          <Typography variant="subtitle1">Nazwisko: {order?.lastName}</Typography>
          <Typography variant="subtitle1">Numer telefonu: {order?.phoneNumber}</Typography>
          <Typography variant="subtitle1">Miejsce odbioru: {order?.deliveryLocation.name}</Typography>
          <Typography variant="subtitle1">Czas odbioru: {<DateTimeRangeDisplay date={order.deliveryDate}/>}
          </Typography>
          <Typography variant="subtitle1">Numer zamówienia: {order?.orderId}</Typography>
          <CompletionMark />
          <Typography>Dziękujemy!</Typography>
          <Stack width={'100%'} direction={'column'} style={{ marginTop: 20}}>
            <Link to={`/zamówienie/${order.orderId}`} >
              <Button variant="contained" color="primary" style={{ width: '100%' }}>Zobacz zamówienie</Button>
            </Link>
            <Link to={'/'} >
              <Button variant="outlined" color="primary" style={{ marginTop: 16, width: '100%' }}>Wróć do strony głównej</Button>
            </Link>
          </Stack>
        </Paper>
    </div>
  );
});

export default OrderCompletion
