import { TableCell, IconButton, TextField } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { ShoppingCartItem } from '../../../global/models/shoppingCart';

interface Props {
  item: ShoppingCartItem
  onRemove: (item: ShoppingCartItem) => void
  onEdit: (item: ShoppingCartItem, value: string) => void
}

export const ShoppingCartTableItem = ({ item, onRemove, onEdit }: Props) => {
  return (
    <>
      <TableCell>{item.productName}</TableCell>
      <TableCell align="right">
        <TextField
          value={item.quantity}
          onChange={(e) => onEdit(item, e.target.value)}
        />
        {item.unit}
      </TableCell>
      <TableCell align="right">{item.price.amount} {item.price.currency}/{item.price.unit}</TableCell>
      <TableCell align="right">{item.totalValue.toFixed(1)} {item.price.currency}</TableCell>
      <TableCell align="right">
        <IconButton onClick={() => onRemove(item)}>
          <DeleteIcon />
        </IconButton>
        <IconButton onClick={() => onEdit(item, item.quantity.toString())}>
          <EditIcon />
        </IconButton>
      </TableCell>
    </>
  );
}
