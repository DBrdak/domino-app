import { Button, Typography } from '@mui/material'
import React from 'react'
import { useNavigate } from 'react-router-dom'

interface Props {
  content: string
}

function SideNavbarBtn(props: Props) {
  const navigate = useNavigate()
  return (
      <Button
      variant="contained"
      fullWidth
      style={{ marginBottom: '10px', padding: '7px' }}
      onClick={() => navigate(props.content.replace(' ', '-'))}
    >
      <Typography>
        {props.content}
      </Typography>
    </Button>
  )
}

export default SideNavbarBtn