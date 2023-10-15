import React from 'react'
import { Product } from '../../../global/models/product'
import { IconButton, ListItem, ListItemText, Stack, TableCell, TableRow, Typography } from '@mui/material'
import {
  Block,
  Delete,
  Discount,
  DiscountTwoTone,
  Done,
  Edit,
  EditTwoTone,
  Https,
  NoEncryption,
  RemoveShoppingCart
} from '@mui/icons-material'
import { useStore } from '../../../global/stores/store'
import { observer } from 'mobx-react-lite'
import ConfirmModal from '../../../components/ConfirmModal'
import ProductEditModal from '../productCreation/ProductEditModal'

interface Props {
  product: Product
}

function ProductListItem({product}: Props) {
  const {modalStore, adminProductStore} = useStore()

  const handleProductUnlock = async () => {
    adminProductStore.setProductUpdateValues(product,true)
    modalStore.closeModal()
    await adminProductStore.updateProduct()
  }

  const handleProductLock = async () => {
    adminProductStore.setProductUpdateValues(product,false)
    modalStore.closeModal()
    await adminProductStore.updateProduct()
  }

  async function handleProductEdit(product: Product) {
    adminProductStore.setProductUpdateValues(product, null)
    modalStore.closeModal()
    await adminProductStore.updateProduct()
  }

  async function handleProductDelete() {
    modalStore.closeModal()
    await adminProductStore.deleteProduct(product.id)
  }

  return (
    <TableRow>
      <TableCell style={{textAlign: 'center'}}>
        <Typography variant='h5'>{product.name}</Typography>
      </TableCell>
      <TableCell>
        <Stack direction={'row'} style={{display: 'flex', justifyContent: 'space-around'}} >
          <IconButton style={{borderRadius: '0px', color: '#000000', flexDirection:'column', width: '33%'}} size='medium'
          onClick={() => modalStore.openModal(
          <ProductEditModal product={product}
                            onPhotoChange={(photo) => adminProductStore.setPhoto(photo)}
                            onSubmit={async (product) => await handleProductEdit(product)} />)}>
            <Edit />
            <Typography variant='caption'>Edytuj</Typography>
          </IconButton>
          { product.details.isAvailable ?
          <IconButton style={{borderRadius: '0px', color: '#A0A0A0', flexDirection:'column', width: '33%'}}
          size='medium' onClick={() => modalStore.openModal(
            <ConfirmModal text={`Czy na pewno chcesz zablokować możliwość zamawiania produktu ${product.name}?`} 
            onConfirm={async () => await handleProductLock()}/>)}
          >
            <Https />
            <Typography variant='caption'>Zablokuj</Typography>
          </IconButton> :
          <IconButton style={{borderRadius: '0px', color: '#109800', flexDirection:'column', width: '33%'}}
          size='medium' onClick={() => modalStore.openModal(
            <ConfirmModal text={`Czy na pewno chcesz przywrócić możliwość zamawiania produktu ${product.name}?`} 
            onConfirm={async () => await handleProductUnlock()}/>)}
          >
            <NoEncryption />
            <Typography variant='caption'>Odblokuj</Typography>
          </IconButton>
          }
          <IconButton color={'primary'} style={{borderRadius: '0px', flexDirection:'column', width: '33%'}}
                      size='medium' onClick={() => modalStore.openModal(
              <ConfirmModal text={`Czy na pewno chcesz usunąc produkt ${product.name}?`} important={true}
                            onConfirm={async () => await handleProductDelete()}/>)}
          >
            <Delete />
            <Typography variant='caption'>Usuń</Typography>
          </IconButton>
        </Stack>
      </TableCell>
    </TableRow>
  )
}

export default observer(ProductListItem)