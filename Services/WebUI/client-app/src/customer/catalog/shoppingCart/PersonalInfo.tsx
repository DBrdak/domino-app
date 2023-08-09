import { Paper, Typography, Grid, TextField, Button, AppBar, Toolbar, Stack } from "@mui/material";
import React from "react";
import { Link } from "react-router-dom";

const PersonalInfo: React.FC = () => {
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
        <Grid container spacing={2}>
          <Grid item xs={6}>
            <TextField fullWidth label="Imię" />
          </Grid>
          <Grid item xs={6}>
            <TextField fullWidth label="Nazwisko" />
          </Grid>
          <Grid item xs={12}>
            <TextField fullWidth label="Numer telefonu" />
          </Grid>
          <Grid item xs={12}>
            <Stack width={'100%'} justifyContent={'space-between'} direction={'row'}>
              <Link to={'/koszyk'} >
                <Button variant="outlined" color="primary" style={{ marginTop: 16 }}>Wróć</Button>
              </Link>
              <Link to={'/koszyk/dane-wysyłki'} >
                <Button variant="contained" color="primary" style={{ marginTop: 16 }}>Dalej</Button>
              </Link>
            </Stack>
          </Grid>
        </Grid>
      </Paper>
    </div>
  );
};

export default PersonalInfo;