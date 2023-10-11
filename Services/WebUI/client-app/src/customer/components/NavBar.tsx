import React, { useState } from 'react';
import { AppBar, Toolbar, IconButton, Menu, MenuItem, useMediaQuery, Stack, MenuList } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import '../../components/componentStyles.css'
import {Link, useLocation, useNavigate} from 'react-router-dom';

const Navbar = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const isMobile = useMediaQuery((theme: any) => theme.breakpoints.down('sm'));
  const currentLocation = useLocation()
  const navigate = useNavigate()

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
          <Stack direction={'row'} justifyContent={'space-between'} style={{padding: '10px'}}>
            {/* Mobile Menu */}
            <img src='/assets/logoimg.png' alt='Logo Domino' width={'16%'} onClick={() => navigate('/')} style={{cursor: 'pointer'}} />
            <IconButton color='primary' onClick={handleMenuOpen}>
              <MenuIcon />
            </IconButton>
            <Menu
              anchorEl={anchorEl}
              open={Boolean(anchorEl)}
              onClose={handleMenuClose}
            >
              <MenuList style={{width: '100vw', textAlign: 'center'}}>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose} disabled={currentLocation.pathname === '/produkty'}>
                  <Link replace to={'/produkty'} className='navbarLink'>Nasze produkty</Link>
                </MenuItem>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose} disabled={currentLocation.pathname === '/o-nas'}>
                  <Link replace to={'/o-nas'} className='navbarLink'>O nas</Link>
                </MenuItem>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose} disabled={currentLocation.pathname === '/dla-firm'}>
                  <Link replace to={'/dla-firm'} className='navbarLink'>Dla firm</Link>
                </MenuItem>
                <MenuItem className='navbarMenuItem' onClick={handleMenuClose} disabled={currentLocation.pathname === '/kontakt'}>
                  <Link replace to={'/kontakt'} className='navbarLink'>Kontakt</Link>
                </MenuItem>
              </MenuList>
            </Menu>
          </Stack>
        ) : (
          <Stack direction={'row'} display={'flex'} justifyContent={'space-between'} alignItems={'center'} width={'100vw'}>
          {/* Desktop Navbar */}
            <Link replace className='navbarLink' to={'/produkty'}>
              <button className='navbarBtn' disabled={currentLocation.pathname === '/produkty'}>Nasze produkty</button>
            </Link>
            <Link replace className='navbarLink' to={'/o-nas'}>
              <button className='navbarBtn' disabled={currentLocation.pathname === '/o-nas'}>O nas</button>
            </Link>
            <img src='/assets/logoimg.png' alt='Logo Domino' width={'7%'} style={{margin: '10px 0px 10px 0px', cursor: 'pointer'}}
                 onClick={() => navigate('/')} />
            <Link replace className='navbarLink' to={'/dla-firm'}>
              <button className='navbarBtn' disabled={currentLocation.pathname === '/dla-firm'}>Dla firm</button>
            </Link>
            <Link replace className='navbarLink' to={'/kontakt'}>
              <button className='navbarBtn' disabled={currentLocation.pathname === '/kontakt'}>Kontakt</button>
            </Link>
          </Stack>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default Navbar