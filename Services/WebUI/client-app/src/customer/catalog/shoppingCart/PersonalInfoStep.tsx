import { Paper, Typography, Grid, TextField, Button, AppBar, Toolbar, Stack } from "@mui/material";
import { Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Link, useBeforeUnload, useNavigate } from "react-router-dom";
import * as Yup from 'yup';
import { PersonalInfo } from "../../../global/models/common";
import { useStore } from "../../../global/stores/store";
import { observer } from "mobx-react-lite";
import MyTextInput from "../../../components/MyTextInput";
import { usePreventNavigation } from "../../../global/router/routeProtection";

//TODO Ustawić na API walidację numeru telefonu

const PersonalInfoStep = () => {
  const {shoppingCartStore} = useStore()
  const [initValues, setInitValues] = useState<PersonalInfo>(shoppingCartStore.personalInfo || {firstName: '', lastName: '', phoneNumber: ''})
  const navigate = useNavigate();

  usePreventNavigation([shoppingCartStore.shoppingCart], '/koszyk')

  const validationSchema = Yup.object({
    firstName: Yup.string()
      .required("Proszę podać imię")
      .max(24, "Imię może mieć maksymalnie 24 litery")
      .matches(/^[^\d]*$/, "Imię nie może zawierać cyfr"),
  
    lastName: Yup.string()
      .required("Proszę podać nazwisko")
      .max(32, "Nazwisko może mieć maksymalnie 32 litery")
      .matches(/^[^\d]*$/, "Nazwisko nie może zawierać cyfr"),
  
    phoneNumber: Yup.string()
      .required("Proszę podać numer telefonu")
      .length(9, "Proszę podać prawidłowy numer telefonu")
      .matches(/^\d+$/, "Numer telefonu nie może zawierać liter")
  });


  function handleFormSubmit(personalInfo: PersonalInfo) {
    shoppingCartStore.setPersonalInfo(personalInfo)
    navigate('/koszyk/dane-wysyłki');
  }

  return (
    <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
      <Paper style={{ padding: 50, margin: 'auto', maxWidth: 500 }}>
        <AppBar position="static" style={{ marginBottom: '20px' }}>
          <Toolbar>
            <Typography textAlign={'center'} width={'100%'} variant="h5">
              Dane Osobowe
            </Typography>
          </Toolbar>
        </AppBar>
        { initValues &&
        <Formik
          validationSchema={validationSchema}
          initialValues={initValues!} 
          onSubmit={values => handleFormSubmit(values)}
          validateOnMount>
            {({ handleSubmit, isValid}) => (
              <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                <Grid container spacing={2}>
                  <Grid item xs={6}>
                    <MyTextInput name='firstName' placeholder="Imię" showErrors/>
                  </Grid>
                  <Grid item xs={6}>
                    <MyTextInput name='lastName' placeholder="Nazwisko" showErrors />
                  </Grid>
                  <Grid item xs={12}>
                    <MyTextInput name='phoneNumber' placeholder="Numer telefonu" showErrors />
                  </Grid>
                  <Grid item xs={12}>
                    <Stack width={'100%'} justifyContent={'space-between'} direction={'row'}>
                      <Link to={'/koszyk'} >
                        <Button type='reset' variant="outlined" color="primary" style={{ marginTop: 16 }}>Wróć</Button>
                      </Link>
                      <Button type="submit" variant="contained" color="primary" 
                      style={{ marginTop: 16 }} disabled={!isValid}>
                        Dalej
                        </Button>
                    </Stack>
                  </Grid>
                </Grid>
              </Form>
            )}
        </Formik>}
      </Paper>
    </div>
  );
};

export default observer(PersonalInfoStep);