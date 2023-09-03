import React from 'react'
import SideNavbar from '../components/SideNavbar/SideNavbar'
import { Container, Grid, Paper, Stack, Typography } from '@mui/material'

function AdminMain() {
  return (
    <Stack direction={'row'}>
      <SideNavbar />
      <div style={{width: '100%', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
        <Paper style={{padding: '40px'}}>
          <Typography variant='h3' style={{textAlign: 'center'}}>
            Funkcja dostępna wkrótce
          </Typography>
        </Paper>
      </div>
    </Stack>
  )
}

export default AdminMain