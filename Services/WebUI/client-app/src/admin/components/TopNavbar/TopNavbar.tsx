import { AppBar, Toolbar, Stack } from '@mui/material'
import React from 'react'
import { Link, useNavigate } from 'react-router-dom'
import TopNavbarBtnList from '../topNavbar/TopNavbarBtnList'

function TopNavbar() {
  const navigate = useNavigate()

  return (
    <AppBar position="static" style={{backgroundColor: '#FFFFFF', minHeight: '12vh', padding: '10px'}}>
      <Toolbar style={{height: '100%'}}>
        <TopNavbarBtnList />
      </Toolbar>
    </AppBar>
  )
}

export default TopNavbar