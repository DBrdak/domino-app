import React from 'react';
import * as Yup from 'yup';
import { Button, Container, Typography, Paper } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const LoginPage: React.FC = () => {
  const navigate = useNavigate()
  return (
    <Container style={{display:'flex', justifyContent: 'center', alignItems: 'center', width: '100vw', height: '100vh'}}>
      <Paper style={{padding:'20px'}}>
        <Typography textAlign={'center'}
        variant='h5'
        paddingBottom={'15px'} >
          AUTORYZACJA
        </Typography>
        <Button
        variant='contained'
        onClick={() => navigate('główna')}>
          Przejdź do strony admina
        </Button>
      </Paper>
    </Container>
  );
};

export default LoginPage;
