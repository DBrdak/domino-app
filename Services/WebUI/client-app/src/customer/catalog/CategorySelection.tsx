import { Paper, Stack, Typography, Button } from '@mui/material'
import React from 'react'
import { Link } from 'react-router-dom'
import NavBar from '../components/NavBar'
import { useStore } from '../../global/stores/store'
import { observer } from 'mobx-react-lite'

function CategorySelection() {

  return (
    <div style={{backgroundColor: '#E4E4E4', minHeight:'100vh'}}>
      <NavBar />
      <div style={{height: '75vh', width:'100%', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
        <Paper style={{padding: '40px'}} >
          <Stack direction={'column'} width={'100%'}>
            <Typography variant='h4' marginBottom={2}>Wybierz kategorię</Typography>
            <Stack direction={'row'} spacing={4} style={{display: 'flex', justifyContent: 'center', width: '100%'}}>
              <Link to={'mięso'}><Button variant='contained'>Mięso</Button></Link>
              <Link to={'wędliny'}><Button variant='contained'>Wędliny</Button></Link>
            </Stack>
          </Stack>
        </Paper>
      </div>
    </div>
  )
}

export default observer(CategorySelection)