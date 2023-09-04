import { Button, Typography } from '@mui/material'
import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useStore } from '../../../global/stores/store'
import { observer } from 'mobx-react-lite'

interface Props {
  text: string
  content: JSX.Element
}

function TopNavbarBtn(props: Props) {
  const {adminLayoutStore} = useStore()
  return (
      <Button
      variant="outlined"
      style={{ padding: '15px 30px', alignItems: 'center' }}
      onClick={() => adminLayoutStore.setSection(props.content)}
    >
      <Typography>
        {props.text}
      </Typography>
    </Button>
  )
}

export default observer(TopNavbarBtn)