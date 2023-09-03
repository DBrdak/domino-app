import React, { useEffect } from 'react';
import { Paper, Typography, Box, Divider, Table, TableBody, TableCell, TableHead, TableRow, Button, Stack, useMediaQuery } from '@mui/material';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../global/stores/store';
import DateTimeRangeDisplay from '../../../components/DateTimeRangeDisplay';
import theme from '../../../global/layout/theme';
import OrderTableItem from './OrderTableItem';
import { Place } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import ConfirmModal from '../../../components/ConfirmModal';
import LoadingComponent from '../../../components/LoadingComponent';
import NotFoundPage from '../../../components/NotFoundPage';
import { setPageTitle } from '../../../global/utils/pageTitle';

const OrderPage: React.FC = () => {
  const { orderStore, modalStore } = useStore();
  const {order} = orderStore;
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))
  const navigate = useNavigate()

  useEffect(() => {
    const getOrder = async () => {
      await orderStore.loadOrder()
      console.log(orderStore.order)
      if(!orderStore.order) {
        <NotFoundPage text='Nie znaleźliśmy podanego zamówienia' />
      }
      navigate(`/zamówienie/${order?.id}`)
    }
    getOrder()
    setPageTitle('Zamówienie')
  }, [])

  if(orderStore.loading) {
    return <LoadingComponent text='Wczytywanie zamówienia...' fullScreen/>
  }

  function handleOrderCancel(): void {
    modalStore.openModal(<ConfirmModal 
      text={'Czy na pewno chcesz anulować zamówienie?'} reversed
      important onConfirm={() => {
        orderStore.cancelOrder()
        modalStore.closeModal()
      }} />)
  }

  function getBgColor(): import("csstype").Property.BackgroundColor | undefined {
    switch (order?.status?.statusMessage) {
      case 'Potwierdź kod SMS':
        return '#C32B28'
      case 'Oczekuje na potwierdzenie':
        return '#B2B2B2'
      case 'Potwierdzone':
        return '#50CD1F'
      case 'Potwierdzone ze zmianami':
        return '#1343E5'
      case 'Anulowane':
        return '#1343E5'      
      case 'Odrzucone':
        return '#1343E5'
      case 'Odebrane':
        return '#1343E5'
      //TODO Pozmieniać kolorki + Potwierdzanie kodu gdy niepotwierdzone w miejscu wyświetlania zamówienia
    }
  }

  return (order &&
    (isMobile ?
    <Box height="100vh">
      <Paper style={{ padding: '20px', minWidth: '40%', textAlign: 'center' }}>  
        <Stack direction={'column'} spacing={4}>
          {<Typography variant='h4' color={'white'} style={{backgroundColor: getBgColor(), padding: '10px', borderRadius: '25px'}}>
            {order.status?.statusMessage !== "Potwierdź kod SMS" ?
             `Zamówienie ${order.status?.statusMessage}` :
             `${order.status?.statusMessage}`}
           </Typography>}
          <Box>
            <Typography variant="h5" align="center" gutterBottom>
              Dane osobowe
            </Typography>
            <Divider />
            <Typography variant='subtitle1'><strong>Imię</strong></Typography>
            <Typography variant='body1'>{order.firstName}</Typography>
            <Typography variant='subtitle1'><strong>Nazwisko</strong></Typography>
            <Typography variant='body1'>{order.lastName}</Typography>
            <Typography variant='subtitle1'><strong>Numer telefonu</strong></Typography>
            <Typography variant='body1'>{order.phoneNumber}</Typography>
          </Box>

          <Divider orientation='vertical' variant='middle' />

          <Box>
            <Typography variant="h5" align="center" gutterBottom>
              Dane zamówienia 
            </Typography>
            <Divider />
            <Typography><strong>Wartość zamówienia</strong></Typography>
            <Typography>~ {Number(order.totalPrice.amount.toFixed(1))} {order.totalPrice.currency.code}</Typography>
            <Typography><strong>Numer zamówienia</strong></Typography>
            <Typography>{order.id}</Typography>
            <Typography><strong>Data zamówienia</strong></Typography>
            <Typography>{new Date(order.createdDate!).toLocaleDateString('pl-PL')}</Typography>
            <Typography><strong>Miejsce odbioru</strong></Typography>
            <Button 
            href={`https://www.google.com/maps/search/?api=1&query=${order.deliveryLocation.latitude},${order.deliveryLocation.longitude}`} 
            target="_blank" 
            rel="noopener noreferrer"
            variant='outlined'
            style={{ textDecoration: 'none', color: 'inherit',
            justifyContent: 'center', verticalAlign:'center' }}
            >
              <Place color='primary' /> 
              <Typography variant='subtitle1' color={'primary'}>{order.deliveryLocation.name}</Typography>
            </Button>
            <Typography><strong>Czas odbioru</strong></Typography>
            <DateTimeRangeDisplay date={order.deliveryDate} />
            <Typography><strong>Status</strong></Typography>
            <Typography>{order.status?.statusMessage}</Typography>
          </Box>
          <Box>
            <Paper style={{padding:'15px', boxShadow: '0px 0px 5px #BDBDBD'}}>
              <Typography variant="h5" align="center" gutterBottom>
                Zamówione produkty
              </Typography>
              <Table>
              <TableHead>
                <TableRow>
                  <TableCell><strong>Nazwa produktu</strong></TableCell>
                  <TableCell align="center"><strong>Cena</strong></TableCell>
                  <TableCell align="center"><strong>Ilość</strong></TableCell>
                  <TableCell align="center"><strong>Wartość</strong></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {order.items.map(item => (
                  item.id && <OrderTableItem key={item.id} item={item} />
                ))}
              </TableBody>
            </Table>
            </Paper>
          </Box>
          <Stack direction={'row'} justifyContent={'space-between'}>
            <Button variant={'outlined'} onClick={() => navigate('/')}>
              <Typography>Wróć do strony głównej</Typography>
            </Button>
            <Button variant={'contained'} onClick={() => handleOrderCancel()} disabled={order.completionDate !== null}>
              <Typography>Anuluj zamówienie</Typography>
            </Button>
          </Stack>
        </Stack>
      </Paper>
    </Box>
    :
    <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
      <Paper style={{ padding: '40px', minWidth: '40%', textAlign: 'center' }}>  
        <Stack direction={'column'} spacing={3}>
          {<Typography variant='h4' color={'white'} style={{backgroundColor: getBgColor(), padding: '10px', borderRadius: '25px'}}>
              {order.status?.statusMessage !== "Potwierdź kod SMS" ?
              `Zamówienie ${order.status?.statusMessage}` :
              `${order.status?.statusMessage}`}
            </Typography>}
          <Stack direction={'row'} display={'flex'} justifyContent={'center'} spacing={4}
          alignItems={'center'} >
            <Box>
              <Typography variant="h5" align="center" gutterBottom>
                Dane osobowe
              </Typography>
              <Divider />
              <Typography variant='subtitle1'><strong>Imię</strong></Typography>
              <Typography variant='body1'>{order.firstName}</Typography>
              <Typography variant='subtitle1'><strong>Nazwisko</strong></Typography>
              <Typography variant='body1'>{order.lastName}</Typography>
              <Typography variant='subtitle1'><strong>Numer telefonu</strong></Typography>
              <Typography variant='body1'>{order.phoneNumber}</Typography>
            </Box>

            <Divider orientation='vertical' variant='middle' />

            <Box>
              <Typography variant="h5" align="center" gutterBottom>
                Dane zamówienia 
              </Typography>
              <Divider />
              <Typography><strong>Wartość zamówienia</strong></Typography>
              <Typography>~ {Number(order.totalPrice.amount.toFixed(1))} {order.totalPrice.currency.code}</Typography>
              <Typography><strong>Numer zamówienia</strong></Typography>
              <Typography>{order.id}</Typography>
              <Typography><strong>Data zamówienia</strong></Typography>
              <Typography>{new Date(order.createdDate!).toLocaleDateString('pl-PL')}</Typography>
              <Typography><strong>Miejsce odbioru</strong></Typography>
              <Button 
              href={`https://www.google.com/maps/search/?api=1&query=${order.deliveryLocation.latitude},${order.deliveryLocation.longitude}`} 
              target="_blank" 
              rel="noopener noreferrer"
              variant='outlined'
              style={{ textDecoration: 'none', color: 'inherit',
              justifyContent: 'center', verticalAlign:'center' }}
              >
                <Place color='primary' /> 
                <Typography variant='subtitle1' color={'primary'}>{order.deliveryLocation.name}</Typography>
              </Button>
              <Typography><strong>Czas odbioru</strong></Typography>
              <DateTimeRangeDisplay date={order.deliveryDate} />
              <Typography><strong>Status</strong></Typography>
              <Typography>{order.status?.statusMessage}</Typography>
            </Box>
          </Stack>

          <Box>
            <Paper style={{padding:'10px', boxShadow: '0px 0px 5px #BDBDBD'}}>
              <Typography variant="h5" align="center" gutterBottom>
                Zamówione produkty
              </Typography>
              <Table>
              <TableHead>
                <TableRow>
                  <TableCell><strong>Nazwa produktu</strong></TableCell>
                  <TableCell align="center"><strong>Cena</strong></TableCell>
                  <TableCell align="center"><strong>Ilość</strong></TableCell>
                  <TableCell align="center"><strong>Wartość</strong></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {order.items.map(item => (
                  item.id && <OrderTableItem key={item.id} item={item} />
                ))}
              </TableBody>
            </Table>
            </Paper>
          </Box>
          <Stack direction={'row'} justifyContent={'space-between'}>
            <Button variant={'outlined'} onClick={() => navigate('/')}>
              <Typography>Wróć do strony głównej</Typography>
            </Button>
            <Button variant={'contained'} onClick={() => handleOrderCancel()} disabled={order.completionDate !== null}>
              <Typography>Anuluj zamówienie</Typography>
            </Button>
          </Stack>
        </Stack>
      </Paper>
    </Box>)
  );
};

export default observer(OrderPage);
