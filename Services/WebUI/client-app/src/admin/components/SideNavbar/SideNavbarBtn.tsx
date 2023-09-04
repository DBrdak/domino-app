import { Button, Typography } from '@mui/material'
import React from 'react'
import { useNavigate } from 'react-router-dom'

interface Props {
  path?: string | null
  content: string
}

function SideNavbarBtn(props: Props) {
  const navigate = useNavigate()
  return (
    <Button
    variant="contained"
    fullWidth
    style={{ marginBottom: '10px', padding: '7px' }}
    onClick={() => navigate(`/admin/${props.path ? props.path : props.content.replace(' ', '-')}`)}
    >
      <Typography>
        {props.content}
      </Typography>
    </Button>
  )
}

export default SideNavbarBtn