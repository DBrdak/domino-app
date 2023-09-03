import { OrderItem } from '../../../global/models/order';
import { TableCell, TableRow } from '@mui/material';

interface Props {
  item: OrderItem
}

function OrderTableItem({item}: Props) {
  return (
    <TableRow>
      <TableCell>{item.productName}</TableCell>
      <TableCell align="center">{item.price.amount.toFixed(1)} {item.price.currency.code}/{item.price.unit?.code}</TableCell>
      <TableCell align="center">{item.quantity.value} {item.quantity.unit.code}</TableCell>
      <TableCell align="center">{Number(item.totalValue.amount.toFixed(1))} {item.totalValue.currency.code}</TableCell>
    </TableRow>
  )
}

export default OrderTableItem