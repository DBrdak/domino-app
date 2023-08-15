import { Box, Button, Stack, Typography } from '@mui/material'
import { observer } from 'mobx-react-lite'
import { title } from 'process'
import React from 'react'
import theme from '../global/layout/theme'
import { useStore } from '../global/stores/store'

interface Props {
  text: string
  onConfirm: () => void
  reversed?: boolean
  important?: boolean
}

function ConfirmModal({text, important, reversed, onConfirm}: Props) {
  const isMobile = theme.breakpoints.down('md')
  const {modalStore} = useStore()

  return (
    <Stack textAlign="center" width={'100%'} direction={'column'} spacing={4}>
      <Typography variant="h5" gutterBottom>
        {text}
      </Typography>
      {important && <Typography variant='body2'>Ta operacja jest nieodwracalna</Typography>}
      <Stack direction={'column'} spacing={2} justifyContent={'center'} width={'100%'}>
        <Button variant={reversed ? "outlined" : 'contained'} color="primary" onClick={onConfirm} >
          Tak
        </Button>
        <Button variant={reversed ? "contained" : 'outlined'} color="primary" onClick={() => modalStore.closeModal()} >
          Nie
        </Button>
      </Stack>
    </Stack>
  )
}

export default ConfirmModal