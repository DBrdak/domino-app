import React, { useState } from 'react';
import { FormControl, FormControlLabel, Switch, useMediaQuery, IconButton, Stack } from '@mui/material';
import theme from '../../global/layout/theme';
import MyTextInput from '../components/MyTextInput';
import { Form, Formik } from 'formik';
import * as Yup from 'yup';
import { observer } from 'mobx-react-lite';
import { AddShoppingCart } from '@mui/icons-material';
import { useStore } from '../../global/stores/store';

interface Props {
  isPcsAllowed: boolean
}

function QuantityInput({isPcsAllowed}: Props) {
  const [quantity, setQuantity] = useState(0);
  const [unit, setUnit] = useState('kg');
  const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
  const {shoppingCartStore, catalogStore} = useStore()

  const handleQuantityChange = (newQuantity:number) => {
    if (newQuantity >= 0 && (unit === 'kg' || unit === 'szt')) {
      shoppingCartStore.setQuantity(quantity, unit)
      catalogStore.resetQuantityMode()
    }
  };

  const handleSwitchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if(event.target.checked) setUnit('szt');
    else setUnit('kg')
  };

  return (
    <FormControl component="fieldset" style={{padding: '10px', width: '100%'}}>
    <div style={{ display: 'flex', alignItems: 'center' }}>
      {isMobile ?
      <>
        <Formik
        validationSchema={Yup.object({quantity: Yup.number().required('Proszę podać ilość').moreThan(0,"Podaj ilość większą niż 0")})}
        initialValues={{quantity: 0}} 
        onSubmit={values => handleQuantityChange(values.quantity)} 
        validateOnMount={true}>
          {({ handleSubmit, isValid}) => (
          <Form className='ui form' onSubmit={handleSubmit} autoComplete='off' >
            <Stack direction={'row'}>
              <MyTextInput placeholder='Ilość' name='quantity'/>
              <IconButton type='submit' style={{borderRadius: '6px'}} color='primary' disabled={!isValid}>
                <AddShoppingCart />
              </IconButton>
            </Stack>
          </Form>
          )}
        </Formik>
        <FormControlLabel
        control={
          <Switch 
            checked={unit === 'szt'} 
            onChange={handleSwitchChange} 
            name="checked" 
            color="primary"
            disabled={!isPcsAllowed}
          />
        }
        label={unit}
        />
      </>
      :
      <Stack direction={'column'}>
        <Formik
        validationSchema={Yup.object({quantity: Yup.number().required('Proszę podać ilość').moreThan(0,"Podaj ilość większą niż 0")})}
        initialValues={{quantity: quantity}} 
        onSubmit={values => handleQuantityChange(values.quantity)} 
        validateOnMount={true}>
          {({ handleSubmit, isValid}) => (
          <Form className='ui form' onSubmit={handleSubmit} autoComplete='off' >
            <Stack direction={'row'}>
              <MyTextInput placeholder='Ilość' name='quantity'/>
              <IconButton type='submit' style={{borderRadius: '6px'}} color='primary' disabled={!isValid}>
                <AddShoppingCart />
              </IconButton>
            </Stack>
          </Form>
          )}
        </Formik>
        <FormControlLabel
        style={{width:'100%', display: 'flex', justifyContent:'center'}}
        control={
          <Switch 
            checked={unit === 'szt'} 
            onChange={handleSwitchChange} 
            name="checked" 
            color="primary"
            disabled={!isPcsAllowed}
          />
        }
        label={unit}
        />
      </Stack>
      }
    </div>
  </FormControl>
  );
}

export default observer(QuantityInput);