import { TableCell, IconButton, TextField } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { ShoppingCartItem } from '../../../global/models/shoppingCart';
import { useState } from 'react';

interface Props {
  item: ShoppingCartItem
  onRemove: (item: ShoppingCartItem) => void
  onEdit: (item: ShoppingCartItem, value: string) => void
}

export const ShoppingCartTableItem = ({ item, onRemove, onEdit }: Props) => {
  const [editMode, setEditMode] = useState()

  return (
    <>
      <TableCell>{item.productName}</TableCell>
      <TableCell align="center">
        {item.quantity} {item.unit}
      </TableCell>
      <TableCell align="center">{item.price.amount} {item.price.currency}/{item.price.unit}</TableCell>
      <TableCell align="center">{item.totalValue.toFixed(1)} {item.price.currency}</TableCell>
      {/*<TableCell align="right">
        <IconButton onClick={() => onRemove(item)}>
          <DeleteIcon />
        </IconButton>
        <IconButton onClick={() => onEdit(item, item.quantity.toString())}>
          <EditIcon />
      </TableCell>
  </IconButton>*/}
    </>
  );
}
