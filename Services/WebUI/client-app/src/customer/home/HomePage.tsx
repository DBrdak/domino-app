import { Button, Stack } from '@mui/material'
import React from 'react'
import './homeStyles.css'
import { Link } from 'react-router-dom'
import { observer } from 'mobx-react-lite'
import { useStore } from '../../global/stores/store'
import OrderCredsForm from '../catalog/order/OrderCredsForm'

function HomePage() {
  const {modalStore} = useStore()
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
            <Button style={{backgroundColor: '#C32B28', width: '55%', borderRadius: '20px'}} 
            size='large' variant='contained' onClick={() => modalStore.openModal(<OrderCredsForm />)}>
              Twoje Zamówienia
            </Button>
          <Link to={'o-nas'} style={{width: '55%'}}>
            <Button style={{backgroundColor: '#C32B28', width: '100%', borderRadius: '20px'}} 
            size='large' variant='contained'>
              O nas
            </Button>
          </Link>
          <Link to={'dla-firm'} style={{width: '55%'}}>
            <Button style={{backgroundColor: '#C32B28', width: '100%', borderRadius: '20px'}} size='large' variant='contained'>
              Dla firm
              </Button>
          </Link>
          <Link to={'kontakt'} style={{width: '55%'}}>
            <Button style={{backgroundColor: '#C32B28', width: '100%', borderRadius: '20px'}} size='large' variant='contained'>
              Kontakt
            </Button>
          </Link>
        </Stack>
      </div>
    </div>
  )
}

export default observer(HomePage)