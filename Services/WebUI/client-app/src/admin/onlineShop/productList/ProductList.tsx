import { List, ListItem, ListItemText, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material'
import React, { useEffect, useState } from 'react'
import { useStore } from '../../../global/stores/store'
import { Product } from '../../../global/models/product'
import { observer } from 'mobx-react-lite'
import ProductListItem from './ProductListItem'
import LoadingTableRow from "../../../components/LoadingTableRow";
import PriceListListItem from "../../pricelists/priceListList/PriceListListItem";

interface Props {
  products: Product[]
  loading: boolean
}

function ProductList({products, loading}: Props) {

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
          {
            loading ?
                <LoadingTableRow rows={4} cells={3} />
                :
                products &&
                products.map((p) => (
                    <ProductListItem key={p.id} product={p} />
                ))
          }
        </TableBody>
      </Table>
    </TableContainer>
  )
}

export default ProductList