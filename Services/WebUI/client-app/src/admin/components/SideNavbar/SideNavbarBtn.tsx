import { Button, Typography } from '@mui/material'
import React from 'react'
import { useNavigate } from 'react-router-dom'
import {useStore} from "../../../global/stores/store";
import {observer} from "mobx-react-lite";

interface Props {
  path?: string | null
  content: string
}

function SideNavbarBtn(props: Props) {
  const {adminLayoutStore} = useStore()
  const navigate = useNavigate()

  const handleChange = () => {
    navigate(`/admin/${props.path ? props.path : props.content.replace(' ', '-')}`)
    adminLayoutStore.clearSection()
  }

  return (
    <Button
    variant="contained"
    fullWidth
    style={{ marginBottom: '10px', padding: '7px' }}
    onClick={() => handleChange()}
    >
      <Typography>
        {props.content}
      </Typography>
    </Button>
  )
}

export default observer(SideNavbarBtn)