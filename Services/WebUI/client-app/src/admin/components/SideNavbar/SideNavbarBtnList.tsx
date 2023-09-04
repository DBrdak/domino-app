import React from 'react'
import { Box } from '@mui/material'
import SideNavbarBtn from './SideNavbarBtn'

function SideNavbarBtnList() {
  return (
    <Box>
      <SideNavbarBtn content='sklep online'/>
      <SideNavbarBtn content='sprzedaż' path={'sprzedaz'}/>
      <SideNavbarBtn content='cenniki'/>
      <SideNavbarBtn content='sklepy'/>
      <SideNavbarBtn content='paliwo'/>
      <SideNavbarBtn content='flota'/>
      <SideNavbarBtn content='kontrahenci'/>
      <SideNavbarBtn content='masarnia'/>
      <SideNavbarBtn content='statystyki'/>
      <SideNavbarBtn content='kalkulatory'/>
    </Box>
  )
}

export default SideNavbarBtnList