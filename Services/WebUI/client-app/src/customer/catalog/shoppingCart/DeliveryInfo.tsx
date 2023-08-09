import { Paper, Typography, Button, Stack, AppBar, Toolbar } from "@mui/material";
import React from "react";
import { Link } from "react-router-dom";

const DeliveryInfo: React.FC = () => {
  return (
    <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
      <Paper style={{ padding: 50, margin: 'auto', maxWidth: 500 }}>
        <AppBar position="static" style={{ marginBottom: '20px' }}>
          <Toolbar>
            <Typography textAlign={'center'} width={'100%'} variant="h5">
              Dane Wysyłki
            </Typography>
          </Toolbar>
        </AppBar>
        <div style={{ height: 300, width:350, backgroundColor: '#e0e0e0', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
          <Typography>Map Placeholder</Typography>
        </div>
        <Stack width={'100%'} justifyContent={'space-between'} direction={'row'}>
          <Link to={'/koszyk/dane-osobowe'} >
            <Button variant="outlined" color="primary" style={{ marginTop: 16 }}>Wróć</Button>
          </Link>
          <Link to={'/koszyk/zamówienie'} >
            <Button variant="contained" color="primary" style={{ marginTop: 16 }}>Dalej</Button>
          </Link>
        </Stack>
      </Paper>
    </div>
  );
};

export default DeliveryInfo;
