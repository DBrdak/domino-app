import React, { useEffect } from 'react'
import Navbar from '../components/NavBar'
import { Paper, Typography } from '@mui/material'
import { setPageTitle } from '../../global/utils/pageTitle'

function ContractorsMain() {
  useEffect(() => {
    setPageTitle('Dla firm')
  }, [])

  return (
    <div>
      <Navbar />
      <div style={{height: '75vh ', width:'100%', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
        <Paper style={{padding: '40px'}}>
          <Typography variant='h3' style={{textAlign: 'center'}}>
            Funkcja dostępna wkrótce
          </Typography>
        </Paper>
      </div>
    </div>
  )
}

export default ContractorsMain