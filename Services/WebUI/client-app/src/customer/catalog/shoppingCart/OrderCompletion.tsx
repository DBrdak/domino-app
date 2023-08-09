import React, { useState, useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Paper, CircularProgress, Typography, AppBar, Toolbar, Container, Stack, Button } from "@mui/material";
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import './shoppingCartStyles.css'
import { Link } from "react-router-dom";

const OrderCompletion: React.FC = observer(() => {
  const [isLoading, setIsLoading] = useState(true);
  const [isCompleted, setIsCompleted] = useState(false);

  useEffect(() => {
    setTimeout(() => {
      setIsLoading(false);
      setIsCompleted(true);
    }, 1000);
  }, []);

  if (isLoading) {
    return (
      <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
        <Paper style={{ padding: 24, margin: 'auto', maxWidth: 500, textAlign: 'center' }}>
          <CircularProgress />
        </Paper>
      </div>
    );
  }

  return (
    <div style={{ display: 'flex', height: '100vh', justifyContent: 'center', alignItems: 'center' }}>
        <Paper style={{ padding: 50, margin: 'auto', maxWidth: 750, textAlign: 'center' }}>
          <AppBar position="static" style={{ marginBottom: '20px' }}>
            <Toolbar>
              <Typography textAlign={'center'} width={'100%'} variant="h5">
                Dane Zamówienia
              </Typography>
            </Toolbar>
          </AppBar>
          <Typography variant="subtitle1">First Name: Jan</Typography>
          <Typography variant="subtitle1">Last Name: Kowal</Typography>
          <Typography variant="subtitle1">Phone Number: 555666777</Typography>
          <Typography variant="subtitle1">Delivery Location: Załuski</Typography>
          <Typography variant="subtitle1">Delivery Time: 11.20</Typography>
          <Typography variant="subtitle1">Order ID: AA298DNA21</Typography>
          {isCompleted && (
            <CheckCircleOutlineIcon 
            style={{ 
              color: 'green', 
              fontSize: 60, 
              animation: 'scaleAndSpin 0.5s forwards, pulse 1.5s forwards',
              animationFillMode: 'forwards',
              borderRadius: '50%',
              backgroundColor: 'rgba(0, 255, 0, 0.1)',
              marginTop: 20
            }} 
          />
          )}
          <Stack width={'100%'} direction={'column'} style={{ marginTop: 20}}>
            <Link to={'/zamówienie'} >
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

export default OrderCompletion;
