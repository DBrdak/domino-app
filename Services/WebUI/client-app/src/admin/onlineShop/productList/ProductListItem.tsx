import React from 'react'
import { Product } from '../../../global/models/product'
import { IconButton, ListItem, ListItemText, Stack, TableCell, TableRow, Typography } from '@mui/material'
import { Block, Discount, DiscountTwoTone, Done, Edit, EditTwoTone, Https, NoEncryption, RemoveShoppingCart } from '@mui/icons-material'
import { useStore } from '../../../global/stores/store'
import { observer } from 'mobx-react-lite'
import ConfirmModal from '../../../components/ConfirmModal'
import ProductEditModal from './ProductEditModal'

interface Props {
  product: Product
}

function ProductListItem({product}: Props) {
  const {modalStore} = useStore()

  const handleProductUnlock = () => {
    console.log('Hello')
  }

  const handleProductLock = () => {
    console.log('Hello')
  }

  function handleProductEdit(): void {
    console.log('Hello')
  }

  function handleProductDelete(): void {
    console.log('Hello')
  }

  return (
    <TableRow>
      <TableCell style={{textAlign: 'center'}}>
        <Typography variant='h5'>{product.name}</Typography>
      </TableCell>
      <TableCell>
        <Stack direction={'row'} style={{display: 'flex', justifyContent: 'space-around'}} >
          <IconButton style={{borderRadius: '0px', color: '#000000', flexDirection:'column', width: '50%'}} size='medium' 
          onClick={() => modalStore.openModal(
          <ProductEditModal product={product} onDelete={() => handleProductDelete()} onSubmit={() => handleProductEdit()} />)}>
            <Edit />
            <Typography variant='caption'>Edytuj</Typography>
          </IconButton>
          { product.details.isAvailable ?
          <IconButton style={{borderRadius: '0px', color: '#FF4747', flexDirection:'column', width: '50%'}} 
          size='medium' onClick={() => modalStore.openModal(
            <ConfirmModal text={`Czy na pewno chcesz zablokować możliwość zamawiania produktu ${product.name}?`} 
            onConfirm={() => handleProductLock()}/>)}
          >
            <Https />
            <Typography variant='caption'>Zablokuj</Typography>
          </IconButton> :
          <IconButton style={{borderRadius: '0px', color: '#109800', flexDirection:'column', width: '50%'}} 
          size='medium' onClick={() => modalStore.openModal(
            <ConfirmModal text={`Czy na pewno chcesz przywrócić możliwość zamawiania produktu ${product.name}?`} 
            onConfirm={() => handleProductUnlock()}/>)}
          >
            <NoEncryption />
            <Typography variant='caption'>Odblokuj</Typography>
          </IconButton>
          }
        </Stack>
      </TableCell>
    </TableRow>
  )
}

export default observer(ProductListItem)