import React, { useState } from 'react';
import { AppBar, Toolbar, IconButton, Typography, Menu, MenuItem, useMediaQuery, Button, Container, Stack, MenuList, Divider } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import theme from '../../global/layout/theme';
import './componentStyles.css'

const Navbar = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const isMobile = useMediaQuery((theme: any) => theme.breakpoints.down('sm'));

  const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };
  
  return (
    <AppBar position="static" style={{backgroundColor: '#FFFFFF'}}>
      <Toolbar>
        {isMobile ? (
          <Stack direction={'row'} justifyContent={'space-between'}>
            {/* Mobile Menu */}
            <img src='assets/logoimg.png' alt='Logo Domino' width={'16%'} />
            <IconButton color='primary' onClick={handleMenuOpen}>
              <MenuIcon />
            </IconButton>
            <Menu
              anchorEl={anchorEl}
              open={Boolean(anchorEl)}
              onClose={handleMenuClose}
            >
              <MenuList style={{width: '100vw', textAlign: 'center'}}>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose}>Nasze produkty</MenuItem>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose}>O nas</MenuItem>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose}>Dla firm</MenuItem>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose}>Kontakt</MenuItem>
              </MenuList>
            </Menu>
          </Stack>
        ) : (
          <Stack direction={'row'} display={'flex'} justifyContent={'space-between'} alignItems={'center'} width={'100vw'}>
          {/* Desktop Navbar */}
            <button className='navbarBtn' >Nasze produkty</button>
            <button className='navbarBtn'  >O nas</button>
            <img src='assets/logoimg.png' alt='Logo Domino' width={'7%'} style={{margin: '10px 0px 10px 0px'}} />
            <button className='navbarBtn'  >Dla firm</button>
            <button className='navbarBtn'  >Kontakt</button>
          </Stack>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default Navbar