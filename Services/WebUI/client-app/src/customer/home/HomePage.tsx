import { Button, Stack } from '@mui/material'
import React from 'react'
import './homeStyles.css'
import { Link } from 'react-router-dom'

function HomePage() {
  return (
    <div className='homeContainer' >
      <div className='homeStack'>
        <Stack
        maxWidth={'sm'}
        height={'100vh'}
        spacing={4}
        justifyContent={'start'} 
        alignItems={'center'} 
        direction={'column'}>
          <img src='assets/logoimg.png' alt='Logo Domino' width={'70%'} style={{padding: '50px'}}/>
          <Link to={'produkty'} style={{width: '55%'}}>
            <Button style={{backgroundColor: '#C32B28', width: '100%', borderRadius: '20px'}} 
            size='large' variant='contained'>
              Nasze Produkty
            </Button>
          </Link>
          <Button style={{backgroundColor: '#C32B28', width: '55%', borderRadius: '20px'}} size='large' variant='contained'>O nas</Button>
          <Button style={{backgroundColor: '#C32B28', width: '55%', borderRadius: '20px'}} size='large' variant='contained'>Dla firm</Button>
          <Button style={{backgroundColor: '#C32B28', width: '55%', borderRadius: '20px'}} size='large' variant='contained'>Kontakt</Button>
        </Stack>
      </div>
    </div>
    //TODO Fix the router
  )
}

export default HomePage