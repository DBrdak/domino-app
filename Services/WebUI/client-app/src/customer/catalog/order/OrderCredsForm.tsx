import React from 'react';
import { Button, Typography, Box, Stack } from '@mui/material';
import { Form, Formik } from 'formik';
import * as yup from 'yup';
import MyTextInput from '../../../components/MyTextInput';
import { useNavigate } from 'react-router-dom';
import { useStore } from '../../../global/stores/store';
import { observer } from 'mobx-react-lite';

const OrderCredsForm: React.FC = () => {
  const navigate = useNavigate()
  const {orderStore, modalStore} = useStore()

  const validationSchema = yup.object({
    phoneNumber: yup.string().required('Proszę podać numer telefonu'),
    orderId: yup.string().required('Proszę podać numer zamówienia'),
  });

  const handleFormSubmit = (phoneNumber: string, orderId: string) => {
    orderStore.setId(orderId)
    orderStore.setPhoneNumber(phoneNumber)
    modalStore.closeModal()
    localStorage.setItem('ord-ph-num', phoneNumber)
    localStorage.setItem('ord-id', orderId)
    navigate(`/zamówienie/${orderId}`)
  }

  function getInitOrderId(): string {
    if(localStorage.getItem('ord-id')) return localStorage.getItem('ord-id')!
    else return ''
  }

  function getInitPhoneNumber(): string {
    if(localStorage.getItem('ord-ph-num')) return localStorage.getItem('ord-ph-num')!
    else return ''
  }

  return (
    <Stack direction={'column'} spacing={3} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
      <Typography variant="h4" gutterBottom>
        Podaj dane zamówienia
      </Typography>
      <Formik
        validationSchema={validationSchema}
        initialValues={{phoneNumber: getInitPhoneNumber(), orderId: getInitOrderId()}} 
        onSubmit={values => handleFormSubmit(values.phoneNumber, values.orderId)}
        validateOnMount={true}>
          {({ handleSubmit, isValid}) => (
          <Form style={{width:'100%'}} onSubmit={handleSubmit} autoComplete='off' >
            <Stack direction={'column'} spacing={3} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
              <MyTextInput 
              placeholder={'Numer telefonu'} 
              name={'phoneNumber'} 
              type='number' 
              showErrors
              maxLength={9}
              />
              <MyTextInput 
              placeholder={'Numer zamówienia'} 
              name={'orderId'} 
              showErrors
              maxLength={8}
              capitalize
              />
              <Box mt={3}>
                <Button variant="contained" color="primary" type='submit' disabled={!isValid}>
                  Wyświetl Zamówienie
                </Button>
              </Box>
            </Stack>
          </Form>
          )}
        </Formik>
    </Stack>
  );
};

export default observer(OrderCredsForm);
