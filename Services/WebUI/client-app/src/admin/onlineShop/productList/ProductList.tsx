import { List, ListItem, ListItemText, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material'
import React, { useEffect, useState } from 'react'
import { useStore } from '../../../global/stores/store'
import { Product } from '../../../global/models/product'
import { observer } from 'mobx-react-lite'
import ProductListItem from './ProductListItem'

function ProductList() {
  const {adminProductStore} = useStore()
  const {products} = adminProductStore

  return (
    <TableContainer>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell width={'50%'} style={{textAlign: 'center'}} >
              <Typography variant='h5'><strong>Nazwa Produktu</strong></Typography>
            </TableCell>
            <TableCell width={'50%'} style={{textAlign: 'center'}}>
              <Typography variant='h5'><strong>Akcje</strong></Typography>
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {products &&
            products.map((p) => (
              <ProductListItem key={p.id} product={p} />
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  )
}

export default observer(ProductList)