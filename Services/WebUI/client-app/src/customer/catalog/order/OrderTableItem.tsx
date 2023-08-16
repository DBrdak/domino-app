import { OrderItem } from '../../../global/models/order';
import { TableCell, TableRow } from '@mui/material';

interface Props {
  item: OrderItem
}

function OrderTableItem({item}: Props) {
  return (
    <TableRow>
      <TableCell>{item.productName}</TableCell>
      <TableCell align="center">{item.price.amount.toFixed(1)} {item.price.currency}/{item.price.unit}</TableCell>
      <TableCell align="center">{item.quantity} {item.unit}</TableCell>
      <TableCell align="center">{Number(item.totalValue.toFixed(1))} {item.price.currency}</TableCell>
      <TableCell align="center">{item.status}</TableCell>
    </TableRow>
  )
}

export default OrderTableItem