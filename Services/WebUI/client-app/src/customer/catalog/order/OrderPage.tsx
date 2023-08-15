import React, { useEffect } from 'react';
import { Paper, Typography, Box, List, ListItem, Divider, ListSubheader, TableContainer, Table, TableBody, TableCell, TableHead, TableRow, Button, Stack, useMediaQuery } from '@mui/material';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../global/stores/store';
import { usePreventNavigation } from '../../../global/router/routeProtection';
import DateTimeRangeDisplay from '../../../components/DateTimeRangeDisplay';
import theme from '../../../global/layout/theme';
import OrderListItem from './OrderTableItem';
import { isTemplateSpan } from 'typescript';
import OrderTableItem from './OrderTableItem';
import { Place } from '@mui/icons-material';
import { useLocation, useNavigate } from 'react-router-dom';
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
      if(!orderStore.order) {
        <NotFoundPage text='Nie znaleźliśmy podanego zamówienia' />
      }
      navigate(`/zamówienie/${order?.orderId}`)
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
    switch (order?.status) {
      case 'Odrzucone':
        return '#C32B28'
      case 'Anulowane':
        return '#B2B2B2'
      case 'Oczekuje na potwierdzenie':
        return '#EAAA11'
      case 'Potwierdzone':
        return '#50CD1F'
      case 'Potwierdzone częściowo':
        return '#1343E5'
    }
  }

  return (order &&
    (isMobile ?
    <Box height="100vh">
      <Paper style={{ padding: '20px', minWidth: '40%', textAlign: 'center' }}>  
        <Stack direction={'column'} spacing={3}>
          {<Typography variant='h4' color={'white'} style={{backgroundColor: getBgColor(), padding: '10px', borderRadius: '25px'}}>
            Zamówienie {order.status}
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
              <Typography>~ {Number(order.totalPrice.toFixed(1))} {order.currency}</Typography>
              <Typography><strong>Numer zamówienia</strong></Typography>
              <Typography>{order.orderId}</Typography>
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
              <Typography>{order.status}</Typography>
            </Box>
          </Stack>

          <Box>
            <Paper style={{padding:'5px', boxShadow: '0px 0px 5px #BDBDBD'}}>
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
                  <TableCell align="center"><strong>Status</strong></TableCell>
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
            <Button variant={'contained'} onClick={() => handleOrderCancel()} disabled={order.isCanceled!}>
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
            Zamówienie {order.status}
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
              <Typography>~ {Number(order.totalPrice.toFixed(1))} {order.currency}</Typography>
              <Typography><strong>Numer zamówienia</strong></Typography>
              <Typography>{order.orderId}</Typography>
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
              <Typography>{order.status}</Typography>
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
                  <TableCell align="center"><strong>Status</strong></TableCell>
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
            <Button variant={'contained'} onClick={() => handleOrderCancel()} disabled={order.isCanceled!}>
              <Typography>Anuluj zamówienie</Typography>
            </Button>
          </Stack>
        </Stack>
      </Paper>
    </Box>)
  );
};

export default observer(OrderPage);
