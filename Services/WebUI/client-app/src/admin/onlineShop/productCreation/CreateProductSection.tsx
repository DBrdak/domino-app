import { Add, AddCircleOutlined, AddOutlined } from '@mui/icons-material'
import { Box, Button, IconButton } from '@mui/material'
import React from 'react'
import { useStore } from '../../../global/stores/store'
import CreateProductModal from './CreateProductModal'
import { observer } from 'mobx-react-lite'

function CreateProductSection() {
  const {modalStore} = useStore()

  return (
    <Box display={'flex'} justifyContent={'right'}>
      <Button color='secondary' style={{display: 'flex', gap: '10px', backgroundColor: '#C32B28',
        justifyContent: 'center', alignItems: 'center'}} onClick={() => modalStore.openModal(<CreateProductModal />)}>
        <AddCircleOutlined fontSize='medium' />
        Dodaj nowy produkt
      </Button>
    </Box>
  )
}

export default observer(CreateProductSection)