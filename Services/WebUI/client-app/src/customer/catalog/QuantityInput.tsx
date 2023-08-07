import React, { useState } from 'react';
import { FormControl, FormControlLabel, Switch, useMediaQuery, IconButton, Stack } from '@mui/material';
import theme from '../../global/layout/theme';
import MyTextInput from '../components/MyTextInput';
import { Form, Formik } from 'formik';
import * as Yup from 'yup';
import { observer } from 'mobx-react-lite';
import { AddShoppingCart, CancelOutlined, Undo } from '@mui/icons-material';
import { useStore } from '../../global/stores/store';

interface Props {
  isPcsAllowed: boolean
  onInputSubmit: () => void
}

function QuantityInput({isPcsAllowed, onInputSubmit}: Props) {
  const [unit, setUnit] = useState('kg');
  const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
  const {shoppingCartStore, catalogStore} = useStore()

  const handleQuantityChange = (newQuantity:number) => {
    if (newQuantity && newQuantity >= 0 && (unit === 'kg' || unit === 'szt')) {
      shoppingCartStore.setQuantity(newQuantity, unit)
      onInputSubmit()
    }
  };

  const handleSwitchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if(event.target.checked) setUnit('szt');
    else setUnit('kg')
  };

  return (
    <FormControl component="fieldset" style={{padding: '10px', width: '100%'}}>
    <div style={{ display: 'flex', alignItems: 'center', width:'100%'}}>
        <Formik
        validationSchema={Yup.object({quantity: Yup.number().required('Proszę podać ilość').moreThan(0,"Podaj ilość większą niż 0")})}
        initialValues={{quantity: ''}} 
        onSubmit={values => handleQuantityChange(Number(values.quantity))}
        validateOnMount={true}>
          {({ handleSubmit, isValid}) => (
          <Form style={{width:'100%'}} onSubmit={handleSubmit} autoComplete='off' >
            <Stack direction={'column'} >
              <Stack direction={'row'} width={'100%'} justifyContent={'center'}>
                <MyTextInput placeholder='Ilość' name='quantity'/>
                <FormControlLabel
                style={{marginLeft: '5%'}}
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
              <Stack direction={'row'} marginTop={'10px'}>
                <IconButton type='submit' style={{width: '50%', borderRadius: '6px'}} color='primary' disabled={!isValid}>
                  <AddShoppingCart />
                </IconButton>
                <IconButton color='primary' style={{width: '50%', borderRadius: '6px'}}
                onClick={() => catalogStore.resetQuantityMode()}>
                  <Undo />
                </IconButton>
              </Stack>
            </Stack>
          </Form>
          )}
        </Formik>
    </div>
  </FormControl>
  );
}

export default observer(QuantityInput);