import OrdersList from "./OrdersList";
import {OnlineOrder} from "../../../global/models/order";
import {
    ButtonGroup,
    Divider,
    IconButton,
    List,
    ListItem,
    ListItemText,
    Table, TableBody, TableCell, TableHead,
    TableRow,
    Typography
} from "@mui/material";
import React from "react";
import DateTimeRangeDisplay from "../../../components/DateTimeRangeDisplay";
import {Visibility} from "@mui/icons-material";
import OrdersTableRow from "./OrdersTableRow";

interface Props {
    orders: OnlineOrder[]
}

function OrdersTable({orders} : Props) {
    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell />
                    <TableCell style={{textAlign: 'center'}}>
                        <Typography variant={'subtitle1'}><strong>Lokalizacja</strong></Typography>
                    </TableCell>
                    <TableCell style={{textAlign: 'center'}}>
                        <Typography variant={'subtitle1'}><strong>Sklep</strong></Typography>
                    </TableCell>
                    <TableCell style={{textAlign: 'center'}}>
                        <Typography variant={'subtitle1'}><strong>Data odbioru</strong></Typography>
                    </TableCell>
                    <TableCell />
                </TableRow>
            </TableHead>
            <TableBody>
                {orders.map(o=> (
                    <OrdersTableRow key={o.id} order={o}/>
                ))}
            </TableBody>
        </Table>
    );
}

export default OrdersTable