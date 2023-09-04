import React from 'react';
import { Button, Box, Typography, Divider } from '@mui/material';
import { styled } from '@mui/material/styles';
import { Logout } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import SideNavbarBtnList from './SideNavbarBtnList';

const SideNavbar: React.FC = () => {
  const navigate = useNavigate()
  return (
    <Box
    sx={{
      height: '100vh',
      width: '300px',  
      backgroundColor: '#5d5d5d',
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'space-between',
      padding: '20px',
      margin: '0px',
      overflowY: 'auto',
      scrollbarWidth: 'none',
      '&::-webkit-scrollbar': {
        display: 'none',
      },
      msOverflowStyle: 'none'
    }}
  >
      <img src='/assets/logoimg.png' alt='Logo Domino' width={'70%'} style={{padding: '15px'}}/>
      <Divider style={{ width: '80%', marginBottom: '10px' }} />
      <SideNavbarBtnList />
      <Divider style={{ width: '80%', marginBottom: '10px' }} />
      <Box>
        <Button variant="contained" 
        color="secondary" 
        fullWidth
        style={{gap:'20px'}}>
          <Logout />
          <Typography>
            Wyloguj się
          </Typography>
        </Button>
      </Box>
    </Box>
  );
};

export default SideNavbar;